using Data.Beans;
using System.Collections.Generic;
using UI_Strength;

public class SkillDetailWindow : BaseWindow
{

    UI_SkillDetailWindow _window;
    //技能表
    t_skillBean jinengBean;
    private int dengji;
    private int jinengId;
    private string miaoshu;


    public override void OnOpen()
    {
        base.OnOpen();
        _window = getUiWindow<UI_SkillDetailWindow>();
        _window.m_mask.onClick.Add(OnCloseBtn);
        _window.m_closeBtn.onClick.Add(OnCloseBtn);
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        if (Info.param == null)
        {
            Logger.err("未传入技能id");
            return;
        }
        TwoParam<int, int> twoParam = (TwoParam<int, int>)Info.param;
        //1是id，2是等级
        dengji = twoParam.value2;
        jinengId = twoParam.value1;
        jinengBean = ConfigBean.GetBean<t_skillBean, int>(jinengId);
        if (jinengBean == null)
        {
            Logger.err("技能表内没有此技能！");
            return;
        }
        Data();
        FillIcon();
        OnGaoDu();
        OnJiaoBiao();
    }
    private void FillIcon()
    {
        UIGloader.SetUrl(_window.m_TouXiang,jinengBean.t_icon);
        //UIGloader.SetUrl(_window.m_BeiJing, UIUtils.GetBorderByQuality(0));
    }

    private void Data()
    {
        miaoshu = jinengBean.t_effect_str;
        
        _window.m_nameLabel.text = jinengBean.t_name;
        _window.m_typeLabel.text = jinengBean.t_type_name;
        _window.m_lvLabel.text = dengji.ToString();
        if (dengji > 0)
        {
            _window.m_lvLabel.text = dengji.ToString();
            _window.m_lock.visible = false;
            _window.m_unlock.visible = true;
        }
        else
        {
            _window.m_unlock.visible = false;
            _window.m_lock.visible = true;
        }

        if (string.IsNullOrEmpty(jinengBean.t_describe))
        { Logger.err("SKillDetailWindow：Data:技能介绍语言包id为空"); }
        else
            _window.m_MiaoShu.text = jinengBean.t_describe;
        
        UIGloader.SetUrl(_window.m_TouXiang, jinengBean.t_icon);

        //====================================================
        if (jinengBean.t_type == 1 || jinengBean.t_type == 2)
        {
            OnZhuDong();
        }
        else if(jinengBean.t_type == 3 ||jinengBean.t_type == 4 || jinengBean.t_type == 5 || jinengBean.t_type == 6)
        {
            OnBeiDongJi();
        }
       
    }
    //主动效果或者主动和附加效果
    private void OnZhuDong()
    {
        //如果不为空
        if (!(string.IsNullOrEmpty(jinengBean.t_effect_str_type)))
        {
            int[] xiaoguoleixing = GTools.splitStringToIntArray(jinengBean.t_effect_str_type);
            t_skill_effectBean zhudongxiaoguoBean = ConfigBean.GetBean<t_skill_effectBean,int>(jinengBean.t_main_effect_id);
            if (zhudongxiaoguoBean == null)
            {
                Logger.err("未能读取到主动效果");
                return;
            }
            List<int> shuzhixiaoguo = new List<int>();
            List<int> gailvxiaoguo = new List<int>();
            gailvxiaoguo.Add(zhudongxiaoguoBean.t_param1_base + zhudongxiaoguoBean.t_param1_grow * (dengji - 1));
            shuzhixiaoguo.Add(zhudongxiaoguoBean.t_param2_base + zhudongxiaoguoBean.t_param2_grow * (dengji - 1));
            string[] miaoshushuzi = new string[xiaoguoleixing.Length];
            if(xiaoguoleixing.Length > 2)
            {
                if (!(string.IsNullOrEmpty(jinengBean.t_extra_effect_id)))
                {
                    int[] fujiaxiaoguoId = GTools.splitStringToIntArray(jinengBean.t_extra_effect_id);
                    t_skill_effectBean fujiaxiaoguoBean = ConfigBean.GetBean<t_skill_effectBean, int>(fujiaxiaoguoId[0]);
                    if (fujiaxiaoguoBean != null)
                    {
                        gailvxiaoguo.Add(fujiaxiaoguoBean.t_param1_base + fujiaxiaoguoBean.t_param1_grow * (dengji - 1));
                        shuzhixiaoguo.Add(fujiaxiaoguoBean.t_param2_base + fujiaxiaoguoBean.t_param2_grow * (dengji - 1));
                    }
                }
            }
            int gailv = 0;
            int shuzhi = 0;
            for(int i = 0; i < xiaoguoleixing.Length; ++i)
            {
                if (xiaoguoleixing[i] == 1)
                { miaoshushuzi[i] = ((float)gailvxiaoguo[gailv++] / 10000).ToString(); }
                else if (xiaoguoleixing[i] == 2)
                { miaoshushuzi[i] = shuzhixiaoguo[shuzhi++].ToString(); }
            }
            _window.m_XiaoGuo.text = UIUtils.onXiaoGuo(miaoshu, miaoshushuzi);
        }
    }
    private void OnBeiDongJi()
    {
        if (string.IsNullOrEmpty(jinengBean.t_bd_effect_id))
        {
            Logger.err("未能获得技能效果id" + jinengBean.t_id);
            return;
        }
        //读取所有不为零的效果
        List<int> shuzhixiaoguo = new List<int>();
        List<int> gailvxiaoguo = new List<int>();
        int[] xiaoguo = GTools.splitStringToIntArray(jinengBean.t_bd_effect_id);
        if (!(string.IsNullOrEmpty(jinengBean.t_bd_effect_id)))
        {
            for (int i = 0; i < xiaoguo.Length; ++i)
            {
                t_skill_effectBean effectBean = ConfigBean.GetBean<t_skill_effectBean, int>(xiaoguo[i]);
                if (effectBean == null)
                {
                    Logger.err("技能效果表找不到对应id" + jinengBean.t_bd_effect_id);
                    _window.m_XiaoGuo.text = "技能效果表找不到对应id" + jinengBean.t_bd_effect_id;
                    continue;
                }
                else
                {

                    //效果读取
                    int gailvzhi = (int)(effectBean.t_param1_base + ((float)effectBean.t_param1_grow) * (dengji - 1));
                    gailvxiaoguo.Add(gailvzhi);
                    int shuzhi = (int)(effectBean.t_param2_base + ((float)effectBean.t_param2_grow - 1) * (dengji - 1));
                    shuzhixiaoguo.Add(shuzhi);
                }
            }
        }
        if (string.IsNullOrEmpty(jinengBean.t_effect_str_type))
        {
            Logger.err("未能拿到技能效果类型，无法显示" + jinengBean.t_id);
            return;
        }
        int[] xiaoguo_type = GTools.splitStringToIntArray(jinengBean.t_effect_str_type);
        string[] fuhao = new string[xiaoguo_type.Length];
        int number = 0;
        int gailv = 0;
        for (int i = 0; i < fuhao.Length; ++i)
        {
            if (xiaoguo_type[i] == 2)
            {
                fuhao[i] = shuzhixiaoguo[number].ToString();
                number += 1;
            }
            else if (xiaoguo_type[i] == 1)
            {
                fuhao[i] = ((float)gailvxiaoguo[gailv] / 10000 * 100) + "";
                gailv += 1;
            }
        }
        if (xiaoguo_type.Length <= shuzhixiaoguo.Count + gailvxiaoguo.Count)
            _window.m_XiaoGuo.text = UIUtils.onXiaoGuo(miaoshu, fuhao);
        else
            _window.m_XiaoGuo.text = "jinengBean.t_effect_str_type的长度大于读取到数据的长度，无法显示数据";
    }
    /// <summary>
    /// 描述底板高度设置
    /// </summary>
    private void OnGaoDu()
    {
        _window.m_MiaoShuDiPian.height = _window.m_MiaoShu.height + 15;
        _window.m_bg.height = _window.m_kuang.height + _window.m_MiaoShuDiPian.height + _window.m_XiaoGuo.height + 25;
        _window.m_XiaoGuo.y += _window.m_MiaoShu.height + 20;
    }
    ///类型角标设置
    private void OnJiaoBiao()
    {
        if (jinengBean.t_type == 1)
        {
            _window.m_XiaoJiNeng_JiaoBiao.visible = true;
            _window.m_JueJi_JiaoBiao.visible = false;
            t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(10044);
            if (globalBean == null)
            {
                Logger.err("全局表没有小技能角标语言包id" + 10044);
            }
            else
            {
                t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(int.Parse(globalBean.t_string_param));
                if (languageBean == null)
                {
                    Logger.err("语言包表没有小技能角标语言包id" + 10044);
                }
                else
                {
                    _window.m_JieNeng_type.text = languageBean.t_content;
                }
            }
        }
        else if (jinengBean.t_type == 2)
        {
            _window.m_XiaoJiNeng_JiaoBiao.visible = false;
            _window.m_JueJi_JiaoBiao.visible = true;
            t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(10045);
            if (globalBean == null)
            {
                Logger.err("全局表没有小技能角标语言包id" + 10045);
            }
            else
            {
                t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(int.Parse(globalBean.t_string_param));
                if (languageBean == null)
                {
                    Logger.err("语言包表没有小技能角标语言包id" + 10045);
                }
                else
                {
                    _window.m_JieNeng_type.text = languageBean.t_content;
                }
            }
        }
        else
        {
            _window.m_JueJi_JiaoBiao.visible = false;
            _window.m_JieNeng_type.visible = false;
            _window.m_XiaoJiNeng_JiaoBiao.visible = false;
        }
    }
    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
        jinengBean = null;
        _window = null;
        Close();
    }
}
