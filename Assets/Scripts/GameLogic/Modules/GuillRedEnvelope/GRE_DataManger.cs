using System.Collections;
using System.Collections.Generic;
using Message.Guild;
using Message.Role;

public class GRE_DataManger
{
    public bool jinbi;//是否抢过金币红包
    public bool zuanshi;//是否抢过钻石红包
    public bool shenqi;//是否抢过神器之源红包
    public ResOpenHongbaoPage mainevt;
    public ResHongbaoList hongbaoliebiao;
    public List<HongbaoRole> jinbiList;
    public List<HongbaoRole> zuanshiList;
    public List<HongbaoRole> shenqiList;
    public ResHongbaoRank fahongbaopaihang;//发红包排行榜
    public RoleInfo roleInfo;
    public int number;//已发红包次数

    public GRE_DataManger()
    {
        roleInfo = RoleService.Singleton.GetRoleInfo();
        jinbiList = new List<HongbaoRole>();
        zuanshiList = new List<HongbaoRole>();
        shenqiList = new List<HongbaoRole>();
        jinbi = false;
        zuanshi = false;
        shenqi = false;
        AddKeyEvent();
    }
    public void AddKeyEvent()
    {
        GED.ED.addListener(EventID.OnGuildRedSheTuan,OnMainEvent);
        GED.ED.addListener(EventID.OnGuildHongBaoNumChange,OnHongBaoNumChange);
        GED.ED.addListener(EventID.OnGuildFaHongBaoPaiHang,OnFaHongBaoPaiHang);
        GED.ED.addListener(EventID.OnGuildHongBaoLieBiao,OnHongBaoLieBiao);
        GED.ED.addListener(EventID.OnGuildHasHongBao,OnHasHongBao);
        GED.ED.addListener(EventID.OnGuildHuoDeHongBao,OnHuoDeHongBao);
    }
    public void RemoveEvent()
    {
        GED.ED.removeListener(EventID.OnGuildRedSheTuan, OnMainEvent);
        GED.ED.removeListener(EventID.OnGuildHongBaoNumChange, OnHongBaoNumChange);
        GED.ED.removeListener(EventID.OnGuildFaHongBaoPaiHang, OnFaHongBaoPaiHang);
        GED.ED.removeListener(EventID.OnGuildHongBaoLieBiao,OnHongBaoLieBiao);
        GED.ED.removeListener(EventID.OnGuildHasHongBao,OnHasHongBao);
        GED.ED.removeListener(EventID.OnGuildHuoDeHongBao, OnHuoDeHongBao);
    }
    private void OnMainEvent(GameEvent evt)
    {
        mainevt = evt.Data as ResOpenHongbaoPage;
        if (mainevt == null)
        {
            Logger.err("红包主信息有误！！！！！");
            return;
        }
        number = mainevt.num;
        jinbiList.Clear();
        zuanshiList.Clear();
        shenqiList.Clear();
        for (int i = 0; i < mainevt.hongbaos.Count; ++i)
        {
            if (mainevt.hongbaos[i].id == 1)
            { jinbiList.AddRange(mainevt.hongbaos[i].roles); }
            else if (mainevt.hongbaos[i].id == 2)
            { zuanshiList.AddRange(mainevt.hongbaos[i].roles); }
            else if (mainevt.hongbaos[i].id == 3)
            { shenqiList.AddRange(mainevt.hongbaos[i].roles); }
        }
        OnQiangGuo();
    }
    /// <summary>
    /// 得到红包信息
    /// </summary>
    /// <returns></returns>
    public Hongbao GetHongbaoInfo(long hongbaoId)
    {
        if (mainevt != null)
        {
            for (int i = 0; i < mainevt.hongbaos.Count; ++i)
            {
                if (hongbaoId == mainevt.hongbaos[i].id)
                    return mainevt.hongbaos[i];
            }
        }
        return null;
    }
    private void OnHongBaoNumChange(GameEvent evt)
    {
        number = (int)evt.Data;
    }
    //发红包排行榜
    private void OnFaHongBaoPaiHang(GameEvent evt)
    {
        fahongbaopaihang = evt.Data as ResHongbaoRank;
        WinInfo info = new WinInfo();
        TwoParam<int, GRE_DataManger> twoParam = new TwoParam<int, GRE_DataManger>();
        twoParam.value1 = (int)PaiHangType.fahongbao;
        twoParam.value2 = this;
        info.param = twoParam;
        WinMgr.Singleton.Open<GER_PaiHangWindow>(info, UILayer.Popup);
    }
    private void OnHongBaoLieBiao(GameEvent evt)
    {
        hongbaoliebiao = evt.Data as ResHongbaoList;
        GED.ED.dispatchEvent(EventID.OnGuildSleepQiangHongBao);
    }
    private void OnHasHongBao(GameEvent evt)
    {
        //收有红包了消息,判定红包类型并给主城界面通知
        Logger.err("有红包了");
    }
    private void OnHuoDeHongBao(GameEvent evt)
    {
        ResReceiveHongbao huodehongbao = evt.Data as ResReceiveHongbao;
        if (huodehongbao != null)
        {
            for (int i = 0; i < mainevt.hongbaos.Count; ++i)
            {
                if (mainevt.hongbaos[i].id == huodehongbao.hongbao.id)
                {
                    mainevt.hongbaos[i] = huodehongbao.hongbao;
                    break;
                }
            }
            List<string> hongbaorizhi = huodehongbao.logs;//红包日志
            Hongbao hongbao = huodehongbao.hongbao;//红包详情
            int number = huodehongbao.num;//抢红包次数
            if (hongbao.id == 1)
            {
                jinbiList = hongbao.roles;
            }
            else if (hongbao.id == 2)
            {
                zuanshiList = hongbao.roles;
            }
            else if (hongbao.id == 3)
            {
                shenqiList = hongbao.roles;
            }
            else
            {
                if (hongbaoliebiao != null)
                {
                    for (int i = 0; i < hongbaoliebiao.hongbaos.Count; ++i)
                    {
                        if (hongbao.id == hongbaoliebiao.hongbaos[i].id)
                        {
                            hongbaoliebiao.hongbaos[i] = hongbao;
                            //发消息刷新红包列表
                            GED.ED.dispatchEvent(EventID.OnGuildSleepQiangHongBao);
                            return;
                        }
                    }
                }
            }
            //刷新社团红包界面
            OnQiangGuo();
        }
    }
    private void OnQiangGuo()
    {
        for (int i = 0; i < jinbiList.Count; ++i)
        {
            if (jinbiList[i].roleId == roleInfo.roleId)
                jinbi = true;
        }
        for (int i = 0; i < zuanshiList.Count; ++i)
        {
            if (zuanshiList[i].roleId == roleInfo.roleId)
                zuanshi = true;
        }
        for (int i = 0; i < shenqiList.Count; ++i)
        {
            if (shenqiList[i].roleId == roleInfo.roleId)
                shenqi = true;
        }
        RedDotManager.Singleton.SetRedDotValue("Guild/hongbao", jinbi);
        RedDotManager.Singleton.SetRedDotValue("Guild/hongbao", zuanshi);
        RedDotManager.Singleton.SetRedDotValue("Guild/hongbao", shenqi);
        GED.ED.dispatchEvent(EventID.OnGuildRedDataManger);
    }
    public void Close()
    {
        mainevt = null;
        hongbaoliebiao = null;
        jinbiList = null;
        zuanshiList = null;
        shenqiList = null  ;
        fahongbaopaihang = null;//发红包排行榜
        roleInfo = null;
        RemoveEvent();
    }
}
