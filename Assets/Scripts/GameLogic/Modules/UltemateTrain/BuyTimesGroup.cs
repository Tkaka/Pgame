using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Data.Beans;
using Message.Challenge;

public class BuyTimesGroup  {

    UI_buyTimesGroup view;

    public int buyTimes;
    private int totalNeedDiamondNum;

    public BuyTimesGroup(UI_buyTimesGroup view)
    {
        this.view = view;

        view.m_buyTimesBtn.onClick.Add(OnBuyTimesBtnClick);
    }

    public void RefreshView()
    {
        ResTrialSkip trainSkipInfo = UltemateTrainService.Singleton.trainSkipInfo;
        if (trainSkipInfo != null)
        {
            int count = trainSkipInfo.diamondBoxInfos.Count;
            IntVsInt boxInfo = null;
            for (int i = 0; i < count; i++)
            {
                boxInfo = trainSkipInfo.diamondBoxInfos[i];
                totalNeedDiamondNum += ConsumeDiamondNumByTimes(boxInfo.int2, buyTimes);
            }

            view.m_diamondNumLabel.text = totalNeedDiamondNum + "";
        }

        view.m_buyTimesBtn.text = string.Format("全买{0}次", buyTimes);
    }

    public int ConsumeDiamondNumByTimes(int baseTimes, int buyTimes)
    {
        int consumeNum = 0;

        // 1801001  终极试炼付费宝箱价格，最大100

        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(1801001);
        if (globalBean != null && !string.IsNullOrEmpty(globalBean.t_string_param))
        {
            string[] priceArr = globalBean.t_string_param.Split('+');
            int length = priceArr.Length;

            int maxTimes = UltemateTrainService.Singleton.GetSecretMaxOpenTimes();
            int curTimes = baseTimes;
            for (int i = 0; i < buyTimes; i++, curTimes = baseTimes + i)
            {
                if (curTimes >= maxTimes)
                    break;

                if (curTimes >= length)
                    consumeNum += int.Parse(priceArr[length - 1]);
                else
                    consumeNum += int.Parse(priceArr[curTimes]);
            }
        }

        

        return consumeNum;
    }

    private void OnBuyTimesBtnClick()
    {
        if (IsEnoughDiamond())
        {
            if (IsHaveTimes())
            {
                UltemateTrainService.Singleton.ReqUltemateTrialBatchBoxOpen(buyTimes);
            }
            else
            {
                TipWindow.Singleton.ShowTip("次数不足");
            }
        }
        else
        {
            TipWindow.Singleton.ShowTip("钻石不足");
        }
    }


    private bool IsEnoughDiamond()
    {
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.damond >= totalNeedDiamondNum;
    }

    private bool IsHaveTimes()
    {
        ResTrialSkip trialSkipInfo = UltemateTrainService.Singleton.trainSkipInfo;
        if (trialSkipInfo != null)
        {
            int maxTimes = UltemateTrainService.Singleton.GetSecretMaxOpenTimes();
            int count = trialSkipInfo.diamondBoxInfos.Count;
            IntVsInt boxInfo = null;
            for (int i = 0; i < count; i++)
            {
                boxInfo = trialSkipInfo.diamondBoxInfos[i];
                if (boxInfo.int2 < maxTimes)
                    return true;
            }
        }

        return false;
    }
}
