using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TestTimeline : MonoBehaviour {

    public PlayableDirector Director;

	// Use this for initialization
	void Start ()
    {
        Director.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
