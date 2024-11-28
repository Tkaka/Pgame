using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class CustomDragDropManager {

    private GComponent _agent;
    private object _sourceData;

    private static CustomDragDropManager _inst;
    public static CustomDragDropManager inst
    {
        get
        {
            if (_inst == null)
                _inst = new CustomDragDropManager();
            return _inst;
        }
    }

    public CustomDragDropManager()
    {
    }

    /// <summary>
    /// Loader object for real dragging.
    /// 用于实际拖动的Loader对象。你可以根据实际情况设置loader的大小，对齐等。
    /// </summary>
    public GComponent dragAgent
    {
        get { return _agent; }
        set
        {
            _agent = value;
            InitAgent();
        }
    }

    private void InitAgent()
    {
        GRoot.inst.AddChild(_agent);
        _agent.draggable = true;
        _agent.SetPivot(0.5f, 0.5f, true);
        _agent.sortingOrder = int.MaxValue;
        _agent.onDragEnd.Add(__dragEnd);
    }

    /// <summary>
    /// Is dragging?
    /// 返回当前是否正在拖动。
    /// </summary>
    public bool dragging
    {
        get { return _agent.parent != null; }
    }

    /// <summary>
    /// Start dragging.
    /// 开始拖动。
    /// </summary>
    /// <param name="source">Source object. This is the object which initiated the dragging.</param>
    /// <param name="icon">Icon to be used as the dragging sign.</param>
    /// <param name="sourceData">Custom data. You can get it in the onDrop event data.</param>
    /// <param name="touchPointID">Copy the touchId from InputEvent to here, if has one.</param>
    public void StartDrag(GObject source, GComponent dragComponet, object sourceData, int touchPointID = -1)
    {
        //if (_agent.parent != null)
        //    return;
        _sourceData = sourceData;
        dragAgent = dragComponet;
        _agent.xy = GRoot.inst.GlobalToLocal(Stage.inst.GetTouchPosition(touchPointID));
        _agent.StartDrag(touchPointID);
    }

    /// <summary>
    /// Cancel dragging.
    /// 取消拖动。
    /// </summary>
    public void Cancel()
    {
        if (_agent.parent != null)
        {
            _agent.StopDrag();
            GRoot.inst.RemoveChild(_agent);
            _sourceData = null;
        }
    }

    private void __dragEnd(EventContext evt)
    {
        if (_agent.parent == null) //cancelled
            return;

        GRoot.inst.RemoveChild(_agent);

        object sourceData = _sourceData;
        _sourceData = null;

        GObject obj = GRoot.inst.touchTarget;
        while (obj != null)
        {
            if (obj is GComponent)
            {
                if (!((GComponent)obj).onDrop.isEmpty)
                {
                    obj.RequestFocus();
                    ((GComponent)obj).onDrop.Call(sourceData);
                    return;
                }
            }

            obj = obj.parent;
        }
    }
}
