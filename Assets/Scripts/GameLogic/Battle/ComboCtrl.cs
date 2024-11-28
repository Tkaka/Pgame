
using FairyGUI;
using UnityEngine;

public enum ComboStatus
{
    UnStart,               //未开始状态
    RunForward,
    UpHand,
    Circle,
    RunBackward,
    Finish,
}

public enum ComboModel
{
    Circle,     
    Line,       
}

//模型点击：只有第一个生效 （curcmdid 小于 0）
//头像点击：只要点击一定添加到队列
//toucher点击：在某些阶段才有响应
public class ComboCtrl : SingletonTemplate<ComboCtrl>
{

    private ComboView view;

    private GGraph toucher;

    private ComboStatus curStatus = ComboStatus.UnStart;

    private bool IsInterrupt = false;

    private Vector3 targetPos;

    private float startTime = 0;

    //当前指令id
    private int curCmdId = -1;

    //public ComboCtrl()
    //{
    //    toucher = new GGraph();
    //    view = new ComboView();
    //    view.SetModel();
    //    view.SetVisible(false);
    //    toucher.onTouchBegin.Add(OnClick);
    //    toucher.touchable = false;
    //}

    public void Init(GGraph toucher)
    {
        this.toucher = toucher;

        view = new ComboView();
        view.SetModel();
        view.SetVisible(false);
        toucher.onTouchBegin.Add(OnClick);
        toucher.touchable = false;
    }

    public void SetSpeed()
    {
        if (view != null)
            view.SetSpeed();
    }

    public void ToggleTouch(bool flag)
    {
        if (toucher != null)
            toucher.touchable = flag;
    }

    public void ChangeState(int cmdId, ComboStatus status, Vector3? worldPos = null, bool showCircle=true)
    {
        if (status == ComboStatus.RunForward)
        {
            IsInterrupt = false;
            curCmdId = cmdId;
        }

        //上一个角色的切换状态指令抛弃掉
        if (curCmdId > 0 && curCmdId != cmdId)
            return;

        //如果被中断直接抛弃,打断后不再出圈
        if (IsInterrupt && status != ComboStatus.Finish)
            return;

        curStatus = status;
        if (status == ComboStatus.UpHand)
        {
            if(showCircle && FightManager.Singleton.GetNextActor(ActorCamp.CampFriend) > 0)
                toucher.touchable = true;
        }
        else if (status == ComboStatus.Circle)
        {
            //如果没有被打断才出圈
            if (!IsInterrupt)
            {
                if (worldPos.HasValue)
                {
                    startTime = Time.time;
                    targetPos = WinMgr.Singleton.WorldToScreen(worldPos.Value, 1);
                    view.SetPos(targetPos);
                    view.SetVisible(true);
                    view.PlayAni(Hide);
                }
                else
                {
                    Logger.err("连击出圈时未传入坐标点");
                }
            }
        }
        else if (status == ComboStatus.RunBackward)
        {

        }
        else if (status == ComboStatus.Finish)
        {
            toucher.touchable = false;
            IsInterrupt = false;
            curStatus = ComboStatus.UnStart;
            FightManager.ComboCount = 1;
            BattleCDCtrl.Singleton.PauseToggle(false);
            curCmdId = -1;
        }
    }

    public void OnModelClick()
    {
        //第一个才生效
        if (curCmdId < 0)
        {
            ThreeParam<long, ComboType, bool> param = new ThreeParam<long, ComboType, bool>();
            param.value2 = ComboType.Normal;
            param.value3 = true;
            NextActor(param);
        }
    }

    public void OnHeadBarClick(long actorId)
    {
        bool beyond = CmdMgr.Singleton.normalCmdNum > 1;
        if (curCmdId < 0 || beyond)
        {
            ThreeParam<long, ComboType, bool> param = new ThreeParam<long, ComboType, bool>();
            param.value1 = actorId;
            param.value2 = ComboType.Normal;
            param.value3 = true;
            if (beyond)
            {
                param.value3 = false;
                //第一个不弹combo
                if(FightManager.Singleton.normalAtkPhase)
                    ShowRandomTip(ComboType.Normal);
            }
            NextActor(param);
        }
        else
        {
            OnCombo(actorId);
        }
    }

    public void OnClick()
    {
        OnCombo(0);
    }

    public void OnCombo(long actorId)
    {
        IsInterrupt = true;
        toucher.touchable = false;
        ThreeParam<long, ComboType, bool> param = new ThreeParam<long, ComboType, bool>();
        param.value1 = actorId;
        ComboType comboType = ComboType.Normal;
        if (curStatus == ComboStatus.UnStart  || curStatus == ComboStatus.RunForward)
        {
//            if (actorId > 0)
//            {
                ShowRandomTip(ComboType.Normal);
//                param.value1 = actorId;
//                param.value2 = ComboType.Normal;
//                param.value3 = false;
//                NextActor(param);
//                return;
//            }
        }
        else if (curStatus == ComboStatus.UpHand)
        {
//            comboType = ComboType.Normal;
            ShowRandomTip(ComboType.Normal);
        }
        else if (curStatus == ComboStatus.Circle)
        {
            float delta = Time.time - startTime;
            comboType = CheckLevel(delta, targetPos);
            view.Stop();
            Hide();
        }
        else if (curStatus == ComboStatus.RunBackward)
        {
            new ComboTip(targetPos, ComboType.Normal);
//            comboType = ComboType.Normal;
        }
        else if (curStatus == ComboStatus.Finish)
        {
            Logger.err("此时应该不能点击了");
            return;
        }
        param.value2 = comboType;
        param.value3 = true;
        NextActor(param);
    }

    private void ShowRandomTip(ComboType comboType)
    {
        int randX = Random.Range(100, 200);
        int randY = Random.Range(-50, 50);
        new ComboTip(new Vector3(GRoot.inst.width / 2 + randX, GRoot.inst.height / 2 + randY), comboType);
    }

    private void NextActor(ThreeParam<long, ComboType, bool> param)
    {
        long actorId = param.value1;
        if (actorId <= 0)
            actorId = FightManager.Singleton.GetNextActor(ActorCamp.CampFriend);
        if (actorId > 0)
        {
            ActorPet pet = ActorManager.Singleton.Get(actorId) as ActorPet;
            if (pet != null)
            {
                pet.headBar.TouchToggle(false);
                param.value1 = actorId;
                GED.ED.dispatchEvent(EventID.OnPetNormalSkill, param);
            }
        }
    }

    public void Hide()
    {
        view.SetVisible(false);
    }

    public ComboType CheckLevel(float delta, Vector3 targetPos)
    {
        //1600  0-500 500-800 800-1100 1100-1300 1300-1600
        //800  0-100 100-400 400-700 700-900 900-1200
        if (delta <= 0.1f)
        {
            new ComboTip(targetPos, ComboType.Normal);
            return ComboType.Normal;
        }
        else if (delta > 0.1f && delta <= 0.4f)
        {
            new ComboTip(targetPos, ComboType.NotBad);
            return ComboType.NotBad;
        }
        else if (delta > 0.4f && delta <= 0.7f)
        {
            new ComboTip(targetPos, ComboType.Good);
            return ComboType.Good;
        }
        else if (delta > 0.7 && delta <= 0.9f)
        {
            VirtualCameraMgr.Singleton.Shake();
            new ComboTip(targetPos, ComboType.Perfect);
            return ComboType.Perfect;
        }
        else if (delta > 0.9f && delta <= 1.2f)
        {
            new ComboTip(targetPos, ComboType.Good);
            return ComboType.Good;
        }
        else
        {
            new ComboTip(targetPos, ComboType.Normal);
            return ComboType.Normal;
        }
    }

    public float GetTime(ComboType type)
    {
        if (type == ComboType.Normal)
            return 0.08f;
        else if (type == ComboType.NotBad)
            return 0.25f;
        else if (type == ComboType.Good)
            return 0.55f;
        else if (type == ComboType.Perfect)
            return 0.08f;
        return 0.08f;
    }

    public void OnClose()
    {
        if (view != null)
            view.Dispose();
        toucher = null;
        mSingleton = null;
    }

}