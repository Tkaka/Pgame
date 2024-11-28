using Data.Beans;
using Message.Pet;

public class ActorInitHelper
{

    public static void InitProperty(Actor actor)
    {
        ActorType type = actor.getActorType();
        if (actor.isActorType(ActorType.Monster))
        {
            t_monsterBean mBean = ConfigBean.GetBean<t_monsterBean, int>(actor.getTemplateId());
            actor.setBaseProperty(PropertyType.Hp, mBean.t_hp);
            actor.setBaseProperty(PropertyType.Atk, mBean.t_atk);
            actor.setBaseProperty(PropertyType.Mp, BattleParam.MaxMp);
            actor.setBaseProperty(PropertyType.ShangHaiLv, mBean.t_shanghai_lv);
            actor.setBaseProperty(PropertyType.MianShangLv, mBean.t_mianshang_lv);
            OtherProperty(actor);
            actor.setName(mBean.t_name);

            //初始化当前属性
            actor.PropertyMgr.CopyBaseToNow();
            actor.SetProperty(PropertyType.Mp, 0);
            //_AddCanAttackProperty(actor);
        }
        else if (actor.isActorType(ActorType.Pet))
        {
            InitPet(actor);
        }
        else if (actor.isActorType(ActorType.Boss))
        {
            InitBoss(actor, null);
            //_AddCanAttackProperty(actor);
        }
        else if (type == ActorType.Player)
        {
            //读取玩家表
            //t_professionBean mBean = ConfigBean.GetBean<t_professionBean, int>(actor.getTemplateId());
            actor.PropertyMgr.CopyBaseToNow();
        }
        else
        {
            Logger.err("ActorInitHelper:initProperty:未知的角色类型");
        }

        if (FightManager.Singleton.IsReplay == false)
        {
            FightService.Singleton.SetActorCurProperty(actor);
        }
 

        //if (FightManager.Singleton.IsReplay)
        //{
        //    Debuger.Log("---------------->>重播" + actor.getTemplateId() + "   " + actor.getProperty(PropertyType.Hp) + "   " + actor.getProperty(PropertyType.Def) + "  " + actor.getProperty(PropertyType.Atk));
        //}
        //else
        //{
        //    Debuger.Log("---------------->>直播" + actor.getTemplateId() + "   " + actor.getProperty(PropertyType.Hp) + "   " + actor.getProperty(PropertyType.Def) + "  " + actor.getProperty(PropertyType.Atk));
        //}

    }

    //添加不能攻击属性
    private static void _AddCanAttackProperty(Actor actor)
    {
        if(FightService.Singleton.FightType == EFightType.CoinDungeon || FightService.Singleton.FightType == EFightType.ExpDungeon)
            actor.SetProperty(PropertyType.CanAttack, 1);
    }

    public static void InitPet(Actor actor)
    {
        //初始化成长信息
        t_petBean mBean = ConfigBean.GetBean<t_petBean, int>(actor.getTemplateId());
        if (GameManager.Singleton.IsDebug)
        {
            actor.setBaseProperty(PropertyType.Atk, mBean.t_atk);
            actor.setBaseProperty(PropertyType.Def, mBean.t_def);
            actor.setBaseProperty(PropertyType.Hp, mBean.t_hp);
            actor.setBaseProperty(PropertyType.BaoJiLv, mBean.t_baoji);
            actor.setBaseProperty(PropertyType.KangBaoJiLv, mBean.t_anti_baoji);
            actor.setBaseProperty(PropertyType.BaoJiQiangDu, mBean.t_baoji_strength);
            actor.setBaseProperty(PropertyType.GeDangLv, mBean.t_gedang);
            actor.setBaseProperty(PropertyType.PoJiLv, mBean.t_poji);
            actor.setBaseProperty(PropertyType.GeDangQiangDu, mBean.t_gedang_strength);
            actor.setBaseProperty(PropertyType.ShangHaiLv, mBean.t_shanghai);
            actor.setBaseProperty(PropertyType.MianShangLv, mBean.t_mianshang);
            actor.setBaseProperty(PropertyType.Mp, BattleParam.MaxMp);
            OtherProperty(actor);
            actor.setName(mBean.t_name);
            //怒气基础值修正
            actor.setBaseProperty(PropertyType.KillGetMp, actor.getBaseProperty(PropertyType.KillGetMp) + BattleParam.KillGetMp);
            actor.setBaseProperty(PropertyType.AtkGetMp, actor.getBaseProperty(PropertyType.AtkGetMp) + BattleParam.AtkGetMp);
            actor.setBaseProperty(PropertyType.HurtGetMp, actor.getBaseProperty(PropertyType.HurtGetMp) + BattleParam.HurtGetMp);
            //初始化当前属性
            actor.PropertyMgr.CopyBaseToNow();
            if(!GameManager.Singleton.DebugInfo.maxMp)
                actor.SetProperty(PropertyType.Mp, 0);
        }
        else
        {
            actor.setName(mBean.t_name);
            if (FightManager.Singleton.IsReplay)
            {
                OtherProperty(actor);
                actor.PropertyMgr.CopyBaseToNow();

                ReplayService.Singleton.SetActorProperty(actor);
                actor.setBaseProperty(PropertyType.Mp, BattleParam.MaxMp);

            }
            else
            {
                FightService.Singleton.SetActorProperty(actor);
                OtherProperty(actor);
                //怒气基础值修正
                actor.setBaseProperty(PropertyType.KillGetMp, actor.getBaseProperty(PropertyType.KillGetMp) + BattleParam.KillGetMp);
                actor.setBaseProperty(PropertyType.AtkGetMp, actor.getBaseProperty(PropertyType.AtkGetMp) + BattleParam.AtkGetMp);
                actor.setBaseProperty(PropertyType.HurtGetMp, actor.getBaseProperty(PropertyType.HurtGetMp) + BattleParam.HurtGetMp);
                //初始化当前属性
                actor.PropertyMgr.CopyBaseToNow();
              
                //宠物最大怒气值为1000
                actor.setBaseProperty(PropertyType.Mp, BattleParam.MaxMp);
                //Logger.err(actor.Name + "基础怒气：" + actor.getBaseProperty(PropertyType.Mp) + "当前怒气：" + actor.getProperty(PropertyType.Mp));
            }
        }
    }

    public static void InitBoss(Actor actor, PetInfo petInfo)
    {
        t_monster_boosBean mBean = ConfigBean.GetBean<t_monster_boosBean, int>(actor.getTemplateId());
        actor.setBaseProperty(PropertyType.Atk, mBean.t_atk);
        actor.setBaseProperty(PropertyType.Def, mBean.t_def);
        actor.setBaseProperty(PropertyType.Hp, mBean.t_hp);
        actor.setBaseProperty(PropertyType.BaoJiLv, mBean.t_baoji_lv);
        actor.setBaseProperty(PropertyType.KangBaoJiLv, mBean.t_kangbaolv);
        actor.setBaseProperty(PropertyType.BaoJiQiangDu, mBean.t_baoji_qiangdu);
        actor.setBaseProperty(PropertyType.GeDangLv, mBean.t_gedang);
        actor.setBaseProperty(PropertyType.PoJiLv, mBean.t_poji_lv);
        actor.setBaseProperty(PropertyType.GeDangQiangDu, mBean.t_gedang_qiangdu);
        actor.setBaseProperty(PropertyType.ShangHaiLv, GTools.ScaleInt2LNumber(mBean.t_shanghai_lv));
        actor.setBaseProperty(PropertyType.MianShangLv, GTools.ScaleInt2LNumber(mBean.t_mianshang_lv));
        //actor.setBaseProperty(PropertyType.Mp, BattleParam.MaxMp);
        actor.setBaseProperty(PropertyType.Mp, mBean.t_max_mp);     //boss怒气上限值不一定是1000
        actor.setBaseProperty(PropertyType.XiXueLv, mBean.t_xixue_lv);
        actor.setBaseProperty(PropertyType.ZhiLiaoLv, mBean.t_zhiliao_lv);
        actor.setBaseProperty(PropertyType.ZhiLiaoXiaoGuo, mBean.t_zhiliao);
        actor.setBaseProperty(PropertyType.KongZhiLv, mBean.t_kongzhi_lv);
        actor.setBaseProperty(PropertyType.MianKongLv, mBean.t_miankong_lv);
        actor.setBaseProperty(PropertyType.JueJiShangHaiLv, mBean.t_jueji_shanghai_lv);
        actor.setBaseProperty(PropertyType.JueJiFangYuLv, mBean.t_jueji_fangyu_lv);
        actor.setBaseProperty(PropertyType.GongJiLv, mBean.t_gongji_lv);
        actor.setBaseProperty(PropertyType.FangYuLv, mBean.t_fangyu_lv);
        actor.setBaseProperty(PropertyType.ShangHaiFanTanLv, mBean.t_shanghai_fantan_lv);
        actor.setBaseProperty(PropertyType.DuiGongShangHaiLv, mBean.t_duigong_shanghai_lv);
        actor.setBaseProperty(PropertyType.DuiJiShangHaiLv, mBean.t_duiji_shanghai_lv);
        actor.setBaseProperty(PropertyType.DuiFangShangHaiLv, mBean.t_duifang_shanghai_lv);
        actor.setBaseProperty(PropertyType.DuiGongMianShangLv, mBean.t_duigong_mianshang_lv);
        actor.setBaseProperty(PropertyType.DuiJiMianShangLv, mBean.t_duiji_mianshang_lv);
        actor.setBaseProperty(PropertyType.DuiFangMianShangLv, mBean.t_duifang_mianshang_lv);
        actor.setBaseProperty(PropertyType.NengLiangHuiHuSuDu, mBean.t_nengliang_speed);
        actor.setBaseProperty(PropertyType.DiKangLv, mBean.t_dikang_lv);
        actor.setBaseProperty(PropertyType.XueLiangXingDiKangLv, mBean.t_xueliangxing_dikang_lv);
        actor.setBaseProperty(PropertyType.DebuffMianYiLv, mBean.t_debuff_mianyi_lv);
        actor.setBaseProperty(PropertyType.SmallSkillRateAdd, mBean.t_xiaojineng_gailv_jiacheng);
        actor.setBaseProperty(PropertyType.KillGetMp, BattleParam.KillGetMp);
        actor.setBaseProperty(PropertyType.AtkGetMp, GTools.ScaleInt2LNumber(mBean.att_energy_add) + BattleParam.AtkGetMp);
        actor.setBaseProperty(PropertyType.HurtGetMp, GTools.ScaleInt2LNumber(mBean.hit_energy_add) + BattleParam.HurtGetMp);
        actor.setBaseProperty(PropertyType.TurnGetMp, GTools.ScaleInt2LNumber(mBean.turn_energy_add));
        actor.setBaseProperty(PropertyType.FriendDeadGetMp, GTools.ScaleInt2LNumber(mBean.fdead_energy_add));
        actor.setBaseProperty(PropertyType.EnemyDeadGetMp, GTools.ScaleInt2LNumber(mBean.enemydead_energy_add));
        OtherProperty(actor);
        actor.setName(mBean.t_name);
        //初始化当前属性
        actor.PropertyMgr.CopyBaseToNow();
        actor.SetProperty(PropertyType.Mp, GTools.ScaleInt2LNumber(mBean.t_mp));
        Logger.log(actor.Name + " boss 出场怒气 " + actor.getProperty(PropertyType.Mp));
    }

    private static void OtherProperty(Actor actor)
    {
        actor.setBaseProperty(PropertyType.HurtAdd1, 0);
        actor.setBaseProperty(PropertyType.HurtAdd2, 0);
        actor.setBaseProperty(PropertyType.IsNumbness, 0);
        actor.setBaseProperty(PropertyType.IsDizziness, 0);
        actor.setBaseProperty(PropertyType.IsIce, 0);
        actor.setBaseProperty(PropertyType.ImmuneCtrlPriority, 0);
        actor.setBaseProperty(PropertyType.ImmuneDebuffPriority, 0);
        actor.setBaseProperty(PropertyType.IsSilence, 0);
        actor.setBaseProperty(PropertyType.FuHuoLv, 0);
        actor.setBaseProperty(PropertyType.HasBehead, 0);
        actor.setBaseProperty(PropertyType.CanAttack, 0);
    }

}
