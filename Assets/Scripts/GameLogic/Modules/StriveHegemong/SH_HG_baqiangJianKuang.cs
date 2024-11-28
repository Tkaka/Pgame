using Message.KingFight;
using System.Collections.Generic;
using UI_StriveHegemong;

public class SH_HG_baqiangJianKuang : UI_SH_HG_baqiangJianKuang
{
    private UI_SH_HG_baqiangJianKuang window;
    private List<EightFightInfo> eightFightInfos;//八强匹配信息
    private List<long> roleId = new List<long>(11);
    private List<EightInfo> eightInfo;//昨日回顾八强
    public SH_HG_baqiangJianKuang(UI_SH_HG_baqiangJianKuang xs = null)
    {
        if (xs != null)
        {
            window = xs;
            AddKeyEvent();
        }
        else
        {
            AddHGEvent();
        }
    }
    public SH_HG_baqiangJianKuang()
    {
    }
    public new static SH_HG_baqiangJianKuang CreateInstance()
    {
        return (SH_HG_baqiangJianKuang)UI_SH_HG_baqiangJianKuang.CreateInstance();
    }
    public void HuiGuiInit()
    {
        AddHGEvent();
    }
    private void AddHGEvent()
    {
        m_One.m_one.onClick.Add(OnChaKan_One_One);
        m_One.m_two.onClick.Add(OnChaKan_One_Two);
        m_Two.m_one.onClick.Add(OnChaKan_Two_One);
        m_Two.m_two.onClick.Add(OnChaKan_Two_Two);
        m_Three.m_one.onClick.Add(OnChaKan_Three_One);
        m_Three.m_two.onClick.Add(OnChaKan_Three_Two);
        m_Four.m_one.onClick.Add(OnChaKan_Four_One);
        m_Four.m_two.onClick.Add(OnChaKan_Four_Two);
        m_five.onClick.Add(OnChaKan_Five);
        m_six.onClick.Add(OnChaKan_Six);
        m_seven.onClick.Add(OnChaKan_Seven);
        m_chakan_one.onClick.Add(OnLuXiang_One);
        m_chakan_two.onClick.Add(OnLuXiang_Two);
        m_chakan_three.onClick.Add(OnLuXiang_Three);
        m_chakan_four.onClick.Add(OnLuXiang_Four);
        m_chakan_five.onClick.Add(OnLuXiang_Five);
        m_chakan_six.onClick.Add(OnLuXiang_Six);
        m_chakan_seven.onClick.Add(OnLuXiang_Seven);
        GED.ED.addListener(EventID.OnStriveHegemongHuiGu,OnBaQiangHuiGu);
    }
    private void AddKeyEvent()
    {
        window.m_One.m_one.onClick.Add(OnChaKan_One_One);
        window.m_One.m_two.onClick.Add(OnChaKan_One_Two);
        window.m_Two.m_one.onClick.Add(OnChaKan_Two_One);
        window.m_Two.m_two.onClick.Add(OnChaKan_Two_Two);
        window.m_Three.m_one.onClick.Add(OnChaKan_Three_One);
        window.m_Three.m_two.onClick.Add(OnChaKan_Three_Two);
        window.m_Four.m_one.onClick.Add(OnChaKan_Four_One);
        window.m_Four.m_two.onClick.Add(OnChaKan_Four_Two);
        window.m_five.onClick.Add(OnChaKan_Five);
        window.m_six.onClick.Add(OnChaKan_Six);
        window.m_seven.onClick.Add(OnChaKan_Seven);
        window.m_chakan_one.onClick.Add(OnLuXiang_One);
        window.m_chakan_two.onClick.Add(OnLuXiang_Two);
        window.m_chakan_three.onClick.Add(OnLuXiang_Three);
        window.m_chakan_four.onClick.Add(OnLuXiang_Four);
        window.m_chakan_five.onClick.Add(OnLuXiang_Five);
        window.m_chakan_six.onClick.Add(OnLuXiang_Six);
        window.m_chakan_seven.onClick.Add(OnLuXiang_Seven);
    }
    public void Init()
    {
        eightFightInfos = StriveHegemongService.Singleton.eightMatchInfo.info;
        //摆阵，根据数量摆阵
        if (eightFightInfos.Count == 4)
        {
            OnBaQiangPiPei();
        }
        else if (eightFightInfos.Count == 6)
        {
            OnSiQiangPiPei();
        }
        else if (eightFightInfos.Count == 8)
        {
            OnJueSaiPiPei();
        }
        else if (eightFightInfos[0].winer != -1)
        {
            OnJueSaiJieGuo();
        }
    }
    //八强匹配完毕，进入准备状态
    private void OnBaQiangPiPei()
    {
        roleId.Clear();
        //第一组
        {
            UIGloader.SetUrl(window.m_One.m_one.m_touxiang,"");
            window.m_One.m_one.m_jieguo.visible = false;
            window.m_One.m_one.m_name.text = eightFightInfos[0].roles[0].name;
            window.m_One.m_one.m_ZhanLi.text = eightFightInfos[0].roles[0].fightPower.ToString();
            roleId.Add(eightFightInfos[0].roles[0].roleId);

            UIGloader.SetUrl(window.m_One.m_two.m_touxiang,"");
            window.m_One.m_two.m_jieguo.visible = false;
            window.m_One.m_two.m_name.text = eightFightInfos[0].roles[1].name;
            window.m_One.m_two.m_ZhanLi.text = eightFightInfos[0].roles[1].fightPower.ToString();
            roleId.Add(eightFightInfos[0].roles[1].roleId);
        }
        //第二组
        {
            UIGloader.SetUrl(window.m_Two.m_one.m_touxiang,"");
            window.m_Two.m_one.m_jieguo.visible = false;
            window.m_Two.m_one.m_name.text = eightFightInfos[1].roles[0].name;
            window.m_Two.m_one.m_ZhanLi.text = eightFightInfos[1].roles[0].fightPower.ToString();
            roleId.Add(eightFightInfos[1].roles[0].roleId);

            UIGloader.SetUrl(window.m_Two.m_two.m_touxiang,"");
            window.m_Two.m_two.m_jieguo.visible = false;
            window.m_Two.m_two.m_name.text = eightFightInfos[1].roles[1].name;
            window.m_Two.m_two.m_ZhanLi.text = eightFightInfos[1].roles[1].fightPower.ToString();
            roleId.Add(eightFightInfos[1].roles[1].roleId);
        }
        //第三组
        {
            UIGloader.SetUrl(window.m_Three.m_one.m_touxiang,"");
            window.m_Three.m_one.m_jieguo.visible = false;
            window.m_Three.m_one.m_name.text = eightFightInfos[2].roles[0].name;
            window.m_Three.m_one.m_ZhanLi.text = eightFightInfos[2].roles[0].fightPower.ToString();
            roleId.Add(eightFightInfos[2].roles[0].roleId);

            UIGloader.SetUrl(window.m_Three.m_two.m_touxiang,"");
            window.m_Three.m_two.m_jieguo.visible = false;
            window.m_Three.m_two.m_name.text = eightFightInfos[2].roles[1].name;
            window.m_Three.m_two.m_ZhanLi.text = eightFightInfos[2].roles[1].fightPower.ToString();
            roleId.Add(eightFightInfos[2].roles[1].roleId);
        }
        //第四组
        {
            UIGloader.SetUrl(window.m_Four.m_one.m_touxiang,"");
            window.m_Four.m_one.m_jieguo.visible = false;
            window.m_Four.m_one.m_name.text = eightFightInfos[3].roles[0].name;
            window.m_Four.m_one.m_ZhanLi.text = eightFightInfos[3].roles[0].fightPower.ToString();
            roleId.Add(eightFightInfos[3].roles[0].roleId);

            UIGloader.SetUrl(window.m_Four.m_two.m_touxiang,"");
            window.m_Four.m_two.m_jieguo.visible = false;
            window.m_Four.m_two.m_name.text = eightFightInfos[3].roles[1].name;
            window.m_Four.m_two.m_ZhanLi.text = eightFightInfos[3].roles[1].fightPower.ToString();
            roleId.Add(eightFightInfos[3].roles[1].roleId);
        }
        window.m_five.visible = false;
        window.m_six.visible = false;
        window.m_seven.visible = false;

        window.m_chakan_one.visible = false;
        window.m_chakan_two.visible = false;
        window.m_chakan_three.visible = false;
        window.m_chakan_four.visible = false;
        window.m_chakan_five.visible = false;
        window.m_chakan_six.visible = false;
        window.m_chakan_seven.visible = false;

        window.m_zhishi_one.visible = false;
        window.m_zhishi_tow.visible = false;
        window.m_zhishi_three.visible = false;
        window.m_zhishi_four.visible = false;
        window.m_zhishi_five.visible = false;
        window.m_zhishi_six.visible = false;
    }
    //四强战斗准备，将线条走向标识出来，中间留空
    private void OnSiQiangPiPei()
    {
        OnBaQiangPiPei();
        //调节走向线条颜色和查看录像按钮显示
        //第一组
        if (eightFightInfos[0].winer != -1)
        {
            window.m_zhishi_one.visible = true;
            if (eightFightInfos[0].roles[0].roleId == eightFightInfos[0].winer)
            {
                window.m_zhishi_one.m_you.grayed = true;
                window.m_One.m_two.grayed = true;
            }
            else if (eightFightInfos[0].roles[1].roleId == eightFightInfos[0].winer)
            {
                window.m_zhishi_one.m_zuo.grayed = true;
                window.m_One.m_one.grayed = true;
            }
            window.m_chakan_one.visible = true;
        }
        //第二组
        if (eightFightInfos[1].winer != -1)
        {
            window.m_zhishi_tow.visible = true;
            if (eightFightInfos[1].roles[0].roleId == eightFightInfos[1].winer)
            {
                window.m_zhishi_tow.m_zuo.grayed = true;
                window.m_Two.m_two.grayed = true;
            }
            else if (eightFightInfos[1].roles[1].roleId == eightFightInfos[1].winer)
            {
                window.m_zhishi_tow.m_you.grayed = true;
                window.m_Two.m_one.grayed = true;
            }
            window.m_chakan_two.visible = true;
        }
        //第三组
        if (eightFightInfos[2].winer != -1)
        {
            window.m_zhishi_three.visible = true;
            if (eightFightInfos[2].roles[0].roleId == eightFightInfos[2].winer)
            {
                window.m_zhishi_three.m_you.grayed = true;
                window.m_Three.m_two.grayed = true;
            }
            else if (eightFightInfos[2].roles[1].roleId == eightFightInfos[2].winer)
            {
                window.m_zhishi_three.m_zuo.grayed = true;
                window.m_Three.m_one.grayed = true;
            }
            window.m_chakan_three.visible = true;
        }
        //第四组
        if (eightFightInfos[3].winer != -1)
        {
            window.m_zhishi_four.visible = true;
            if (eightFightInfos[3].roles[0].roleId == eightFightInfos[3].winer)
            {
                window.m_zhishi_four.m_zuo.grayed = true;
                window.m_Four.m_two.grayed = true;
            }
            else if (eightFightInfos[3].roles[1].roleId == eightFightInfos[3].winer)
            {
                window.m_zhishi_four.m_you.grayed = true;
                window.m_Four.m_one.grayed = true;
            }
            window.m_chakan_four.visible = true;
        }
        window.m_five.visible = true;
        window.m_six.visible = true;
    }
    //填写中间两个的头像信息，并画出标识
    private void OnJueSaiPiPei()
    {
        OnSiQiangPiPei();

        UIGloader.SetUrl(window.m_five.m_touxiang,"");
        window.m_five.m_ZhanLi.text = eightFightInfos[6].roles[0].fightPower.ToString();
        window.m_five.m_name.text = eightFightInfos[6].roles[0].name;
        roleId.Add(eightFightInfos[6].roles[0].roleId);

        UIGloader.SetUrl(window.m_six.m_touxiang,"");
        window.m_six.m_ZhanLi.text = eightFightInfos[6].roles[1].fightPower.ToString();
        window.m_six.m_name.text = eightFightInfos[6].roles[1].name;
        roleId.Add(eightFightInfos[6].roles[1].roleId);

        window.m_chakan_five.visible = true;
        window.m_chakan_six.visible = true;

        window.m_zhishi_five.visible = true;
        window.m_zhishi_six.visible = true;

        window.m_seven.visible = true;
    }
    //决赛结果，填写最终胜利者的头像信息
    private void OnJueSaiJieGuo()
    {
        OnJueSaiPiPei();
        window.m_zhishi_five.visible = true;
        window.m_zhishi_six.visible = true;
        //头像置灰
        if (eightFightInfos[6].roles[0].roleId == eightFightInfos[0].winer)
        {
            window.m_six.grayed = true;
            UIGloader.SetUrl(window.m_seven.m_touxiang,"");
            window.m_seven.m_ZhanLi.text = eightFightInfos[0].roles[0].fightPower.ToString();
            window.m_seven.m_name.text = eightFightInfos[0].roles[0].name;
            roleId.Add(eightFightInfos[6].roles[0].roleId);
        }
        else if (eightFightInfos[6].roles[1].roleId == eightFightInfos[0].winer)
        {
            window.m_five.grayed = true;
            UIGloader.SetUrl(window.m_seven.m_touxiang,"");
            window.m_seven.m_ZhanLi.text = eightFightInfos[0].roles[1].fightPower.ToString();
            window.m_seven.m_name.text = eightFightInfos[0].roles[1].name;
            roleId.Add(eightFightInfos[6].roles[1].roleId);
        }
        window.m_chakan_seven.visible = true;
    }
    private void OnChaKan_One_One()
    { StriveHegemongService.Singleton.OnReqFightInfo(roleId.Count > 0 ? roleId[0] : -1); }
    private void OnChaKan_One_Two()
    { StriveHegemongService.Singleton.OnReqFightInfo(roleId.Count > 0 ? roleId[1] : -1); }
    private void OnChaKan_Two_One()                  
    { StriveHegemongService.Singleton.OnReqFightInfo(roleId.Count > 0 ? roleId[2] : -1); }
    private void OnChaKan_Two_Two()                 
    { StriveHegemongService.Singleton.OnReqFightInfo(roleId.Count > 0 ? roleId[3] : -1); }
    private void OnChaKan_Three_One()                
    { StriveHegemongService.Singleton.OnReqFightInfo(roleId.Count > 0 ? roleId[4] : -1); }
    private void OnChaKan_Three_Two()                
    { StriveHegemongService.Singleton.OnReqFightInfo(roleId.Count > 0 ? roleId[5] : -1); }
    private void OnChaKan_Four_One()                
    { StriveHegemongService.Singleton.OnReqFightInfo(roleId.Count > 0 ? roleId[6] : -1); }
    private void OnChaKan_Four_Two()                 
    { StriveHegemongService.Singleton.OnReqFightInfo(roleId.Count > 0 ? roleId[7] : -1); }
    private void OnChaKan_Five()                     
    { StriveHegemongService.Singleton.OnReqFightInfo(roleId.Count > 8 ? roleId[8] : -1); }
    private void OnChaKan_Six()                      
    { StriveHegemongService.Singleton.OnReqFightInfo(roleId.Count > 9 ? roleId[9] : -1); }
    private void OnChaKan_Seven()                    
    { StriveHegemongService.Singleton.OnReqFightInfo(roleId.Count > 10 ? roleId[10] : -1); }
    /// <summary>
    /// 一号位录像，查看第一组战斗
    /// </summary>
    private void OnLuXiang_One()
    { }
    /// <summary>
    /// 二号位录像，查看第二组战斗
    /// </summary>
    private void OnLuXiang_Two()
    { }
    /// <summary>
    /// 三号位录像，查看第三组战斗
    /// </summary>
    private void OnLuXiang_Three()
    { }
    /// <summary>
    /// 四号位录像，查看第四组战斗
    /// </summary>
    private void OnLuXiang_Four()
    { }
    /// <summary>
    /// 五号位录像，查看四进二第一组战斗
    /// </summary>
    private void OnLuXiang_Five()
    { }
    /// <summary>
    /// 六位录像，查看四进二第二组战斗
    /// </summary>
    private void OnLuXiang_Six()
    { }
    /// <summary>
    /// 七号位录像，查看决赛
    /// </summary>
    private void OnLuXiang_Seven()
    { }
    public void OnBaQiangHuiGuInit()
    {
        //得到八强信息
        List<EightInfo> eightInfo = StriveHegemongService.Singleton.Yesterday.infos;
    }

    //=====================================================================================
    //昨日回顾八强情况处理

    private void OnBaQiangHuiGu(GameEvent evt)
    {
        On_HuiGuInit();
        if (eightInfo.Count > 0)
            OnHG_BaQing();
        else
        {
            //第一次打开，没有信息
            OnFirst();
        }
    }
    private void On_HuiGuInit()
    {
        eightInfo = StriveHegemongService.Singleton.Yesterday.infos;
    }
    private void OnHG_BaQing()
    {
        roleId.Clear();
        eightInfo.Sort(StorPaml);
        //第一组
        {
            UIGloader.SetUrl(m_One.m_one.m_touxiang,"");
            m_One.m_one.m_jieguo.visible = false;
            m_One.m_one.m_name.text = eightInfo[0].winer.name;
            m_One.m_one.m_ZhanLi.text = eightInfo[0].winer.fightPower.ToString();
            roleId.Add(eightInfo[0].winer.roleId);

            UIGloader.SetUrl(m_One.m_two.m_touxiang, "");
            m_One.m_two.m_jieguo.visible = false;
            m_One.m_two.m_name.text = eightInfo[0].loser.name;
            m_One.m_two.m_ZhanLi.text = eightInfo[0].loser.fightPower.ToString();
            roleId.Add(eightInfo[0].loser.roleId);
        }
        //第二组
        {
            UIGloader.SetUrl(m_Two.m_one.m_touxiang,"");
            m_Two.m_one.m_jieguo.visible = false;
            m_Two.m_one.m_name.text = eightInfo[1].winer.name;
            m_Two.m_one.m_ZhanLi.text = eightInfo[1].winer.fightPower.ToString();
            roleId.Add(eightInfo[1].winer.roleId);

            UIGloader.SetUrl(m_Two.m_two.m_touxiang,"");
            m_Two.m_two.m_jieguo.visible = false;
            m_Two.m_two.m_name.text = eightInfo[1].loser.name;
            m_Two.m_two.m_ZhanLi.text = eightInfo[1].loser.fightPower.ToString();
            m_Two.m_two.grayed = true;
            roleId.Add(eightInfo[1].loser.roleId);
        }
        //第三组
        {
            UIGloader.SetUrl(m_Three.m_one.m_touxiang,"");
            m_Three.m_one.m_jieguo.visible = false;
            m_Three.m_one.m_name.text = eightInfo[2].winer.name;
            m_Three.m_one.m_ZhanLi.text = eightInfo[2].winer.fightPower.ToString();
            roleId.Add(eightInfo[2].winer.roleId);

            UIGloader.SetUrl(m_Three.m_two.m_touxiang,"");
            m_Three.m_two.m_jieguo.visible = false;
            m_Three.m_two.m_name.text = eightInfo[2].loser.name;
            m_Three.m_two.m_ZhanLi.text = eightInfo[2].loser.fightPower.ToString();
            m_Three.m_two.grayed = true;
            roleId.Add(eightInfo[2].loser.roleId);
        }
        //第四组
        {
            UIGloader.SetUrl(m_Four.m_one.m_touxiang,"");
            m_Four.m_one.m_jieguo.visible = false;
            m_Four.m_one.m_name.text = eightInfo[3].winer.name;
            m_Four.m_one.m_ZhanLi.text = eightInfo[3].winer.fightPower.ToString();
            roleId.Add(eightInfo[3].winer.roleId);

            UIGloader.SetUrl(m_Four.m_two.m_touxiang,"");
            m_Four.m_two.m_jieguo.visible = false;
            m_Four.m_two.m_name.text = eightInfo[3].loser.name;
            m_Four.m_two.m_ZhanLi.text = eightInfo[3].loser.fightPower.ToString();
            m_Four.m_two.grayed = true;
            roleId.Add(eightInfo[3].loser.roleId);
        }
        m_chakan_one.visible = true;
        m_chakan_two.visible = true;
        m_chakan_three.visible = true;
        m_chakan_four.visible = true;
        m_chakan_five.visible = true;
        m_chakan_six.visible = true;
        m_chakan_seven.visible = true;

        m_zhishi_one.visible = true;
        m_zhishi_one.m_zuo.grayed = false;
        m_zhishi_one.m_you.grayed = true;
        m_zhishi_tow.visible = true;
        m_zhishi_tow.m_zuo.grayed = true;
        m_zhishi_three.visible = true;
        m_zhishi_three.m_you.grayed = true;
        m_zhishi_four.visible = true;
        m_zhishi_four.m_zuo.grayed = true;
        m_zhishi_five.visible = true;
        m_zhishi_six.visible = true;
        //决赛
        bool xianshi = false;//为真胜者在第一二组，为假胜者在三四组
        for (int i = 0; i < 2; ++i)
        {
            if (eightInfo[0].winer == eightInfo[eightInfo.Count - 1].winer)
                xianshi = true;
            else if (eightInfo[0].loser == eightInfo[eightInfo.Count - 1].winer)
                xianshi = true;
        }
        if (xianshi)
        {
            //决赛胜利者
            UIGloader.SetUrl(m_five.m_touxiang,"");
            UIGloader.SetUrl(m_five.m_jieguo,"");
            m_five.m_name.text = eightInfo[eightInfo.Count - 1].winer.name;
            m_five.m_ZhanLi.text = eightInfo[eightInfo.Count - 1].winer.fightPower.ToString();
            roleId.Add(eightInfo[eightInfo.Count - 1].winer.roleId);
            //决赛失败者
            UIGloader.SetUrl(m_six.m_touxiang,"");
            UIGloader.SetUrl(m_six.m_jieguo,"");
            m_six.m_name.text = eightInfo[eightInfo.Count - 1].loser.name;
            m_six.m_ZhanLi.text = eightInfo[eightInfo.Count - 1].loser.fightPower.ToString();
            roleId.Add(eightInfo[eightInfo.Count - 1].loser.roleId);
            m_six.grayed = true;
        }
        else
        {
            //决赛胜利者
            UIGloader.SetUrl(m_five.m_touxiang,"");
            UIGloader.SetUrl(m_five.m_jieguo,"");
            m_five.m_name.text = eightInfo[eightInfo.Count - 1].loser.name;
            m_five.m_ZhanLi.text = eightInfo[eightInfo.Count - 1].loser.fightPower.ToString();
            roleId.Add(eightInfo[eightInfo.Count - 1].loser.roleId);
            m_five.grayed = true;
            //决赛失败者
            UIGloader.SetUrl(m_six.m_touxiang,"");
            UIGloader.SetUrl(m_six.m_jieguo,"");
            m_six.m_name.text = eightInfo[eightInfo.Count - 1].winer.name;
            m_six.m_ZhanLi.text = eightInfo[eightInfo.Count - 1].winer.fightPower.ToString();
            roleId.Add(eightInfo[eightInfo.Count - 1].winer.roleId);
        }
        //决赛胜利者
        UIGloader.SetUrl(m_seven.m_touxiang,"");
        UIGloader.SetUrl(m_seven.m_jieguo,"");
        m_seven.m_name.text = eightInfo[eightInfo.Count - 1].winer.name;
        m_seven.m_ZhanLi.text = eightInfo[eightInfo.Count - 1].winer.fightPower.ToString();
        roleId.Add(eightInfo[eightInfo.Count - 1].winer.roleId);
    }
    private void OnFirst()
    {
        m_One.visible = false;
        m_Two.visible = false;
        m_Three.visible = false;
        m_Four.visible = false;
        m_five.visible = false;
        m_six.visible = false;
        m_seven.visible = false;

        m_chakan_one.visible = false;
        m_chakan_two.visible = false;
        m_chakan_three.visible = false;
        m_chakan_four.visible = false;
        m_chakan_five.visible = false;
        m_chakan_six.visible = false;
        m_chakan_seven.visible = false;

        m_zhishi_one.visible = false;
        m_zhishi_tow.visible = false;
        m_zhishi_three.visible = false;
        m_zhishi_four.visible = false;
        m_zhishi_five.visible = false;
        m_zhishi_six.visible = false;

        m_First.visible = true;
    }
    private int StorPaml(EightInfo a, EightInfo b)
    {
        int resA = 0;
        int resB = 0;

        if (a.index > b.index)
            resA += 1000;
        else if (a.index < b.index)
            resB += 1000;
        if (resA > resB)
            return 1;
        else if (resA == resB)
            return 0;
        else
            return -1;
    }
    public override void Dispose()
    {
        GED.ED.removeListener(EventID.OnStriveHegemongHuiGu, OnBaQiangHuiGu);
        base.Dispose();
    }
    //=====================================================================================
    public void Close()
    {
        if (window == null)
        {
            GED.ED.removeListener(EventID.OnStriveHegemongHuiGu,OnBaQiangHuiGu);
        }
        window = null;
        eightFightInfos = null;
    }
}
