using FairyGUI;
using Message.Pet;
using UI_Common;
using UnityEngine;
using Data.Beans;

public class ChongWuDongHuaWindow : BaseWindow
{
    private UI_ChongWuDongHuaWindow window;
    private int petId;
    PetInfo petInfo;
    private ActorUI actor;
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_ChongWuDongHuaWindow>();
        AddKeyEvent();
        if (Info.param == null)
        {
            Logger.err("ChongWuDongHuaWindow：未能获得宠物id，无法加载窗口");
            return;
        }
        petInfo = PetService.Singleton.GetPetInfo(petId);
        petId = (int)Info.param;
        InitView();
    }
    private void AddKeyEvent()
    {
        window.m_CloseBtn.onClick.Add(OnCloseBtn);
        window.m_close.onClick.Add(OnCloseBtn);
    }
    public override void InitView()
    {
        base.InitView();
        OnShowModel();
        OnFillData();
        OnDingWei();

        t_petBean bean = ConfigBean.GetBean<t_petBean, int>(petId);
        //资质
        window.m_zizhi.text = UIUtils.GetZiZhiStr(bean.t_zizhi);
    }
    private void OnSuMingJiHuo()
    {
        //检查是否激活宿命
        if (PetService.Singleton.suMing)
        {
            PetService.Singleton.suMing = false;
            if (PetService.Singleton.suMingId.Count != 0)
            {
                WinInfo info = new WinInfo();
                info.param = petId;
                WinMgr.Singleton.Open<SuMingJiHuoWindow>(info,UILayer.Popup);
            }
        }
    }
    private void OnShowModel()
    {
        this.CacheWrapper(window.m_moxing);
        var wrapper = new GoWrapper();
        window.m_moxing.SetNativeObject(wrapper);
        actor = this.NewActorUI(petId,ActorType.Pet,wrapper);
        actor.SetTransform(new Vector3(0, 40, 350), 250, new Vector3(0,180,0));
        //动画播放
        actor.PlayRandomAni();

       GameObject game = this.LoadGo("eff_ui_pet_background");
        if (game != null)
        {
            game.transform.localPosition = new Vector3(0,0,600);
            game.transform.localScale = new Vector3(3,3,3);
            var texiaoWapper = new GoWrapper(game);
            window.m_TeXiao.SetNativeObject(texiaoWapper);
        }

        //宿命激活检查
        OnSuMingJiHuo();
    }
    protected override void OnCloseBtn()
    {
        GED.ED.dispatchEvent(EventID.OnDrawCardDongHuaClose);
        base.OnCloseBtn();
    }
    private void OnFillData()
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petId);

        if (petBean != null)
        {
            if (petInfo == null)
            {
                if (petBean.t_chouka_cardID != 0)
                {
                    t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(petBean.t_chouka_cardID);
                    if (itemBean != null)
                    {
                        string[] pet = GTools.splitString(itemBean.t_value, ';');
                        string miaoshu = "已拥有该宠物，自动转换为{0}个碎片";
                        string[] number = GTools.splitString(pet[1]);
                        miaoshu = string.Format(miaoshu, number[1]);
                        petId = int.Parse(pet[0]);
                        if (PetService.Singleton.yongyou == false)
                        {
                            //获得新宠物
                            window.m_miaoshu.visible = false;
                        }
                        else
                        {
                            //获得已拥有宠物

                            window.m_miaoshu.visible = true;
                            window.m_miaoshu.text = miaoshu;
                        }
                    }
                }
            }
            else
            {
                window.m_miaoshu.text = petBean.t_name;
            }
        }
    }
    private void OnDingWei()
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean,int>(petId);
        if (petBean != null)
        {
            window.m_DingWei.text = petBean.t_desc1;
            //window.m_petName.text = UIUtils.GetPetName(petBean);
        }
        //绝技描述和名字
        if (!(string.IsNullOrEmpty(petBean.t_init_skillID)))
        {
            int[] skillId = GTools.splitStringToIntArray(petBean.t_init_skillID);
            t_skillBean skillBean = ConfigBean.GetBean<t_skillBean, int>(skillId[1]);
            window.m_juejimiaoshu.text = skillBean.t_describe;
            //window.m_jueJiName.text = skillBean.t_name;
            UIGloader.SetUrl(window.m_juejiIcon,skillBean.t_icon);
        }
    }
}
