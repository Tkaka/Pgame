using FairyGUI;
using Message.KingFight;
using Message.Pet;
using System.Collections.Generic;
using UI_StriveHegemong;
using UnityEngine;

public class SH_DF_BuZhen_postion
{
    public int index;
    public Rect rect;
}

public class SH_DF_BuZhenWindow : BaseWindow
{
   
    private UI_SH_DF_BuZhenWindow window;
    private List<BaseInfo> cansaizhenrong;
    private int index;
    private int pengzhuang;//碰撞中的下标
    private Vector2 oldpostion = Vector2.zero;
    private Vector2 newpostion = Vector2.zero;
    private UI_SH_BM_ListItem old;//移动过的目标的索引
    private SH_BM_ListItem olditem;//正在移动中的item
    private SH_BM_ListItem newitem;//碰撞到的下标索引
    private bool isDrag = false;//是否在拖动中
    private Dictionary<int, SH_DF_BuZhen_postion> buzhen = new Dictionary<int, SH_DF_BuZhen_postion>();
    private List<int> shangzhen;
    private List<int> newshangzhen = new List<int>();
    public override void OnOpen()
    {
       
        window = getUiWindow<UI_SH_DF_BuZhenWindow>();
        OnTianChong();
        AddKeyEvent();
        cansaizhenrong = new List<BaseInfo>();
        shangzhen = StriveHegemongService.Singleton.shangzhenList;
        for (int i = 0; i < shangzhen.Count; ++i)
        {
            newshangzhen.Add(shangzhen[i]);
        }
        InitView();
    }
    private void AddKeyEvent()
    {
        window.m_BaoCunBtn.onClick.Add(OnBaoCunBtn);
        window.m_TuiChuBtn.onClick.Add(OnCloseBtn);
    }
    public override void InitView()
    {
        //我的参赛阵容
        cansaizhenrong.Clear();
        BaseInfo info;
        for (int i = 0; i < 10; ++i)
        {
            info = new BaseInfo();
            PetInfo petinfo;
            if (i < newshangzhen.Count)
            {
                petinfo = PetService.Singleton.GetPetInfo(newshangzhen[i]);
            }
            else
                petinfo = null;

            if (petinfo == null)
            {
                info = null;
            }
            else
            {
                info.petBaseInfo.id = petinfo.petId;
                info.petBaseInfo.color = petinfo.basInfo.color;
                info.petBaseInfo.star = petinfo.basInfo.star;
                info.precedeValue = petinfo.priority;
                info.fightPower = petinfo.fightInfo.fightPower;
                info.index = i;
            }
            cansaizhenrong.Add(info);
        }

        SH_ZR_DianFen dianFen;
        for (int i = 0; i < 3; ++i)
        {
            if (i < 3)
            {
                if (i == 2)
                {
                    UI_SH_tianchong tianchong = UI_SH_tianchong.CreateInstance();
                    window.m_BuZhenList.AddChild(tianchong);
                }
                dianFen = SH_ZR_DianFen.CreateInstance();
                dianFen.Init(cansaizhenrong[3 * i + 0], cansaizhenrong[3 * i + 1], cansaizhenrong[3 * i + 2],i);
                dianFen.m_One.onDragStart.Add(OnDargStart);
                //dianFen.m_One.onDragMove.Add(OnDragMove);
                dianFen.m_One.onDragEnd.Add(OnDragEnd);
                dianFen.m_One.onRollOut.Add(() => { OnRollOut(dianFen.m_Three); });
                dianFen.m_One.onRollOver.Add(() => { OnRollOver(dianFen.m_Three); });
                //dianFen.m_One.onRollOut.Add(OnRollOut);
                //dianFen.m_One.onRollOver.Add(OnRollOver);

                dianFen.m_Tow.onDragStart.Add(OnDargStart);
               // dianFen.m_Tow.onDragMove.Add(OnDragMove);
                dianFen.m_Tow.onDragEnd.Add(OnDragEnd);
                dianFen.m_Tow.onRollOut.Add(() => { OnRollOut(dianFen.m_Three); });
                dianFen.m_Tow.onRollOver.Add(() => { OnRollOver(dianFen.m_Three); });
                //dianFen.m_Tow.onRollOut.Add(OnRollOut);
                //dianFen.m_Tow.onRollOver.Add(OnRollOver);

                dianFen.m_Three.onDragStart.Add(OnDargStart);
                //dianFen.m_Three.onDragMove.Add(OnDragMove);
                dianFen.m_Three.onDragEnd.Add(OnDragEnd);
                dianFen.m_Three.onRollOut.Add(() => { OnRollOut(dianFen.m_Three); });
                dianFen.m_Three.onRollOver.Add(() => { OnRollOver(dianFen.m_Three); });
                //dianFen.m_Three.onRollOut.Add(OnRollOut);
                //dianFen.m_Three.onRollOver.Add(OnRollOver);

                window.m_BuZhenList.AddChild(dianFen);

                //闭包添加事件
              
            }
        }
        UI_SH_ZR_SaiCheng tibu = UI_SH_ZR_SaiCheng.CreateInstance();
        tibu.m_Changci.text = "替补";
        tibu.m_juese.draggable = true;
        tibu.m_juese.dragBounds = new Rect(90, 110, 990, 480);
        tibu.m_juese.onDragStart.Add(OnDargStart);
        //tibu.m_juese.onDragMove.Add(OnDragMove);
        tibu.m_juese.onDragEnd.Add(OnDragEnd);
        //tibu.m_juese.onRollOut.Add(OnRollOut);
        //tibu.m_juese.onRollOver.Add(OnRollOver);
        tibu.m_juese.onRollOut.Add(()=> { OnRollOut(tibu.m_juese); });
        tibu.m_juese.onRollOver.Add(()=> { OnRollOver(tibu.m_juese); });

        if (cansaizhenrong[9] != null)
        {
            tibu.m_juese.m_dengji.text = cansaizhenrong[9].petBaseInfo.level.ToString();
            UIGloader.SetUrl(tibu.m_juese.m_pinjie, UIUtils.GetBorderByQuality(cansaizhenrong[9].petBaseInfo.color));
            UIGloader.SetUrl(tibu.m_juese.m_touxiang, UIUtils.GetPetStartIcon(cansaizhenrong[9].petBaseInfo.id, cansaizhenrong[9].petBaseInfo.star));
            tibu.m_juese.m_xuanzhong.visible = false;
            tibu.m_xianshouzhi.text = cansaizhenrong[9].precedeValue.ToString();
            tibu.m_ZhanLiZhi.text = cansaizhenrong[9].fightPower.ToString();
            StarList starList = new StarList((UI_Common.UI_StarList)tibu.m_juese.m_xingji);
            starList.SetStar(cansaizhenrong[9].petBaseInfo.star);
        }
        else
        {
            tibu.m_juese.m_dengji.text = "";
            UIGloader.SetUrl(tibu.m_juese.m_pinjie,"");//替补品阶
            tibu.m_juese.m_xuanzhong.visible = false;
            tibu.m_juese.m_xingji.visible = false;
            tibu.m_xianshouzhi.text = "";
            tibu.m_ZhanLiZhi.text = "";
        }
        
        window.m_BuZhenList.AddChild(tibu);
    }
    public override void RefreshView()
    {
    }
    private void OnBaoCunBtn()
    {
        //发送上阵数组
        StriveHegemongService.Singleton.OnReqSetTeam(newshangzhen);
        OnCloseBtn();
    }
    private void OnTianChong()
    {
        SH_DF_BuZhen_postion one = new SH_DF_BuZhen_postion();
        one.index = 0;
        one.rect = new Rect(window.m_pos1.x, window.m_pos1.y,window.m_pos1.height,window.m_pos1.width);
        buzhen.Add(0,one);

        SH_DF_BuZhen_postion tow = new SH_DF_BuZhen_postion();
        tow.index = 1;
        tow.rect = new Rect(window.m_pos2.x, window.m_pos2.y, window.m_pos1.height, window.m_pos1.width);
        buzhen.Add(1, tow);

        SH_DF_BuZhen_postion three = new SH_DF_BuZhen_postion();
        three.index = 2;
        three.rect = new Rect(window.m_pos3.x, window.m_pos3.y, window.m_pos3.height, window.m_pos3.width);
        buzhen.Add(2, three);


        SH_DF_BuZhen_postion four = new SH_DF_BuZhen_postion();
        four.index = 3;
        four.rect = new Rect(window.m_pos4.x, window.m_pos4.y, window.m_pos4.height, window.m_pos4.width);
        buzhen.Add(3, four);

        SH_DF_BuZhen_postion five = new SH_DF_BuZhen_postion();
        five.index = 4;
        five.rect = new Rect(window.m_pos5.x, window.m_pos5.y, window.m_pos5.height, window.m_pos5.width);
        buzhen.Add(4, five);

        SH_DF_BuZhen_postion six = new SH_DF_BuZhen_postion();
        six.index = 5;
        six.rect = new Rect(window.m_pos6.x, window.m_pos6.y, window.m_pos6.height, window.m_pos6.width);
        buzhen.Add(5, six);

        SH_DF_BuZhen_postion seven = new SH_DF_BuZhen_postion();
        seven.index = 6;
        seven.rect = new Rect(window.m_pos7.x , window.m_pos7.y, window.m_pos7.height, window.m_pos7.width);
        buzhen.Add(6, seven);

        SH_DF_BuZhen_postion eight = new SH_DF_BuZhen_postion();
        eight.index = 7;
        eight.rect = new Rect(window.m_pos8.x, window.m_pos8.y, window.m_pos8.height, window.m_pos8.width);
        buzhen.Add(7, eight);

        SH_DF_BuZhen_postion nine = new SH_DF_BuZhen_postion();
        nine.index = 8;
        nine.rect = new Rect(window.m_pos9.x, window.m_pos9.y, window.m_pos9.height, window.m_pos9.width);
        buzhen.Add(8, nine);

        SH_DF_BuZhen_postion ten = new SH_DF_BuZhen_postion();
        ten.index = 9;
        ten.rect = new Rect(window.m_pos10.x, window.m_pos10.y, window.m_pos10.height, window.m_pos10.width);
        buzhen.Add(9, ten);
    }
    //拖动开始
    private void OnDargStart(EventContext context)
    {
        //Vector2 pos1 = Stage.inst.touchPosition;
        //Vector2 localPos = window.m_BeiJing.GlobalToLocal(pos1);
        ////获得初始下标并计算
        ////通过鼠标坐标判断是在哪个矩形内，并获得其所在item的引用，进而获得item的下标
        //UI_SH_BM_ListItem zhenrongiten = context.initiator as UI_SH_BM_ListItem;
        //for (int i = 0; i < 10; ++i)
        //{
        //    if (UIUtils.isSpotToRect(localPos, buzhen[i].rect))
        //    {
        //        pengzhuang = buzhen[i].index;
        //        index = buzhen[i].index;
        //        oldpostion = zhenrongiten.position;
        //        break;
        //    }
        //}
        //Logger.log(localPos.x + "+" + buzhen[3].rect.x + "---" + localPos.y + "+" + buzhen[3].rect.y);
        SH_BM_ListItem item = context.initiator as SH_BM_ListItem;
        if (item != null)
        {
            olditem = item;
            oldpostion = item.position;
            Logger.err(olditem.position.y + "/" + olditem.position.y);
        }
        else
        {
            Logger.err("未拿到item");
        }
        isDrag = true;
    }

    //====================================================================
    //离开监听
    private void OnRollOut(GComponent com)
    {
        //SH_BM_ListItem listItem = com as SH_BM_ListItem;
        //if (listItem != null)
        //{
        //    newitem = listItem;
        //}
        //else
        //{
        //    Logger.err("未拿到item");
        //}
        if (isDrag)
        {
            //如果在拖动中
            if (newpostion != Vector2.zero)
            {
                OnRestoreIcon();
            }
        }
        else
        {
            //如果不在拖动中
        }
    }
    //进入监听
    private void OnRollOver(GComponent com)
    {
        SH_BM_ListItem listItem = com as SH_BM_ListItem;
        if (listItem != null)
        {
            if (isDrag)
            {
                if (newpostion == Vector2.zero)
                {
                    //未碰到过item  
                    newitem = listItem;
                    newpostion = newitem.position;
                    OnChangeIcon();
                }
                else
                {
                    OnRestoreIcon();
                    newitem = listItem;
                    newpostion = newitem.position;
                    OnChangeIcon();
                }
            }
            else
            {
                //如果不在拖动中
            }
          
        }
        else
        {
            Logger.err("未拿到item");
        }
       
       
    }
    //================================================================
    ////拖动进行
    ////index是移动的那个item所属下标
    //private void OnDragMove(EventContext context)
    //{
    //    int oldindex = pengzhuang;
    //    UI_SH_BM_ListItem zhenrongiten = context.initiator as UI_SH_BM_ListItem;
    //    if (zhenrongiten != null)
    //    {
    //        UI_SH_ZR_DianFen dianfeng = zhenrongiten.parent as UI_SH_ZR_DianFen;
    //        if (dianfeng != null)
    //        {
    //            Vector2 postion = zhenrongiten.position + dianfeng.position;
    //            postion.x = postion.x + window.m_BuZhenList.x;
    //            postion.y = postion.y + window.m_BuZhenList.y;
    //            //运行中时从碰撞拿到下标
    //            //下标再做替换
    //            Rect rect = new Rect();
    //            rect.x = postion.x;
    //            rect.y = postion.y;
    //            rect.width = zhenrongiten.width;
    //            rect.height = zhenrongiten.height;
    //            for (int i = 0; i < buzhen.Keys.Count; ++i)
    //            {
    //                if (UIUtils.isRectCrash(rect, buzhen[i].rect))
    //                {
    //                    //判断碰撞到的是否不是出发点矩形
    //                    if (buzhen[i].index != oldindex)
    //                    {
    //                        //是否碰撞过其他矩形
    //                        if (buzhen[i].index != index)
    //                        {
    //                            OnJiaoHuan(index, oldindex);
    //                            index = buzhen[i].index;
    //                            OnJiaoHuan(buzhen[i].index, oldindex);
    //                        }
    //                        else
    //                        {
    //                            index = buzhen[i].index;
    //                            OnJiaoHuan(buzhen[i].index, oldindex);
    //                        }
    //                    }
    //                    break;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            UI_SH_ZR_SaiCheng TiBu = zhenrongiten.parent as UI_SH_ZR_SaiCheng;
    //            if (TiBu != null)
    //            {
    //                Vector2 postion = zhenrongiten.position + TiBu.position;
    //                postion.x = postion.x + window.m_BuZhenList.x;
    //                postion.y = postion.y + window.m_BuZhenList.y;
    //                //运行中时从碰撞拿到下标
    //                //下标再做替换
    //                Rect rect = new Rect();
    //                rect.x = postion.x;
    //                rect.y = postion.y;
    //                rect.width = zhenrongiten.width;
    //                rect.height = zhenrongiten.height;
    //                for (int i = 0; i < buzhen.Keys.Count; ++i)
    //                {
    //                    if (UIUtils.isRectCrash(rect, buzhen[i].rect))
    //                    {
    //                        //判断碰撞到的是否是新的矩形
    //                        if (buzhen[i].index != oldindex)
    //                        {
    //                            //是否释放过，没有释放过
    //                            if (buzhen[i].index != index)
    //                            {
    //                                OnJiaoHuan(index, oldindex);
    //                                index = buzhen[i].index;
    //                                OnJiaoHuan(buzhen[i].index, oldindex);
    //                            }
    //                            else
    //                            {
    //                                index = buzhen[i].index;
    //                                OnJiaoHuan(buzhen[i].index, oldindex);
    //                            }
    //                        }
    //                        break;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    //Logger.log(zhenrongiten.position.x + "--"+ zhenrongiten.position.y);
        
    //}
    //拖动结束
    private void OnDragEnd()
    {
        //手动调用item里面的函数
        //根据下标调换新的上阵
        window.m_BuZhenList.RemoveChildren(0,-1,true);
        InitView();
        index = -1;
        if (newpostion != oldpostion)
        {
            OnChangeData();
        }
        newpostion = Vector2.zero;
        newitem = null;
    }
    
    /// <summary>
    /// 根据下标将对应的item的图片替换
    /// </summary>
    /// <param name="oldindex">正在移动额item所属下标</param>
    /// <param name="newindex">碰撞到的矩形所属下标</param>
    //private void OnJiaoHuan(int newindex, int oldindex)
    //{
    //    UI_SH_BM_ListItem olditem = null;//移动中的item的下标
    //    UI_SH_BM_ListItem newitem = null;//碰撞到的iten的下标
    //    UI_SH_ZR_DianFen newzhenrongitem;
    //    UI_SH_ZR_DianFen oldzhenrongitem;
    //    int[] xb = {0,1,3,4 };//显示列表中的下标数据
    //    int item = oldindex / 3;
    //    //碰撞处的索引
    //    newzhenrongitem = window.m_BuZhenList.GetChildAt(xb[item]) as UI_SH_ZR_DianFen;
    //    if (newzhenrongitem != null)
    //    {
    //        if (newindex % 3 == 0)
    //        {
    //            newitem = newzhenrongitem.m_One;
    //        }
    //        else if (newindex % 3 == 1)
    //        {
    //            newitem = newzhenrongitem.m_Tow;
    //        }
    //        else if (newindex % 3 == 2)
    //        {
    //            newitem = newzhenrongitem.m_Three;
    //        }
    //    }
    //    else
    //    {
    //        UI_SH_ZR_SaiCheng tibu = window.m_BuZhenList.GetChildAt(4) as UI_SH_ZR_SaiCheng;
    //        if (tibu != null)
    //        {
    //            newitem = tibu.m_juese;
    //        }
    //    }
    //    //鼠标按住的索引
    //    oldzhenrongitem = window.m_BuZhenList.GetChildAt(xb[newindex / 3]) as UI_SH_ZR_DianFen;
    //    if (oldzhenrongitem != null)
    //    {
    //        if (oldindex % 3 == 0)
    //        {
    //            olditem = oldzhenrongitem.m_One;
    //        }
    //        else if (oldindex % 3 == 1)
    //        {
    //            olditem = oldzhenrongitem.m_Tow;
    //        }
    //        else if (oldindex % 3 == 2)
    //        {
    //            olditem = oldzhenrongitem.m_Three;
    //        }
    //    }
    //    else
    //    {
    //        UI_SH_ZR_SaiCheng tibu = window.m_BuZhenList.GetChildAt(4) as UI_SH_ZR_SaiCheng;
    //        if (tibu != null)
    //        {
    //            olditem = tibu.m_juese;
    //        }
    //    }

    //    newitem.x = (oldzhenrongitem.x - newzhenrongitem.x) + oldpostion.x;
    //    newitem.y = (oldzhenrongitem.y - newzhenrongitem.y) + oldpostion.y;

    //    int one = newshangzhen[oldindex];
    //    newshangzhen[oldindex] = newshangzhen[newindex];
    //    newshangzhen[newindex] = one;
    //}
    //交换图标
    private void OnChangeIcon()
    {
        if (newitem != null)
        {
            newitem.x = (olditem.parent.position.x - newitem.parent.position.x) + oldpostion.x;
            newitem.y = (olditem.parent.position.y - newitem.parent.position.y) + oldpostion.y;
        }
    }
    //还原位置并置空
    private void OnRestoreIcon()
    {
        newitem.position = newpostion;
        newitem = null;
        newpostion = Vector2.zero;
    }
    //交换数据
    private void OnChangeData()
    {
        int indexone = 0;
        int indextwo = 0;
        if (newpostion != oldpostion)
        {
            for (int i = 0; i < newshangzhen.Count; ++i)
            {
                if (newitem.petId == newshangzhen[i])
                    indexone = i;
                if (olditem.petId == newshangzhen[i])
                    indextwo = i;
            }
            int one = newshangzhen[indexone];
            newshangzhen[indexone] = newshangzhen[indextwo];
            newshangzhen[indextwo] = one;
        }
    }
    protected override void OnCloseBtn()
    {
        window = null;
        cansaizhenrong = null;
        old = null; buzhen = null;
        shangzhen = null;
        newshangzhen = null;
        base.OnCloseBtn();
    }
}
