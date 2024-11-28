using Data.Beans;
using Message.Pet;
using UI_PetParticulars;

public class JiBanMianBan : UI_JiBanMianBan
{
    private  t_petBean mBean;
    private t_fetterBean fetterBean;
    private PetInfo petinfo;
    private float m_hight;
    public new static JiBanMianBan CreateInstance()
    {
        return (JiBanMianBan)UI_JiBanMianBan.CreateInstance();
    }
    
    public void Init(int petid)
    {
        mBean = ConfigBean.GetBean<t_petBean,int>(petid);
        if (mBean == null)
        {
            Logger.err("JiBanMianBan:InitView:未在宠物表找到传入宠物id对应的数据");
            return;
        }
        if (string.IsNullOrEmpty(mBean.t_fetter))
        {
            Logger.err("JiBanMianBan:InitView:未在宠物表找到宠物的羁绊数据");
            return;
        }
        //得到其所有的羁绊的id
        FillData();
    }
    /// <summary>
    /// 面板填充数据
    /// </summary>
    private void FillData()
    {
        height = m_SuMingList.y;
        m_SuMingList.RemoveChildren(0,-1,true);
        m_SuMingList.height = 0;
        m_hight = 0;
        int[] fetterAtty = GTools.splitStringToIntArray(mBean.t_fetter);
        JiBanListItem jiBanList;
        //加载羁绊填充数据
        //函数返回拼接后的字符串赋值给列表元素
        for (int i = 0; i < fetterAtty.Length; ++i)
        {
            jiBanList = JiBanListItem.CreateInstance();
            fetterBean = ConfigBean.GetBean<t_fetterBean, int>(fetterAtty[i]);
            if (fetterBean == null)
            {
                Logger.err("JiBanMianBan:FillData:在羁绊表未获得第" + i + "个羁绊的数据，羁绊id为" + fetterAtty[i]);
                continue;
            }
            if (fetterBean.t_type == 0)
            {
                Logger.err("JiBanMianBan:FillData:羁绊表中为填写 羁绊类型" + fetterAtty[i]);
                continue;
            }
            else
                jiBanList.Init(fetterAtty[i], mBean.t_id);
            m_SuMingList.AddChild(jiBanList);
            m_hight += jiBanList.height;
        }
        m_SuMingList.height = m_hight;
        height += m_SuMingList.height + 20;
        m_bg.height = m_SuMingList.height + 20;
    }

    public override void Dispose()
    {
        base.Dispose();
        mBean = null;
        fetterBean = null;
    }
    
}
