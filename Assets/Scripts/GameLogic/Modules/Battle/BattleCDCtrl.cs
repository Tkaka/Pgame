using FairyGUI;
using UI_Battle;
using UnityEngine;

public class BattleCDCtrl : SingletonTemplate<BattleCDCtrl>
{

    private GTextField cdTxt;

    private GImage wqImge;

    private UI_ActorSelector selector;

    private SimpleInterval interval;

    private int nowVal = BattleParam.MasterSkillCD;

    //是否暂停
    private bool isPaused = false;

    public void Init(GTextField cdTxt, GImage wqImg)
    {
        this.cdTxt = cdTxt;
        this.wqImge = wqImg;
        cdTxt.visible = false;
        wqImge.visible = true;
        interval = new SimpleInterval(true);
        selector = UI_ActorSelector.CreateInstance();
        selector.name = "actorSelector";
        WinMgr.Singleton.NoticeLayer.AddChild(selector);
        selector.visible = false;
    }

    /// <summary>
    /// 重新设置倒计时
    /// </summary>
    /// <param name="val"></param>
    public void ResetVal(int val)
    {
        nowVal = val;
        cdTxt.text = nowVal + "";
    }

    public void Start()
    {
        if (interval != null)
        {
            ToggleSelector(true);
            nowVal = BattleParam.MasterSkillCD;
            cdTxt.text = nowVal + "";
            cdTxt.visible = true;
            wqImge.visible = false;
            interval.DoAction(1, DoAction, true);
        }
    }

    public bool IsShowing
    {
        get
        {
            if(selector != null)
                return selector.visible;
            return false;
        }
    }

    public void ToogleSelectActor(bool flag)
    {
        if (flag)
            Stage.inst.onTouchEnd.Add(OnTouchEnd);
        else
            Stage.inst.onTouchEnd.Remove(OnTouchEnd);
    }

    private void OnTouchEnd()
    {
        if (!IsTouchOnUI())
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector2(Stage.inst.touchPosition.x, Screen.height - Stage.inst.touchPosition.y));
            if (Physics.Raycast(ray, out hit, 1 << LayerMask.NameToLayer("Actor")))
            {
                if (hit.transform != null)
                {
                    ActorBehavior ab = hit.transform.GetComponent<ActorBehavior>();
                    if (ab != null)
                    {
                        GED.ED.dispatchEvent(EventID.OnSelectActor, ab.actorId);
                    }
                }
            }
        }
    }

    public bool IsTouchOnUI()
    {
        if (GRoot.inst.touchTarget != null && 
            GRoot.inst.touchTarget.name != "comboToucher" &&
            GRoot.inst.touchTarget.name != "actorSelector")
        {
            return true;
        }
        return false;
    }

    public void ToggleSelector(bool flag)
    {
        if (selector != null)
            selector.visible = flag;
        ToogleSelectActor(flag);
    }

    public void ResetSelectorPos(Vector3 wpos)
    {
        if (selector != null)
        {
            Vector3 targetPos = WinMgr.Singleton.WorldToScreen(wpos);
            if (selector != null)
                selector.SetXY(targetPos.x - selector.actualWidth / 2, targetPos.y - selector.actualHeight / 2);
        }
    }


    public void PauseToggle(bool flag)
    {
        if (isPaused == flag)
            return;
        if (interval != null && interval.IsRunning)
        {
            ToggleSelector(!flag);
            isPaused = flag;
            if (isPaused)
            {
                cdTxt.visible = false;
                wqImge.visible = true;
            }
            else
            {
                cdTxt.visible = true;
                wqImge.visible = false;
            }
        }
    }

    private void DoAction()
    {
        if (isPaused)
            return;
        cdTxt.text = nowVal + "";
        nowVal--;
        if (nowVal <= -1)
        {
            Stop();
            //开始自动战斗
            BattleWindMgr.CurrentBtlWin.OnAutoBtn();
        }
    }

    /// <summary>
    /// 开始攻击 或者 点击了自动按钮时
    /// </summary>
    public void Stop()
    {
        if (interval != null)
        {
            ToggleSelector(false);
            isPaused = false;
            cdTxt.visible = false;
            wqImge.visible = true;
            interval.Kill();
        }
    }

    public void OnClose()
    {
        mSingleton = null;
        wqImge = null;
        cdTxt = null;
    }

}
