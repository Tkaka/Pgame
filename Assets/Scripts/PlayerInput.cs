
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;
using FairyGUI;


public class CPlayerInput
{

    private static CPlayerInput m_instance = null;
    private Vector3 m_mousePos = Vector3.zero;
    public bool Enable = false;
    private bool m_isTouchUI = false;
    private List<GoWrapper> clickEffectCachaes = new List<GoWrapper>();
    ResPack respack;
    SimpleInterval simpleInterval;
    public static CPlayerInput GetInstance()
    {
        if (m_instance == null)
            m_instance = new CPlayerInput();
        return m_instance;
    }

    public CPlayerInput()
    {

    }

    public void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            if (Enable != false)
                m_isTouchUI = GRoot.inst.touchTarget != null;
        }

        if (Input.GetMouseButtonUp(0))
        {
            CreateClickEffect();
            if (Enable != false)
            {
                if (m_isTouchUI)
                {
                    Debug.Log("点在了UI上");
                    return;
                }

                _CheckObjects();
            }
        }
    }

    private void CreateClickEffect()
    {

        if (clickEffectCachaes.Count == 0)
        {
            CreateNewClickEffect();
            return;
        }

        ParticleSystem ps = null;
        int cout = clickEffectCachaes.Count;
        int index = 0;
        for (int i = 0; i < cout; i++)
        {
            if (clickEffectCachaes[i].wrapTarget == null)
                continue;
            ps = clickEffectCachaes[i].wrapTarget.GetComponentInChildren<ParticleSystem>();
            if (ps != null && ps.isPlaying == false)
            {
                index = i;
                break;
            }
            else
                ps = null;
        }

        if(ps != null)
        {
            GoWrapper wraper = clickEffectCachaes[index];
            wraper.wrapTarget.SetActive(true);
            wraper.position = GRoot.inst.GlobalToLocal(Stage.inst.touchPosition);
            ps.Play(true);

            if (simpleInterval == null)
                simpleInterval = new SimpleInterval();
            simpleInterval.Kill();
            simpleInterval.DoActionWithTimes(ps.main.duration, OnClickEffectEnd);
            return;
        }

        CreateNewClickEffect();
    }

    private void CreateNewClickEffect()
    {
        if (respack == null)
            respack = new ResPack(this);

        GameObject effectGO = respack.LoadGo("eff_ui_dianji");
        GoWrapper wrapper = new GoWrapper(effectGO);
        GGraph graph = new GGraph();
        graph.sortingOrder = int.MaxValue;
        graph.size = new Vector2(10, 10);
        graph.SetPivot(0.5f, 0.5f, true);
        GRoot.inst.AddChild(graph);
        Vector2 mousePos = Stage.inst.touchPosition;
        graph.position = GRoot.inst.GlobalToLocal(mousePos);
        graph.SetNativeObject(wrapper);

        clickEffectCachaes.Add(wrapper);

        if (simpleInterval == null)
            simpleInterval = new SimpleInterval();
        simpleInterval.Kill();
        if (effectGO != null)
        {
            ParticleSystem ps = effectGO.GetComponentInChildren<ParticleSystem>();
            if (ps != null)
                simpleInterval.DoActionWithTimes(ps.main.duration, OnClickEffectEnd);
        }
    }

    private void OnClickEffectEnd()
    {
        while (clickEffectCachaes.Count > 5)
        {
            clickEffectCachaes[0].Dispose();
            clickEffectCachaes.RemoveAt(0);
        }

        int count = clickEffectCachaes.Count;
        GoWrapper wrapper = null ;
        for (int i = 0; i < count; i++)
        {
            wrapper = clickEffectCachaes[i];
            wrapper.wrapTarget.SetActive(false);
        }
    }

    /**
     * 功能：检测是否点击到物体
     */
    void _CheckObjects()
    {

        GameObject cemareObj  = GameObject.Find("Main Camera");
        if (cemareObj == null)
            return;

        m_mousePos = Input.mousePosition;
        var camera = cemareObj.transform.GetComponent<Camera>();
        Ray _ray = camera.ScreenPointToRay(m_mousePos);
        RaycastHit objhit;
        if (Physics.Raycast(_ray, out objhit, 1000))
        {
            int layer = objhit.collider.gameObject.layer;

            if (layer == LayerMask.NameToLayer("Actor"))
            {
                GED.ED.dispatchEvent(EventID.GameObjectClick, objhit.collider.gameObject.name);
                //Debug.Log("-------------------->>>>>>>>>>>>>>点击到模型" + objhit.collider.gameObject.name);
            }

        }

    }

    public void ClearClickEffect()
    {
        if (clickEffectCachaes != null)
        {
            int count = clickEffectCachaes.Count;
            for (int i = count -1; i >= 0; i--)
            {
                if (clickEffectCachaes[i] != null)
                    clickEffectCachaes[i].Dispose();
            }
        }
    }


}
	

