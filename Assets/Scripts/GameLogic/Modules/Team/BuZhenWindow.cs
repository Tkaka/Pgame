using UI_BuZhen;
using FairyGUI;
using UnityEngine;
using Data.Beans;
using System.Collections.Generic;
using DG.Tweening;

public class BuZhenWindow : BaseWindow
{

    private UI_BuZhenWindow window;

    private BuZhenGrid grid;

    private GameObject effectGO;

    public override void OnOpen()
    {
        base.OnOpen();

        StageCameraView.ChangeCamera(true);

        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        window = getUiWindow<UI_BuZhenWindow>();

        grid = new BuZhenGrid(window);
        (window.m_commonTop as UI_Common.UI_commonTop).m_closeBtn.onClick.Add(OnCloseBtn);
        (window.m_commonTop as UI_Common.UI_commonTop).m_title.text = "布阵";

        window.m_starFightBtn.visible = IsShowFightStarBtn();
        window.m_starFightBtn.onClick.Add(OnStarFightBtnClick);

        //t_monsterBean bean = ConfigBean.GetBean<t_monsterBean, int>(1001);
        //ShowModels(0, bean.t_city_prefab);
        //bean = ConfigBean.GetBean<t_monsterBean, int>(1000);
        //ShowModels(4, bean.t_city_prefab);

        t_petBean bean;
        List<int> zhenRongList = PetService.Singleton.GetTeamList(PetService.Singleton.zhenRongType,false);
        if (zhenRongList == null)
            return;

        int haveCount = 0;
        for (int i = 0; i < zhenRongList.Count; ++i)
        {
            int petId = zhenRongList[i];
            if (petId != 0)
            {
                bean = ConfigBean.GetBean<t_petBean, int>(zhenRongList[i]);
                if (bean != null)
                {
                    ShowModels(i, UIUtils.GetCityPrefab(bean), petId);
                }
                haveCount++;
            }
        }

        // 显示队伍的人数
        window.m_shangZhenNum.text = string.Format("{0}/{1}", haveCount, zhenRongList.Count);

        ShowPosProperty();
    }
    /// <summary>
    /// 显示位置属性
    /// </summary>
    private void ShowPosProperty()
    {
        // 前排
        Dictionary<PropertyType, PropertyStruct> posPropertyDict = PetService.Singleton.GetPosProperty(1);
        window.m_frontPropertyList.RemoveChildren(0, -1, true);
        List<PropertyType> propertyTypeList = new List<PropertyType>();
        propertyTypeList.AddRange(posPropertyDict.Keys);
        PropertyStruct propertyStruct = null;
        UI_buZhenPropertyItem posPropertyItem = null;
        int num = 0;
        // 位置偏移
        int offsetPos = 10;
        for (int i = 0; i < propertyTypeList.Count; i++)
        {
            propertyStruct = posPropertyDict[propertyTypeList[i]];
            if (propertyStruct.attachValue.Floor != 0)
            {
                posPropertyItem = UI_buZhenPropertyItem.CreateInstance();
                posPropertyItem.m_context.text =
                    string.Format("{0}+{1}", 
                    UIUtils.GetTextByAttributeID((int)(propertyTypeList[i])), propertyStruct.attachValue.Floor.ToString("0.00"));
                window.m_frontPropertyList.AddChild(posPropertyItem);
                posPropertyItem.x -= num * offsetPos;
                num++;
            }
            if (propertyStruct.percentValue.Floor != 0)
            {
                posPropertyItem = UI_buZhenPropertyItem.CreateInstance();
                posPropertyItem.m_context.text =
                    string.Format("{0}+{1}%", 
                    UIUtils.GetTextByAttributeID((int)(propertyTypeList[i])), (propertyStruct.percentValue.Floor * 0.01f).ToString("0.00"));
                window.m_frontPropertyList.AddChild(posPropertyItem);
                posPropertyItem.x -= num * offsetPos;
                num++;
            }
        }
        // 后排
        posPropertyDict = PetService.Singleton.GetPosProperty(2);
        propertyTypeList.Clear();
        propertyTypeList.AddRange(posPropertyDict.Keys);
        window.m_backPropertyList.RemoveChildren(0, -1, true);
        num = 0;
        for (int i = 0; i < propertyTypeList.Count; i++)
        {
            propertyStruct = posPropertyDict[propertyTypeList[i]];
            if (propertyStruct.attachValue.Floor != 0)
            {
                posPropertyItem = UI_buZhenPropertyItem.CreateInstance();
                posPropertyItem.m_context.text =
                    string.Format("{0}+{1}", 
                    UIUtils.GetTextByAttributeID((int)(propertyTypeList[i])), propertyStruct.attachValue.Floor.ToString("0.00"));
                window.m_backPropertyList.AddChild(posPropertyItem);
                posPropertyItem.x -= num * offsetPos;
                num++;
            }
            if (propertyStruct.percentValue.Floor != 0)
            {
                posPropertyItem = UI_buZhenPropertyItem.CreateInstance();
                posPropertyItem.m_context.text =
                    string.Format("{0}+{1}%", 
                    UIUtils.GetTextByAttributeID((int)(propertyTypeList[i])), (propertyStruct.percentValue.Floor * 0.01f).ToString("0.00"));
                window.m_backPropertyList.AddChild(posPropertyItem);
                posPropertyItem.x -= num * offsetPos;
                num++;
            }
        }
    }

    private bool IsShowFightStarBtn()
    {
        bool isShow = false;
        switch (PetService.Singleton.zhenRongType)
        {
            case ZhenRongType.Normal:
                isShow = false;
                break;
            case ZhenRongType.EMeng:
                isShow = false;
                break;
            case ZhenRongType.ZhongJiShiLian:
                isShow = true;
                break;
            case ZhenRongType.GoldTiaoZhan:
                isShow = true;
                break;
            case ZhenRongType.ExpTiaoZhan:
                isShow = true;
                break;
            case ZhenRongType.NvGeDouJia:
                isShow = true;
                break;
            case ZhenRongType.HuanXiangTiaoZhan:
                isShow = true;
                break;
            default:
                break;
        }

        return isShow;
    }

    private void ShowModels(int index, string modelPath, int petId)
    {
        //UI_BuZhenHolder holder = (UI_BuZhenHolder)window.GetChild("pos" +  index);
        //
        BuZhenHolderWrapper holderWrapper = grid.GetWrapperById(index);
        if (holderWrapper != null && holderWrapper.holder != null)
        {
            // 加载模型
            this.CacheWrapper(holderWrapper.holder.m_holder);
            GoWrapper warpper = new GoWrapper();
            holderWrapper.holder.m_holder.SetNativeObject(warpper);
            Message.Pet.PetInfo petInfo = PetService.Singleton.GetPetInfo(petId);
            int star = petInfo == null ? -1 : petInfo.basInfo.star;
            ActorUI actorUI = this.NewActorUI(petId, ActorType.Pet, warpper);
            Vector3 pos = new Vector3(0, 0, 1000);
            actorUI.SetTransform(pos, 100, new Vector3(-21.4f, 80.4f, -29.7f));

            holderWrapper.hasWrapper = true;
            holderWrapper.holder.draggable = true;
            holderWrapper.holder.onDragStart.Add(OnDragStart);
            holderWrapper.ChongWuID = petId;
            holderWrapper.holder.onDragEnd.Add(OnDragEnd);
            holderWrapper.holder.onDragMove.Add(OnDragMove);

            // 加载特效
            t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petId);
            string effectName = "";
            if (petBean != null)
            {
                switch (petBean.t_type)
                {
                    case 1:
                        effectName += "eff_ui_buzhen_red";
                        break;
                    case 2:
                        effectName += "eff_ui_buzhen_yellow";
                        break;
                    case 3:
                        effectName += "eff_ui_buzhen_blue";
                        break;
                    default:
                        break;
                }
            }
            effectGO = this.LoadGo(effectName, pos);
            effectGO.transform.localEulerAngles = new Vector3(55, 0, 0);
            warpper = new GoWrapper(effectGO);
            holderWrapper.holder.m_effrectHolder.SetNativeObject(warpper);
        }
        else
        {
            Logger.err("BuZhenWindow:ShowModels:获取holder失败:pos" + index);
        }
    }

    private BuZhenHolderWrapper dragingWrapper = null;
    private void OnDragStart(EventContext context)
    {
        UI_BuZhenHolder holder = (UI_BuZhenHolder)context.sender;
        if (holder != null)
        {
            dragingWrapper = grid.GetHolderWrapper(holder);
        }
        else
        {
            Logger.err("BuZhenWindow:OnDragEnd:转型失败:" + context.sender);
        }
    }

    private void OnDragEnd(EventContext context)
    {
        //UI_BuZhenHolder holder = (UI_BuZhenHolder)context.sender;
        if (dragingWrapper != null)
        {
            //holder.position = grid.PosArr[dragingPos];
            //交换并修正位置
            Vector2 pos = Stage.inst.touchPosition;
            pos = GRoot.inst.GlobalToLocal(pos);
            //得到停留的位置
            int res = grid.IsInGrid(pos);
            //如果最终停留的区域小于0则还原
            if (res < 0 || affectedWrapper == null)
            {

                dragingWrapper.holder.position = grid.PosArr[dragingWrapper.index];

            }
            else
            {
                if (affectedWrapper != null)
                {
                    dragingWrapper.holder.position = grid.PosArr[affectedWrapper.index];
                    grid.SwapHolderPos(dragingWrapper.holder, affectedWrapper.holder);
                }
            }
        }
        else
        {
            Logger.err("BuZhenWindow:OnDragEnd:转型失败:" + context.sender);
        }
        dragingWrapper = null;
        affectedWrapper = null;
        window.m_hightLight.visible = false;
    }

    //上次被影响的区域
    private BuZhenHolderWrapper affectedWrapper = null;
    private void OnDragMove(EventContext context)
    {
        Vector2 pos = Stage.inst.touchPosition;
        pos = GRoot.inst.GlobalToLocal(pos);
        int res = grid.IsInGrid(pos);
        if (affectedWrapper != null)
        {
            //移动回原来的位置
            if (affectedWrapper.index != res)
            {
                affectedWrapper.holder.position = grid.PosArr[affectedWrapper.index];
                affectedWrapper = null;
            }
        }
        if (res >= 0)
        {
            window.m_hightLight.visible = true;
            window.m_hightLight.position = grid.PosArr[res];
            if (dragingWrapper.index != res)
            {
                BuZhenHolderWrapper wrapper = grid.GetWrapperById(res);
                if (wrapper != null)
                {
                    //Logger.log("affect zone id: " + wrapper.index);
                    affectedWrapper = wrapper;
                    wrapper.holder.position = grid.PosArr[dragingWrapper.index];
                }
                else
                {
                    Logger.err("BuZhenWindow:OnDragMove:Can not find holder: " + res);
                }
            }
        }
        else
        {
           
        }
    }

    private void OnStarFightBtnClick()
    {
        if (IsHavePetInTeam())
        {
            OnCloseBtn();
            GED.ED.dispatchEvent(EventID.OnBuZhenStarFightBtnClick);
             
        }
        else
            TipWindow.Singleton.ShowTip("没有上阵的宠物");
    }

    private bool IsHavePetInTeam()
    {
        List<int> teamList = PetService.Singleton.GetTeamList(PetService.Singleton.zhenRongType);
        int count = teamList.Count;
        for (int i = 0; i < count; i++)
        {
            int id = teamList[i];
            if (id != 0)
                return true;
        }

        return false;
    }
    //关闭窗口
    protected override void OnCloseBtn()
    {
        int[] ZhanWei = new int[6];
        List<int> zhanwei = new List<int>();
        //是否发送标记
        bool fasong = false;
        int i = 0;
        List<int> zhenRongList = PetService.Singleton.GetTeamList(PetService.Singleton.zhenRongType);
        if (zhenRongList == null)
            return;
        //得到站位数组
        foreach (KeyValuePair<UI_BuZhenHolder, BuZhenHolderWrapper> keyPair in grid.HolderDic)
        {
            if (zhenRongList.Count > keyPair.Value.index)
            {
                if ((zhenRongList[keyPair.Value.index]) != keyPair.Value.ChongWuID)
                {
                    fasong = true;
                }
                ZhanWei[keyPair.Value.index] = keyPair.Value.ChongWuID;
            }
 

        }
        if (fasong)
        {
            //将数组发送到服务器
            zhanwei.AddRange(ZhanWei);
            PetService.Singleton.SaveTeamToServer(zhanwei, PetService.Singleton.zhenRongType);
            GED.ED.dispatchEvent(EventID.OnPetTeamListChanged);
        }
        ZhanWei = null;

        if (effectGO != null)
            GameObject.DestroyObject(effectGO);

        StageCameraView.ChangeCamera();

        Close();
    }

}
