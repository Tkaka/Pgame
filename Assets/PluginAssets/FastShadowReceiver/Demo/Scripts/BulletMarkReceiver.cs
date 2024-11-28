using UnityEngine;
using System.Collections.Generic;

namespace FastShadowReceiver.Demo {
	/// <summary>
	/// This class demonstrates how to use MeshTree and MeshTreeSearch.
	/// </summary>
	[RequireComponent(typeof(MeshRenderer))]
	public class BulletMarkReceiver : MonoBehaviour {
		public MeshTreeBase m_meshTree;
		public Transform    m_meshTransform;
		public float        m_scissorMargin = 0.0f;
		public bool         m_allowDelay = true;

		class SearchInstance {
			public MeshTreeSearch search;
			public Matrix4x4      uvProjection;
		}
		private Stack<SearchInstance> m_freeSearch;
		private Queue<SearchInstance> m_activeSearch;
		private Transform             m_transform;

		public void AddShot(IProjector bulletProjector)
		{
			SearchInstance searchInstance;
			if (0 < m_freeSearch.Count) {
				searchInstance = m_freeSearch.Pop();
			}
			else {
				searchInstance = new SearchInstance();
				searchInstance.search = m_meshTree.CreateSearch();
				searchInstance.search .m_bBackfaceCulling = true;
				searchInstance.search .m_bScissor = true;
				searchInstance.search .m_bOutputNormals = true;
			}
			bulletProjector.GetClipPlanes(ref searchInstance.search.m_clipPlanes, m_meshTransform);

			// Usually, far clip plane is not used for scissoring. Scissoring by far clip plane will just waste CPU time.
			// To remove far clip plane from scissoring, "scissorPlaneCount" is less than "clipPlaneCount".
			// However, in this case, far clip plane is not so far. It is better to scissor polygons by far clip plane.
			// Also, if m_allowDelay is true, we don't need to care about CPU time.
			searchInstance.search.m_clipPlanes.scissorPlaneCount = searchInstance.search.m_clipPlanes.clipPlaneCount;

			searchInstance.search.m_scissorMargin = m_scissorMargin;
			searchInstance.search.SetProjectionDir(bulletProjector.isOrthographic, m_meshTransform.InverseTransformDirection(bulletProjector.direction), m_meshTransform.InverseTransformPoint(bulletProjector.position));
			searchInstance.search.AsyncStart(m_meshTree);
			searchInstance.uvProjection = bulletProjector.uvProjectionMatrix;
			m_activeSearch.Enqueue(searchInstance);
		}

		void Awake()
		{
#if !NETFX_CORE
			Nyahoon.ThreadPool.InitInstance(); // required for MeshTreeSearch.AsyncStart()
#endif
			m_freeSearch = new Stack<SearchInstance>();
			m_activeSearch = new Queue<SearchInstance>();
			m_transform = transform;
			if (!m_meshTree.IsBuildFinished()) {
				if (m_meshTree.IsPrebuilt()) {
					m_meshTree.BuildFromPrebuiltData();
				}
				else {
					m_meshTree.AsyncBuild();
				}
			}
			InitMesh();
		}

		void Start()
		{
			if (!m_meshTree.IsBuildFinished()) {
				m_meshTree.WaitForBuild();
			}
		}

		void LateUpdate()
		{
			bool bAdded = false;
			while (0 < m_activeSearch.Count) {
				SearchInstance searchInstance = m_activeSearch.Peek();
				if (!m_allowDelay) {
					searchInstance.search.Wait();
				}
				if (searchInstance.search.IsDone()) {
					if (AddBulletMark(searchInstance)) {
						bAdded = true;
					}
					m_freeSearch.Push(m_activeSearch.Dequeue());
				}
				else {
					break;
				}
			}
			if (bAdded) {
				SwapMesh();
			}
			m_transform.position = m_meshTransform.position;
			m_transform.rotation = m_meshTransform.rotation;
		}
		struct UVBuffer {
			public Vector2[] uv1;
			public Vector2[] uv2;
		}
		static Dictionary<int, UVBuffer> s_uvBuffer = new Dictionary<int, UVBuffer>();
		bool AddBulletMark(SearchInstance searchInstance)
		{
			Vector3[] vertices = searchInstance.search.vertices;
			if (vertices == null) {
				return false;
			}
			int vertexCount = vertices.Length;
			if (vertexCount == 0) {
				return false;
			}
			UVBuffer buffer;
			if (!s_uvBuffer.TryGetValue(vertexCount, out buffer)) {
				buffer = new UVBuffer();
				buffer.uv1 = new Vector2[vertexCount];
				buffer.uv2 = new Vector2[vertexCount];
				s_uvBuffer.Add(vertexCount, buffer);
			}
			Matrix4x4 mat = searchInstance.uvProjection * m_meshTransform.localToWorldMatrix;
			for (int i = 0; i < vertexCount; ++i) {
				Vector4 v = vertices[i]; v.w = 1.0f;
				v = mat * v;
				buffer.uv1[i].x = v.x;
				buffer.uv1[i].y = v.y;
				buffer.uv2[i].x = v.z;
				buffer.uv2[i].y = v.w;
			}
			Mesh mesh;
			if (m_meshCombine[0].mesh == null) {
				mesh = nextMesh;
			}
			else {
				mesh = m_tempMesh;
			}
			mesh.Clear();
			mesh.vertices = vertices;
			mesh.normals = searchInstance.search.normals;
			mesh.uv = buffer.uv1;
			mesh.uv2 = buffer.uv2;
			mesh.triangles = searchInstance.search.triangles;
			if (m_meshCombine[0].mesh != null) {
				m_meshCombine[1].mesh = mesh;
				nextMesh.CombineMeshes(m_meshCombine, true, false);
			}
			m_meshCombine[0].mesh = nextMesh; // now we created a new mesh. so, next time we add bullet mark, it should be combined with nextMesh.
			return true;
		}

		// for mesh rendering
		const int BUFFER_COUNT = 2;
		private int m_nCurrentBuffer;
		private Mesh[] m_meshes;
		private Mesh m_tempMesh;
		private MeshFilter m_meshFilter;
		private Renderer m_renderer;
		private CombineInstance[] m_meshCombine;

		void InitMesh()
		{
			m_meshes = new Mesh[BUFFER_COUNT];
			for (int i = 0; i < BUFFER_COUNT; ++i) {
				m_meshes[i] = new Mesh();
			}
			m_tempMesh = new Mesh();
			m_nCurrentBuffer = 0;
			m_meshFilter = GetComponent<MeshFilter>();
			if (m_meshFilter == null) {
				m_meshFilter = gameObject.AddComponent<MeshFilter>();
			}
			m_meshFilter.mesh = m_meshes[0];
			m_meshCombine = new CombineInstance[2];
			m_meshCombine[0].mesh = null;
			m_meshCombine[0].subMeshIndex = 0;
			m_meshCombine[1].subMeshIndex = 0;
		}

		Mesh currentMesh
		{
			get { return m_meshes[m_nCurrentBuffer]; }
		}

		Mesh nextMesh
		{
			get { return m_meshes[(m_nCurrentBuffer + 1) % BUFFER_COUNT]; }
		}

		void SwapMesh()
		{
			m_nCurrentBuffer = (m_nCurrentBuffer + 1) % BUFFER_COUNT;
			m_meshFilter.mesh = currentMesh;
		}
	}
}
