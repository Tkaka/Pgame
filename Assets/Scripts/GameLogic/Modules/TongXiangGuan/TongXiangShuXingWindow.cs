using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_TongXiangGuan;
using Data.Beans;
using Message.Team;
using Message.Pet;
using FairyGUI;

public class TongXiangShuXingWindow : BaseWindow {

    UI_TongXiangShuXingWindow window;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_TongXiangShuXingWindow>();

        window.m_switchBtn.onClick.Add(OnSwitchBtnClick);
        window.m_closeBtn.onClick.Add(OnCloseBtn);

        InitView();
    }

    public override void InitView()
    {
        base.InitView();

        InitBaseInfo();
        InitModel();
    }

    private void InitBaseInfo()
    {
        Statue statue = TongXiangGuanServices.Singleton.statueInfo;
        if (statue != null)
        {
            TongXiangMaterial material = (TongXiangMaterial)(statue.currentStatueId % 100 / 10);
            window.m_colorLabel.text = UIUtils.GetTongXiangMaterialName(material);

            t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(statue.petId);
            string nameStr = "";
            if (petBean != null)
            {
                nameStr += UIUtils.GetPetName(petBean);
                window.m_nameLabel.text = nameStr;
            }

            int exhibitionID = TongXiangGuanServices.Singleton.exhibitionInfo.exhibitionId;
            t_exhibitionBean exhibitionBean = ConfigBean.GetBean<t_exhibitionBean, int>(exhibitionID);
            if (exhibitionBean != null)
                UIGloader.SetUrl(window.m_typeLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetPetTypeUrl(exhibitionBean.t_type)));
            string typeString = string.Format("[color=#FF8000]{0}[/color]", "攻属性格斗家");
            window.m_jiaChengLabel.text = string.Format("对所有{0}加成", typeString);

            float totalExtraAdd = (statue.colorAdd + statue.starAdd + statue.quantityAdd) * 0.01f;
            int tongXiangID = statue.currentStatueId / 10;
            int attributeIndex = statue.currentStatueId % 10;
            t_statueBean statueBean = ConfigBean.GetBean<t_statueBean, int>(tongXiangID);
            if (statueBean != null)
            {
                if (!string.IsNullOrEmpty(statueBean.t_add_prop))
                {
                    string[] addPropArr = statueBean.t_add_prop.Split(';');
                    if (attributeIndex < addPropArr.Length)
                    {
                        addPropArr = addPropArr[attributeIndex].Split('+');
                        if (addPropArr.Length == 3)
                        {
                            window.m_atkLabel.text = string.Format("{0} + {1}%", addPropArr[0], totalExtraAdd);
                            window.m_defLabel.text = string.Format("{0} + {1}%", addPropArr[1], totalExtraAdd);
                            window.m_hpLabel.text = string.Format("{0} + {1}%", addPropArr[2], totalExtraAdd);
                        }
                    }
                }
            }

            PetInfo petInfo = PetService.Singleton.GetPetByID(statue.petId);
            string colorStr;
            if (petInfo != null)
            {
                //string starStr = string.Format("[color=#FF9912]{0}[/color][img]{1}[/img]", petInfo.basInfo.star, UIUtils.GetLoaderUrl(WinEnum.UI_Common, "UI_TY_tubiao_xing_huangse_xiao"));
                string starStr = string.Format("[color=#FF9912]{0}[/color]<img src='{1}'/>", petInfo.basInfo.star, UIUtils.GetLoaderUrl(WinEnum.UI_Common, "UI_TY_tubiao_xing_huangse_xiao"));
                window.m_starAddLabel.text = string.Format("{0}达到{1}，属性额外提升{2}%", nameStr, starStr, statue.starAdd * 0.01f);

                string colorHtml = ColorUtility.ToHtmlStringRGB(UIUtils.GetColorByQuality(petInfo.basInfo.color));
                colorStr = string.Format("[color=#{0}]{1}[/color]", colorHtml, UIUtils.GetColorName(petInfo.basInfo.color));
                window.m_colorAddLabel.text = string.Format("{0}达到{1}，属性额外提升{2}%", nameStr, colorStr, statue.colorAdd * 0.01f);
            }
            else
            {
                colorStr = string.Format("[color=#FF9912]{0}[/color]", nameStr);
                window.m_starAddLabel.text = string.Format("获得格斗家{0}可得到额外的铜像数值加成", colorStr);
                window.m_colorAddLabel.text = string.Format("获得格斗家{0}可得到额外的铜像数值加成", colorStr);
            }

            colorStr = string.Format("[color=#FF9912]{0}[/color]", statue.statueUnitId.Count);
            window.m_tongXiangAddLabel.text = string.Format("铜像总数达到{0}个，属性额外提升{1}%", colorStr, statue.quantityAdd * 0.01f);

            window.m_valueLabel.text = statue.value + "";
        }
    }

    private void InitModel()
    {
        Statue statue = TongXiangGuanServices.Singleton.statueInfo;
        if (statue != null)
        {
            int statueID = statue.currentStatueId / 10;
            t_statueBean statueBean = ConfigBean.GetBean<t_statueBean, int>(statueID);
            if (statueBean != null)
            {
                GameObject model = this.LoadGo(statueBean.t_model);
                model.transform.localPosition = new Vector3(0, 0, 1000);
                model.transform.localEulerAngles = new Vector3(0, 180, 0);
                model.transform.localScale = new Vector3(120, 120, 120);

                GoWrapper wrapper = new GoWrapper(model);
                window.m_modelPos.SetNativeObject(wrapper);
                model.setLayer("UIActor");
            }

            int rank = statue.currentStatueId % 10;
            window.m_rankLabel.text = UIUtils.GetTongXiangRankName((TongXiangRank)rank);
        }
    }

    private void OnSwitchBtnClick()
    {
        // 点击进入铜像购买界面
        WinMgr.Singleton.Open<BuyTongXiangWindow>(null, UILayer.Popup);

        OnCloseBtn();
    }
}
