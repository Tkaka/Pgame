using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestShader : MonoBehaviour
{

    public Button addBtn;

    public Button subBtn;

    public Button showBtn;

    private float defaultInterval = 2;

    public GameObject rimGo;

	void Start ()
    {
        InvokeRepeating("ShowEffect", 1, defaultInterval);
        addBtn.onClick.AddListener(OnAddBtn);
        subBtn.onClick.AddListener(OnSubBtn);
        showBtn.onClick.AddListener(OnShowBtn);
    }

    private void OnShowBtn()
    {
        rimGo.SetActive(!rimGo.activeSelf);
    }

    private void OnAddBtn()
    {
        CancelInvoke("ShowEffect");
        defaultInterval += 0.5f;
        InvokeRepeating("ShowEffect", 1, defaultInterval);
    }

    private void OnSubBtn()
    {
        CancelInvoke("ShowEffect");
        defaultInterval -= 0.5f;
        if (defaultInterval <= 0)
            defaultInterval = 0.1f;
        InvokeRepeating("ShowEffect", 1, defaultInterval);
    }

    private void ShowEffect()
    {
        Debug.Log("-------------");
        Vector3 pos = Random.onUnitSphere + rimGo.transform.position;
        //Res.Singleton.InstantiateCEffect("Attack/eff_pet_BaoLiLong_skill02", pos);
    }
	
	void Update ()
    {
		
	}

}
