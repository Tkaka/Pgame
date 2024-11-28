using Data.Beans;
using Message.Team;
using UI_HallFame;

public class HF_ShuXingXianShiWindow : BaseWindow
{
    private UI_HF_ShuXingXianShiWindow window;
    private int petId;
    private int type;
    public override void OnOpen()
    {
        window = getUiWindow<UI_HF_ShuXingXianShiWindow>();
        AddKeyEvent();
        InitView();
    }
    private void AddKeyEvent()
    {
        window.m_CloseBtn.onClick.Add(OnCloseBtn);
        window.m_BeiJing.onClick.Add(OnCloseBtn);
    }
    public override void InitView()
    {
        base.InitView();
        if (Info.param == null)
        {
            Logger.err("HF_ShuXingXianShiWindow:InitView:未传入参数");
            return;
        }
        petId = (int)Info.param;
        FillData();
        FillHallFameData();
        Next();
    }
    private void FillData()
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean,int>(petId);
        if (petBean == null)
        {
            Logger.err("HF_ShuXingXianShiWindow:FillData:未能在宠物表找到该宠物---" + petId);
            return;
        }
        window.m_name.text = UIUtils.GetPetName(petBean);
    }
    private void FillHallFameData()
    {
        HofItem hofItem = HallFameService.Singleton.GetHofItem(petId);
        if (hofItem == null)
        {
            Logger.err("HF_ShuXingXianShiWindow:FillData:未能找到宠物数据，请价差服务器发来的数据是否正确---" + petId);
            return;
        }
        hofItem.attrs.Sort(SortPaml);
        window.m_dangqiandengji.text = hofItem.level.ToString() + "级";
        HF_shuxing shuxing;
        window.m_propertyList.RemoveChildren(0,-1,true);
        for (int i = 0; i < hofItem.attrs.Count; ++i)
        {
            shuxing = HF_shuxing.CreateInstance();
            shuxing.Init(hofItem.attrs[i].id,hofItem.attrs[i].value);
            window.m_propertyList.AddChild(shuxing);
        }
        type = hofItem.level % 5;
    }
    private void Next()
    {
        window.m_nextProperty.m_number.text = "+";
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (petBean == null)
        {
            Logger.err("HF_ShuXingXianShiWindow:FillData:未能在宠物表找到该宠物---" + petId);
            return;
        }
        HofItem hofItem = HallFameService.Singleton.GetHofItem(petId);
        if (hofItem == null)
        {
            Logger.err("HF_ShuXingXianShiWindow:FillData:未能找到宠物数据，请价差服务器发来的数据是否正确---" + petId);
            return;
        }
        t_hof_level_upBean level_UpBean = ConfigBean.GetBean<t_hof_level_upBean,int>(hofItem.color + 1);
        if (level_UpBean == null)
        {
            Logger.err("HF_ShuXingXianShiWindow:FillData:未能在宠物表找到该品阶数据---" + (hofItem.color + 1));
            return;
        }
        if (petBean.t_type == 1)
        {
            if (string.IsNullOrEmpty(level_UpBean.t_attr_atk))
            {
                Logger.err("HF_ShuXingXianShiWindow:FillData:品阶表没有对应数据" + level_UpBean.t_id);
                return;
            }
            
            int[,] attr_atk = UIUtils.splitStringTotwodimensionArry(level_UpBean.t_attr_atk);
            t_attr_nameBean nameBean = ConfigBean.GetBean<t_attr_nameBean, int>(attr_atk[type, 0]);
            if (nameBean == null)
            {
                Logger.err("HF_shuxing:Init:属性错误为空！");
                return;
            }
             window.m_nextProperty.m_type.text = nameBean.t_name_id;
            if (attr_atk[type, 0] > 3)
            {
                window.m_nextProperty.m_number.text += ((float)attr_atk[type, 1] / 10000 * 100).ToString() + "%";
            }
            else
                window.m_nextProperty.m_number.text += attr_atk[type, 1].ToString();
        }
        else if (petBean.t_type == 2)
        {
            if (string.IsNullOrEmpty(level_UpBean.t_attr_def))
            {
                Logger.err("HF_ShuXingXianShiWindow:FillData:品阶表没有对应数据" + level_UpBean.t_id);
                return;
            }
            int[,] attr_def = UIUtils.splitStringTotwodimensionArry(level_UpBean.t_attr_def);
            t_attr_nameBean nameBean = ConfigBean.GetBean<t_attr_nameBean, int>(attr_def[type, 0]);
            if (nameBean == null)
            {
                Logger.err("HF_shuxing:Init:属性错误为空！");
                return;
            }
            window.m_nextProperty.m_type.text = nameBean.t_name_id;
            
            if (attr_def[type, 0] > 3)
            {
                window.m_nextProperty.m_number.text += ((float)attr_def[type, 1] / 10000 * 100).ToString() + "%";
            }
            else
                window.m_nextProperty.m_number.text += attr_def[type, 1].ToString();
        }
        else if (petBean.t_type == 3)
        {
            if (string.IsNullOrEmpty(level_UpBean.t_attr_skill))
            {
                Logger.err("HF_ShuXingXianShiWindow:FillData:品阶表没有对应数据" + level_UpBean.t_id);
                return;
            }
            int[,] attr_skill = UIUtils.splitStringTotwodimensionArry(level_UpBean.t_attr_skill);
            t_attr_nameBean nameBean = ConfigBean.GetBean<t_attr_nameBean, int>(attr_skill[type, 0]);
            if (nameBean == null)
            {
                Logger.err("HF_shuxing:Init:属性错误为空！");
                return;
            }
            window.m_nextProperty.m_type.text = nameBean.t_name_id;
            if (attr_skill[type, 0] > 3)
            {
                window.m_nextProperty.m_number.text += ((float)attr_skill[type, 1] / 10000 * 100).ToString() + "%";
            }
            else
                window.m_nextProperty.m_number.text += attr_skill[type, 1].ToString();
        }
    }
    private int SortPaml(Attr a, Attr b)
    {
        int resA = 0;
        int resB = 0;

        if (a.id > b.id)
            resA += 1000;
        else if (a.id < b.id)
            resB += 1000;

        if (resA > resB)
            return 1;
        else if (resA == resB)
            return 0;
        else
            return -1;
    }
    protected override void OnCloseBtn()
    {
        window = null;
        base.OnCloseBtn();
    }
}
