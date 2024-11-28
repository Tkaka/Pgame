using Message.Pet;
using UI_Equip;
using Data.Beans;
using System.Collections.Generic;

public class JueXingChengGongWindow : BaseWindow
{
    UI_JueXingChengGongWindow window;
    //宠物数据
    private Dictionary<PropertyType, PropertyStruct> oldData;
    private Dictionary<PropertyType, PropertyStruct> newData;
    private EquipDataManager equipData;

    public override void OnOpen()
    {
        base.OnOpen();
        InitView();
    }
    public override void InitView()
    {
        base.InitView();
        window = getUiWindow<UI_JueXingChengGongWindow>();
        window.m_BeiJing.onClick.Add(CloseBtn);
        // 显示特效
        window.m_anim.Play();
        (window.m_leftStarAnim as UI_Common.UI_xingxing_ain_l).m_anim_L.Play();
        (window.m_rightStarAnim as UI_Common.UI_xingxing_ain_r).m_anim_R.Play();

        if (Info.param == null)
        {
            Logger.err("未传入道具id");
            return;
        }
        equipData = Info.param as EquipDataManager;
        if (equipData == null)
        {
            Logger.err("未能拿到升星前后的值");
            return;
        }
        oldData = equipData.oldDictProperty;
        newData = equipData.GetAttributeData();
        FillData();
    }
    private void FillData()
    {
        //根据道具id填充数据
        //名字
        PetInfo petinfo = PetService.Singleton.GetPetInfo(equipData.CurSelectPetID);
        int buwei = (int)equipData.CurSelectEquipPos;
        OnName(petinfo.equipInfo.equips[buwei].star);
        int star = petinfo.equipInfo.equips[buwei].star;//星级
        int color = petinfo.equipInfo.equips[buwei].color;//品阶
        //加载品阶框
        PinJieKuang(color);
        //头像框
        TouXiangKuang();

        //星级列表
        XingJiList(star);
        //属性变化填充
        ShuXingBianHua();
    }
    //名字
    private void OnName(int star)
    {
        PetInfo petinfo = PetService.Singleton.GetPetInfo(equipData.CurSelectPetID);
        int buwei = (int)equipData.CurSelectEquipPos;
        PetEquip petEquip = equipData.GetCurSelectPetEquip();
        if (star > 1)
        {
            window.m_OldIcon.m_Name.text = UIUtils.GetPingJieEquipName(petinfo.petId, buwei, star - 1, petEquip.color);
            window.m_OldIcon.m_Name.color = UIUtils.GetColorByQuality(petEquip.color);
            window.m_NewIcon.m_Name.text = UIUtils.GetPingJieEquipName(petinfo.petId, buwei, star, petEquip.color);
            window.m_NewIcon.m_Name.color = UIUtils.GetColorByQuality(petEquip.color);
        }
        else
        {
            window.m_OldIcon.m_Name.text = UIUtils.GetPingJieEquipName(petinfo.petId, buwei, star, petEquip.color);
            window.m_NewIcon.m_Name.text = UIUtils.GetPingJieEquipName(petinfo.petId, buwei, star, petEquip.color);
            window.m_OldIcon.m_Name.color = UIUtils.GetColorByQuality(petEquip.color);
            window.m_NewIcon.m_Name.color = UIUtils.GetColorByQuality(petEquip.color);
        }
    }
    //资质框
    private void PinJieKuang(int color)
    {
        PetInfo petinfo = PetService.Singleton.GetPetInfo(equipData.CurSelectPetID);
        int buwei = (int)equipData.CurSelectEquipPos;
        string name;
        int quality = petinfo.equipInfo.equips[buwei].color;
        name = UIUtils.GetBorderUrl(quality);
        UIGloader.SetUrl(window.m_OldIcon.m_BeiJing,name);
        UIGloader.SetUrl(window.m_NewIcon.m_BeiJing,name);
        PetQualityDou qualityDou = window.m_OldIcon.m_qualityDou as PetQualityDou;
        qualityDou.InitView(quality);
        qualityDou = window.m_NewIcon.m_qualityDou as PetQualityDou;
        qualityDou.InitView(quality);
        //加载品阶标志
    }
    //加载星级标志
    private void XingJiList(int star)
    {
        if (star == 5)
        {
            //如果星级满了
        }
        else
        {
            StarList oldstart = new StarList((UI_Common.UI_StarList)window.m_OldIcon.m_StartList);
            oldstart.SetStar(star - 1);
            StarList newstart = new StarList((UI_Common.UI_StarList)window.m_NewIcon.m_StartList);
            newstart.SetStar(star);
        }
    }
    private void ShuXingBianHua()
    {
        JueXingShuXingItem newshuxingitem = null;
        List<PropertyType> keys = new List<PropertyType>();
        keys.AddRange(oldData.Keys);
        EquipPosition type = equipData.CurSelectEquipPos;
        for (int i = 0; i < keys.Count; ++i)
        {
            newshuxingitem = JueXingShuXingItem.CreateInstance();
            if((int)type < 4)
                newshuxingitem.Init(type,keys[i],(int)oldData[keys[i]].attachValue.Floor,(int)newData[keys[i]].attachValue.Floor);
            else
                newshuxingitem.Init(type, keys[i], (int)oldData[keys[i]].attachValue.Floor, (int)newData[keys[i]].percentValue.Floor);
            window.m_ShuXingList.AddChild(newshuxingitem);
        }
    }
    private void TouXiangKuang()
    {
        PetInfo petinfo = PetService.Singleton.GetPetInfo(equipData.CurSelectPetID);
        int buwei = (int)equipData.CurSelectEquipPos;
        t_petBean bean = ConfigBean.GetBean<t_petBean,int>(petinfo.petId);
        if (string.IsNullOrEmpty(bean.t_awaken_icons) || string.IsNullOrEmpty(bean.t_normal_icons))
        {
            Logger.err("没有头像数据");
            return;
        }
        string[] awakeicons = GTools.splitString(bean.t_awaken_icons);
        string[] normalicons = GTools.splitString(bean.t_normal_icons);
        
        if (awakeicons == null || awakeicons.Length < 6 )
        {
            Logger.err("JueXingChengGongWindow:TouXiangKuang:宠物表内图片信息有误" + bean.t_awaken_icons);
            return;
        }
        if (normalicons == null || normalicons.Length < 6)
        {
            Logger.err("JueXingChengGongWindow:TouXiangKuang:宠物表内图片信息有误" + bean.t_normal_icons);
        }
        //判定是否觉醒
        if (petinfo.equipInfo.equips[buwei].star == 1)
        {
            UIGloader.SetUrl(window.m_OldIcon.m_TouXiang, normalicons[buwei]);
            UIGloader.SetUrl(window.m_NewIcon.m_TouXiang, awakeicons[buwei]);
        }
        else
        {
            UIGloader.SetUrl(window.m_OldIcon.m_TouXiang,awakeicons[buwei]);
            UIGloader.SetUrl(window.m_NewIcon.m_TouXiang,awakeicons[buwei]);
        }
    }
    public void CloseBtn()
    {
        if (window.m_anim.playing)
        {
            window.m_anim.Stop();
        }
        oldData = null;
        newData = null;
        window = null;
        
        Close();
    }
}
