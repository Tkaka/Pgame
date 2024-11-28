using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class AttributeTipShow : SingletonTemplate<AttributeTipShow> {

    /// <summary>
    /// 属性显示的文本
    /// </summary>
    Dictionary<GTextField, float> showTips = new Dictionary<GTextField, float>();
    /// <summary>
    /// 缓存的最大tips
    /// </summary>
    int MaxCacheTips = 10;
    List<GTextField> cacheTips = new List<GTextField>();
    DoActionInterval doAction;
    private float interval = 0.1f;
    List<GTextField> tempTips = new List<GTextField>();

    public void ShowTips(Vector2 pos, List<string> tips, float offsetY = -50f, float offsetX = 0, float duration = 0.5f)
    {
        if (doAction == null)
        {
            doAction = new DoActionInterval();
            doAction.doAction(interval, DoActionCall, null, true);
        }

        int count = tips.Count;
        GTextField tip = null;
        Vector2 endPos;
        for (int i = 0; i < count; i++)
        {
            if (cacheTips.Count > 0)
            {
                tip = cacheTips[0];
                tip.visible = true;
                cacheTips.Remove(tip);
                
            }
            else
            {
                tip = CreateNewTip();
            }

            tip.text = tips[i];
            tip.y += i * tip.height;
            WinMgr.Singleton.PopupLayer.AddChild(tip);
            tip.position = tip.parent.GlobalToLocal(pos);
            showTips.Add(tip, duration);
            endPos = new Vector2(tip.x + offsetX, tip.y + offsetY);
            tip.TweenMove(endPos, duration);
        }
    }

    private GTextField CreateNewTip()
    {
        GTextField tip = new GTextField();
        tip.textFormat.size = 20;
        tip.textFormat.color = Color.green;
        tip.autoSize = AutoSizeType.Both;
        tip.align = AlignType.Left;
        tip.singleLine = true;

        return tip;
    }

    private void DoActionCall(object obj)
    {
        if (showTips.Count > 0)
        {
            tempTips.Clear();
            tempTips.AddRange(showTips.Keys);
            for (int i = 0; i < tempTips.Count; i++)
            {
                showTips[tempTips[i]] -= interval;
                if (showTips[tempTips[i]] <= 0)
                {
                    // 清除该tip
                    AddCache(tempTips[i]);
                    showTips.Remove(tempTips[i]);
                }
            }
        }
        else
        {
            //doAction.kill();
        }
    }

    private void AddCache(GTextField tip)
    {
        if (cacheTips.Count >= MaxCacheTips)
        {
            WinMgr.Singleton.PopupLayer.RemoveChild(tip, true);
        }
        else
        {
            cacheTips.Add(tip);
            tip.visible = false;
        }
    }

    public void OnClose()
    {
        int count = cacheTips.Count;
        for (int i = 0; i < count; i++)
        {
            if(cacheTips[i] != null)
                WinMgr.Singleton.PopupLayer.RemoveChild(cacheTips[i], true);
        }
    }
}
