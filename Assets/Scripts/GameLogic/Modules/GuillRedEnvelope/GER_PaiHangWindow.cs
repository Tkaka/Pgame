using System.Collections.Generic;
using UI_GuillRedEnvelope;
using Message.Guild;
using FairyGUI;
public enum PaiHangType//排行榜类型
{
    jinbi = 1,
    zuanshi = 2,
    shenqi = 3,
    fahongbao = 4,
}

public class GER_PaiHangWindow : BaseWindow
{
    private UI_GER_PaiHangWindow window;
    private List<HongbaoRole> qianghongbaoList;//抢红包列表
    private List<HongbaoRankRole> fahongbaoList;//发红包排行
    private ResHongbaoRank fahongbao;//发红包排行消息
    private PaiHangType paiHangType;//当前打开的排行榜类型
    private string name = null;
    public override void OnOpen()
    {
        window = getUiWindow<UI_GER_PaiHangWindow>();
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_ranklist.SetVirtual();
        window.m_ranklist.itemRenderer = RanderItem;
        TwoParam<int, GRE_DataManger> twoParam = Info.param as TwoParam<int, GRE_DataManger>;
        if (twoParam != null)
        {
            //金币、钻石、神器之源三种红包的排行榜
            paiHangType = (PaiHangType)twoParam.value1;
            if (paiHangType != PaiHangType.fahongbao)
            {
                switch (paiHangType)
                {
                    case PaiHangType.jinbi: qianghongbaoList = twoParam.value2.jinbiList; break;
                    case PaiHangType.zuanshi: qianghongbaoList = twoParam.value2.zuanshiList; break;
                    case PaiHangType.shenqi: qianghongbaoList = twoParam.value2.shenqiList; break;
                }
                if (qianghongbaoList != null)
                {
                    InitView();
                    window.m_ranklist.numItems = qianghongbaoList.Count;
                    window.m_QiangHongBao.visible = true;
                    window.m_FaHongBao.visible = false;
                }
                OnGeRen();
            }
            else
            {
                //发红包的排行榜
                if (fahongbao == null)
                    fahongbao = twoParam.value2.fahongbaopaihang;
                fahongbaoList = fahongbao.rank;
                RefreshView();
                window.m_ranklist.numItems = fahongbaoList.Count;
                window.m_QiangHongBao.visible = false;
                window.m_FaHongBao.visible = true;
                OnGeRen();
            }
            OnTaiTou();
        }
        else
        {
            TwoParam<string, List<HongbaoRole>> param = Info.param as TwoParam<string, List<HongbaoRole>>;
            if (param != null)
            {
                //个人红包的排行榜
                name = param.value1;
                qianghongbaoList = param.value2;
                window.m_ranklist.numItems = qianghongbaoList.Count;
                window.m_QiangHongBao.visible = true;
                window.m_FaHongBao.visible = false;
                OnGeRen();
            }
            OnTaiTou();
        }
    }
    //抢红包排行榜初始化
    public override void InitView()
    {
        qianghongbaoList.Sort(QiangSort);
    }
    //发红包排行榜初始化
    public override void RefreshView()
    {
        fahongbaoList.Sort(FaSort);
    }
    private int QiangSort(HongbaoRole a, HongbaoRole b)
    {
        if (a.num > b.num)
            return 1;
        else if (a.num == b.num)
            return 0;
        else
            return -1;
    }
    private int FaSort(HongbaoRankRole a, HongbaoRankRole b)
    {
        if (a.rank < b.rank)
            return 1;
        else if (a.rank == b.rank)
            return 0;
        else
            return -1;
    }
    private void RanderItem(int index, GObject obj)
    {
        GRE_RankListItem listItem;
        listItem = obj as GRE_RankListItem;
        if (fahongbao == null)
        {
            listItem.Init(index, qianghongbaoList[index]);
        }
        else
        {
            listItem.FaInit(fahongbaoList[index]);
        }
    }
    private void OnGeRen()
    {
        if (fahongbao == null)
        {
            if (qianghongbaoList != null)
            {
                for (int i = 0; i < qianghongbaoList.Count; ++i)
                {
                    if (qianghongbaoList[i].roleId == RoleService.Singleton.RoleInfo.roleInfo.roleId)
                    {
                        window.m_de_number.text = qianghongbaoList[i].num.ToString();
                        return;
                    }
                }
            }
        }
        else
        {
            window.m_geshu.text = fahongbao.num.ToString();
            window.m_zongliang.text = fahongbao.value.ToString();
        }
    }
    private void OnTaiTou()
    {
        if (string.IsNullOrEmpty(name))
        {
            string miaoshu = "今日{0}红包排行榜";
            if (paiHangType != PaiHangType.fahongbao)
            {
                switch (paiHangType)
                {
                    case PaiHangType.jinbi: miaoshu = string.Format(miaoshu, "金币"); break;
                    case PaiHangType.zuanshi: miaoshu = string.Format(miaoshu, "钻石"); break;
                    case PaiHangType.shenqi: miaoshu = string.Format(miaoshu, "神器之源"); break;
                }
                window.m_name.text = miaoshu;
            }
            else
            {
                window.m_name.text = "发红包排行";
            }
        }
        else
        {
            window.m_name.text = name + "的红包排行";
        }
    }
    protected override void OnCloseBtn()
    {
        window = null;
        base.OnCloseBtn();
    }
}
