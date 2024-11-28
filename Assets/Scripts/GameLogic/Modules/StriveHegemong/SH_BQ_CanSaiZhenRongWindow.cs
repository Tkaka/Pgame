using Message.KingFight;
using Message.Pet;
using System.Collections.Generic;
using UI_StriveHegemong;

public class SH_BQ_CanSaiZhenRongWindow : BaseWindow
{
    private UI_SH_BQ_CanSaiZhenRongWindow window;
    private List<BaseInfo> cansaizhenrong;
    
    public override void OnOpen()
    {
        window = getUiWindow<UI_SH_BQ_CanSaiZhenRongWindow>();
        window.m_CliseBtn.onClick.Add(OnCloseBtn);
        if (Info.param != null)
        {
            if ((bool)Info.param)
            {
                List<int> shangzhen = StriveHegemongService.Singleton.shangzhenList;
                //我的参赛阵容
                cansaizhenrong = new List<BaseInfo>();
                BaseInfo info;
                for (int i = 0; i < shangzhen.Count; ++i)
                {
                    info = new BaseInfo();
                    PetInfo petinfo = PetService.Singleton.GetPetInfo(shangzhen[i]);
                    if (petinfo == null)
                    {
                        info = null;
                    }
                    else
                    {
                        info.petBaseInfo.id = petinfo.petId;
                        info.petBaseInfo.color = petinfo.basInfo.color;
                        info.petBaseInfo.star = petinfo.basInfo.star;
                        info.precedeValue = petinfo.priority;
                        info.fightPower = petinfo.fightInfo.fightPower;
                        info.index = i;
                    }
                    cansaizhenrong.Add(info);
                }
            }
            else
            {
                //对手参赛阵容
                if (GameManager.Singleton.IsDebug)
                    Test();
                else
                {
                    List<BaseInfo> duishou = new List<BaseInfo>();
                    cansaizhenrong = StriveHegemongService.Singleton.targetFightInfo.baseInfo;
                    for (int i = 0; i < 10; ++i)
                    {
                        bool yongyou = false;
                        for (int j = 0; j < 10; ++j)
                        {
                            if (cansaizhenrong[j].index == i)
                            {
                                duishou.Add(cansaizhenrong[j]);
                                yongyou = true;
                                break;
                            }
                        }
                        if (yongyou == false)
                        {
                            duishou.Add(null);
                        }
                    }
                    cansaizhenrong = duishou;
                }
            }
        }
        InitView();
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
        SH_ZR_DianFen dianFen;
        for (int i = 0; i < 3; ++i)
        {
            if (i < 3)
            {
                if (i == 2)
                {
                    UI_SH_tianchong tianchong = UI_SH_tianchong.CreateInstance();
                    window.m_zhenrongList.AddChild(tianchong);
                }
                dianFen = SH_ZR_DianFen.CreateInstance();
                dianFen.Init(cansaizhenrong[3 * i + 0], cansaizhenrong[3 * i + 1], cansaizhenrong[3 * i + 2]);
                window.m_zhenrongList.AddChild(dianFen);
            }
        }
        UI_SH_ZR_SaiCheng tibu = UI_SH_ZR_SaiCheng.CreateInstance();
        tibu.m_Changci.text = "替补";
        tibu.m_juese.m_dengji.text = cansaizhenrong[9].petBaseInfo.level.ToString();
        UIGloader.SetUrl(tibu.m_juese.m_pinjie, UIUtils.GetBorderByQuality(cansaizhenrong[9].petBaseInfo.color));
        UIGloader.SetUrl(tibu.m_juese.m_touxiang, UIUtils.GetPetStartIcon(cansaizhenrong[9].petBaseInfo.id, cansaizhenrong[9].petBaseInfo.star));
        tibu.m_juese.m_xuanzhong.visible = false;

        StarList starList = new StarList((UI_Common.UI_StarList)tibu.m_juese.m_xingji);
        starList.SetStar(cansaizhenrong[9].petBaseInfo.star);

        window.m_zhenrongList.AddChild(tibu);
    }
    protected override void OnCloseBtn()
    {
        cansaizhenrong = null;
        window = null;
        base.OnCloseBtn();
    }
}
