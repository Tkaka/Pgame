using UnityEngine;
using System;
using Cinemachine.Utility;

/// <summary>Defines a world-space path, consisting of an array of waypoints,
/// each of which has position and roll settings.  Bezier interpolation
/// is performed between the waypoints, to get a smooth and continuous path.
/// The path will pass through all waypoints, and (unlike CinemachinePath) first 
/// and second order continuity is guaranteed</summary>
//[DocumentationSorting(18.5f, DocumentationSortingAttribute.Level.UserRef)]
//[AddComponentMenu("Cinemachine/CinemachineSmoothPath")]
//[SaveDuringPlay]
public class GameMovePath : GamePathBase
{
    /// <summary>If checked, then the path ends are joined to form a continuous loop</summary>
    [Tooltip("If checked, then the path ends are joined to form a continuous loop.")]
    public bool m_Looped;

    /// <summary>A waypoint along the path</summary>
    //[DocumentationSorting(18.7f, DocumentationSortingAttribute.Level.UserRef)]
    [Serializable]
    public struct GameWaypoint
    {
        /// <summary>Position in path-local space</summary>
        [Tooltip("Position in path-local space")]
        public Vector3 position;

        public Vector3 tangent;

        public int zoneId;

        /// <summary>Defines the roll of the path at this waypoint.  
        /// The other orientation axes are inferred from the tangent and world up.</summary>
        [Tooltip("Defines the roll of the path at this waypoint.  The other orientation axes are inferred from the tangent and world up.")]
        public float roll;

        /// <summary>Representation as Vector4</summary>
        internal Vector4 AsVector4
        {
            get { return new Vector4(position.x, position.y, position.z, roll); }
        }

        internal static GameWaypoint FromVector4(Vector4 v)
        {
            GameWaypoint wp = new GameWaypoint();
            wp.position = new Vector3(v[0], v[1], v[2]);
            wp.roll = v[3];
            return wp;
        }

    }

    /// <summary>The waypoints that define the path.
    /// They will be interpolated using a bezier curve</summary>
    [Tooltip("The waypoints that define the path.  They will be interpolated using a bezier curve.")]
    public GameWaypoint[] m_Waypoints = new GameWaypoint[0];

    /// <summary>The minimum value for the path position</summary>
    public override float MinPos { get { return 0; } }

    /// <summary>The maximum value for the path position</summary>
    public override float MaxPos
    {
        get
        {
            int count = m_Waypoints.Length - 1;
            if (count < 1)
                return 0;
            return m_Looped ? count + 1 : count;
        }
    }
    /// <summary>True if the path ends are joined to form a continuous loop</summary>
    public override bool Looped { get { return m_Looped; } }

    /// <summary>When calculating the distance cache, sample the path this many 
    /// times between points</summary>
    public override int DistanceCacheSampleStepsPerSegment { get { return m_Resolution; } }

    private void OnValidate()
    {
        InvalidateDistanceCache();
        //ResetSpawnerPos();
    }

    /// <summary>Call this if the path changes in such a way as to affect distances
    /// or other cached path elements</summary>
    public override void InvalidateDistanceCache()
    {
        base.InvalidateDistanceCache();
        m_ControlPoints1 = null;
        m_ControlPoints2 = null;
    }

    GameWaypoint[] m_ControlPoints1;
    GameWaypoint[] m_ControlPoints2;
    bool m_IsLoopedCache;

    void UpdateControlPoints()
    {
        int numPoints = (m_Waypoints == null) ? 0 : m_Waypoints.Length;
        if (numPoints > 1
            && (Looped != m_IsLoopedCache
                || m_ControlPoints1 == null || m_ControlPoints1.Length != numPoints
                || m_ControlPoints2 == null || m_ControlPoints2.Length != numPoints))
        {
            Vector4[] p1 = new Vector4[numPoints];
            Vector4[] p2 = new Vector4[numPoints];
            Vector4[] K = new Vector4[numPoints];
            for (int i = 0; i < numPoints; ++i)
                K[i] = m_Waypoints[i].AsVector4;
            if (Looped)
                SplineHelpers.ComputeSmoothControlPointsLooped(ref K, ref p1, ref p2);
            else
                SplineHelpers.ComputeSmoothControlPoints(ref K, ref p1, ref p2);

            m_ControlPoints1 = new GameWaypoint[numPoints];
            m_ControlPoints2 = new GameWaypoint[numPoints];
            for (int i = 0; i < numPoints; ++i)
            {
                m_ControlPoints1[i] = GameWaypoint.FromVector4(p1[i]);
                m_ControlPoints2[i] = GameWaypoint.FromVector4(p2[i]);
            }
            m_IsLoopedCache = Looped;
        }
    }

    /// <summary>Returns normalized position</summary>
    float GetBoundingIndices(float pos, out int indexA, out int indexB)
    {
        pos = NormalizePos(pos);
        int numWaypoints = m_Waypoints.Length;
        if (numWaypoints < 2)
            indexA = indexB = 0;
        else
        {
            indexA = Mathf.FloorToInt(pos);
            if (indexA >= numWaypoints)
            {
                // Only true if looped
                pos -= MaxPos;
                indexA = 0;
            }
            indexB = indexA + 1;
            if (indexB == numWaypoints)
            {
                if (Looped)
                    indexB = 0;
                else
                {
                    --indexB;
                    --indexA;
                }
            }
        }
        return pos;
    }

    /// <summary>Get a worldspace position of a point along the path</summary>
    /// <param name="pos">Postion along the path.  Need not be normalized.</param>
    /// <returns>World-space position of the point along at path at pos</returns>
    public override Vector3 EvaluatePosition(float pos)
    {
        Vector3 result = Vector3.zero;
        if (m_Waypoints.Length > 0)
        {
            UpdateControlPoints();
            int indexA, indexB;
            pos = GetBoundingIndices(pos, out indexA, out indexB);
            if (indexA == indexB)
                result = m_Waypoints[indexA].position;
            else
                result = SplineHelpers.Bezier3(pos - indexA,
                    m_Waypoints[indexA].position, m_ControlPoints1[indexA].position,
                    m_ControlPoints2[indexA].position, m_Waypoints[indexB].position);
        }
        return transform.TransformPoint(result);
    }

    /// <summary>Get the tangent of the curve at a point along the path.</summary>
    /// <param name="pos">Postion along the path.  Need not be normalized.</param>
    /// <returns>World-space direction of the path tangent.
    /// Length of the vector represents the tangent strength</returns>
    public override Vector3 EvaluateTangent(float pos)
    {
        Vector3 result = transform.rotation * Vector3.forward;
        if (m_Waypoints.Length > 1)
        {
            UpdateControlPoints();
            int indexA, indexB;
            pos = GetBoundingIndices(pos, out indexA, out indexB);
            if (!Looped && indexA == m_Waypoints.Length - 1)
                --indexA;
            result = SplineHelpers.BezierTangent3(pos - indexA,
                m_Waypoints[indexA].position, m_ControlPoints1[indexA].position,
                m_ControlPoints2[indexA].position, m_Waypoints[indexB].position);
        }
        return transform.TransformDirection(result);
    }

    /// <summary>Get the orientation the curve at a point along the path.</summary>
    /// <param name="pos">Postion along the path.  Need not be normalized.</param>
    /// <returns>World-space orientation of the path, as defined by tangent, up, and roll.</returns>
    public override Quaternion EvaluateOrientation(float pos)
    {
        Quaternion result = transform.rotation;
        if (m_Waypoints.Length > 0)
        {
            float roll = 0;
            int indexA, indexB;
            pos = GetBoundingIndices(pos, out indexA, out indexB);
            if (indexA == indexB)
                roll = m_Waypoints[indexA].roll;
            else
            {
                UpdateControlPoints();
                roll = SplineHelpers.Bezier1(pos - indexA,
                    m_Waypoints[indexA].roll, m_ControlPoints1[indexA].roll,
                    m_ControlPoints2[indexA].roll, m_Waypoints[indexB].roll);
            }

            Vector3 fwd = EvaluateTangent(pos);
            if (!fwd.AlmostZero())
            {
                Vector3 up = transform.rotation * Vector3.up;
                Quaternion q = Quaternion.LookRotation(fwd, up);
                result = q * Quaternion.AngleAxis(roll, Vector3.forward);
            }
        }
        return result;
    }
}
