using FairyGUI;
using System;
using UI_Battle;
using UnityEngine;

public class ComboView
{

    private UI_ComboCircle circle;

    private UI_ComboCircle_Line line;

    public ComboView()
    {
        circle = UI_ComboCircle.CreateInstance();
        circle.touchable = false;
        WinMgr.Singleton.NoticeLayer.AddChild(circle);
        line = UI_ComboCircle_Line.CreateInstance();
        line.touchable = false;
        WinMgr.Singleton.NoticeLayer.AddChild(line);
    }

    public void SetSpeed()
    {
        if (FightManager.Singleton.comboModel == ComboModel.Circle)
        {
            circle.m_scale.timeScale = FightManager.Singleton.GameSpeed;
        }
        else
        {
            line.m_GO.timeScale = FightManager.Singleton.GameSpeed; 
        }
    }

    public void SetModel()
    {
        if (FightManager.Singleton.comboModel == ComboModel.Circle)
        {
            circle.visible = true;
            line.visible = false;
        }
        else
        {
            circle.visible = false;
            line.visible = true;
        }
    }

    public void PlayAni(PlayCompleteCallback callback)
    {
        if (FightManager.Singleton.comboModel == ComboModel.Circle)
        {
            circle.m_scale.Play(callback);
        }
        else
        {
            line.m_GO.Play(callback);
        }
    }

    public void Stop()
    {
        if (FightManager.Singleton.comboModel == ComboModel.Circle)
        {
            circle.m_scale.Stop();
        }
        else
        {
            line.m_GO.Stop();
        }
    }

    public GComponent GetView()
    {
        if (FightManager.Singleton.comboModel == ComboModel.Circle)
        {
            return circle;
        }
        else
        {
            return line;
        }
    }

    public void SetPos(Vector3 targetPos)
    {
        GComponent view = GetView();
        if(view != null)
            view.SetXY(targetPos.x - view.actualWidth / 2, targetPos.y - view.actualHeight / 2);
    }

    public void SetVisible(bool flag)
    {
        GComponent view = GetView();
        if (view != null)
            view.visible = flag;
    }

    public void Dispose()
    {
        if (circle != null)
            circle.Dispose();
        if (line != null)
            line.Dispose();
    }

}
