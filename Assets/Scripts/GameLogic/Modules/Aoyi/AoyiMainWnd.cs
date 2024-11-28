using UI_AoYi;
using UI_Common;
using Message.Profound;
using Message.Pet;
using FairyGUI;
using System.Collections.Generic;
using Data.Beans;
using UnityEngine;
using System;
public class AoyiMainWnd : BaseWindow
{
    private UI_AoyiMainWnd m_window;
    private List<PetInfo> m_petList;  //æ‹¥æœ‰çš„å® ç‰©åˆ—è¡¨
    private int m_curSelectPetId = -1; //å½“å‰é€‰ä¸­çš„å® ç‰©ID
    private AddAoyiCell m_lastSelectItem;  //ä¸Šæ¬¡é€‰ä¸­
    private UITable m_table;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_AoyiMainWnd>();
 
        _BindEvent();
        _InitList();
        _ShowPetList();
        _RegisterRed();
    }

    private void _InitList()
    {
        m_window.m_petList.SetVirtual();
        m_window.m_petList.itemProvider = _PetItemProvider;
        m_window.m_petList.itemRenderer = _PetItemRender;
    }

    private void _RegisterRed()
    {
        _RegisterRedDot("Aoyi/DrawReward", m_window.m_imgGetRewardRed);
    }

    private void _BindEvent()
    {
        UI_commonTop commonTop = m_window.m_commonTop as UI_commonTop;
        if (commonTop != null)
        {
            commonTop.m_closeBtn.onClick.Add(OnCloseBtn);
        }

        //m_window.m_c1.onChanged.Add(_OnPageChange);
        m_window.m_btnChange.onClick.Add(_OnExchangeClick);
        m_window.m_btnRongLian.onClick.Add(_OnRongLianClick);
        m_window.m_btnSkill.onClick.Add(_OnSkillClick);
        m_window.m_btnOneKeyPut.onClick.Add(_OnKeyPutClick);
        m_window.m_btnOneKeyStrength.onClick.Add(_OnOneKeyStrengthClick);
        m_window.m_btnOneKeyUnEquip.onClick.Add(_OnOneKeyUnEquipClick);
        m_window.m_btnGetStone.onClick.Add(_OnGetStoneClick);

        m_table = new UITable();
        m_table.Init(m_window.m_c1, _OnPageChange);
        m_table.AddFuncLock(0, 21011, m_window.m_tabPrimary);
        m_table.AddFuncLock(1, 21012, m_window.m_tabMiddle);
        m_table.AddFuncLock(2, 21013, m_window.m_tabHigh);

    }

    private string _PetItemProvider(int index)
    {
        return PetItem.URL;
    }

    private void _PetItemRender(int index, GObject obj)
    {
        PetItem petItem = obj as PetItem;
        if (petItem == null)
            return;
        if (index < 0 || index >= m_petList.Count)
            return;

        PetInfo info = m_petList[index];
        petItem.petID = info.petId;
        if (m_curSelectPetId == -1)
        {
            _ShowSelectPetInfo(petItem.petID);
        }

        petItem.RefreshItem(m_curSelectPetId, PetItemType.Pet);
        petItem.onClick.Clear();
        petItem.onClick.Add(() => 
        {
            _ShowSelectPetInfo(petItem.petID);
        });

        _RegisterRedDot(AoyiService.Singleton.GetPetAoyiRedPath(info.petId), petItem.m_redPoint);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.AoyiStoneInfoChange, _OnAoyiStoneInfoChange);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.AoyiStoneInfoChange, _OnAoyiStoneInfoChange);
    }


    //æ˜¾ç¤ºå® ç‰©åˆ—è¡¨
    private void _ShowPetList()
    {
        m_petList = PetService.Singleton.GetPetInfos(true);
        m_window.m_petList.numItems = m_petList.Count;
    }

    //æ˜¾ç¤ºå½“å‰é€‰ä¸­çš„å® ç‰©ä¿¡æ¯
    private void _ShowSelectPetInfo(int petId)
    {
        m_curSelectPetId = petId;
        m_window.m_petList.RefreshVirtualList();
        _ShowPetInfo();

        _RegisterRedDot(AoyiService.Singleton.GetPetAoyiRedPath(petId, AoyiService.EStonePage.Primiry), m_window.m_imgChujiRed);
        _RegisterRedDot(AoyiService.Singleton.GetPetAoyiRedPath(petId, AoyiService.EStonePage.Middle), m_window.m_imgZhongjiRed);
        _RegisterRedDot(AoyiService.Singleton.GetPetAoyiRedPath(petId, AoyiService.EStonePage.Ultima), m_window.m_imgJiuJiRed);
        if (m_window.m_c1.selectedIndex == 0)
        {
            m_window.m_c1.selectedIndex = -1;
        }
        m_window.m_c1.selectedIndex = 0;
    }


    //æ˜¾ç¤ºé€‰ä¸­å® ç‰©ä¿¡æ¯
    private void _ShowPetInfo()
    {
        var petInfo = PetService.Singleton.GetPetByID(m_curSelectPetId);
        var petBean = ConfigBean.GetBean<Data.Beans.t_petBean, int>(petInfo.petId);
        m_window.m_petNameLabel.text = UIUtils.GetPingJiePetName(petInfo.petId, petInfo.basInfo.color, petInfo.basInfo.star);
        m_window.m_petNameLabel.color = UIUtils.GetColorByQuality(petInfo.basInfo.color);
        UIGloader.SetUrl(m_window.m_petTypeLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetPetTypeUrl(petBean.t_type)));

        this.CacheWrapper(m_window.m_modelPos);
        GoWrapper wrapper = new GoWrapper();
        m_window.m_modelPos.SetNativeObject(wrapper);
        ActorUI actor = this.NewActorUI(m_curSelectPetId, ActorType.Pet, wrapper);
        actor.SetTransform(new Vector3(0, 0, 500), 150, new Vector3(0, 180, 0));
    }


    private void _OnPageChange(int index)
    {
        if (m_window.m_c1.selectedIndex < 0)
            return;


        _ShowSelectPageInfo((AoyiService.EStonePage)(m_window.m_c1.selectedIndex + 1));
    }

    //æ˜¾ç¤ºé€‰ä¸­å¥¥ä¹‰é¡µä¿¡æ¯
    private void _ShowSelectPageInfo(AoyiService.EStonePage page)
    {

        _ShowEquipStoneInfo(page);
        _ShowStonePropertyList(page);
        _ShowSkillInfo(page);
    }


    //显示当前页的装备部位信息
    private void _ShowEquipStoneInfo(AoyiService.EStonePage page)
    {
        //å¥¥ä¹‰çŸ³æ ¼å­
        m_window.m_StoneGridList.RemoveChildren(0, -1, true);
        m_lastSelectItem = null;
        int maxGridNum = AoyiService.Singleton.GetPageStoneGridNum(page);
        for (int i = 0; i < maxGridNum; i++)
        {
             

            AddAoyiCell cell = AddAoyiCell.CreateInstance();
            cell.RefreshView(m_curSelectPetId, page, i + 1);
            m_window.m_StoneGridList.AddChild(cell);

            _RegisterRedDot(AoyiService.Singleton.GetPetAoyiRedPath(m_curSelectPetId, page, i + 1), cell.m_imgRed);
            cell.onClick.Add(() => 
            {
                if (m_lastSelectItem != null)
                    m_lastSelectItem.SelectToggle(false);

                cell.SelectToggle(true);
                m_lastSelectItem = cell;
            });

        }
    }


    //æ˜¾ç¤ºå¥¥ä¹‰çŸ³æ·»åŠ çš„å±žæ€§åˆ—è¡¨
    private void _ShowStonePropertyList(AoyiService.EStonePage page)
    {
        m_window.m_stonePropertyList.RemoveChildren(0, -1, true);
        List<AoyiService.PropertyInfo> propertyInfos = AoyiService.Singleton.GetPetPageStoneAddProperty(m_curSelectPetId, page);
        for (int i = 0; i < propertyInfos.Count; i++)
        {
            AoyiService.PropertyInfo propertyInfo = propertyInfos[i];
            UI_objPropertyCel1 cell = UI_objPropertyCel1.CreateInstance();
            t_attr_nameBean propertyBean = ConfigBean.GetBean<t_attr_nameBean, int>(propertyInfo.propertyId);
            if (propertyBean == null)
                continue;

            cell.m_txtPropertyName.text = string.Format("{0}:", propertyBean.t_name_id);

            if (propertyBean.t_value_type == 0)
            {
                cell.m_txtPropertyValue.text = (propertyInfo.propertyValue * 0.01) + "%";
            }
            else
            {
                cell.m_txtPropertyValue.text = propertyInfo.propertyValue + "";
            }

            m_window.m_stonePropertyList.AddChild(cell);
        }
    }


    //æ˜¾ç¤ºå¥¥ä¹‰æŠ€èƒ½ä¿¡æ¯
    private void _ShowSkillInfo(AoyiService.EStonePage page)
    {
        int lightNum = 0;
        int propertyId = 0;
        int aoyiId = AoyiService.Singleton.GetActiveAoyiId(m_curSelectPetId, page);
        if (aoyiId != -1)
        {
            int quility = AoyiService.Singleton.GetLowQuilityInStones(m_curSelectPetId, page, aoyiId);
            t_aoyi_zuheBean zuheBean = ConfigBean.GetBean<t_aoyi_zuheBean, int>(aoyiId);
            if (zuheBean != null)
            {
                
                int[] arrDic = GTools.splitStringToIntArray(zuheBean.t_group, '+');
                if (arrDic != null)
                    lightNum = arrDic.Length;

                string[] propertyInfos = GTools.splitString(zuheBean.t_property, ';');
                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    int[] propertyInfo = GTools.splitStringToIntArray(propertyInfos[i], '+');
                    if (propertyInfo == null || propertyInfo.Length < 2)
                        continue;

                    if (quility == propertyInfo[0])
                    {
                        propertyId = propertyInfo[1];
                        break;
                    }
                }

                m_window.m_txtKillName.text = zuheBean.t_name;
            }


            //Debug.Log("------------------->>æ¿€æ´»æŠ€èƒ½" + aoyiId + "     " + propertyId);
            if (AoyiService.Singleton.GetCurPageAoyiIsEffect(m_curSelectPetId, page))
            {
                m_window.m_txtKillName.grayed = false;
                m_window.m_txtKillName.color = UIUtils.GetColorValueByQuility(quility);
            }
            else
            {
                //æœ‰é‡å¤æŠ€èƒ½ä¸”è¯¥æŠ€èƒ½ä¸èƒ½ç”Ÿæ•ˆ
                m_window.m_txtKillName.grayed = true;
                m_window.m_txtKillName.text = m_window.m_txtKillName.text + "(重复)";
                AoyiIconList iList = m_window.m_iconList as AoyiIconList;
                if (iList != null)
                    iList.ClearChildren();

                m_window.m_skillPropertyList.RemoveChildren(0, -1, true);
                return;
            }
 
        }
        else
        {
            m_window.m_txtKillName.text = "未激活任何奥义";
            m_window.m_txtKillName.grayed = true;
        }
 
        AoyiIconList iconList = m_window.m_iconList as AoyiIconList;
        if (iconList != null)
        {
            iconList.RefreshView(m_curSelectPetId, page, lightNum);
        }

        _ShowSkillAddPropertyList(propertyId);
    }

    //显示奥义技能增加的属性列表
    //å‚æ•°ï¼šå¥¥ä¹‰å±žæ€§ID
    private void _ShowSkillAddPropertyList(int propertyId)
    {
        m_window.m_skillPropertyList.RemoveChildren(0, -1, true);
        if (propertyId == 0)
            return;

        t_skill_propertyBean skillBean = ConfigBean.GetBean<t_skill_propertyBean, int>(propertyId);
        if (skillBean == null)
            return;

        string[] propertyInfos = GTools.splitString(skillBean.t_property, ';');
        for (int i = 0; i < propertyInfos.Length; i++)
        {
            int[] propertyInfo = GTools.splitStringToIntArray(propertyInfos[i], '+');
            if (propertyInfo == null && propertyInfo.Length < 2)
                continue;

            t_attr_nameBean propertyBean = ConfigBean.GetBean<t_attr_nameBean, int>(propertyInfo[0]);
            if (propertyBean == null)
                continue;

            UI_objPropertyCel2 cell = UI_objPropertyCel2.CreateInstance();
             

            if (propertyBean.t_value_type == 0)
            {
                cell.m_txtProperty.text = string.Format("{0}  [color=#cccc00]+{1}[/color]", propertyBean.t_name_id, (propertyInfo[1] * 0.01 + "%"));
            }
            else
            {
                cell.m_txtProperty.text = string.Format("{0}  [color=#cccc00]+{1}[/color]", propertyBean.t_name_id, propertyInfo[1] );
            }

            m_window.m_skillPropertyList.AddChild(cell);
        }

 
    }



    //æ›´æ¢ç‚¹å‡»
    private void _OnExchangeClick()
    {
        TwoParam<int, AoyiService.EStonePage> param = new TwoParam<int, AoyiService.EStonePage>();
        param.value1 = m_curSelectPetId;
        param.value2 = (AoyiService.EStonePage)(m_window.m_c1.selectedIndex + 1);
        WinMgr.Singleton.Open<AoyiChangeWnd>(WinInfo.Create(false, null, true, param), UILayer.Popup);
    }


    //ç†”ç‚¼ç‚¹å‡»
    private void _OnRongLianClick()
    {
        WinMgr.Singleton.Open<AoyiResolveWnd>(null, UILayer.Popup);
    }

    private void _OnSkillClick()
    {
        WinMgr.Singleton.Open<AoyiRewardWnd>(WinInfo.Create(false, null, false, m_curSelectPetId), UILayer.Popup);
    }

    //ä¸€é”®æ”¾ç½®
    private void _OnKeyPutClick()
    {

        ThreeParam<int, AoyiService.EStonePage, Action<object>> param = new ThreeParam<int, AoyiService.EStonePage, Action<object>>();
        param.value1 = m_curSelectPetId;
        param.value2 = (AoyiService.EStonePage)(m_window.m_c1.selectedIndex + 1);
        param.value3 = _OneKeyPutCallBack;
        WinMgr.Singleton.Open<OneKeyPlaceWnd>(WinInfo.Create(false, null, false, param), UILayer.Popup);
    }

    //ä¸€é”®æ”¾ç½®çš„å›žè°ƒ
    private void _OneKeyPutCallBack(object param)
    {
        Dictionary<int, StoneInfoExtra> dic = param as Dictionary<int, StoneInfoExtra>;
        if (dic == null)
            return;

        AoyiService.EStonePage stonePage = (AoyiService.EStonePage)(m_window.m_c1.selectedIndex + 1);
        List<EquipInfo> equipInfos = new List<EquipInfo>();
        int curEquipNum = 0;
        StonePage page = AoyiService.Singleton.GetPetPageStoneInfos(m_curSelectPetId, stonePage);
        if (page != null)
            curEquipNum = page.stones.Count;

        bool isNeedSend = false;
        foreach (var info in dic)
        {
            if (info.Value == null)
                continue;

            if (isNeedSend == false)
            {
                isNeedSend = AoyiService.Singleton.EquipPartIsChange(m_curSelectPetId, stonePage, info.Value, info.Key);

            }

            EquipInfo equipInfo = new EquipInfo();
            equipInfo.equipId = info.Key;
            equipInfo.gridId = info.Value.stoneInfo.id;
            equipInfo.source = info.Value.type;
            equipInfos.Add(equipInfo);
        }

        if (isNeedSend || curEquipNum != equipInfos.Count)
            AoyiService.Singleton.ReqEquip(m_curSelectPetId, stonePage, equipInfos);
    }

    //yä¸€é”®å¼ºåŒ–
    private void _OnOneKeyStrengthClick()
    {
        TwoParam<int, AoyiService.EStonePage> param = new TwoParam<int, AoyiService.EStonePage>();
        param.value1 = m_curSelectPetId;
        param.value2 = (AoyiService.EStonePage)(m_window.m_c1.selectedIndex + 1);
        WinMgr.Singleton.Open<AoyiOnekeyStrengthWnd>(WinInfo.Create(false, null,false,param), UILayer.Popup);
    }


    //ä¸€é”®å¸ä¸‹
    private void _OnOneKeyUnEquipClick()
    {
        StonePage page = AoyiService.Singleton.GetPetPageStoneInfos(m_curSelectPetId, (AoyiService.EStonePage)(m_window.m_c1.selectedIndex + 1));
        if (page == null || page.stones.Count == 0)
        {
            return;
        }

        AoyiService.Singleton.ReqUnEquip(m_curSelectPetId, (AoyiService.EStonePage)(m_window.m_c1.selectedIndex + 1));
    }

    private void _OnGetStoneClick()
    {
        WinMgr.Singleton.Open<AoyiDrawWnd>(null, UILayer.Popup);
    }

    //====================================================================äº‹ä»¶å“åº”
    private void _OnAoyiStoneInfoChange(GameEvent evt)
    {
        _ShowSelectPageInfo((AoyiService.EStonePage)(m_window.m_c1.selectedIndex + 1));
    }

    protected override void OnClose()
    {
        base.OnClose();
        m_lastSelectItem = null;
        m_table = null;
    }
}