using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Plugins.Options;

public class TestPath : MonoBehaviour {

    public GameObject target;

    private GameSmoothPath path;

    private void Awake()
    {
        path = GetComponent<GameSmoothPath>();
    }

    // Use this for initialization
    void Start ()
    {
        GameSmoothPath.GameWaypoint[] points = path.m_Waypoints;
        target.transform.position = path.EvaluatePosition(0);
        //target.transform.rotation = path.EvaluateOrientation(0);

        int len = path.m_Waypoints.Length;
        len = 3;
        Vector3[] path1 = new Vector3[len];
        for (int i = 0; i < len; i++)
        {
            path1[i] = path.EvaluatePosition(i);
        }
        Tween tween = target.transform.DOPath(path1, 5, PathType.CatmullRom, PathMode.Full3D).SetLookAt(0.01f);
        tween.SetEase(Ease.Linear);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
