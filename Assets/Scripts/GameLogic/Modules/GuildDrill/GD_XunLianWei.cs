using Message.Guild;
using UI_GuildDrill;
using Data.Beans;
using Message.Pet;
using UnityEngine;
using FairyGUI;

public class GD_XunLianWei : UI_GD_XunLianWei
{
    private PetInfo petInfo;
    private int index;
    private DoActionInterval doAction;
    private static UIResPack resPacker;
    private int[] murenqipao = { 71601036, 71601037, 71601038, 71601039 };
    private int time = 6;
    public new static GD_XunLianWei CreateInstance()
    {
        return (GD_XunLianWei)UI_GD_XunLianWei.CreateInstance();
    }
    //加载模型一个函数，数据一个函数，经验动效一个函数，
    //对外提供更换模型接口与刷新数据和调用动效接口
    /// <summary>
    /// 
    /// </summary>
    /// <param name="open">当前训练位是否有宠物</param>
    /// <param name="petId">当前训练位的宠物id</param>
    /// <param name="jiasu">是否是为别人加速的窗口</param>
    /// <param name="info">别人的宠物的信息</param>>
    public void Init(bool open,int xiabiao,int petId = -1)
    {
        AddKeyEvent();
        if(resPacker == null)
            resPacker = new UIResPack(this);
        index = xiabiao;
        m_JiaSuBtn.visible = false;
        m_YiKaiQi.visible = open;
        m_WeiKaiQi.visible = !open;
        m_GengHuanBtn.visible = open;
        LoadDeadmanModel();

        if (open)
        {
            m_kaitong.visible = false;
            if (petId == -1)
            {
                m_YiKaiQi.visible = false;
                m_JiaSuBtn.visible = false;
            }
            else
            {
                petInfo = PetService.Singleton.GetPetInfo(petId);
                if (petInfo == null)
                {
                    Logger.err("GD_XuanLianWei:Init:传入的宠物信息有误");
                    return;
                }
                RefreshView();
                LoadPetModel();
                if (doAction == null)
                {
                    doAction = new DoActionInterval();
                    doAction.doAction(1,OnMuRenQiPao);
                }
            }
        }
        else
        {
            t_exphomeBean bean = ConfigBean.GetBean<t_exphomeBean, int>(index);
            if (bean == null)
            {
                Logger.err("GD_XuanLianWei:Init:初始化为解锁条件时配置表没有对应字段！" + index);
                return;
            }
            m_kaitong.visible = false;
            if (bean.t_vipLevel != 0)
            {
                if (RoleService.Singleton.RoleInfo.roleInfo.vip >= bean.t_vipLevel)//判断是否达到条件
                {
                    m_WeiKaiQi.visible = false;
                    m_kaitong.visible = true;
                    m_JiaGe.text = bean.t_money.ToString();
                }
                else
                {
                    m_WeiKaiQi.visible = true;
                    string tiaojian = "贵族{0}解锁";
                    m_jiesuotiaojian.text = string.Format(tiaojian, bean.t_vipLevel);
                }
            }
            else
            {
                if (RoleService.Singleton.RoleInfo.roleInfo.level >= bean.t_level)
                {
                    m_WeiKaiQi.visible = false;
                    m_kaitong.visible = true;
                    m_JiaGe.text = bean.t_money.ToString();
                }
                else
                {
                    m_WeiKaiQi.visible = true;
                    string tiaojian = "等级{0}解锁";
                    m_jiesuotiaojian.text = string.Format(tiaojian, bean.t_level);
                }
            }
            
        }
       
    }
    public void JiaSuInit(PetInfo info,int xiabiao)
    {
        //在外面填好宠物信息再填入
        index = xiabiao;
        AddKeyEvent();
        m_GengHuanBtn.visible = false;
        petInfo = info;
        m_JiaSuBtn.visible = true;
        m_jinyan.width -= (m_JiaSuBtn.width);
        m_WeiKaiQi.visible = false;
        m_kaitong.visible = false;
        RefreshView();
        m_GengHuanBtn.visible = false;
        m_JiaSuBtn.touchable = true;
        LoadPetModel();
        LoadDeadmanModel();
    }

    private void AddKeyEvent()
    {
        m_JiaSuBtn.onClick.Add(OnJiaSuBtn);
        m_KaiTongBtn.onClick.Add(OnKaiTongBtn);
        m_GengHuanBtn.onClick.Add(OnGenghuanBtn);
    }
    private void OnJiaSuBtn()//加速btn
    {
        //发送游戏内消息给为别人加速的窗口，窗口再将人物的id和宠物id发送给Service
        GED.ED.dispatchEvent(EventID.OnGuildDrillJiaSuPetId,index);
        Logger.err(index + "---");
    }
    private void OnKaiTongBtn()//开通btn
    {
        t_exphomeBean bean = ConfigBean.GetBean<t_exphomeBean, int>(index);
        if (bean == null)
        {
            Logger.err("GD_XuanLianWei:Init:初始化为解锁条件时配置表没有对应字段！" + index);
            return;
        }
        if (RoleService.Singleton.RoleInfo.roleInfo.damond > bean.t_money)
        {
            GuildService.Singleton.ReqBuyPos(index);
            Logger.err(index + "  开通");
        }
        else
        {
            TipWindow.Singleton.ShowTip("钻石不足");
        }
        
    }
    private void OnGenghuanBtn()//更换btns
    {
        GED.ED.dispatchEvent(EventID.OnGuildDrillChangePetWindow, index);
    }

    public void RefreshView()//刷新数据
    {
        m_YiKaiQi.visible = true;
        m_jinyan.m_man.visible = false;
        m_name.text = UIUtils.GetPingJiePetName(petInfo.petId, petInfo.basInfo.color, petInfo.basInfo.star);
        m_level.text = petInfo.basInfo.level.ToString();
        m_jinyan.value = petInfo.basInfo.expRemain;
        m_jinyan.max = PetService.Singleton.GetCurLevelExp(petInfo.petId,petInfo.basInfo.level);
        if (m_jinyan.value == m_jinyan.max)
        {
            m_jinyan.m_man.visible = true;
            m_jinyan.m_number.text = "满级";
            m_jinyan.m_man.width = m_jinyan.width;
        }
        else
        {
            m_jinyan.m_man.visible = false;
            m_jinyan.m_number.text = m_jinyan.value + "/" + m_jinyan.max;
        }
    }
    private void LoadPetModel()//加载模型
    {
        resPacker.CacheWrapper(m_pet);
        var wrapper = new GoWrapper();
        m_pet.SetNativeObject(wrapper);
        var actor = resPacker.NewActorUI(petInfo.petId, ActorType.Pet, wrapper,true);
        actor.SetTransform(new Vector3(0, 0, 600), 100, new Vector3(0, 90, 0));
    }
    private void LoadDeadmanModel()
    {
        Vector3 postion = new Vector3(50, 0, 600);
        string modelName = UIUtils.GetPetStartModel(103);
        if (modelName == "")
        {
            Logger.err("GD_XuanLianWei:LoadDeadmanModel:未能获取到木桩所对应的模型名字");
            return;
        }
        resPacker.CacheWrapper(m_muzhuang);
        var wrapper = new GoWrapper();
        m_muzhuang.SetNativeObject(wrapper);
        var actor = resPacker.NewActorUI(103, ActorType.Pet, wrapper,true);
        actor.SetTransform(new Vector3(0,0,600), 100, new Vector3(0, 90, 0));
    }
    public void OnChangeModel(int petid)//更换模型
    {
        PetInfo info = PetService.Singleton.GetPetInfo(petid);
        if (info == null)
        {
            Logger.err("GD_XuanLianWei:OnChangeModel:传入的宠物id不正确");
        }
        else
        {
            petInfo = info;
        }

        LoadPetModel();
        RefreshView();
    }
    public void OnPlay(int number)//播放动效
    {
        m_JingYanHuoDe.visible = true;
        m_jingyanhuode.text = number.ToString();
        m_huodeDongXiao.Play(callback);
    }
    /// <summary>
    /// 木人气泡随机显示
    /// </summary>
    /// <param name="obj"></param>
    private void OnMuRenQiPao(object obj)
    {
        
        if (time == 6)
        {
            m_QiPaoYuYan.visible = true;
            System.Random random = new System.Random();
            int index = random.Next(0, murenqipao.Length - 1);
            t_languageBean bean = ConfigBean.GetBean<t_languageBean, int>(murenqipao[index]);
            if (bean == null)
            {
                Logger.err("语言包中没有对应语言" + murenqipao[index]);
                return;
            }
            m_xianshi.text = bean.t_content;
            random = null;
        }
        if (time == 2)
        {
            m_QiPaoYuYan.visible = false;
        }
        time--;
        if (time <= 0)
            time = 6;
    }
    private void callback()
    {
        m_JingYanHuoDe.visible = false;
    }
    public override void Dispose()
    {
        if (resPacker != null)
        {
            resPacker.ReleaseAllRes();
            resPacker = null;
        }
        if (doAction != null)
        {
            doAction.kill();
            doAction = null;
        }
        petInfo = null;
        base.Dispose();
    }

}