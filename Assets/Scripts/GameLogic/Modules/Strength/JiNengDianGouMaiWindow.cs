using Message.Role;
using UI_Strength;
using Data.Beans;
public class JiNengDianGouMaiWindow : BaseWindow
{
    UI_JiNengDianGouMaiWindow window;
    RoleInfo roleInfo;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_JiNengDianGouMaiWindow>();
        InitView();
        PlayPopupAnim(window.m_BeiJing, window.m_popupView);
    }
    public override void InitView()
    {
        base.InitView();

        window.m_BeiJing.onClick.Add(OnCloseBtn);
        window.m_popupView.m_Close.onClick.Add(OnCloseBtn);
        window.m_popupView.m_GouMai.onClick.Add(OnGouMai);
        window.m_popupView.m_Close.onClick.Add(OnCloseBtn);
        window.m_popupView.m_ChaKan.onClick.Add(OnChaKan);
        roleInfo = RoleService.Singleton.GetRoleInfo();
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean,int>(100401);//技能点初始购买数量
        t_globalBean bean = ConfigBean.GetBean<t_globalBean,int>(100407);//
        if (bean == null)
        {
            Logger.err("全局表没有技能点购买初始价格");
            return;
        }
        //计算价格
        int jiage = 0;
        {
            int[] jiages = GTools.splitStringToIntArray(bean.t_string_param);
            if (roleInfo.skillPointsBuyCount >= jiages.Length)
            {
                jiage = jiages[jiages.Length - 1];
            }
            else
            {
                for (int i = 0; i < jiages.Length; ++i)
                {
                    if (i == roleInfo.skillPointsBuyCount)
                    {
                        jiage = jiages[i];
                        break;
                    }
                }
            }
        }
        if (globalBean != null)
        {
            int jinegndianshu = globalBean.t_int_param ;
            if (roleInfo.damond < jiage)
            {
                window.m_popupView.m_number.text = jiage.ToString();
                window.m_popupView.m_number.color = new UnityEngine.Color(255, 0, 0);
            }
            else
            {
                window.m_popupView.m_number.text = jiage.ToString();
            }
            if (roleInfo.skillPointsBuyCount == 0)
            {
                window.m_popupView.m_GouMai.visible = true;
                window.m_popupView.m_ChaKan.visible = false;
            }
            int number = 0;
            globalBean = ConfigBean.GetBean<t_globalBean, int>(100406);
            number = globalBean.t_int_param - RoleService.Singleton.RoleInfo.roleInfo.skillPointsBuyCount;
            if (roleInfo.vip == 0)
            {
            }
            else
            {
                t_vipBean vipBean = ConfigBean.GetBean<t_vipBean,int>(roleInfo.vip);
                number = vipBean.t_jngmcs;
                number = number - roleInfo.skillPointsBuyCount;
                //得到vip对应的购买次数，再减去已购买次数
            }
            string miaoshu1 = "";
            t_languageBean languageBean;
            if (number > 0)
            { languageBean = ConfigBean.GetBean<t_languageBean, int>(7100403); }
            else
            { languageBean = ConfigBean.GetBean<t_languageBean,int>(7100405); }
            if (languageBean != null)
            {
                miaoshu1 = languageBean.t_content;
            }
            string miaoshu2 = "";
            languageBean = ConfigBean.GetBean<t_languageBean,int>(7100404);
            if (languageBean != null)
            {
                miaoshu2 = languageBean.t_content;
            }
            if (number > 0)
            {
                window.m_popupView.m_TiShi1.text = string.Format(miaoshu1, roleInfo.vip.ToString(), number.ToString());
                window.m_popupView.m_TiShi2.text = string.Format(miaoshu2, jinegndianshu.ToString());
            }
            else
            {
                window.m_popupView.m_TiShi1.text = miaoshu1;
                window.m_popupView.m_TiShi2.visible = false;
                window.m_popupView.m_icon.visible = false;
                window.m_popupView.m_number.visible = false;
            }
            //按键显示管理
            {
                if (number > 0)
                {
                    window.m_popupView.m_GouMai.visible = true;
                    window.m_popupView.m_ChaKan.visible = false;
                    window.m_popupView.m_TiSheng.visible = false;
                }
                if (number <= 0)
                {
                    if (roleInfo.vip > 0)
                    {
                        window.m_popupView.m_GouMai.visible = false;
                        window.m_popupView.m_ChaKan.visible = false;
                        window.m_popupView.m_TiSheng.visible = true;
                    }
                    if (roleInfo.vip == 0)
                    {
                        window.m_popupView.m_GouMai.visible = false;
                        window.m_popupView.m_ChaKan.visible = true;
                        window.m_popupView.m_TiSheng.visible = false;
                    }
                }
            }
        }
    }

    private void OnGouMai()
    {
        if (roleInfo.damond < 30)
        {
            TipWindow.Singleton.ShowTip("钻石数量不足");
            //打开商店
        }
        else
        {
            //购买技能点
            RoleService.Singleton.JiNengDianGouMai();
            OnCloseBtn();
        }
        
    }
    private void OnChaKan()
    {
        //查看vip特权窗口
    }
    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
        Close();
    }

}
