using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Data.Containers;

public class InitServiceCmd : ICommand
{

    public void Excute()
    {
        //Debug.LogError("------confloader init----------");

        //ConfLoader.Singleton.init("123456");
        //GameDataManager.Instance.loadAll();
        //ConfLoader.Singleton.kill();
        //Debug.LogError("------confloader kill----------");

        Logger.print("------service init----------");
        ServiceManager.Singleton.RegisterService<LoginService>(ServiceType.LoginService);
        ServiceManager.Singleton.RegisterService<RoleService>(ServiceType.RoleService);
        ServiceManager.Singleton.RegisterService<PetService>(ServiceType.PetService);
        ServiceManager.Singleton.RegisterService<GMService>(ServiceType.GMService);
        ServiceManager.Singleton.RegisterService<BagService>(ServiceType.BagService);
        ServiceManager.Singleton.RegisterService<LevelService>(ServiceType.LevelService);
        ServiceManager.Singleton.RegisterService<BattleService>(ServiceType.BattleService);
        ServiceManager.Singleton.RegisterService<TaskService>(ServiceType.TaskService);
        ServiceManager.Singleton.RegisterService<DrawCardService>(ServiceType.DrawCardService);
        ServiceManager.Singleton.RegisterService<ChallegeService>(ServiceType.ChallegeService);
        ServiceManager.Singleton.RegisterService<ArenaService>(ServiceType.ArenaService);
        ServiceManager.Singleton.RegisterService<TongXiangGuanServices>(ServiceType.TongXiangGuanService);
        ServiceManager.Singleton.RegisterService<HallFameService>(ServiceType.HallFameService);
        ServiceManager.Singleton.RegisterService<ShopService>(ServiceType.ShopService);
        ServiceManager.Singleton.RegisterService<MailService>(ServiceType.MailService);
        ServiceManager.Singleton.RegisterService<AchievementService>(ServiceType.AchievementService);
        ServiceManager.Singleton.RegisterService<ShenQiService>(ServiceType.ShenQiService);
        ServiceManager.Singleton.RegisterService<TalentService>(ServiceType.TalentService);
        ServiceManager.Singleton.RegisterService<UltemateTrainService>(ServiceType.UltemateTrainService);
        ServiceManager.Singleton.RegisterService<GuildService>(ServiceType.GuildService);
        ServiceManager.Singleton.RegisterService<StriveHegemongService>(ServiceType.StriveHegemongService);
        ServiceManager.Singleton.RegisterService<CloneTeamFightService>(ServiceType.CloneTeamFightService);
        ServiceManager.Singleton.RegisterService<ChatService>(ServiceType.ChatService);
        ServiceManager.Singleton.RegisterService<GuildBossService>(ServiceType.GuildBossService);
        ServiceManager.Singleton.RegisterService<FuncService>(ServiceType.FuncService);
        ServiceManager.Singleton.RegisterService<VipService>(ServiceType.VipService);
        ServiceManager.Singleton.RegisterService<RechargeService>(ServiceType.RechargeService);
        ServiceManager.Singleton.RegisterService<GuideService>(ServiceType.GuideService);
        ServiceManager.Singleton.RegisterService<GuildBattleService>(ServiceType.GuildBattleService);
        ServiceManager.Singleton.RegisterService<TopService>(ServiceType.TopService);
        ServiceManager.Singleton.RegisterService<AoyiService>(ServiceType.AoyiService);
        ServiceManager.Singleton.RegisterService<ReplayService>(ServiceType.ReplayService);
        ServiceManager.Singleton.RegisterService<FightService>(ServiceType.FightService);
        ServiceManager.Singleton.RegisterService<DailyActivityService>(ServiceType.DailyActivity);
    }

}