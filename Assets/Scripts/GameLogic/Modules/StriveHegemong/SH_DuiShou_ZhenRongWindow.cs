using Message.KingFight;
using System.Collections.Generic;
using UI_StriveHegemong;

public class SH_DuiShou_ZhenRongWindow : BaseWindow
{
    UI_SH_DuiShou_ZhenRongWindow window;
    private List<BaseInfo> cansaizhenrong;
    private List<int> mypetid;

    public override void OnOpen()
    {
        window = getUiWindow<UI_SH_DuiShou_ZhenRongWindow>();
        window.m_CloseBtn.onClick.Add(OnCloseBtn);
        if (GameManager.Singleton.IsDebug)
            Test();
        else if (Info.param == null)
            cansaizhenrong = StriveHegemongService.Singleton.targetFightInfo.baseInfo;
        mypetid = StriveHegemongService.Singleton.shangzhenList;
        InitView();
        FillData();
    }
    private void Test()
    {
        cansaizhenrong = new List<BaseInfo>();
        for (int i = 0; i < 10; ++i)
        {
            BaseInfo info = new BaseInfo();
            info.precedeValue = 100;
            info.fightPower = 10000;
            info.petBaseInfo.id = 101;
            info.petBaseInfo.level = 20;
            info.petBaseInfo.star = 5;
            info.petBaseInfo.color = 4;
            cansaizhenrong.Add(info);
        }
    }
    public override void InitView()
    {
        if (Info.param == null)
        {
            SH_ZR_SaiCheng saiCheng;
            for (int i = 0; i < 10; ++i)
            {
                saiCheng = SH_ZR_SaiCheng.CreateInstance();
                if (i < cansaizhenrong.Count)
                    saiCheng.Init(i, cansaizhenrong[i]);
                else
                    saiCheng.Init(i,null);
                window.m_zhenrongList.AddChild(saiCheng);
            }
        }
        else
        {
            SH_ZR_SaiCheng saiCheng;
            for (int i = 0; i < 10; ++i)
            {
                saiCheng = SH_ZR_SaiCheng.CreateInstance();
                if (i < mypetid.Count)
                {
                    saiCheng.OnMyInit(mypetid[i], i);
                }
                else
                {
                    saiCheng.OnMyInit(-1,i);
                }
                window.m_zhenrongList.AddChild(saiCheng);
            }
        }
    }
    private void FillData()
    {
        string zhankuang = "{0}胜{1}负";
        if (Info.param == null)
        {
            if (StriveHegemongService.Singleton.targetFightInfo != null)
            {
                //对手:名字、等级、是否在线、战况
                window.m_name.text = StriveHegemongService.Singleton.targetFightInfo.name;
                window.m_dengji.text = StriveHegemongService.Singleton.targetFightInfo.level.ToString();
                if (StriveHegemongService.Singleton.targetFightInfo.online == 1)
                {
                    window.m_zhuangtai.text = "离线";
                }
                else if (StriveHegemongService.Singleton.targetFightInfo.online == 0)
                {
                    window.m_zhuangtai.text = "在线";
                }
               
                int sheng = StriveHegemongService.Singleton.targetFightInfo.winNum;
                int fu = StriveHegemongService.Singleton.targetFightInfo.failedNum;

                window.m_zhanji.text = string.Format(zhankuang,sheng.ToString(),fu.ToString());
            }
        }
        else
        {
            window.m_name.text = RoleService.Singleton.RoleInfo.roleInfo.roleName;
            window.m_dengji.text = RoleService.Singleton.RoleInfo.roleInfo.level.ToString();
            window.m_zhuangtai.text = "在线";
            int sheng = StriveHegemongService.Singleton.myRaceInfo.winNum;
            int fu = StriveHegemongService.Singleton.myRaceInfo.failedNum;
            window.m_zhanji.text = string.Format(zhankuang,sheng.ToString(),fu.ToString());
        }
    }
    protected override void OnCloseBtn()
    {
        window = null;
        base.OnCloseBtn();
    }
}