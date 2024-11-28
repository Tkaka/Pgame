using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Message.Team;
using Data.Beans;
using Message.Bag;

public enum CultureMethod
{
    Primary = 1,              // 初级
    Middles = 2,              // 中级
    Senior = 3,               // 高级
}

public class ShenQiService : SingletonService<ShenQiService> {

    /// <summary>
    ///  当前选中的神器ID
    /// </summary>
    public int curShenQiID;
    public CultureMethod curCultureMethod;

    public ResArtifactInfo artifactInfo { get; private set; }
    public List<ArtifactRandomAttr> randomAttrList = new List<ArtifactRandomAttr>();
    public List<int> saveInfoList = new List<int>();

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();

        GED.NED.addListener(ResArtifactInfo.MsgId, OnResArtifactInfo);
        GED.NED.addListener(ResArtifactTrain.MsgId, OnResArtifactTrain);
        GED.NED.addListener(ResArtifactTrainSave.MsgId, OnResArtifactTrainSave);
        GED.NED.addListener(ResArtifactUnlock.MsgId, OnResArtifactUnlock);
    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();

        GED.NED.removeListener(ResArtifactInfo.MsgId, OnResArtifactInfo);
        GED.NED.removeListener(ResArtifactTrain.MsgId, OnResArtifactTrain);
        GED.NED.removeListener(ResArtifactTrainSave.MsgId, OnResArtifactTrainSave);
        GED.NED.removeListener(ResArtifactUnlock.MsgId, OnResArtifactUnlock);
    }

    #region 服务器返回
    /// <summary>
    /// 神器信息
    /// </summary>
    /// <param name="evt"></param>
    private void OnResArtifactInfo(GameEvent evt)
    {
        artifactInfo = GetCurMsg<ResArtifactInfo>(evt.EventId);

        WinMgr.Singleton.Open<ShenQiMainWidow>(null, UILayer.Popup);
    }
    /// <summary>
    /// 神器培养回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnResArtifactTrain(GameEvent evt)
    {
        ResArtifactTrain msg = GetCurMsg<ResArtifactTrain>(evt.EventId);
        randomAttrList.Clear();
        randomAttrList.AddRange(msg.attrs);

        SetSaveInfoList();

        GED.ED.dispatchEvent(EventID.OnResShenQiCulture);
    }
    /// <summary>
    /// 神器培养保存回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnResArtifactTrainSave(GameEvent evt)
    {
        ResArtifactTrainSave msg = GetCurMsg<ResArtifactTrainSave>(evt.EventId);
        UpdateShenQiInfo(msg.artifact);

        GED.ED.dispatchEvent(EventID.OnResArtifactTrainSave);
    }
    /// <summary>
    /// 神器解锁回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnResArtifactUnlock(GameEvent evt)
    {
        ResArtifactUnlock msg = GetCurMsg<ResArtifactUnlock>(evt.EventId);
        UpdateShenQiInfo(msg.artifact);
        artifactInfo.artifactId = msg.artifactId;
        artifactInfo.conditions.Clear();
        artifactInfo.conditions.AddRange(msg.conditions);

        GED.ED.dispatchEvent(EventID.OnResShenQiUnlock);
    }

    #endregion;

    #region 请求
    /// <summary>
    /// 请求神器解锁
    /// </summary>
    /// <param name="shenQiID"></param>
    public void ReqArtifactUnlock()
    {
        ReqArtifactUnlock msg = GetEmptyMsg<ReqArtifactUnlock>();
        msg.id = curShenQiID;

        SendMsg<ReqArtifactUnlock>(ref msg);
    }
    /// <summary>
    /// 请求神器保存
    /// </summary>
    /// <param name="shenQiID"></param>
    /// <param name="isSaveIndex">1保存，0取消</param>
    public void ReqArtifactTrainSave()
    {
        ReqArtifactTrainSave msg = GetEmptyMsg<ReqArtifactTrainSave>();
        msg.id = curShenQiID;
        msg.save.AddRange(saveInfoList);

        SendMsg<ReqArtifactTrainSave>(ref msg);
    }
    /// <summary>
    /// 请求神器培养
    /// </summary>
    /// <param name="method">培养的方式</param>
    /// <param name="times">培养的次数</param>
    /// /// <param name="isOneTime">是否是单次，1是0否</param>
    public void ReqArtifactTrain(int isOneTime)
    {
        ReqArtifactTrain msg = GetEmptyMsg<ReqArtifactTrain>();
        msg.type = (int)curCultureMethod;
        msg.number = GetCultureTimes(isOneTime);
        msg.artifactId = curShenQiID;
        msg.isSingle = isOneTime;

        SendMsg<ReqArtifactTrain>(ref msg);
    }
    /// <summary>
    /// 请求神器信息
    /// </summary>
    public void ReqArtifactInfo()
    {
        ReqArtifactInfo msg = GetEmptyMsg<ReqArtifactInfo>();

        SendMsg<ReqArtifactInfo>(ref msg);
    }

    #endregion;

    #region 数据处理
    /// <summary>
    /// 通过神器ID获得神器的信息
    /// </summary>
    /// <returns></returns>
    public Artifact GetShenQiInfoByID(int shenQiID)
    {
        Artifact artifact = null;
        int count = artifactInfo.artifacts.Count;
        for (int i = 0; i < count; i++)
        {
            artifact = artifactInfo.artifacts[i];
            if (artifact.id == shenQiID)
                return artifact;
        }

        return null;
    }
    /// <summary>
    /// 获得神器的属性
    /// </summary>
    /// <returns></returns>
    public ArtifactAttr GetShenQiAttrByID(int id)
    {
        Artifact artifact = GetShenQiInfoByID(curShenQiID);

        int count = artifact.artifactAttrs.Count;
        ArtifactAttr attr = null;
        for (int i = 0; i < count; i++)
        {
            attr = artifact.artifactAttrs[i];
            if (attr.id == id)
                return attr;
        }

        return null;
    }
    /// <summary>
    /// 更新神器信息列表中的神器信息
    /// </summary>
    /// <param name="artifact"></param>
    private void UpdateShenQiInfo(Artifact artifact)
    {
        Artifact tempArtifact = null;
        int count = artifactInfo.artifacts.Count;
        for (int i = 0; i < count; i++)
        {
            tempArtifact = artifactInfo.artifacts[i];
            if (tempArtifact.id == artifact.id)
            {
                artifactInfo.artifacts[i] = artifact;
                return;
            }
        }

        artifactInfo.artifacts.Add(artifact);
    }
    /// <summary>
    /// 获得神器改变的属性值
    /// </summary>
    public Attr GetChangeAttr(int attrID)
    {
        int count = randomAttrList.Count;
        ArtifactRandomAttr randomAttr = null;
        Attr attr = null;
        for (int i = 0; i < count; i++)
        {
            randomAttr = randomAttrList[i];
            if (randomAttr.attr.Count == 3)
            {
                for (int j = 0; j < 3; j++)
                {
                    attr = randomAttr.attr[j];
                    if (attr.id == attrID)
                        return attr;
                }
            }
        }

        return null;
    }

    private void SetSaveInfoList()
    {
        bool isOpen = IsOpenAtuoRecomond();
        saveInfoList.Clear();
        int count = randomAttrList.Count;
        int saveNum = 0;
        for (int i = 0; i < count; i++)
        {
            saveNum = isOpen ? 1 : 0;
            saveInfoList.Add(saveNum);
        }
    }
    /// <summary>
    /// 是否开启了自动推荐
    /// </summary>
    /// <returns></returns>
    public bool IsOpenAtuoRecomond()
    {
        object obj = PlayerLocalData.GetData("OpenAutoRecommend", null);
        bool isOpen = false;
        if (obj != null)
        {
            string openAutoRecommend = obj.ToString();
            isOpen = openAutoRecommend == "1";
        }
        else
        {
            isOpen = true;
            PlayerLocalData.SetData(PlayerLocalDataKey.OpenAutoRecommend, 1);
        }

        return isOpen;
    }
    /// <summary>
    /// 获得随机改变的属性的下标
    /// </summary>
    /// <param name="randomAttr"></param>
    public int GetRandomAttrIndex(ArtifactRandomAttr randomAttr)
    {
        int count = randomAttrList.Count;
        ArtifactRandomAttr tempAttr = null;
        for (int i = 0; i < count; i++)
        {
            tempAttr = randomAttrList[i];
            if (tempAttr == randomAttr)
                return i;
        }

        return -1;
    }
    /// <summary>
    /// 是否满足神器解锁的所有条件
    /// </summary>
    /// <returns></returns>
    public bool IsReachedAllCoditions()
    {
        if (artifactInfo != null)
        {
            int count = artifactInfo.conditions.Count;
            for (int i = 0; i < count; i++)
            {
                if (artifactInfo.conditions[i] != -1)
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }

    public bool IsEnoughNengYuang()
    {
        int needNum = GetNeedDaiBiNum(0);
        int nengYuanNum = GetNengYuanNum();
        return nengYuanNum >= needNum;
    }

    public bool IsEnoughGold()
    {
        int needNum = GetNeedDaiBiNum(1);
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.gold >= needNum;
    }

    public bool IsEnoughDiamond()
    {
        int needNum = GetNeedDaiBiNum(2);
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.damond >= needNum;
    }
    /// <summary>
    /// 获得培养需要的代币数量
    /// </summary>
    /// <param name="type">0能源，1金币，2钻石</param>
    /// <returns></returns>
    public int GetNeedDaiBiNum(int type)
    {
        int needNum = int.MaxValue;
        t_artifactBean artifactBean = ConfigBean.GetBean<t_artifactBean, int>(ShenQiService.Singleton.curShenQiID);
        if (artifactBean != null && !string.IsNullOrEmpty(artifactBean.t_comsume))
        {
            string[] consumeArr = artifactBean.t_comsume.Split(';');
            if (consumeArr.Length == 3)
                consumeArr = consumeArr[type].Split('+');

            if (consumeArr.Length == 2)
                needNum = int.Parse(consumeArr[1]);
        }

        return needNum;
    }

    public int GetNengYuanID()
    {
        int id = 0;
        t_artifactBean artifactBean = ConfigBean.GetBean<t_artifactBean, int>(ShenQiService.Singleton.curShenQiID);
        if (artifactBean != null && !string.IsNullOrEmpty(artifactBean.t_comsume))
        {
            string[] consumeArr = artifactBean.t_comsume.Split(';');
            id = int.Parse(consumeArr[0].Split('+')[0]);
        }

        return id;
    }
    /// <summary>
    /// 获得能培养的次数
    /// </summary>
    /// <returns></returns>
    public int GetCultureTimes(int isSingle)
    {
        if (isSingle == 1)
        {
            return 1;
        }
        else
        {
            int times = 1;
            // 单次需要的资源数据
            int needNengYuanNum = GetNeedDaiBiNum(0);
            int needGoldNum = GetNeedDaiBiNum(1);
            int needDiamondNum = GetNeedDaiBiNum(2);

            int haveNengYuanNum = GetNengYuanNum();
            int times1 = 1;
            int times2 = 1;
            Message.Role.RoleInfo roleInfo;
            switch (curCultureMethod)
            {
                case CultureMethod.Primary:
                    times = haveNengYuanNum / needNengYuanNum;
                    break;
                case CultureMethod.Middles:
                    times1 = haveNengYuanNum / needNengYuanNum;
                    roleInfo = RoleService.Singleton.GetRoleInfo();
                    times2 = (int)(roleInfo.gold / needGoldNum);
                    times = times1 > times2 ? times2 : times1;
                    break;
                case CultureMethod.Senior:
                    times1 = haveNengYuanNum / needNengYuanNum;
                    roleInfo = RoleService.Singleton.GetRoleInfo();
                    times2 = roleInfo.damond / needDiamondNum;
                    times = times1 > times2 ? times2 : times1;
                    break;
                default:
                    break;
            }
            times = times > 10 ? 10 : times;
            return times;
        }
    }

    public int GetNengYuanNum()
    {
        t_artifactBean artifactBean = ConfigBean.GetBean<t_artifactBean, int>(ShenQiService.Singleton.curShenQiID);
        if (artifactBean != null && !string.IsNullOrEmpty(artifactBean.t_comsume))
        {
            string[] consumeArr = artifactBean.t_comsume.Split(';');
            int nengYuanID = int.Parse(consumeArr[0].Split('+')[0]);

            GridInfo gridInfo = BagService.Singleton.GetGridInfo(nengYuanID);
            int nengYuanNum = gridInfo == null ? 0 : gridInfo.itemInfo.num;
            return nengYuanNum;
        }

        return 0;
    }
    #endregion;

}
