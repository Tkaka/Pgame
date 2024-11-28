using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_TongXiangGuan;
using FairyGUI;
using DG.Tweening;
using Message.Team;
using Data.Beans;

public class ZhanTingWindow : BaseWindow {

    UI_ZhanTingWindow window;

    SwipeGesture swipeGesture;

    List<TongXiangItem> tongXiangList = new List<TongXiangItem>();
    Vector3[] posArr = { new Vector3(0, 0, 500), new Vector3(0, 0, 700), new Vector3(0, 0, 900), new Vector3(0, 0, 900), new Vector3(0, 0, 700) };
    long coroutineID;
    private bool isSwipe;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_ZhanTingWindow>();
        coroutineID = -1;

        BindEvent();
        InitView();
        RefreshView();
    }

    private void BindEvent()
    {
        window.m_backBtn.onClick.Add(OnCloseBtn);
        window.m_switchLeftBtn.onClick.Add(OnSwitchLeftBtnClick);
        window.m_switchRightBtn.onClick.Add(OnSwitchRightBtnClick);

        swipeGesture = new SwipeGesture(window.m_touchHolder);
        swipeGesture.onMove.Add(OnGestureMove);
        swipeGesture.onEnd.Add(OnGestureMoveEnd);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResStatueInfo, OnResStatueInfo);
        GED.ED.addListener(EventID.OnExhibitionInfoChange, OnExhibitionInfoChanged);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResStatueInfo, OnResStatueInfo);
        GED.ED.removeListener(EventID.OnExhibitionInfoChange, OnExhibitionInfoChanged);
    }

    public override void InitView()
    {
        base.InitView();

        InitModels();
    }

    private void InitModels()
    {
        List<GGraph> graphList = new List<GGraph>();
        graphList.Add(window.m_firstTXPos);
        graphList.Add(window.m_secondTXPos);
        graphList.Add(window.m_thridTXPos);
        graphList.Add(window.m_fourthTXPos);
        graphList.Add(window.m_fifthTXPos);

        int cout = graphList.Count;
        TongXiangItem tongXiangItem = null;
        Exhibition exhibition = TongXiangGuanServices.Singleton.exhibitionInfo;
        if (exhibition != null)
        {
            t_exhibitionBean exhibitionBean = ConfigBean.GetBean<t_exhibitionBean, int>(exhibition.exhibitionId);
            string[] petIDArr = null;
            if (exhibitionBean != null)
            {
                if (!string.IsNullOrEmpty(exhibitionBean.t_pets))
                {
                    petIDArr = exhibitionBean.t_pets.Split('+');
                }
            }

            for (int i = 0; i < cout; i++)
            {
                tongXiangItem = TongXiangItem.CreateInstance();
                tongXiangItem.position = posArr[i];
                if (petIDArr != null && i < petIDArr.Length)
                {
                    int petID = int.Parse(petIDArr[i]);
                    int index = TongXiangGuanServices.Singleton.GetPetIDIndex(petID);
                    tongXiangItem.petID = petID;
                    if (index != -1)
                    {
                        tongXiangItem.tongXiangColorType = UIUtils.GetTongXiangMaterial(exhibition.currentStatueIds[index]);
                        tongXiangItem.isHave = true;
                    }
                    else
                    {
                        tongXiangItem.tongXiangColorType = 0;
                        tongXiangItem.isHave = false;
                    }

                }
                graphList[i].ReplaceMe(tongXiangItem);
                tongXiangItem.Init(this);
                tongXiangList.Add(tongXiangItem);
                posArr[i] = tongXiangItem.position;
            }
        }
        
    }

    public override void RefreshView()
    {
        base.RefreshView();

        RefreshModels();
        RefreshBaseInfo();
    }

    private void RefreshModels()
    {
        int count = tongXiangList.Count;
        TongXiangItem tongXiangItem = null;
        Exhibition exhibition = TongXiangGuanServices.Singleton.exhibitionInfo;
        if (exhibition != null)
        {
            for (int i = 0; i < count; i++)
            {
                tongXiangItem = tongXiangList[i];
                int index = TongXiangGuanServices.Singleton.GetPetIDIndex(tongXiangItem.petID);
                if (index != -1)
                {
                    tongXiangItem.tongXiangColorType = UIUtils.GetTongXiangMaterial(exhibition.currentStatueIds[index]);
                    tongXiangItem.isHave = true;
                }
                else
                {
                    tongXiangItem.tongXiangColorType = 0;
                    tongXiangItem.isHave = false;
                }
                tongXiangItem.RefreshView();
            }
        }
    }

    private void RefreshBaseInfo()
    {
        Exhibition zhanTingInfo = TongXiangGuanServices.Singleton.exhibitionInfo;
        if (zhanTingInfo != null)
        {
            t_exhibitionBean exhibitionBean = ConfigBean.GetBean<t_exhibitionBean, int>(zhanTingInfo.exhibitionId);
            if (exhibitionBean != null)
            {
                UIGloader.SetUrl(window.m_typeLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetPetTypeUrl(exhibitionBean.t_type)));

                string typeStr = "";
                switch (exhibitionBean.t_type)
                {
                    case 1:
                        typeStr = "攻属性格斗家";
                        break;
                    case 2:
                        typeStr = "防属性格斗家";
                        break;
                    case 3:
                        typeStr = "技属性格斗家";
                        break;
                    default:
                        break;
                }
                string typeFormat = string.Format("[color=#FF0000]{0}[/color]", typeStr);
                window.m_jiaChengLabel.text = string.Format("展厅铜像对所有{0}加成", typeFormat);
            }

            window.m_atkLabel.text = zhanTingInfo.exhibitionAtk + "";
            window.m_defLabel.text = zhanTingInfo.exhibitionDef + "";
            window.m_hpLabel.text = zhanTingInfo.exhibitionHp + "";

            window.m_totalValueLabel.text = zhanTingInfo.value + "";
        }
    }

    /// <summary>
    /// 向左旋转铜像
    /// </summary>
    IEnumerator RotateLeftEnumerator()
    {
        float t = 0;
        int count = tongXiangList.Count;
        while (t <= 1)
        {
            t += 0.05f * 2;
            tongXiangList[count - 1].position = Vector3.Lerp(posArr[count - 1], posArr[0], t);
            for (int i = 0; i < count - 1; i++)
            {
                tongXiangList[i].position = Vector3.Lerp(posArr[i], posArr[i+1], t);
            }
            if (t >= 1)
                break;
            yield return new WaitForSeconds(0.05f);
        }

        Vector3 lastPos = posArr[count - 1];
        posArr[count - 1] = posArr[0];
        for (int i = 0; i < count - 2; i++)
        {
            posArr[i] = posArr[i+1];
        }
        posArr[count - 2] = lastPos;

        coroutineID = -1;
    }

    /// <summary>
    /// 向右旋转铜像
    /// </summary>
    IEnumerator RotateTXRightEnumerator()
    {
        float t = 0;
        int count = tongXiangList.Count;
        while (t <= 1)
        {
            t += 0.05f * 2;
            tongXiangList[0].position = Vector3.Lerp(posArr[0], posArr[count - 1], t);
            for (int i = count - 1; i > 0; i--)
            {
                tongXiangList[i].position = Vector3.Lerp(posArr[i], posArr[i - 1], t);
            }
            if (t >= 1)
                break;
            yield return new WaitForSeconds(0.05f);
        }

        Vector3 firstPos = posArr[0];
        posArr[0] = posArr[count - 1];
        for (int i = count - 1; i > 1; i--)
        {
            posArr[i] = posArr[i - 1];
        }
        posArr[1] = firstPos;

        coroutineID = -1;
    }

    public void RotateTXLeft()
    {
        if (coroutineID != -1)
            return;

        coroutineID = CoroutineManager.Singleton.startCoroutine(RotateLeftEnumerator());
    }

    public void RotateTERight()
    {
        if (coroutineID != -1)
            return;

        coroutineID = CoroutineManager.Singleton.startCoroutine(RotateTXRightEnumerator());
    }

    #region   事件响应 -----------------------------------------------------------------------------------------------------------------------------

    private void OnSwitchLeftBtnClick()
    {
        RotateTXLeft();
    }

    private void OnSwitchRightBtnClick()
    {

        RotateTERight();
    }

    private void OnGestureMove()
    {
        if (isSwipe == false)
        {
            if (swipeGesture.delta.x > 0)
            {
                RotateTERight();
            }
            else
            {
                RotateTXLeft();
            }

            isSwipe = true;
        }
    }

    private void OnGestureMoveEnd()
    {
        isSwipe = false;    
    }

    private void OnResStatueInfo(GameEvent evt)
    {
        int count = tongXiangList.Count;
        TongXiangItem item;
        for (int i = 0; i < count; i++)
        {
            item = tongXiangList[i];
            item.OnResStatueInfo();
        }
    }

    private void OnExhibitionInfoChanged(GameEvent evt)
    {
        RefreshView();
    }

    #endregion

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
