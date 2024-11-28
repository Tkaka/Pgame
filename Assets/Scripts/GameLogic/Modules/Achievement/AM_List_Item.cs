using Message.Achievement;
using UI_Achievement;
using Data.Beans;

public class AM_List_Item : UI_AM_List_Item
{
    private AchievementInfo achievementInfo;
    private t_achievementBean achievementBean;
    public new static AM_List_Item CreateInstance()
    {
        return (AM_List_Item)UI_AM_List_Item.CreateInstance();
    }
    public void Init(AchievementInfo info)
    {
        achievementInfo = info;
        achievementBean = ConfigBean.GetBean<t_achievementBean,int>(achievementInfo.id);
        if (achievementBean == null)
        {
            Logger.err("AM_List_Item:Init:成就表没有此id数据，请检查" + achievementInfo.id + "这个id是否正确");
            return;
        }
        FillIcon();
        FillData();
        OnJinDuTiao();
        FillState();
        m_kelingquBtn.onClick.Add(OnLingQu);
    }
    private void OnLingQu()
    {
        AchievementService.Singleton.OnReqFinish(achievementBean.t_id);
    }
    private void FillIcon()
    {
        //加载图标
        UIGloader.SetUrl(m_AM_Icon,achievementBean.t_icon);
        //加载星级
        if (!(string.IsNullOrEmpty(achievementBean.t_target)))
        {
            AM_StartListItem startListItem;
            int[] starnumber = GTools.splitStringToIntArray(achievementBean.t_target);
            m_AM_Start_List.RemoveChildren(0,-1,true);
            for (int i = 0; i < starnumber.Length; ++i)
            {
                startListItem = AM_StartListItem.CreateInstance();
                if (i >= achievementInfo.star)
                    startListItem.Init(false);
                else
                    startListItem.Init(true);
                m_AM_Start_List.AddChild(startListItem);
            }
        }
    }
    private void FillData()
    {
        t_languageBean languageBean;
        int start = achievementInfo.star;
        //加载成就名字
        m_AM_Name.text = achievementBean.t_name;
        //加载成就描述
        //可获得积分
        string miaoshu = "";
        if (achievementInfo.state == 0 || achievementInfo.state == 1)
        {
            if (!(string.IsNullOrEmpty(achievementBean.t_desc)))
            {
                miaoshu = achievementBean.t_desc;
                
                if (!(string.IsNullOrEmpty(achievementBean.t_target)))
                {
                    string[] targetArry = GTools.splitString(achievementBean.t_target);
                    if (start == targetArry.Length)
                        start -= 1;
                    miaoshu = string.Format(miaoshu, targetArry[start]);
                }
                else
                {
                    Logger.err("AM_List_Item:FillData:成就表目标字段为空---" + achievementBean.t_id);
                }
                languageBean = ConfigBean.GetBean<t_languageBean, int>(70805004);
                if (languageBean == null)
                {
                    Logger.err("AM_List_Item:FillData:语言包没有可获得积分描述的语言id----" + 70805004);
                }
                else
                {
                    miaoshu += languageBean.t_content;
                    if (!(string.IsNullOrEmpty(achievementBean.t_reward)))
                    {
                        string[] rewardArry = GTools.splitString(achievementBean.t_reward);
                        
                        miaoshu = string.Format(miaoshu,rewardArry[start]);
                        //填写先手值奖励
                        m_AM_XianShouZhi.text = rewardArry[start];
                    }
                }
            }
        }
        else if (achievementInfo.state == 2)
        {
            m_AM_WeiWnaCheng.visible = false;
            if (!(string.IsNullOrEmpty(achievementBean.t_desc)))
            {
                miaoshu = achievementBean.t_desc;
                if (!(string.IsNullOrEmpty(achievementBean.t_target)))
                {
                    string[] targetArry = GTools.splitString(achievementBean.t_target);
                    if (start == targetArry.Length)
                        start -= 1;
                    miaoshu = string.Format(miaoshu, targetArry[start]);
                }
                else
                {
                    Logger.err("AM_List_Item:FillData:成就表目标字段为空---" + achievementBean.t_id);
                }
                languageBean = ConfigBean.GetBean<t_languageBean, int>(70805005);
                if (languageBean == null)
                {
                    Logger.err("AM_List_Item:FillData:语言包没有可获得积分描述的语言id----" + 70805005);
                }
                else
                {
                    miaoshu += languageBean.t_content;
                    if (!(string.IsNullOrEmpty(achievementBean.t_reward)))
                    {
                        int[] rewardArry = GTools.splitStringToIntArray(achievementBean.t_reward);
                        int num = 0;
                        for (int i = 0; i < rewardArry.Length; ++i)
                        {
                            num += rewardArry[i];
                        }
                        miaoshu = string.Format(miaoshu, num.ToString());
                    }
                }
            }
        }
        m_AM_MiaoShu.text = miaoshu;
    }
    private void OnJinDuTiao()
    {
        //加载进度条
        int start = achievementInfo.star;
        long value = 0;
        long max = 0;
        if (string.IsNullOrEmpty(achievementBean.t_target))
        {
            Logger.err("AM_List_Item:FillData:成就表目标字段为空---" + achievementBean.t_id);
        }
        else
        {
            value = achievementInfo.schedule;
            long[] target = UIUtils.splitStringToLongArray(achievementBean.t_target);
            if (start == target.Length)
                start -= 1;
            max = target[start];
        }
        m_jianglijindu.max = max;
        m_jianglijindu.value = value;
        m_jianglijindu.m_number.text = value.ToString() + "/" + max.ToString();
    }
    private void FillState()
    {
        //加载成就状态
        //不可领取点击领取按钮隐藏
        m_AM_yiwancheng.visible = false;
        if (achievementInfo.state == 0)
        {
            m_AM_prize_icon.grayed = true;
            m_kelingquBtn.visible = false;
            m_bukelingqu.visible = true;
            m_AM_WeiWnaCheng.visible = true;
        }
        else if (achievementInfo.state == 1)
        {
            m_AM_prize_icon.grayed = false;
            m_bukelingqu.visible = false;
            m_kelingquBtn.visible = true;
            m_AM_WeiWnaCheng.visible = true;
        }
        else if (achievementInfo.state == 2)
        {
            m_AM_WeiWnaCheng.visible = false;
            m_kelingquBtn.visible = false;
            m_AM_yiwancheng.visible = true;
        }
        else
        {
            Logger.err("AM_List_Item:FillState:服务器传来的状态有误！" + achievementInfo.state);
        }
    }
}
