using System;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class TestList : MonoBehaviour
{
    GComponent _mainView;
    GList _list;

    public void Start()
    {
        //UIPackage.AddPackage("UI_WorldMap");
        _mainView = this.GetComponent<UIPanel>().ui;
        _list = _mainView.GetChild("scrollList").asList;
        FixScale();
        Stage.inst.onStageResized.Add(FixScale);
    }

    private void FixScale()
    {
        float h = GRoot.inst.root.height;
        float scale = h / _list.height;
        _list.SetScale(scale, scale);
    }

}