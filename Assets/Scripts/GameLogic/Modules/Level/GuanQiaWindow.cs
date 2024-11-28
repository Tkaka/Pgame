using UI_Level;
using UI_Common;
using FairyGUI;
using Message.Role;
using Data.Beans;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GuanQiaWindow : BaseWindow
{
    private UI_GuanQiaWindow m_window;
    private List<GImage> m_imgList = new List<GImage>();
    private int m_levelId;             //关卡ID
    private int m_starLevel;           //星级


    public override void OnOpen()
    {
        base.OnOpen();
         
        m_window = getUiWindow<UI_GuanQiaWindow>();
        m_window.m_btnBgMask.onClick.Add(Close);
        _InitInfo();
        InitView();
        PlayPopupAnim(m_window.m_btnBgMask, m_window.m_guanQiaPopupView);
    }

    private void _InitInfo()
    {
        m_imgList.Clear();
        if (m_window != null)
        {
            m_imgList.Add(m_window.m_guanQiaPopupView.m_imgStar.m_star1);
            m_imgList.Add(m_window.m_guanQiaPopupView.m_imgStar.m_star2);
            m_imgList.Add(m_window.m_guanQiaPopupView.m_imgStar.m_star3);
        }

        OneParam<int> param = Info.param as OneParam<int>;
        if (param != null)
        {
            m_levelId = param.value;
        }


        m_starLevel = LevelService.Singleton.GetActStarInfoById(m_levelId);
        
    }

    public override void InitView()
    {
        base.InitView();

        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(m_levelId);
        if (null != actBean)
        {
            m_window.m_guanQiaPopupView.m_txtGuanQia.text = actBean.t_name_id;
            m_window.m_guanQiaPopupView.m_txtBossDescribe.text = actBean.t_intro_id;
            //m_window.m_txtLevelTarget.text = "目标描述";
            SetStarTxt();

            m_window.m_guanQiaPopupView.m_txtComsumeNum.text =  string.Format("X{0}", actBean.t_comsumePower);

            m_window.m_guanQiaPopupView.m_txtComsumeNum.color = roleInfo.energy >= actBean.t_comsumePower ? Color.green : Color.red;

            List<int> itemList = GTools.splitStringToIntList(actBean.t_drop_show_id);
            m_window.m_guanQiaPopupView.m_rewardList.RemoveChildren(0, -1, true);
            for (int i = 0; i < itemList.Count; i++)
            {
                CommonItem item = CommonItem.CreateInstance();
                if (null != item)
                {
                    item.isShowNum = false;
                    item.itemId = itemList[i];
                    item.AddPopupEvent();
                    item.RefreshView();
                    m_window.m_guanQiaPopupView.m_rewardList.AddChild(item);
                }

            }

            m_window.m_guanQiaPopupView.m_rewardList.EnsureBoundsCorrect();

            // 加载扉页图片
            t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(actBean.t_chapter_id);
            UIGloader.SetUrl(m_window.m_guanQiaPopupView.m_imgGuanQia, chapterBean.t_feiye_img);

            UIGloader.SetUrl(m_window.m_guanQiaPopupView.m_lihuiLoader, actBean.t_lihui);
        }
 
        //星级

        for (int i = 0; i < m_imgList.Count; i++)
        {
            m_imgList[i].visible = m_starLevel >= i + 1;
        }

        m_window.m_guanQiaPopupView.m_btnSaoDang.visible = m_starLevel > 0;
        m_window.m_guanQiaPopupView.m_btnSaoDang10.visible = LevelService.Singleton.CheckCanSweep10(m_levelId);
        m_window.m_guanQiaPopupView.m_btnSaoDang50.visible = LevelService.Singleton.CheckCanSweep50(m_levelId);

        
        m_window.m_guanQiaPopupView.m_btnSaoDang.onClick.Add(_OnSaoDangClick);
        m_window.m_guanQiaPopupView.m_btnChallange.onClick.Add(_OnChallengeClick);
        m_window.m_guanQiaPopupView.m_btnSaoDang10.onClick.Add(_OnFastSaoDang10Click);
        m_window.m_guanQiaPopupView.m_btnSaoDang50.onClick.Add(_OnFastSaoDang50Click);

    }

    public void SetStarTxt()
    {
        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(m_levelId);
        t_languageBean lanbean = null;
        if (bean != null && !string.IsNullOrEmpty(bean.t_star_hit))
        {
            int[] arr = GTools.splitStringToIntArray(bean.t_star_hit);
            if (arr != null && arr.Length >= 2)
            {
                switch (arr[0])
                {
                    case (int)ComboType.Normal:
                        lanbean = ConfigBean.GetBean<t_languageBean, int>(30002);
                        break;
                    case (int)ComboType.NotBad:
                        lanbean = ConfigBean.GetBean<t_languageBean, int>(30003);
                        break;
                    case (int)ComboType.Good:
                        lanbean = ConfigBean.GetBean<t_languageBean, int>(30004);
                        break;
                    case (int)ComboType.Perfect:
                        lanbean = ConfigBean.GetBean<t_languageBean, int>(30005);
                        break;
                    default:
                        break;
                }
            }
            if (lanbean != null)
            {
                string tempStr = "";
                if (arr[1] >= 10000)
                {
                    //个数
                    var lb = ConfigBean.GetBean<t_languageBean, int>(30009);
                    if (lb != null)
                        tempStr = string.Format(lb.t_content, lanbean.t_content, arr[1] / 10000);
                }
                else
                {
                    //百分比
                    var lb = ConfigBean.GetBean<t_languageBean, int>(30001);
                    if (lb != null)
                        tempStr = string.Format(lb.t_content, lanbean.t_content, arr[1] / 100);
                }

                m_window.m_guanQiaPopupView.m_txtLevelTarget.text = string.Format("{0}{1}",UIUtils.GetStrByLanguageID(30010) , tempStr);
            }
        }
    }
    private void _OnSaoDangClick()
    {
        if (LevelService.Singleton.CheckCanDo(m_levelId, 1))
        {
            LevelService.Singleton.SingleActSweep(m_levelId, 1);
        }
        
    }

    private void _OnFastSaoDang10Click()
    {

        if (LevelService.Singleton.CheckCanDo(m_levelId,10))
        {
            LevelService.Singleton.SingleActSweep(m_levelId, 10);
        }
    }

    private void _OnFastSaoDang50Click()
    {
        if (LevelService.Singleton.CheckCanDo(m_levelId, 50))
        {
            LevelService.Singleton.SingleActSweep(m_levelId, 50);
        }
         
    }

    private void _OnChallengeClick()
    {
        if (LevelService.Singleton.CheckCanDo(m_levelId, 1, false))
        {
            LevelService.Singleton.ReqFightStart(m_levelId);
            Close();
        }
         
    }

 

    protected override void OnClose()
    {
        base.OnClose();
        RestoreWndMgr.Singleton.SaveWndData<GuanQiaWindow>(Info);
        m_imgList.Clear();
    }
}


