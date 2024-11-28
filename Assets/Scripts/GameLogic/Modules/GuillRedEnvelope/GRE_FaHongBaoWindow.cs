using UI_GuillRedEnvelope;
using Data.Beans;
using System.Collections.Generic;

class GRE_FaHongBaoWindow : BaseWindow
{
    private UI_GRE_FaHongBaoWindow window;
    private int type;//红包类型
    private GRE_DataManger manger;
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_GRE_FaHongBaoWindow>();
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        InitView();
    }
    public override void InitView()
    {
        base.InitView();
        TwoParam<int, GRE_DataManger> twoParam = Info.param as TwoParam<int,GRE_DataManger>;
        if (twoParam != null)
        {
            type = twoParam.value1;
            manger = twoParam.value2;
            if (type != 0)
            {
                OnFillData();
            }
        }
    }
    private void OnFillData()
    {
        List<t_hongbaoBean> hongbaoBeans = ConfigBean.GetBeanList<t_hongbaoBean>();
        if (hongbaoBeans != null)
        {
            GRE_FaHongBao_type listItem;
            window.m_typeList.RemoveChildren(0,-1,true);
            for (int i = 0; i < hongbaoBeans.Count; ++i)
            {
                if (hongbaoBeans[i].t_type == type)
                {
                    listItem = GRE_FaHongBao_type.CreateInstance();
                    listItem.Init(hongbaoBeans[i],manger);
                    window.m_typeList.AddChild(listItem);
                }
            }
        }
    }
    public override void RefreshView()
    {
        base.RefreshView();
    }
    protected override void OnCloseBtn()
    {
        window.m_typeList.RemoveChildren(0, -1, true);
        window = null;
        manger = null;
        base.OnCloseBtn();
    }
}
