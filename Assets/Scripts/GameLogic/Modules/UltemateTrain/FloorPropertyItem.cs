using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Message.Challenge;

public class FloorPropertyItem : UI_floorPropertyItem {

    public TrialSingleAttr attr;
    public int index;

    public new static FloorPropertyItem CreateInstance()
    {
        return UI_floorPropertyItem.CreateInstance() as FloorPropertyItem;
    }

    public void InitView()
    {
        m_toucher.onClick.Add(OnClickItem);
        AddListenner();
    }

    private void AddListenner()
    {
        GED.ED.addListener(EventID.OnSelectPetListItem, OnSelectPetListItem);
    }

    private void RemoveListener()
    {
        GED.ED.removeListener(EventID.OnSelectPetListItem, OnSelectPetListItem);
    }

    public void RefreshView()
    {
        m_starNumLabel.text = attr.costStar + "";
        m_buyIcon.visible = attr.isExchange == 1;

        AdditionPropertyType type = (AdditionPropertyType)attr.attrId;
        string attrIconStr = "";
        bool isZhenShu = false;
        switch (type)
        {
            case AdditionPropertyType.Atk:
                attrIconStr += "atk";
                isZhenShu = false;
                break;
            case AdditionPropertyType.Def:
                attrIconStr += "def";
                isZhenShu = false;
                break;
            case AdditionPropertyType.GeDang:
                attrIconStr += "geDang";
                isZhenShu = false;
                break;
            case AdditionPropertyType.BaoJi:
                attrIconStr += "baoJi";
                isZhenShu = false;
                break;
            case AdditionPropertyType.XiXue:
                attrIconStr += "xiXue";
                isZhenShu = false;
                break;
            case AdditionPropertyType.FanShang:
                attrIconStr += "fanShang";
                isZhenShu = false;
                break;
            case AdditionPropertyType.SingleHuiNu:
                attrIconStr += "singleHuiNu";
                isZhenShu = true;
                break;
            case AdditionPropertyType.AllHuiNu:
                attrIconStr += "allHuiNu";
                isZhenShu = true;
                break;
            case AdditionPropertyType.SingleHuiXue:
                attrIconStr += "singleHuiXue";
                isZhenShu = false;
                break;
            case AdditionPropertyType.AllHuiXue:
                attrIconStr += "allHuiXue";
                isZhenShu = false;
                break;
            case AdditionPropertyType.FuHuo:
                attrIconStr += "fuHuo";
                isZhenShu = false;
                break;
            default:
                break;
        }
        UIGloader.SetUrl(m_nameLoader, UIUtils.GetLoaderUrl(WinEnum.UI_UltemateTrain, attrIconStr));

        string valueStr = "";
        if (isZhenShu)
            valueStr += attr.attrValue;
        else
            valueStr = string.Format("{0}%", attr.attrValue * 0.01f);
            
        m_valueLabel.text = valueStr;
    }

    private void OnClickItem()
    {
        
        if (attr.isExchange == 0)
        {
            if (IsEnoughStar())
            {
                AdditionPropertyType type = (AdditionPropertyType)attr.attrId;
                TwoParam<int, ShangZhenSelectType> param = new TwoParam<int, ShangZhenSelectType>();
                switch (type)
                {
                    case AdditionPropertyType.Atk:
                    case AdditionPropertyType.Def:
                    case AdditionPropertyType.GeDang:
                    case AdditionPropertyType.BaoJi:
                    case AdditionPropertyType.XiXue:
                    case AdditionPropertyType.FanShang:
                    case AdditionPropertyType.AllHuiNu:
                    case AdditionPropertyType.AllHuiXue:
                        UltemateTrainService.Singleton.ReqUltemateTrialAttrExchange(attr.floor, index, 0);
                        break;
                    case AdditionPropertyType.SingleHuiNu:
                        param.value1 = 0;
                        param.value2 = ShangZhenSelectType.HuiNu;
                        WinMgr.Singleton.Open<ShangZhenWindow>(WinInfo.Create(false, null, false, param), UILayer.Popup);
                        break;
                    case AdditionPropertyType.SingleHuiXue:
                        param.value1 = 0;
                        param.value2 = ShangZhenSelectType.HuiXue;
                        WinMgr.Singleton.Open<ShangZhenWindow>(WinInfo.Create(false, null, false, param), UILayer.Popup);
                        break;
                    case AdditionPropertyType.FuHuo:
                        param.value1 = 0;
                        param.value2 = ShangZhenSelectType.FuHuo;
                        WinMgr.Singleton.Open<ShangZhenWindow>(WinInfo.Create(false, null, false, param), UILayer.Popup);
                        break;
                    default:
                        break;
                }

                UltemateTrainService.Singleton.curSelectPropertyIndex = index;
            }
            else
            {
                TipWindow.Singleton.ShowTip("关卡星不足");
            }
        }
        else
        {
            TipWindow.Singleton.ShowTip("该加成以购买");
        }
    }
    /// <summary>
    /// 星星是否足够
    /// </summary>
    /// <returns></returns>
    private bool IsEnoughStar()
    {
        ResTrialInfo trialInfo = UltemateTrainService.Singleton.trainInfo;
        if (trialInfo != null)
        {
            return trialInfo.trialInfo.star >= attr.costStar;
        }

        return false;
    }

    private void OnSelectPetListItem(GameEvent evt)
    {
        if (UltemateTrainService.Singleton.curSelectPropertyIndex == index)
        {
            UltemateTrainService.Singleton.ReqUltemateTrialAttrExchange(attr.floor, index, (int)evt.Data);
        }
    }

    public override void Dispose()
    {
        RemoveListener();
        base.Dispose();
    }
}
