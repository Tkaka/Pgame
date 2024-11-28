using Data.Beans;
using FairyGUI;
using Message.DrawCard;
using System.Collections.Generic;
using UI_DrawCard;
using UnityEngine;

public class ZhaoHuanWindow : BaseWindow
{
    private UI_ZhaoHuanWindow window;
    GameObject moxing;
    private GameObject texiao;
    private int type;
    private  List<int> typeId = new List<int>();
    private SimpleAnimation animation;
    private DoActionInterval doAction;
    private double time;
    private GameObject changjing;
    private Animator animator;
    private ResDraw m_resDrawResult;

    public override void AddEventListener()
    {
        GED.ED.addListener(EventID.OnResDrawCard, OnResDrawCardInfo);
        GED.ED.addListener(EventID.OnResAcross,OnZuanShiMianFeiChongZhi);
        GED.ED.addListener(EventID.OnDrawCard, OnZhaoHuan);
        GED.ED.addListener(EventID.OnDrawCardEndZhanShi,OnZhanShiJieShu);
    }
    public override void RemoveEventListener()
    {
        GED.ED.removeListener(EventID.OnResDrawCard, OnResDrawCardInfo);
        GED.ED.removeListener(EventID.OnResAcross, OnZuanShiMianFeiChongZhi);
        GED.ED.removeListener(EventID.OnDrawCard, OnZhaoHuan);
        GED.ED.removeListener(EventID.OnDrawCardEndZhanShi, OnZhanShiJieShu);
    }
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_ZhaoHuanWindow>();
        AddKeyEvent();
        InitView();
    }
    
    public override void InitView()
    {
        base.InitView();
        TopRoleInfo.Show();
        window.m_TaiTou.visible = true;
        if (Info.param == null)
        {
            Logger.err("ZhaoHuanWindow:InitView:未传入奖励类型无法启动界面");
            return;
        }

        PlayOpenEffect();
        ((UI_Common.UI_commonTop)window.m_TaiTou).m_anim.Play();
        type = (int)Info.param;//得到模式数据
        //加载奖励一览按钮
        //加载语言显示按钮
        if (type == 1)
        {
            window.m_zuanshiyilanBtn.visible = false;
            //window.m_jinbiyilanBtn.visible = true;
            window.m_ZuanShiMiaoShu.visible = false;
            //window.m_JinBiMiaoShu.visible = true;
        }
        else if (type == 2)
        {
            //window.m_zuanshiyilanBtn.visible = true;
            window.m_jinbiyilanBtn.visible = false;
            window.m_JinBiMiaoShu.visible = false;
            //window.m_ZuanShiMiaoShu.visible = true;
        }
        OnChouJiangMoShi();
        //分类加载模型
        OnShowModel();
    }
    public override void RefreshView()
    {
        base.RefreshView();

        DC_Type listItem;
        window.m_moshiList.RemoveChildren(0, -1, true);
        for (int i = 0; i < typeId.Count; ++i)
        {
            t_draw_cardBean cardBean = ConfigBean.GetBean<t_draw_cardBean, int>(typeId[i]);
            if (RoleService.Singleton.RoleInfo.roleInfo.level >= cardBean.t_level)
            {
                listItem = DC_Type.CreateInstance();
                listItem.name = "Type" + cardBean.t_num;
                listItem.Init(type, cardBean.t_num);
                window.m_moshiList.AddChild(listItem);
            }
        }
    }
    private void AddKeyEvent()
    {
        ((UI_Common.UI_commonTop)window.m_TaiTou).m_closeBtn.onClick.Add(CloseBtn);
        ((UI_Common.UI_commonTop)window.m_TaiTou).m_title.text = "宝可梦召唤";
        window.m_jinbiyilanBtn.onClick.Add(OnJiangLiYiLan);
        window.m_zuanshiyilanBtn.onClick.Add(OnJiangLiYiLan);
    }
    private void CloseBtn()
    {
        RemoveEventListener();
        window = null;
        changjing = null;
        GED.ED.dispatchEvent(EventID.OnDrawCardCloseZhaoHuan);
        Close();
    }
    /// <summary>
    /// 模型加载
    /// </summary>
    private void OnShowModel()
    {
        if (type == 1)
        {
            moxing = GameObject.Find("eff_ui_zhaohuan/pet_chengjiehupa_ui");
        }
        else if (type == 2)
        {
            moxing = GameObject.Find("eff_ui_zhaohuan/pet_jiefanghupa_ui");
        }
        if (moxing != null)
        {
            if (type == 1)
            {
                animation = moxing.GetComponentInChildren<SimpleAnimation>(true);
            }
            else
            {
                animation = moxing.GetComponentInChildren<SimpleAnimation>(true);
            }
            moxing.setLayer("UIActor");
        }
        if (animation != null)
            animation.Play("idle");
		
        changjing = GameObject.Find("eff_ui_zhaohuan");
        if (changjing == null)
        {
            Logger.err("未能获得场景引用");
            return;
        }
        animator = changjing.GetComponent<Animator>();
        if (animator == null)
        {
            Logger.err("未能获得动画控制器引用");
        }
    }
    /// <summary>
    /// 回包
    /// </summary>
    /// <param name="evt"></param>
    private void OnResDrawCardInfo(GameEvent evt)
    {
        m_resDrawResult = (ResDraw)evt.Data;
        //抽卡成功
        OnZhaoHuan(null);

        //ResDraw res = (ResDraw)evt.Data;
        //WinInfo info = new WinInfo();
        //TwoParam<int, ResDraw> param = new TwoParam<int, ResDraw>();
        //param.value2 = res;
        //param.value1 = type;
        //info.param = param;
        //WinMgr.Singleton.Open<JiangPinZhanShiWindow>(info, UILayer.Popup);
        //RefreshView();
    }

    private  void _ShowDrawResult()
    {
        //ResDraw res = (ResDraw)evt.Data;
        WinInfo info = new WinInfo();
        TwoParam<int, ResDraw> param = new TwoParam<int, ResDraw>();
        param.value2 = m_resDrawResult;
        param.value1 = type;
        info.param = param;
        WinMgr.Singleton.Open<JiangPinZhanShiWindow>(info, UILayer.Popup);
        RefreshView();
    }

    //抽奖模式加载
    private void OnChouJiangMoShi()
    {
        List<t_draw_cardBean> cardBeans = ConfigBean.GetBeanList<t_draw_cardBean>();
        for (int i = 0; i < cardBeans.Count; ++i)
        {
            if (cardBeans[i].t_type == type)
            {
                typeId.Add(cardBeans[i].t_id);
            }
        }
        typeId.Sort();
        RefreshView();
    }
    private void OnZuanShiMianFeiChongZhi(GameEvent evt)
    {
        RefreshView();
    }
    private void OnJiangLiYiLan()
    {
        WinInfo info = new WinInfo();
        info.param = type;
        WinMgr.Singleton.Open<KeHuoDeWindow>(info, UILayer.Popup);
    }
    //点击了抽奖
    private void OnZhaoHuan(GameEvent evt)
    {
        if (doAction != null)
        { return; }
        window.m_zujian.visible = false;
        if (animation != null)
        {
            animation.Play("idle");
            animation.Play("summon1");
            if(animator != null)
            {
                if (type == 1)
                { animator.Play("eff_ani_ui_zhaohuan_left_summon1"); }
                else if (type == 2)
                { animator.Play("eff_ani_ui_zhaohuan_right_summon1"); }
            }
            if (texiao != null)
                GameObject.DestroyObject(texiao);
            if(type == 1)
                texiao = LoadGo("eff_ui_chengjiehupa_start");
            else if(type == 2)
                texiao = LoadGo("eff_ui_jiefanghupa_start");
            if (texiao != null)
            {
                texiao.transform.parent = moxing.transform;
                if(type == 1)
                    texiao.transform.transform.localPosition = new Vector3(0,0,0);
                else if(type == 2)
                    texiao.transform.transform.localPosition = new Vector3(0, 0, 0);
                texiao.setLayer("UIActor");
            }
            TopRoleInfo.Hide(this);
            window.m_TaiTou.visible = false;
            doAction = new DoActionInterval();
            doAction.doAction(0.1f,OnDaoJiShi);
            if (type == 1)
                time = 4.0f;
            else if (type == 2)
                time = 3.0f;
        }
    }
    //抽奖进行中
    private void OnDaoJiShi(object obj)
    {
        time-= 0.1;
        if (time < 0)
        {
            if (doAction != null)
            {
                doAction.kill();
                doAction = null;
            }
            if (texiao != null)
            {
                GameObject.DestroyObject(texiao);
                if (type == 1)
                    texiao = LoadGo("eff_ui_chengjiehupa_loop");
                else if (type == 2)
                    texiao = LoadGo("eff_ui_jiefanghupa_loop");
            }
            if (texiao != null)
            {
                texiao.transform.parent = moxing.transform;
                if (type == 1)
                    texiao.transform.transform.localPosition = new Vector3(0, 0, 0);
                else if (type == 2)
                    texiao.transform.transform.localPosition = new Vector3(0, 0.2f, 0);
                texiao.setLayer("UIActor");
            }
            animation.Play("summon2");


            _ShowDrawResult();
            //GED.ED.dispatchEvent(EventID.OnDrawCardEndAnimition);
        }
    }
    private void OnZhanShiJieShu(GameEvent evt)
    {
        window.m_zujian.visible = true;
        TopRoleInfo.Show();
        window.m_TaiTou.visible = true;
        if (doAction != null)
        {
            doAction.kill();
            doAction = null;
        }
        if (texiao != null)
        {
            GameObject.DestroyObject(texiao);
            animation.Play("summon3");
            if(animator != null)
            {
                if (type == 1)
                { animator.Play("eff_ani_ui_zhaohuan_left_summon3"); }
                else if (type == 2)
                { animator.Play("eff_ani_ui_zhaohuan_right_summon3"); }
            }
            if (type == 1)
                texiao = LoadGo("eff_ui_chengjiehupa_end");
            else if (type == 2)
                texiao = LoadGo("eff_ui_jiefanghupa_end"); if (texiao != null)
            {
                texiao.transform.parent = moxing.transform;
                if (type == 1)
                    texiao.transform.transform.localPosition = new Vector3(0, 0, 0);
                else if (type == 2)
                    texiao.transform.transform.localPosition = new Vector3(0, 0.2f, 0);
                texiao.setLayer("UIActor");
            }
            if (doAction != null)
            {
                doAction.kill();
                doAction = null;
            }
            time = 1;
            doAction = new DoActionInterval();
            doAction.doAction(1,OnJieShu,null,true);
        }
    }
    private void OnJieShu(object obj)
    {
        time--;
        if(time < 0)
        {
            if (doAction != null)
            {
                doAction.kill();
                doAction = null;
            }
            animation.Play("idle");
        }
    }
}
