using System;
using UnityEngine;
using Data.Beans;
using System.Collections.Generic;

public class Skill
{

    protected int mTemplateId;           //技能模板数据ID

    public Actor Owner { protected set; get; }

    protected t_skillBean mSkillBean;

    public SkillConfig SkillConfig { protected set; get; }

    protected Vector3 oldForward;

    protected object mParam;

    /// <summary>
    /// 用于表现的ID
    /// </summary>
    public int ShowID { set; get; }

    /// <summary>
    /// 效果唯一ID
    /// </summary>
    public long EffectID { protected set; get; }

    /// <summary>
    /// 技能等级(要减1)
    /// </summary>
    public int SkillLevel { protected set; get; }

    /// <summary>
    /// 技能效果固定值
    /// </summary>
    public LNumber SkillEffectPer { get; protected set; }

    /// <summary>
    /// 技能效果百分比
    /// </summary>
    public LNumber SkillEffectFixed { get; protected set; }

    /// <summary>
    /// 技能参考目标id
    /// </summary>
    public long TargetID { get; set; }

    /// <summary>
    /// 施法朝向
    /// </summary>
    public Vector3 skillDir;
    /// <summary>
    /// 施法点
    /// </summary>
    public Vector3 skillPos;

    /// <summary>
    /// 是否显示combo
    /// </summary>
    //public bool showCombo = false;

    /// <summary>
    /// 是否连击提示
    /// </summary>
    public bool showComboTip = false;

    /// <summary>
    /// 是否显示连击圈 
    /// </summary>
    public bool showComboCircle = false;

    /// <summary>
    /// 是否手动
    /// </summary>
    public bool isMannul = false;

    /// <summary>
    /// 命令id
    /// </summary>
    public int cmdId;

    /// <summary>
    /// 技能主效果目标
    /// </summary>
    public List<long> defenders = new List<long>();

    protected SkillFlowCtrl skillFlow;

    /// <summary>
    /// 自动战斗连击类型
    /// </summary>
    public ComboType autoComboType = ComboType.Normal;

    /// <summary>
    /// 主效果之前的
    /// </summary>
    public List<EffectInfo> beforeEffectList = new List<EffectInfo>();

    /// <summary>
    /// 主效果之后的
    /// </summary>
    public List<EffectInfo> afterEffectList = new List<EffectInfo>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="templateId">技能模板ID</param>
    public Skill()
    {

    }

    /// <summary>
    /// 获取技能时间
    /// </summary>
    /// <returns></returns>
    public float GetSkillTime()
    {
        string aniName = SkillConfig.aniName.ToString();
        float aniTime = Owner.GetActionManager().GetAniLen(aniName);
        float dis = Vector3.Distance(skillPos, Owner.TransformExt.position);
        float moveDur = dis / GameConfig.Velocity;
        if (SkillConfig.HasMoveForward())
            moveDur *= 2;
        return (aniTime + moveDur + SkillConfig.GetFreezeTime());
    }

    /// <summary>
    /// 是否是绝技
    /// </summary>
    /// <returns></returns>
    public bool IsMasterSkill()
    {
        return SkillConfig.skillType == SkillType.MasterSkill;
    }

    public bool IsCoreSkill()
    {
        return SkillConfig.skillType == SkillType.CoreSkill;
    }

    public virtual void Init(SkillConfig config, Actor owner)
    {
        SkillConfig = config;
        mTemplateId = config.skillId;
        mSkillBean = ConfigBean.GetBean<t_skillBean, int>(mTemplateId);
        Owner = owner;
        SkillLevel = config.skillLevel;
        if (!IsCoreSkill())
        {
            int[] arr = AttackUtils.GetSkillHurt(SkillLevel, mSkillBean);
            SkillEffectPer = LNumber.Create(arr[0] / 10000, arr[0] % 10000);// GTools.ScaleInt2LNumber(arr[0]);
            SkillEffectFixed = arr[1];
           // Debuger.Err("Skill " + config.skillId + " " + SkillEffectPer.raw + " " + SkillEffectFixed.raw);
        }
        //生成效果唯一ID
        EffectID = AttackUtils.GetEffectId(owner.getCamp(), EffectType.Skill, mTemplateId, owner.GridId);
        skillFlow = new SkillFlowCtrl(this);
        //Logger.log("effectid is:" + EffectID);

        InitEffects();
    }

    /// <summary>
    /// 初始化效果
    /// </summary>
    private void InitEffects()
    {
        if (mSkillBean == null)
            return;
        if (string.IsNullOrEmpty(mSkillBean.t_extra_effect_id) || mSkillBean.t_extra_effect_id == "0")
            return;

        int[] effects = GTools.splitStringToIntArray(mSkillBean.t_extra_effect_id);
        for (int i = 0; i < effects.Length; i++)
        {
            if (effects[i] <= 0)
                continue;

            t_skill_effectBean eftBean = ConfigBean.GetBean<t_skill_effectBean, int>(effects[i]);
            if (eftBean != null)
            {
                EffectInfo info = new EffectInfo();
                info.effectId = effects[i];
                info.ownerId = Owner.getActorId();
                info.level = SkillLevel;
                info.skill = this;
                //额外效果在技能主效果之前还是之后(1=主效果之后, 2 = 主效果之前)
                if (eftBean.t_effect_turn == 2)
                    beforeEffectList.Add(info);                    
                else if (eftBean.t_effect_turn == 1)
                    afterEffectList.Add(info);
            }
        }
    }

    /// <summary>
    /// 设置施法参数
    /// </summary>
    public void SetSkillParam(Vector3 skillPos, Vector3 skillDir, long targetId)
    {
        this.skillPos = skillPos;
        this.skillDir = skillDir;
        TargetID = targetId;
    }

    public void SelectTarget()
    {
        int dir = 1;
        if (Owner.isCampOf(ActorCamp.CampEnemy))
            dir = -1;

        if (!FightManager.Singleton.IsReplay)
        {
            switch (SkillConfig.standingPoint)
            {
                case StandingPoint.SingleFrontRow:
                    if (FightManager.Singleton.PlayerTurn && FightManager.Singleton.LastSeletedId > 0)
                        TargetID = FightManager.Singleton.LastSeletedId;
                    else
                        TargetID = RangeSelector.Singleton.GetReferenceTarget(LineupType.FrontRow, Owner);
                    break;
                case StandingPoint.SingleBackRow:
                    if (FightManager.Singleton.PlayerTurn && FightManager.Singleton.LastSeletedId > 0)
                        TargetID = RangeSelector.Singleton.GetSelectedBackRowTarget(FightManager.Singleton.LastSeletedId, LineupType.BackRow, Owner);
                    else
                        TargetID = RangeSelector.Singleton.GetReferenceTarget(LineupType.BackRow, Owner);
                    break;
                case StandingPoint.AssistMid:
                case StandingPoint.BulletMid:
                    Transform transMid = SpawnerHelper.Singleton.GetAssist(1);
                    if (transMid != null)
                    {
                        //判断前排是否有人
                        int aliveNum = FightManager.Singleton.Grid.RowAliveNum(AttackUtils.GetEnemyCamp(Owner.getCamp()), GridEnum.Row0);
                        if (aliveNum > 0)
                        {
                            skillDir = transMid.forward * dir;
                            if (SkillConfig.standingPoint == StandingPoint.BulletMid)
                                skillPos = transMid.position - skillDir;
                            else
                                skillPos = transMid.position;
                            TargetID = 0;
                        }
                        else
                        {
                            skillDir = transMid.forward * dir;
                            if (SkillConfig.standingPoint == StandingPoint.BulletMid)
                                skillPos = transMid.position + skillDir;
                            else
                                skillPos = transMid.position + 2 * skillDir;
                            TargetID = 0;
                        }
                    }
                    break;
                case StandingPoint.AssistCol:
                    if (FightManager.Singleton.PlayerTurn && FightManager.Singleton.LastSeletedId > 0)
                        TargetID = FightManager.Singleton.LastSeletedId;
                    else
                        TargetID = RangeSelector.Singleton.GetReferenceTarget(LineupType.FrontRow, Owner);
                    //获取目标所在列
                    int col = FightManager.Singleton.Grid.InWhichCol(AttackUtils.GetEnemyCamp(Owner.getCamp()), TargetID);
                    Transform trans1 = SpawnerHelper.Singleton.GetAssist(col);
                    if (trans1 != null)
                    {
                        skillDir = trans1.forward * dir;
                        skillPos = trans1.position;
                    }
                    break;
                case StandingPoint.BulletCol:
                    if (FightManager.Singleton.PlayerTurn && FightManager.Singleton.LastSeletedId > 0)
                        TargetID = FightManager.Singleton.LastSeletedId;
                    else
                        TargetID = RangeSelector.Singleton.GetReferenceTarget(LineupType.FrontRow, Owner);
                    //获取目标所在列
                    int bCol = FightManager.Singleton.Grid.InWhichCol(AttackUtils.GetEnemyCamp(Owner.getCamp()), TargetID);
                    Transform btrans1 = SpawnerHelper.Singleton.GetAssist(bCol);
                    if (btrans1 != null)
                    {
                        //判断前排是否有人
                        int aliveNum = FightManager.Singleton.Grid.RowAliveNum(AttackUtils.GetEnemyCamp(Owner.getCamp()), GridEnum.Row0);
                        if (aliveNum > 0)
                        {
                            skillDir = btrans1.forward * dir;
                            skillPos = btrans1.position - skillDir;
                        }
                        else
                        {
                            skillDir = btrans1.forward * dir;
                            skillPos = btrans1.position + skillDir;
                        }
                    }

                    break;
                case StandingPoint.Original:
                    skillDir = Owner.TransformExt.forward;
                    skillPos = Owner.TransformExt.position;
                    TargetID = 0;
                    break;
                case StandingPoint.OppositeCenter:
                    skillDir = Owner.TransformExt.forward;
                    skillPos = SpawnerHelper.Singleton.GetColCenter(AttackUtils.GetEnemyCamp(Owner.getCamp()), GridEnum.Col1);
                    TargetID = 0;
                    break;
            }

        }
        //else
        //{
        //    if (FightManager.Singleton.PlayerTurn && FightManager.Singleton.LastSeletedId > 0)
        //        TargetID = FightManager.Singleton.LastSeletedId;
        //    else
        //        TargetID = 0;
        //}


        //3.设置技能施法方向和坐标及参考目标（参考目标可能没有）
        if (SkillConfig.standingPoint == StandingPoint.SingleFrontRow || SkillConfig.standingPoint == StandingPoint.SingleBackRow)
        {
            Actor defender = ActorManager.Singleton.Get(TargetID);
            if (defender != null)
            {
                skillDir = -defender.OriginDir;
                skillPos = defender.OriginPos;
                skillPos += defender.OriginDir * 1.2f;
            }
        }

        Actor defender2 = ActorManager.Singleton.Get(TargetID);
        //水平偏移
        var right = new Vector3(skillDir.z, skillDir.y, -skillDir.x).normalized;
        skillPos += SkillConfig.standOffset.x * right;
        //垂直偏移
        skillPos += SkillConfig.standOffset.x * skillDir.normalized;
        //朝向被攻击者
        if (defender2 != null && Vector3.Distance(skillPos, defender2.OriginPos) > 0.1f)
            skillDir = (defender2.OriginPos - skillPos).normalized;
    }

    /// <summary>
    /// 返回技能实例Id
    /// </summary>
    /// <returns></returns>
    public int getTemplateId()
    {
        return mTemplateId;
    }

    public t_skillBean getSkillBean()
    {
        return mSkillBean;
    }

    /// <summary>
    /// 发动技能前调用
    /// </summary>
    public virtual void onEnter(object param = null)
    {
        mParam = param;
        doSkill();
        //Debug.Log(Owner.GetActionManager().GetAniLen(SkillConfig.aniName.ToString()));
    }

    protected long shotCoroId;
    protected bool closeShot = false;
    /// <summary>
    /// 技能特写
    /// </summary>
    protected virtual void ShowCloseShot()
    {
        if (SkillConfig.closeShot != null)
        {
            int res;
            if (GTools.WillOccur(50, 100, out res))
            {
                if (FightManager.Singleton.CanPlayCloseShot())
                {
                    closeShot = true;
                    FightManager.Singleton.ChangeCameraCulling(true, (float)SkillConfig.closeShot.duration);
                    //FightManager.Singleton.AddCloseShotCd((long)(SkillConfig.closeShot.duration));
                    SkillConfig.closeShot.gameObject.SetActive(true);
                    VirtualCameraMgr.Singleton.SetCameraEase(0);
                    SkillConfig.closeShot.Play();
                    shotCoroId = CoroutineManager.Singleton.delayedCall((float)SkillConfig.closeShot.duration, () =>
                    {
                        SkillConfig.closeShot.gameObject.SetActive(false);
                    });
                }
            }
        }
    }

    protected virtual void doSkill()
    {
        oldForward = Owner.TransformExt.forward;
        //Owner.setDirection(skillDir);
    }

    protected virtual void PlaySound()
    {
        //if (!string.IsNullOrEmpty(SkillConfig.attackSound))
        //    SoundManager.Singleton.PlaySFX(SkillConfig.attackSound);
    }

    //****************根据动作列表自动播放动画*****************//
    protected virtual void playAnimation(string aniName, Action<int> onActionFinish = null, Action<int> onKeyAction = null)
    {
        int[] frames = SkillConfig.GetOtherKeyframes();
        Owner.GetActionManager().PlayAnimation(aniName, frames, onActionFinish, onKeyAction);
    }

    protected virtual void playEffect(MountPointType type, string eftPath)
    {
        if (string.IsNullOrEmpty(eftPath))
            return;

        MountPoint mp = SkillConfig.GetMountPoint(type);
        if (mp == null)
            return;

        if (mp.trans != null)
        {
            GameObject mLastEffect = FightManager.R.LoadGo(eftPath, mp.trans.position);
            if (mLastEffect != null)
            {
                if (mp.isLocal)
                {
                    mLastEffect.transform.SetParent(mp.trans, false);
                    mLastEffect.transform.resetLocal();
                }
                else
                {
                    if (mp.onlyRotateY)
                        mLastEffect.transform.rotation = Quaternion.Euler(0, Owner.TransformExt.rotation.eulerAngles.y, 0);
                    else
                        mLastEffect.transform.rotation = Owner.TransformExt.rotation;
                }
            }
        }
    }

    /// <summary>
    /// 关键帧回调
    /// </summary>
    /// <param name="name"></param>
    protected virtual void onKeyAction(int name)
    {

    }


    /// <summary>
    /// 动画播放完成后的回调
    /// </summary>
    protected virtual void onActionFinish(int key)
    {
        Owner.getStateMachine().changeState(ActorState.idle);
        //Owner.changeState(ActorState.DaiJi);
    }

    /// <summary>
    /// 发射子弹
    /// </summary>
    protected virtual void ShootBullet(SkillKeyFrame keyframe, Vector3? pos = null)
    {
        Vector3 startPos;
        if (pos.HasValue)
            startPos = pos.Value;
        else
            startPos = GetStartPos();

        //GameObject obj = Res.Singleton.InstantiateCEffect(SkillConfig.bulletPrefab, startPos);
        GameObject obj = FightManager.R.LoadGo(SkillConfig.bulletPrefab, startPos);
        if (obj == null)
            return;
        obj.transform.forward = Owner.TransformExt.forward;
        //string className = Enum.GetName(typeof(SkillKeyFrame.Type), keyframe.type);
        //Type classType = Type.GetType(className);
        Bullet bullet = obj.AddComponent<Bullet>();
        Vector3 targetPos = Vector3.zero;
        Actor target = ActorManager.Singleton.Get(TargetID);
        if (target != null && target.TransformExt != null)
        {
            targetPos = target.TransformExt.position;
            if (target.monoBehavior.hitPos != null)
                targetPos = target.monoBehavior.hitPos.position;
        }
        //如果是trigger模式把距离拉远
        if (keyframe.BulletModel == BulletModel.Trigger)
        {
            targetPos += Owner.TransformExt.forward * 5;
        }
        bullet.setSkill(SkillConfig.flySpeed, targetPos);
    }

    /// <summary>
    /// 获取子弹起始点坐标
    /// </summary>
    /// <returns></returns>
    public virtual Vector3 GetStartPos()
    {
        MountPoint mp = SkillConfig.GetMountPoint(MountPointType.Bullet);
        if (mp != null)
        {
            if (mp.trans != null)
                return mp.trans.position;
        }
        return Owner.TransformExt.position;
    }

    /// <summary>
    /// 发动技能中调用
    /// </summary>
    public virtual void onUpdate()
    {

    }

    protected virtual Vector3 GetColCenter(int col)
    {
        ActorCamp camp;
        if (Owner.isCampOf(ActorCamp.CampEnemy))
            camp = ActorCamp.CampFriend;
        else
            camp = ActorCamp.CampEnemy;
        return SpawnerHelper.Singleton.GetColCenter(camp, col);
    }

    protected virtual Vector3 GetRowCenter(int row)
    {
        ActorCamp camp;
        if (Owner.isCampOf(ActorCamp.CampEnemy))
            camp = ActorCamp.CampFriend;
        else
            camp = ActorCamp.CampEnemy;
        return SpawnerHelper.Singleton.GetRowCenter(camp, row);
    }

    /// <summary>
    /// 获取落雷位置(TODO:相对位置)
    /// </summary>
    /// <returns></returns>
    protected virtual Vector3 GetThunderPoint(ThunderPoint type)
    {
        switch (type)
        {
            case ThunderPoint.Target:
                Actor target = ActorManager.Singleton.Get(TargetID);
                if (target != null)
                    return target.OriginPos;//target.TransformExt.position;
                return Vector3.zero;
            case ThunderPoint.OppositeCenter:
                return GetColCenter(GridEnum.Col1);
            case ThunderPoint.FrontRowCenter:
                return GetRowCenter(GridEnum.Row0);
            case ThunderPoint.BackRowCenter:
                return GetRowCenter(GridEnum.Row1);
            case ThunderPoint.ColCenter:
                Actor target1 = ActorManager.Singleton.Get(TargetID);
                if (target1 != null)
                {
                    int col = FightManager.Singleton.Grid.InWhichCol(AttackUtils.GetEnemyCamp(Owner.getCamp()), TargetID);
                    return GetColCenter(col);
                }
                return Vector3.zero;
        }
        return Vector3.zero;
    }


    /// <summary>
    /// 停止技能时调用
    /// </summary>
    public virtual void stop()
    {
        mParam = null;
        TargetID = 0;
        if(defenders != null)
            defenders.Clear();
        showComboTip = false;
        showComboCircle = false;
        isMannul = false;
        closeShot = false;
        autoComboType = ComboType.Normal;
        //Owner.TransformExt.forward = oldForward;
        Owner.TweenDirection(Owner.OriginDir);
        CoroutineManager.Singleton.stopCoroutine(shotCoroId);
    }

}

