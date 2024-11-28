using UI_DrawCard;
using Data.Beans;
using UnityEngine;
using FairyGUI;

public class DaoJuListItem : UI_DaoJuListItem
{
    /// <summary>
    /// 宠物的元件，加载模型
    /// </summary>
    private t_petBean petBean;
    private static UIResPack pack;

    public virtual string shader { get; set; }

    public new static DaoJuListItem CreateInstance()
    {
        return (DaoJuListItem)UI_DaoJuListItem.CreateInstance();
    }
    public void Init(int itemId)
    {
        t_itemBean bean = ConfigBean.GetBean<t_itemBean, int>(itemId);
        if (bean != null)
        {
            if (!(string.IsNullOrEmpty( bean.t_value)))
            {
                string[] pet = GTools.splitString(bean.t_value,';');
                int petid = int.Parse(pet[0]);
                petBean = ConfigBean.GetBean<t_petBean, int>(petid);
                if (petBean == null)
                {
                    Logger.err("DaoJuListItem:Init:抽卡表钻石宠物十连抽必得宠物数据有误");
                    return;
                }
                if (pack == null)
                {
                    pack = new UIResPack(this);
                }
                m_Name.text = UIUtils.GetPetName(petBean);
                AddKeyEvent();
                OnShowModel(petid);
                SetStar(bean.t_star);
            }
        }
    }
    private void AddKeyEvent()
    {
        m_XiangQing.onClick.Add(OnChongWuXiangQing);
    }
    private void OnChongWuXiangQing()
    {
        WinInfo info = new WinInfo();
        info.param = petBean.t_id;
        WinMgr.Singleton.Open<ChongWuXiangQingWindow>(info,UILayer.Popup);
    }
    private void OnShowModel(int petid)
    {
        string name = UIUtils.GetPetStartModel(petid);
        if (name == "")
        {
            Logger.err("传入的宠物id有误");
            return;
        }
        pack.CacheWrapper(m_MoXing);
        var wrapper = new GoWrapper();
        m_MoXing.SetNativeObject(wrapper);
        var actor = pack.NewActorUI(petBean.t_id,ActorType.Pet,wrapper,true);
        actor.SetTransform(new Vector3(0,0,0),100,new Vector3(0,180,0));
    }

    public void CacheModel()
    {
        if (pack != null)
            pack.CacheWrapper(m_MoXing);
    }

    public void SetStar(int star)
    {
        StarList starList = new StarList((UI_Common.UI_StarList)m_XingJi);
        starList.SetStar(star);
    }
    public override void Dispose()
    {
        //CacheModel();
        if (pack != null)
        {
            pack.ReleaseAllRes();
            pack = null;
        }
        if (petBean != null)
            petBean = null;
        base.Dispose();
    }
}