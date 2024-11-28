using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVAnim : MonoBehaviour {

    public float scrollSpeed_X = 0.5f;
    public float scrollSpeed_Y = 0.5f;
    protected Material mat = null;
    protected Vector2 vec2 = new Vector2(0, 0);
    // Use this for initialization
    void Start () {
        Renderer render = this.gameObject.GetComponent<Renderer>();
        if(render == null)
        {
            return;
        }
        mat = render.material;
	}
	
	// Update is called once per frame
	void Update () {
		if(mat == null)
        {
            return;
        }

        vec2.x = Time.time * scrollSpeed_X;
        vec2.y = Time.time * scrollSpeed_Y;
        mat.mainTextureOffset = vec2;

    }
}
