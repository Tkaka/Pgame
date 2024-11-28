using UnityEngine;
using System.Collections;

public class DissolveCtr : MonoBehaviour {

    public float startTime = 1;
    public float endTime = 4;


    float t = 0;
    float v = 0;

    Material mat;

	void Start () {
        mat = GetComponent<MeshRenderer>().material;
        if (mat != null)
        {
            mat.SetFloat("_Amount", 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (t >= startTime)
        {
            if (mat != null)
            {
                mat.SetFloat("_Amount",v);
            }
            v += Time.deltaTime / Mathf.Max(0.01F, (endTime - startTime));
        }
        if (t <=endTime)
        {
            t += Time.deltaTime; 
        }
	}
}
