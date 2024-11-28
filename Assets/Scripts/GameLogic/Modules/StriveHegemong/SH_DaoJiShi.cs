using Data.Beans;
using System;
using UI_StriveHegemong;

public class SH_DaoJiShi : UI_SH_DaoJiShi
{
    private DoActionInterval doAction;
    private int time;
    public new static SH_DaoJiShi CreateInstance()
    {
        return (SH_DaoJiShi)UI_SH_DaoJiShi.CreateInstance();
    }
    public void Init()
    {
        doAction = new DoActionInterval();
        time = OnComputeTime();
        if(time > 0)
            doAction.doAction(1, OnDaoJiShi,null,true);
    }
    private void OnDaoJiShi(object obj)
    {
        time--;
        m_daojishi.text = (time / (60 * 60)) + ":" + ((time % (60 * 60)) / 60) + ":" + (time % 60);
    }
    private int OnComputeTime()
    {
        int downtime = 0;

        //计算时间
        DateTime dateTime = TimeUtils.currentServerDateTime2();
        int currhour = dateTime.Hour;
        int currminute = dateTime.Minute;
        int currsecound = dateTime.Second;

        //得到开始时间
        t_globalBean bean = ConfigBean.GetBean<t_globalBean, int>(1702002);
        //t_globalBean bean = null;
        if (bean == null)
        {
            if (currhour < 20)
            {
                downtime = (22 - currhour - 1) * 60 * 60;
                downtime += (60 - currminute) * 60;
                downtime += (60 - currsecound);
            }
            else
            {
                return -1;
            }
        }
        else
        {
            string[] opentime = GTools.splitString(bean.t_string_param, ';');
            opentime = GTools.splitString(opentime[0]);
            opentime = GTools.splitString(opentime[0], ':');
            int open = int.Parse(opentime[0]);
            if (currhour < open)
            {
                downtime = (open - currhour) * 60 * 60;
                downtime += (60 - currminute) * 60;
                downtime += (60 - currsecound);
            }
            else
            {
                return -1;
            }
        }
        //返回的是秒数
        return downtime;
    }
    public override void Dispose()
    {
        if (doAction != null)
        {
            doAction.kill();
            doAction = null;
        }
        base.Dispose();
    }
}
