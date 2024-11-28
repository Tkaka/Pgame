using Message.DrawCard;
using UI_DrawCard;
using Data.Beans;

public class DC_Type : UI_DC_Type
{
    NumInfo drawInfo;
    t_draw_cardBean cardBean;
    private int type;//抽奖类型，1金币，2钻石
    private int number;//次数
    private int time;//记录倒计时时间
    private DoActionInterval doAction;//计时协程
    private bool mianfei;//是否免费
    private bool quan;//是否用券
    public new static DC_Type CreateInstance()
    {
        return (DC_Type)UI_DC_Type.CreateInstance();
    }
    public void Init(int leixing,int cishu)
    {
        type = leixing;
        if (type == 1)
            UIGloader.SetUrl(m_type_icon, UIUtils.GetBuyGoodsPriceIcon((int)ItemType.Gold));
        else if(type == 2)
            UIGloader.SetUrl(m_type_icon, UIUtils.GetBuyGoodsPriceIcon((int)ItemType.Damond));

        number = cishu;
        drawInfo = DrawCardService.Singleton.GetChouXiangXinXi(type, number);
        if (drawInfo == null)
        {
            Logger.err(type + "类型" + number + "次抽奖数据为空");
            return;
        }
        cardBean = ConfigBean.GetBean<t_draw_cardBean,int>(type * 10000 + number);
        if (cardBean == null)
        {
            Logger.err("未在抽卡表找到" + type + "类型" + number + "次抽奖数据");
            return;
        }
        AddEvent();
        OnTaiTouAndJiaGe();
        OnAnNiu();
    }
    private void AddEvent()
    {
        m_YiCiBtn.onClick.Add(OnGouMai);
        m_ShiCiBtn.onClick.Add(OnGouMai);
        m_BaiCiBtn.onClick.Add(OnGouMai);
    }
    //价格和抬头加载
    private void OnTaiTouAndJiaGe()
    {
        string xianzhi = "购买次数:{0}/{1}";
        if (type == 1)//类型金币
        {
            if (number == 1)//单次抽奖
            {
                OnJinBiDanChouTaiTou();
            }
            else//其它次数，加载价格和次数限制
            {
                m_jiage.text = (cardBean.t_price).ToString();
            }
        }
        else if (type == 2)
        {
            if (number == 1)//钻石单抽
            {
                ZuanShiDanChouTaiTou();
            }
            else//其它类型，只加载价格
            {
                m_jiage.text = (cardBean.t_price).ToString();
            }
        }
        //抽取次数限制
        if (cardBean.t_sum_num > 0)
        {
            m_cishuxianzhi.text = string.Format(xianzhi, (cardBean.t_sum_num - drawInfo.buyNum), cardBean.t_sum_num);
        }
        else
        { m_cishuxianzhi.text = ""; }
    }
    //按钮加载
    private void OnAnNiu()
    {
        string miaoshu = "购买{0}次";
        if (type == 1)
        {
            m_YiCiBtn.visible = true;
            m_ShiCiBtn.visible = false;
            m_BaiCiBtn.visible = false;
            m_YiCiBtn.m_miaoshu.text = string.Format(miaoshu,number);
        }
        else if(type == 2)
        {
            m_YiCiBtn.visible = false;
            m_ShiCiBtn.visible = false;
            m_BaiCiBtn.visible = true;
            m_BaiCiBtn.m_miaoshu.text = string.Format(miaoshu,number);
        }
    }
    private void DaoJiShi(object obj)
    {
        string jinbimiaoshu = "免费次数{0}/{1}";
        time--;
        if (time < 0)
        {
            if (doAction != null)
            {
                doAction.kill();
                doAction = null;
            }

            if (type == 1)
                OnJinBiDanChouTaiTou();
            else if (type == 2)
                ZuanShiDanChouTaiTou();

            //int xianzhi = cardBean.t_free_num;
            //if (drawInfo.hasFreeBuyNum())
            //{ m_cishu.text = string.Format(jinbimiaoshu, (xianzhi - drawInfo.freeBuyNum), xianzhi); }
            //else
            //{ m_cishu.text = string.Format(jinbimiaoshu, xianzhi, xianzhi); }
        }
        else
        {
            int hour = time / (60 * 60);
            int minute = time / 60;
            int secounds = time % 60;
            string shi = hour.ToString();
            string fen = minute.ToString();
            string miao = secounds.ToString();
            if (hour < 10)
            {
                shi = "0" + shi;
            }
            if (minute < 10)
            {
                fen = "0" + fen;
            }
            if (secounds < 10)
            {
                miao = "0" + miao;
            }
            m_cishu.text = shi + ":" + fen + ":" + miao + "后免费";
        }
    }

    public override void Dispose()
    {
        GED.ED.removeListener(EventID.OnDrawCardEndAnimition, OnQingQiu);
        base.Dispose();
    }
    /// <summary>
    /// 金币单抽抬头文字
    /// </summary>
    private void OnJinBiDanChouTaiTou()
    {
        string jinbimiaoshu = "免费次数{0}/{1}";
        long lasttime = drawInfo.lastFlushTime;
        long curTime = TimeUtils.currentMilliseconds();
        time = (int)(lasttime - curTime);
        time /= 1000;
        if (lasttime > curTime)//倒计时未结束
        {
            if (doAction == null)
            {
                doAction = new DoActionInterval();
                doAction.doAction(1, DaoJiShi, null, true);
            }
            m_jiage.text = cardBean.t_price.ToString();
            mianfei = false;
        }
        else
        {
            if (doAction != null)
            {
                doAction.kill();
                doAction = null;
            }
            int xianzhi = cardBean.t_free_num;
            //已抽次数
            if (drawInfo.hasFreeBuyNum())
            {
                if (drawInfo.freeBuyNum < cardBean.t_free_num)
                {
                    m_jiage.text = "本次免费";
                    mianfei = true;
                    m_cishu.text = string.Format(jinbimiaoshu, (xianzhi - drawInfo.freeBuyNum), xianzhi);
                }
                else
                {
                    mianfei = false;
                    m_jiage.text = cardBean.t_price.ToString();
                    m_cishu.text = "";
                }
            }
            else
            {
                mianfei = true;
                m_jiage.text = "本次免费";
                m_cishu.text = string.Format(jinbimiaoshu, xianzhi, xianzhi);
            }
        }
    }
    /// <summary>
    /// 钻石抽奖抬头
    /// </summary>
    private void ZuanShiDanChouTaiTou()
    {
        //抬头
        string miaoshu = "{0}次后必得宝可梦(非)";
        t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(71202001);
        int zuigaocishu = 10;//
        int num = ((zuigaocishu - 1) - (drawInfo.buyNum % zuigaocishu));
        if (languageBean != null)
        {
            miaoshu = languageBean.t_content;
        }
        if (num > 0)
            m_cishu.text = string.Format(miaoshu, num);
        else
        {
            m_cishu.text = string.Format(miaoshu, "本");
        }
        //价格
        if (drawInfo.hasFreeBuyNum())//有抽奖数据
        {
            if (drawInfo.freeBuyNum < cardBean.t_free_num)
            {
                m_jiage.text = "本次免费";
                mianfei = true;
            }
            else
            {
                if (drawInfo.halfPrice)
                {
                    m_banjia_Icon.visible = true;
                    m_jiage.text = (cardBean.t_price / 2).ToString();
                }
                else
                {
                    m_banjia_Icon.visible = false;
                    m_jiage.text = cardBean.t_price.ToString();
                }
                mianfei = false;
            }
        }
        else//没有抽奖数据，肯定为免费
        {
            m_jiage.text = "本次免费";
            mianfei = true;
        }
    }


    //检查能否购买
    private bool _CheckCanBuy()
    {
        if (mianfei)
        {
            return true;
        }

        if (cardBean != null)
        {
            if (type == 1)
            {
                //判断代币数量是否足够
                long num = RoleService.Singleton.RoleInfo.roleInfo.gold;
                if (cardBean.t_price > num)
                {
                    TipWindow.Singleton.ShowTip("金币数量不足");
                    return false;
                }
                //判断抽奖次数是否足够
                if (drawInfo.buyNum >= cardBean.t_sum_num)
                {
                    TipWindow.Singleton.ShowTip("抽取次数不足");
                    return false;
                }
            }
            else if (type == 2)
            {
                //钻石数量是否足够
                int num = RoleService.Singleton.RoleInfo.roleInfo.damond;
                int price = cardBean.t_price;
                if (drawInfo.halfPrice)
                {
                    price /= 2;
                }

                if (price > num)
                { //判断是否有抽奖券
                    t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(12020);
                    if (globalBean == null)
                    {
                        Logger.err("MoshiMianBan:OnZuanShiDanChou:未能从全局表获得钻石抽奖券id数据");
                    }
                    else
                    {
                        int quanNumber = BagService.Singleton.GetItemNum(globalBean.t_int_param);
                        if (quanNumber <= 0)
                        {
                            TipWindow.Singleton.ShowTip("钻石数量不足");
                            return false;
                        }
                    }
                }

            }
        }

        return true;
    }

    private void OnGouMai()
    {
 
        if(_CheckCanBuy())
            OnQingQiu(null);
        //GED.ED.removeListener(EventID.OnDrawCardEndAnimition, OnQingQiu);
        //GED.ED.addListenerOnce(EventID.OnDrawCardEndAnimition, OnQingQiu);
        //GED.ED.dispatchEvent(EventID.OnDrawCard);
    }
    private void OnQingQiu(GameEvent evt)
    {
        int quanNumber = 0;
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(12020);
        if (globalBean == null)
        {
            Logger.err("MoshiMianBan:OnZuanShiDanChou:未能从全局表获得钻石抽奖券id数据");
        }
        else
        {
            quanNumber = BagService.Singleton.GetItemNum(globalBean.t_int_param);
        }
        if (quanNumber > 0)
            DrawCardService.Singleton.OnReqDraw(type, number, mianfei, true);
        else
            DrawCardService.Singleton.OnReqDraw(type, number, mianfei, false);
    }
}
