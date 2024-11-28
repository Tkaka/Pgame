using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestSimpleAni : MonoBehaviour
{

    public Transform targetPos;

    public SimpleAnimation ani;

	// Use this for initialization
	void Start ()
    {
        Logger.CurLevel = Logger.Level.All;
        //ani = GetComponent<SimpleAnimation>();
        /*foreach (SimpleAnimation.State state in ani.GetStates())
        {
            Logger.log(state.clip.name + state.clip.length);
        }
        Logger.err("clip count:", ani.GetClipCount().ToString());

        SimpleAnimation.State idle = ani.GetState("YiDong");
        Logger.err(idle.clip.name);*/

        Invoke("OnDelay", 3);

    }

    private void OnDelay()
    {
        ani.Play("DaiJi");
        //Invoke("Move", 0);
    }

    private void Move()
    {
        transform.DOMove(targetPos.position, 0.5f);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
