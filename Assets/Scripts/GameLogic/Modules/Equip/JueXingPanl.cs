using UI_Equip;
using Data.Beans;
using Message.Pet;
using System.Collections.Generic;
using FairyGUI;

public class JueXingPanl : TabPage
{
    private UI_JueXing juexingPanel;
    private EquipStrengthWindow window;

    //宠物id
    private int petId;
    private PetInfo petInfo;
    private t_petBean bean;
    //当前装备的部位
    private int wuqihunId;//武器魂id
    private int wuqihunshuliang;//武器魂数量
    private int buwei;
    private int xuqiushuliang;//觉醒石数量
    //装备是普通装备还是特殊装备，为真是特殊，为假是普通
    private bool leixing;
    public void AddEvntLisent()
    {
        GED.ED.addListener(EventID.ResBagUpdate, OnHeCheng);
        GED.ED.addListener(EventID.OnJueXingJieGuo, OnJueXing);
    }
    public void RemoveEventLisent()
    {
        GED.ED.removeListener(EventID.ResBagUpdate, OnHeCheng);
        GED.ED.removeListener(EventID.OnJueXingJieGuo, OnJueXing);
    }
    public JueXingPanl(EquipStrengthWindow equipStrengthWindow, UI_JueXing juexing)
    {
        window = equipStrengthWindow;
        juexingPanel = juexing;
        AddEvntLisent();
        Init();
        juexingPanel.m_JuXingShi.onClick.Add(OpenLaiYuanWindow);
        juexingPanel.m_ZhuanBeiHun.onClick.Add(OpenXiangQingWindow);
        juexingPanel.m_JiangXing.onClick.Add(OpenJiangXingWindow);
        juexingPanel.m_JueXingBtn.onClick.Add(OnJueXingBtn);
        xuqiushuliang = 0;
    }

    public EquipDataManager propData
    { get { return window.equipData; } }
    public void Init()
    {

        //得到宠物id
        petId = window.equipData.CurSelectPetID;
        petInfo = PetService.Singleton.GetPetInfo(petId);
        if (petInfo == null)
        {
            Logger.err("没有对应的宠物");
            return;
        }
        //判定装备列表是否有数据
        if (petInfo.equipInfo.equips == null)
        {
            Logger.err("服务器中没有此装备星级数据");
            return;
        }
        bean = ConfigBean.GetBean<t_petBean, int>(petInfo.petId);
        if (bean == null)
        {
            Logger.err("宠物表中没有对应数据");
            return;
        }
        buwei = (int)propData.CurSelectEquipPos;
        wuqihunId = 0;
        wuqihunshuliang = 0;

        //降星按钮
        int xj = petInfo.equipInfo.equips[buwei].star;
        JiangXingBtn(xj);
        //觉醒宿命语言
        JueXingYuYan();


        if (xj <= 4)
        {
            juexingPanel.m_ManXing.visible = false;
            juexingPanel.m_JueXing.visible = true;

            //材料加载
            JueXingCaiLiao();
            //头像加载
            TouXiangJiaZai();
            //觉醒按键
            
        }
        else
        {
            juexingPanel.m_JueXing.visible = false;
            juexingPanel.m_ManXing.visible = true;

            //加载模型
            OnShowModel();
            //数值
            OnShuZhi();
        }
        juexingPanel.m_JueXingBtn.grayed = false;
        OnSuMingMiaoShu();
        OnJueXingTiao();
    }
    public void OnHeCheng(GameEvent evt)
    {
        Init();
    }
    private void OnJueXing(GameEvent evt)
    {
        WinInfo info = new WinInfo();
        info.param = propData;
        WinMgr.Singleton.Open<JueXingChengGongWindow>(info,UILayer.Popup);
    }

    public override void RefreshView(bool isNet = false)
    {
        Init();
    }

    public override void OnClose()
    {
        juexingPanel = null;
        window = null;
        petInfo = null;
        bean = null;
        RemoveEventLisent();
    }

    public override void OnHide()
    {
       // Init();
    }

    public override void OnShow()
    {
        Init();
    }
    //头像加载
    private void TouXiangJiaZai()
    {
        //得到当前星级
        if (petInfo.equipInfo.equips == null)
        {
            Logger.err("服务器中没有此装备星级数据");
            return;
        }
        //名字
        OnName(petInfo.equipInfo.equips[buwei].star);
        //获取当前装备的星级和等级
        int star = petInfo.equipInfo.equips[buwei].star;//星级
        int color = petInfo.equipInfo.equips[buwei].color;//等级
        //加载品阶框
        PinJieKuang();
        TouXiangKuang();

        //星级列表
        XingJiList(star);
        //属性变化
        ShuXingBianHua();
        //显示等级
        juexingPanel.m_OldRole.m_Level.text = "+" + (petInfo.equipInfo.equips[buwei].level).ToString();
        juexingPanel.m_NewRole.m_Level.text = "+" + (petInfo.equipInfo.equips[buwei].level).ToString();
    }
    //觉醒装备数量加载
    private void JueXingCaiLiao()
    {
        //判断是否是特殊装备
        //判断当前装备的类型
        //判断t_weap_star_id_nums转换为数组后的长度，小于15则为普通武器
        //获取当前武器的星级
        int juexingshishuliang = 0;
        int star = petInfo.equipInfo.equips[buwei].star;
        //星级限制
        if (star == 6)
        {
            juexingPanel.m_JueXing.visible = false;
            juexingPanel.m_ManXing.visible = true;
        }
        string awaken = null;
        switch (buwei)
        {
            case 0: { awaken = bean.t_weap_star_id_nums; } break;
            case 4: { awaken = bean.t_badge_star_id; } break;
            case 5: { awaken = bean.t_book_star_nums; } break;
        }
        //判断是否有道具魂
        if (buwei == 0 || buwei == 4 || buwei == 5)
        {
           leixing = true;
           JueXingCaiLiaoJiaZai(awaken);
        }
        else
        {
            leixing = false;
            juexingPanel.m_JuXingShi.visible = false;
            //觉醒石
            t_globalBean global = ConfigBean.GetBean<t_globalBean, int>(106002);
            if (global == null)
            {
                Logger.err("未能获得普通装备升星所需觉醒石数量");
                return;
            }
            int[] shuliang = GTools.splitStringToIntArray(global.t_string_param);

            t_itemBean item = ConfigBean.GetBean<t_itemBean, int>(106000);
            //获得需求觉醒石数量
            if (star == 5)
            {
                return;
            }
            juexingshishuliang = shuliang[star];
            if (item == null)
                Logger.err("道具表中没有觉醒石数据");
            else
                UIGloader.SetUrl(juexingPanel.m_ZhuanBeiHun.m_TouXiang,item.t_icon);
            UIGloader.SetUrl(juexingPanel.m_ZhuanBeiHun.m_PinJie, UIUtils.GetIocnBorderByQuility(int.Parse(item.t_quality)));
            int juexingshi = BagService.Singleton.GetItemNum(106000);
            //如果有
            if (juexingshi > 0)
                juexingPanel.m_ZhuanBeiHun.m_Jiaao.visible = false;
            else
                juexingPanel.m_ZhuanBeiHun.m_Jiaao.visible = true;
            //觉醒石数量
            xuqiushuliang = juexingshishuliang;
            juexingPanel.m_ZhuanBeiHun.m_number.text = juexingshi.ToString() + "/" + juexingshishuliang.ToString();
        }

    }
    /// <summary>
    /// 觉醒语言,羁绊
    /// </summary>
    private void JueXingYuYan()
    {
        if (buwei == 4 || buwei == 5 || buwei == 0)
        {
            //全局表中读取图片路径，这个是宿命全局表中id暂定为1
            juexingPanel.m_SuMing.text = "觉醒宿命";
        }
        else if (buwei == 2 || buwei == 3 || buwei == 1)
        {
            //全局表中读取图片路径，这个是属性提升，全局表中id暂定为2
            juexingPanel.m_SuMing.text = "觉醒可大幅提升装备属性的成长值，能够高效提升宠物的综合战斗力";
        }
        else
            Logger.err("这个装备的类型填写错误");
    }
    private void OpenXiangQingWindow()
    {
        if (leixing)
        {
            WinInfo info = new WinInfo();
            //装备id
            info.param = wuqihunId;
            WinMgr.Singleton.Open<XiangQingWindow>(info,UILayer.Popup);
        }
        else
        {
            TwoParam<int, int> two = new TwoParam<int, int>();
            WinInfo info = new WinInfo();
            two.value1 = 106000;
            two.value2 = xuqiushuliang;
            info.param = two;
            WinMgr.Singleton.Open<LaiYuanWindow>(info,UILayer.Popup);//如果是普通装备点击觉醒石直接显示来源
        }

    }
    private void OpenLaiYuanWindow()
    {
        TwoParam<int, int> two = new TwoParam<int, int>();
        WinInfo info = new WinInfo();
        two.value1 = 106000;
        two.value2 = xuqiushuliang;
        info.param = two;
        WinMgr.Singleton.Open<LaiYuanWindow>(info,UILayer.Popup);
    }
    //星级列表
    private void XingJiList(int star)
    {
        if (star == 0)
        {
            StarList oldstart = new StarList((UI_Common.UI_StarList)juexingPanel.m_OldRole.m_StartList);
            oldstart.SetStar(star);
            StarList start = new StarList((UI_Common.UI_StarList)juexingPanel.m_NewRole.m_StartList);
            start.SetStar(star + 1);
        }
        else
        {
            if (star < 5)
            {
                StarList oldstart = new StarList((UI_Common.UI_StarList)juexingPanel.m_OldRole.m_StartList);
                oldstart.SetStar(star);
                StarList newstart = new StarList((UI_Common.UI_StarList)juexingPanel.m_NewRole.m_StartList);
                newstart.SetStar(star + 1);
            }
            else
            {
                StarList oldstart = new StarList((UI_Common.UI_StarList)juexingPanel.m_OldRole.m_StartList);
                oldstart.SetStar(star);
                StarList newstart = new StarList((UI_Common.UI_StarList)juexingPanel.m_NewRole.m_StartList);
                newstart.SetStar(star);
            }
        }
    }
    private void JiangXingBtn(int xj)
    {
        //降星显示
        if (xj <= 0)
        {
            juexingPanel.m_JiangXing.grayed = true;
            juexingPanel.m_JiangXing.touchable = true;
            juexingPanel.m_JiangXing.touchable = false;
        }
        else
        {
            juexingPanel.m_JiangXing.grayed = false;
            juexingPanel.m_JiangXing.touchable = false;
            juexingPanel.m_JiangXing.touchable = true;
        }
    }
    private void OnName(int star)
    {
        //当前名字
        //判断是否觉醒
        PetEquip petEquip = propData.GetCurSelectPetEquip();

        if (star > 0)
        {
            juexingPanel.m_OldRole.m_Name.text = UIUtils.GetPingJieEquipName(petInfo.petId, buwei, star, petEquip.color);
            juexingPanel.m_NewRole.m_Name.text = UIUtils.GetPingJieEquipName(petInfo.petId, buwei, star, petEquip.color);
        }
        else
        {
            juexingPanel.m_OldRole.m_Name.text = UIUtils.GetPingJieEquipName(petInfo.petId, buwei, star, petEquip.color);
            juexingPanel.m_NewRole.m_Name.text = UIUtils.GetPingJieEquipName(petInfo.petId, buwei, star + 1, petEquip.color);
        }
        juexingPanel.m_OldRole.m_Name.color = UIUtils.GetColorByQuality(petEquip.color);
        juexingPanel.m_NewRole.m_Name.color = UIUtils.GetColorByQuality(petEquip.color);
    }
    //属性值填充
    private void ShuXingBianHua()
    {
        PetInfo petInfo = PetService.Singleton.GetPetInfo(propData.CurSelectPetID);
        PetEquip equip = propData.GetCurSelectPetEquip();
        Dictionary<PropertyType, PropertyStruct> oldData = propData.GetAttributeData();
        Dictionary<PropertyType, PropertyStruct> newData = propData.GetAttributeDataByParam(equip.star + 1, equip.level, equip.color);//获得下个星级的数据

        juexingPanel.m_OldShuXingList.RemoveChildren(0, -1, true);
        juexingPanel.m_NewShuXingList.RemoveChildren(0, -1, true);
        ShuXingItem oldshuxingitem = null;
        ShuXingItem newshuxingitem = null;
        EquipPosition position = propData.CurSelectEquipPos;
        List<PropertyType> oldkeys = new List<PropertyType>();
        List<PropertyType> newkeys = new List<PropertyType>();
        oldkeys.AddRange(oldData.Keys);
        newkeys.AddRange(newData.Keys);
        for (int i = 0; i < oldkeys.Count; ++i)
        {
            oldshuxingitem = ShuXingItem.CreateInstance();
            newshuxingitem = ShuXingItem.CreateInstance();
            if (equip.id < 4)
            {
                oldshuxingitem.Init(position, oldkeys[i], (int)oldData[oldkeys[i]].attachValue.Floor);
                newshuxingitem.Init(position, newkeys[i], (int)newData[newkeys[i]].attachValue.Floor);
            }
            else
            {

                oldshuxingitem.Init(position, oldkeys[i], (int)oldData[oldkeys[i]].percentValue.Floor);
                newshuxingitem.Init(position, newkeys[i], (int)newData[newkeys[i]].percentValue.Floor);
            }
            juexingPanel.m_OldShuXingList.AddChild(oldshuxingitem);
            juexingPanel.m_NewShuXingList.AddChild(newshuxingitem);
        }
    }
    private void OnSuMingMiaoShu()
    {
        PetEquip equipinfo = propData.GetCurSelectPetEquip();
        string miaoshu;
        string name = UIUtils.GetPingJieEquipName(petInfo.petId, equipinfo.id, equipinfo.star, equipinfo.color); ;
        if (leixing)
        {
            //特殊装备带宿命
            if (!(string.IsNullOrEmpty(bean.t_fetter)))
            {
                t_fetterBean fetterBean;
                int[] suming = GTools.splitStringToIntArray(bean.t_fetter);
                if (equipinfo.id == 0)
                {
                    for (int i = 0; i < suming.Length; ++i)
                    {
                        fetterBean = ConfigBean.GetBean<t_fetterBean, int>(suming[i]);
                        if (fetterBean != null)
                        {
                            if (fetterBean.t_type == 2)
                            {
                                juexingPanel.m_SuMingName.visible = true;
                                juexingPanel.m_SuMingName.text = fetterBean.t_name + ":";
                                int[] jinengid = GTools.splitStringToIntArray(fetterBean.t_propety_type);
                                miaoshu = fetterBean.t_content;
                                string oldskillname = "";//旧的技能名字
                                string newskillname = "";//新的技能名字
                                t_skillBean skillBean = ConfigBean.GetBean<t_skillBean, int>(jinengid[1]);
                                if(skillBean != null)
                                    oldskillname = skillBean.t_name;
                                skillBean = ConfigBean.GetBean<t_skillBean, int>(jinengid[2]);
                                if(skillBean != null)
                                    newskillname = skillBean.t_name;
                                juexingPanel.m_SuMing.text = string.Format(miaoshu, name,oldskillname, newskillname);
                                juexingPanel.m_SuMing.x = juexingPanel.m_SuMingName.x + juexingPanel.m_SuMingName.width;
                                break;
                            }
                        }
                        juexingPanel.m_SuMingName.visible = false;
                        juexingPanel.m_SuMing.x = juexingPanel.m_SuMingName.x;
                        juexingPanel.m_SuMing.text = "觉醒可大幅提升属性";
                    }
                }
                else if (equipinfo.id == 4 || equipinfo.id == 5)
                {
                    for (int i = 0; i < suming.Length; ++i)
                    {
                        fetterBean = ConfigBean.GetBean<t_fetterBean, int>(suming[i]);
                        if (fetterBean != null)
                        {
                            if (fetterBean.t_type == 3)
                            {
                                juexingPanel.m_SuMingName.visible = true;
                                juexingPanel.m_SuMingName.text = fetterBean.t_name + ":";
                                int[] jinengid = GTools.splitStringToIntArray(fetterBean.t_propety_type);

                                miaoshu = fetterBean.t_content;
                                string shuxingleixing;//属性类型名字
                                string shuxingzengjia;//属性增加值
                                t_attr_nameBean skillBean = ConfigBean.GetBean<t_attr_nameBean, int>(jinengid[0]);
                                shuxingleixing = skillBean.t_name_id;
                                shuxingzengjia = ((float)jinengid[1] / 10000 * 100).ToString();
                                juexingPanel.m_SuMing.text = string.Format(miaoshu, name);
                                juexingPanel.m_SuMing.text = juexingPanel.m_SuMing.text + shuxingleixing + "增加" + shuxingzengjia + "%";
                                juexingPanel.m_SuMing.x = juexingPanel.m_SuMingName.x + juexingPanel.m_SuMingName.width;
                                break;
                            }
                        }
                        juexingPanel.m_SuMingName.visible = false;
                        juexingPanel.m_SuMing.x = juexingPanel.m_SuMingName.x;
                        juexingPanel.m_SuMing.text = "觉醒可大幅提升属性";
                    }
                }
            }
        }
        else
        {
            juexingPanel.m_SuMingName.visible = false;
            miaoshu = "觉醒可大幅提升属性";
            juexingPanel.m_SuMing.text = miaoshu;
            juexingPanel.m_SuMing.x = juexingPanel.m_SuMingName.x;
        }
    }
    //资质框
    private void PinJieKuang()
    {
        string name = null;
        name = UIUtils.GetBorderUrl(petInfo.equipInfo.equips[buwei].color);
        if (string.IsNullOrEmpty(name))
            return;
        UIGloader.SetUrl(juexingPanel.m_OldRole.m_BeiJing,name);
        UIGloader.SetUrl(juexingPanel.m_NewRole.m_BeiJing,name);
    }
    //头像框
    private void TouXiangKuang()
    {
        if (string.IsNullOrEmpty(bean.t_awaken_icons) || string.IsNullOrEmpty(bean.t_normal_icons))
        {
            Logger.err("没有头像数据");
            return;
        }

        string[] awakeicons = GTools.splitString(bean.t_awaken_icons);//觉醒后
        string[] normalicons = GTools.splitString(bean.t_normal_icons);//觉醒前

        if (awakeicons == null || awakeicons.Length != 6)
        {
            Logger.err("JueXingPanl:TouXiangKuang:配置表数据错误:头像数组长度有误" + bean.t_awaken_icons);
            return;
        }

        if (normalicons == null || normalicons.Length != 6)
        {
            Logger.err("JueXingPanl:TouXiangKuang:配置表数据错误:图标数组长度有误" + bean.t_normal_icons);
            return;
        }

        //判定是否觉醒
        if (petInfo.equipInfo.equips[buwei].star < 1)
        {
            UIGloader.SetUrl(juexingPanel.m_OldRole.m_TouXiang, normalicons[buwei]);
            UIGloader.SetUrl(juexingPanel.m_NewRole.m_TouXiang, awakeicons[buwei]);
        }
        else
        {
            UIGloader.SetUrl(juexingPanel.m_OldRole.m_TouXiang, awakeicons[buwei]);
            UIGloader.SetUrl(juexingPanel.m_NewRole.m_TouXiang, awakeicons[buwei]);
        }
    }
    //降星界面
    private void OpenJiangXingWindow()
    {
        TwoParam<int, int> twoParam = new TwoParam<int, int>();
        twoParam.value1 = petInfo.petId;
        twoParam.value2 = buwei;
        WinInfo info = new WinInfo();
        info.param = twoParam;
        WinMgr.Singleton.Open<JiangXingWindow>(info,UILayer.Popup);
    }
    //觉醒按键
    private void OnJueXingBtn()
    {
        List<GridInfo> grids = new List<GridInfo>();
        int star = petInfo.equipInfo.equips[buwei].star;
        if (buwei == 0 || buwei == 4 || buwei == 5)
        {
            if (buwei == 0)
            {
                if (wuqihunshuliang != 0)
                {
                    //推入装备魂数量
                    if (BagService.Singleton.GetItemNum(wuqihunId) < wuqihunshuliang)
                    {
                        TipWindow.Singleton.ShowTip("装备觉醒道具数量不足");
                        return;
                    }
                    GridInfo grid = new GridInfo();
                    grid.gridId = BagService.Singleton.GetGridInfo(wuqihunId).id;
                    grid.num = wuqihunshuliang;
                    grids.Add(grid);
                    //计算装备数量
                    if (BagService.Singleton.GetItemNum(106000) < xuqiushuliang)
                    {
                        TipWindow.Singleton.ShowTip("装备觉醒道具数量不足");
                        return;
                    }
                    GridInfo gridjuexingshi = new GridInfo();
                    gridjuexingshi.gridId = BagService.Singleton.GetGridInfo(106000).id;
                    gridjuexingshi.num = xuqiushuliang;
                    grids.Add(gridjuexingshi);
                }
                else
                {
                    if (BagService.Singleton.GetItemNum(106000) < xuqiushuliang)
                    {
                        TipWindow.Singleton.ShowTip("装备觉醒道具数量不足");
                        return;
                    }
                    GridInfo gridjuexingshi = new GridInfo();
                    gridjuexingshi.gridId = BagService.Singleton.GetGridInfo(106000).id;
                    gridjuexingshi.num = xuqiushuliang;
                    grids.Add(gridjuexingshi);
                }
            }
            else
            {
                if (BagService.Singleton.GetItemNum(wuqihunId) < wuqihunshuliang)
                {
                    TipWindow.Singleton.ShowTip("装备觉醒道具数量不足");
                    return;
                }
                GridInfo grid = new GridInfo();
                grid.gridId = BagService.Singleton.GetGridInfo(wuqihunId).id;
                grid.num = wuqihunshuliang;
                grids.Add(grid);
                //计算装备数量
                if (BagService.Singleton.GetItemNum(106000) < xuqiushuliang)
                {
                    TipWindow.Singleton.ShowTip("装备觉醒道具数量不足");
                    return;
                }
                GridInfo gridjuexingshi = new GridInfo();
                gridjuexingshi.gridId = BagService.Singleton.GetGridInfo(106000).id;
                gridjuexingshi.num = xuqiushuliang;
                grids.Add(gridjuexingshi);
            }
        }
        else
        {
            t_globalBean global = ConfigBean.GetBean<t_globalBean, int>(106002);
            if (global == null)
            {
                Logger.err("全局表中没有普通装备的觉醒石数据");
                return;
            }
            int[] daoju = GTools.splitStringToIntArray(global.t_string_param);
            int juexingshi = daoju[star];
            if (BagService.Singleton.GetItemNum(106000) < juexingshi)
            {
                TipWindow.Singleton.ShowTip("装备觉醒道具数量不足");
                return;
            }
            GridInfo gridjuexingshi = new GridInfo();
            gridjuexingshi.gridId = BagService.Singleton.GetGridInfo(106000).id;
            gridjuexingshi.num = juexingshi;
            grids.Add(gridjuexingshi);
        }
        //发送觉醒请求
        propData.UpdateOldEquipProperty();
        PetService.Singleton.ReqEquipAwak(petInfo.petId, buwei, grids);
    }
    //
    private void OnShowModel()
    {
        if(string.IsNullOrEmpty(bean.t_awaken_icons))
        {
            Logger.err("没有头像数据");
            return;
        }
        string[] awakeicons = GTools.splitString(bean.t_awaken_icons);//觉醒后

        if (awakeicons == null || awakeicons.Length != 6)
        {
            Logger.err("JueXingPanl:TouXiangKuang:配置表数据错误:头像数组长度有误" + bean.t_awaken_icons);
            return;
        }
        UIGloader.SetUrl(juexingPanel.m_Model, awakeicons[buwei]);
    }
    private void OnShuZhi()
    {
        juexingPanel.m_ManXingShuXngList.RemoveChildren(0, -1, true);
        ManXingShuXingItem shuxingitem = null;

        Dictionary<PropertyType, PropertyStruct> shuxing = propData.GetAttributeData();
        EquipPosition position = propData.CurSelectEquipPos;
        List<PropertyType> keys = new List<PropertyType>();
        keys.AddRange(shuxing.Keys);
        for (int i = 0; i < keys.Count; ++i)
        {
            shuxingitem = ManXingShuXingItem.CreateInstance();
            shuxingitem.Init(position,(int)keys[i],(int)shuxing[keys[i]].attachValue.Floor);
            juexingPanel.m_ManXingShuXngList.AddChild(shuxingitem);
        }
    }
    //觉醒条文字管理
    private void OnJueXingTiao()
    {
        EquipPosition position = propData.CurSelectEquipPos;
        int weizhi = (int)position;
        int star = petInfo.equipInfo.equips[weizhi].star;
        if (weizhi == 0)
        {
            if (OnIsWuQiSuMing())
            {
                if (star > 0)
                {
                    juexingPanel.m_SuMingYiJiHuo.visible = true;
                    juexingPanel.m_SuMingWeiJiHuo.visible = false;
                    juexingPanel.m_ShuXingYiJiHuo.visible = false;
                    juexingPanel.m_ShuXingWeiJiHuo.visible = false;
                    juexingPanel.m_sumingyijihuo.text = "羁绊已激活";
                }
                else
                {
                    juexingPanel.m_SuMingYiJiHuo.visible = false;
                    juexingPanel.m_SuMingWeiJiHuo.visible = true;
                    juexingPanel.m_ShuXingYiJiHuo.visible = false;
                    juexingPanel.m_ShuXingWeiJiHuo.visible = false;
                    juexingPanel.m_sumingweijihuo.text = "羁绊未激活";
                }
            }
            else
            {
                if (star > 0)
                {
                    juexingPanel.m_SuMingYiJiHuo.visible = false;
                    juexingPanel.m_SuMingWeiJiHuo.visible = false;
                    juexingPanel.m_ShuXingYiJiHuo.visible = true;
                    juexingPanel.m_ShuXingWeiJiHuo.visible = false;
                    juexingPanel.m_shuxingyijihuo.text = "已觉醒大幅提升属性";
                }
                else
                {
                    juexingPanel.m_SuMingYiJiHuo.visible = false;
                    juexingPanel.m_SuMingWeiJiHuo.visible = false;
                    juexingPanel.m_ShuXingYiJiHuo.visible = false;
                    juexingPanel.m_ShuXingWeiJiHuo.visible = true;
                    juexingPanel.m_shuxingweijihuo.text = "觉醒可提升属性";
                }
            }
        }
        else if (weizhi == 4 || weizhi == 5)
        {
            if (star > 0)
            {
                juexingPanel.m_SuMingYiJiHuo.visible = true;
                juexingPanel.m_SuMingWeiJiHuo.visible = false;
                juexingPanel.m_ShuXingYiJiHuo.visible = false;
                juexingPanel.m_ShuXingWeiJiHuo.visible = false;
                juexingPanel.m_sumingyijihuo.text = "羁绊已激活";
            }
            else
            {
                juexingPanel.m_SuMingYiJiHuo.visible = false;
                juexingPanel.m_SuMingWeiJiHuo.visible = true;
                juexingPanel.m_ShuXingYiJiHuo.visible = false;
                juexingPanel.m_ShuXingWeiJiHuo.visible = false;
                juexingPanel.m_sumingweijihuo.text = "觉醒可激活羁绊";
            }
        }
        else
        {
            if (star > 0)
            {
                juexingPanel.m_SuMingYiJiHuo.visible = false;
                juexingPanel.m_SuMingWeiJiHuo.visible = false;
                juexingPanel.m_ShuXingYiJiHuo.visible = true;
                juexingPanel.m_ShuXingWeiJiHuo.visible = false;
                juexingPanel.m_shuxingyijihuo.text = "已觉醒大幅提升属性";
            }
            else
            {
                juexingPanel.m_SuMingYiJiHuo.visible = false;
                juexingPanel.m_SuMingWeiJiHuo.visible = false;
                juexingPanel.m_ShuXingYiJiHuo.visible = false;
                juexingPanel.m_ShuXingWeiJiHuo.visible = true;
                juexingPanel.m_shuxingweijihuo.text = "觉醒可大幅提升属性";
            }
        }
    }
    /// <summary>
    /// 判断是否有武器宿命
    /// </summary>
    private bool OnIsWuQiSuMing()
    {
        if (string.IsNullOrEmpty(bean.t_fetter))
        {
            int[] sumingId = GTools.splitStringToIntArray(bean.t_fetter);
            t_fetterBean fetterBean;
            for (int i = 0; i < sumingId.Length; ++i)
            {
                fetterBean = ConfigBean.GetBean<t_fetterBean,int>(sumingId[i]);
                if (fetterBean.t_type == 2)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void JueXingCaiLiaoJiaZai(string type)
    {
        string[] cailiaoID = GTools.splitString(type, ';');
        int star = petInfo.equipInfo.equips[buwei].star;
        if (buwei == 0)
        {
            for (int i = 0; i < cailiaoID.Length; ++i)
            {
                if (i == star)
                {
                    int[] shuliang = GTools.splitStringToIntArray(cailiaoID[i]);
                    if (shuliang.Length > 1)
                    {
                        wuqihunId = shuliang[0];
                        wuqihunshuliang = shuliang[1];
                        //需求装备魂和觉醒石
                        t_itemBean item = ConfigBean.GetBean<t_itemBean, int>(shuliang[0]);
                        if (item == null)
                        {
                            Logger.err("道具表没有此道具" + shuliang[0] + "-----" + petInfo.petId + "-------" + buwei);
                            return;
                        }
                        if (item.t_icon == null)
                            Logger.err("武器魂的图片路径为空");
                        else
                            UIGloader.SetUrl(juexingPanel.m_ZhuanBeiHun.m_TouXiang, item.t_icon);
                        //品阶加载
                        UIGloader.SetUrl(juexingPanel.m_ZhuanBeiHun.m_PinJie, UIUtils.GetIocnBorderByQuility(int.Parse(item.t_quality)));
                        int wuqihun;
                        //根据装备魂id在背包里寻找
                        wuqihun = BagService.Singleton.GetItemNum(shuliang[0]);

                        //如果有
                        if (wuqihun > 0)
                            juexingPanel.m_ZhuanBeiHun.m_Jiaao.visible = false;
                        else
                            juexingPanel.m_ZhuanBeiHun.m_Jiaao.visible = true;
                        //最后显示
                        juexingPanel.m_ZhuanBeiHun.m_number.text = wuqihun.ToString() + "/" + shuliang[1].ToString();

                        //觉醒石显示图标
                        juexingPanel.m_JuXingShi.visible = true;
                        //觉醒石
                        item = ConfigBean.GetBean<t_itemBean, int>(106000);
                        if (item == null)
                            Logger.err("道具表中没有觉醒石数据");
                        else
                            UIGloader.SetUrl(juexingPanel.m_JuXingShi.m_TouXiang, item.t_icon);
                        int juexingshi = BagService.Singleton.GetItemNum(106000);
                        UIGloader.SetUrl(juexingPanel.m_JuXingShi.m_PinJie, UIUtils.GetIocnBorderByQuility(int.Parse(item.t_quality)));
                        //如果有
                        if (juexingshi > 0)
                            juexingPanel.m_JuXingShi.m_Jiaao.visible = false;
                        else
                            juexingPanel.m_JuXingShi.m_Jiaao.visible = true;
                        //觉醒石数量
                        xuqiushuliang = shuliang[2];
                        juexingPanel.m_JuXingShi.m_number.text = juexingshi.ToString() + "/" + shuliang[2].ToString();
                    }
                    else
                    {
                        leixing = false;
                        wuqihunId = 0;
                        wuqihunshuliang = 0;
                        //只需求觉醒石
                        juexingPanel.m_JuXingShi.visible = false;
                        //觉醒石
                        t_itemBean item = ConfigBean.GetBean<t_itemBean, int>(106000);
                        //获得需求觉醒石数量
                        if (star == 5)
                        {
                            return;
                        }
                        int juexingshishuliang = shuliang[0];
                        if (item == null)
                            Logger.err("道具表中没有觉醒石数据");
                        else
                            UIGloader.SetUrl(juexingPanel.m_ZhuanBeiHun.m_TouXiang, item.t_icon);
                        UIGloader.SetUrl(juexingPanel.m_ZhuanBeiHun.m_PinJie, UIUtils.GetIocnBorderByQuility(int.Parse(item.t_quality)));
                        int juexingshi = BagService.Singleton.GetItemNum(106000);
                        //如果有
                        if (juexingshi > 0)
                            juexingPanel.m_ZhuanBeiHun.m_Jiaao.visible = false;
                        else
                            juexingPanel.m_ZhuanBeiHun.m_Jiaao.visible = true;
                        //觉醒石数量
                        xuqiushuliang = juexingshishuliang;
                        juexingPanel.m_ZhuanBeiHun.m_number.text = juexingshi.ToString() + "/" + juexingshishuliang.ToString();
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < cailiaoID.Length; ++i)
            {
                if (i == star)
                {
                    int[] shuliang = GTools.splitStringToIntArray(cailiaoID[i]);

                    wuqihunId = shuliang[0];
                    wuqihunshuliang = shuliang[1];
                    //需求装备魂和觉醒石
                    t_itemBean item = ConfigBean.GetBean<t_itemBean, int>(shuliang[0]);
                    if (item == null)
                    {
                        Logger.err("道具表没有此道具" + shuliang[0] + "-----" + petInfo.petId + "-------" + buwei);
                        return;
                    }
                    if (item.t_icon == null)
                        Logger.err("武器魂的图片路径为空");
                    else
                        UIGloader.SetUrl(juexingPanel.m_ZhuanBeiHun.m_TouXiang, item.t_icon);
                    UIGloader.SetUrl(juexingPanel.m_ZhuanBeiHun.m_PinJie, UIUtils.GetIocnBorderByQuility(int.Parse(item.t_quality)));
                    int wuqihun;
                    //根据装备魂id在背包里寻找
                    wuqihun = BagService.Singleton.GetItemNum(shuliang[0]);

                    //如果有
                    if (wuqihun > 0)
                        juexingPanel.m_ZhuanBeiHun.m_Jiaao.visible = false;
                    else
                        juexingPanel.m_ZhuanBeiHun.m_Jiaao.visible = true;
                    //最后显示
                    juexingPanel.m_ZhuanBeiHun.m_number.text = wuqihun.ToString() + "/" + shuliang[1].ToString();

                    //觉醒石显示图标
                    juexingPanel.m_JuXingShi.visible = true;
                    //觉醒石
                    item = ConfigBean.GetBean<t_itemBean, int>(106000);
                    if (item == null)
                        Logger.err("道具表中没有觉醒石数据");
                    else
                        UIGloader.SetUrl(juexingPanel.m_JuXingShi.m_TouXiang, item.t_icon);
                    UIGloader.SetUrl(juexingPanel.m_JuXingShi.m_PinJie, UIUtils.GetIocnBorderByQuility(int.Parse(item.t_quality)));
                    int juexingshi = BagService.Singleton.GetItemNum(106000);
                    //如果有
                    if (juexingshi > 0)
                        juexingPanel.m_JuXingShi.m_Jiaao.visible = false;
                    else
                        juexingPanel.m_JuXingShi.m_Jiaao.visible = true;
                    //觉醒石数量
                    xuqiushuliang = shuliang[2];
                    juexingPanel.m_JuXingShi.m_number.text = juexingshi.ToString() + "/" + shuliang[2].ToString();
                }
            }
        }
    }
}
