using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_TongXiangGuan;
using FairyGUI;
using Data.Beans;
using Message.Team;

public class TongXiangItem : UI_TongXiangItem {

    public int petID;
    public int tongXiangColorType;
    public bool isHave;

    private SwipeGesture swipeGesture;
    private bool isSwipe;
    ZhanTingWindow parentUI;
    GameObject model;
    ResPack resPack;
    public new static TongXiangItem CreateInstance()
    {
        return (TongXiangItem)UIPackage.CreateObject("UI_TongXiangGuan", "TongXiangItem");
    }


    public void Init(ZhanTingWindow parentUI)
    {
        this.parentUI = parentUI;

        m_toucher.onClick.Add(OnClickItem);
        swipeGesture = new SwipeGesture(m_toucher);
        swipeGesture.onMove.Add(OnGestureMove);
        swipeGesture.onEnd.Add(OnGestureMoveEnd);
        RefreshView();
    }

    private void SetModel()
    {
        // TODO : 没有拥有的铜像
        if (isHave)
        {
            if (model == null)
            {
                resPack = new ResPack(this);
                int tongXiangID = UIUtils.GetTongXiangID(petID, tongXiangColorType);
                t_statueBean statueBean = ConfigBean.GetBean<t_statueBean, int>(tongXiangID);
                if (statueBean != null)
                {
                    model = resPack.LoadGo(statueBean.t_model, Vector3.zero);
                    model.transform.localScale = new Vector3(150, 150, 150);
                    model.transform.localEulerAngles = new Vector3(0, 180, 0);
                    GoWrapper wrapper = new GoWrapper(model);
                    m_modelLoader.SetNativeObject(wrapper);
                    model.setLayer("UIActor");
                }
            }
        }
        else
        {

        }
    }

    private void SetText()
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petID);
        if(petBean != null)
        {
            m_nameLabel.text = string.Format("{0}的铜像", UIUtils.GetPetName(petBean));
        }
        if (isHave)
        {
            m_colorNameLabel.visible = true;
            m_buyTipLabel.visible = false;
            TongXiangMaterial material = (TongXiangMaterial)(tongXiangColorType);
            m_colorNameLabel.text = UIUtils.GetTongXiangMaterialName(material);
        }
        else
        {
            m_colorNameLabel.visible = false;
            m_buyTipLabel.visible = true;
        }

        if (tongXiangColorType != 0)
        {
            m_colorNameLabel.text = UIUtils.GetTongXiangMaterialName((TongXiangMaterial)tongXiangColorType);
        }
    }

    public void RefreshView()
    {
        SetText();
        SetModel();
    }

    private void OnClickItem()
    {
        // 点击铜像，发送获取铜像信息的请求
        int zhanTingID = TongXiangGuanServices.Singleton.exhibitionInfo.exhibitionId;
        TongXiangGuanServices.Singleton.ReqStatueInfo(petID, zhanTingID);
    }

    public void OnResStatueInfo()
    {
        Statue statue = TongXiangGuanServices.Singleton.statueInfo;
        if (statue != null && statue.petId == petID)
        {
            if (isHave)
            {
                WinMgr.Singleton.Open<TongXiangShuXingWindow>(null, UILayer.Popup);
            }
            else
            {
                WinMgr.Singleton.Open<BuyTongXiangWindow>(null, UILayer.Popup);
            }
        }
    }

    private void OnGestureMove()
    {
        if (isSwipe == false)
        {
            if (swipeGesture.delta.x > 0)
            {
                parentUI.RotateTERight();
            }
            else
            {
                parentUI.RotateTXLeft();
            }

            isSwipe = true;
        }
    }

    private void OnGestureMoveEnd()
    {
        isSwipe = false;
    }

    public override void Dispose()
    {
        parentUI = null;
        if (swipeGesture != null)
            swipeGesture.Dispose();
        if(resPack != null)
            resPack.ReleaseAllRes();
        if (model != null)
            GameObject.Destroy(model);
        base.Dispose();
    }
}
