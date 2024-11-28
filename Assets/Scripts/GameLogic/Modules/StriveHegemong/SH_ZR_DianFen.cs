using UI_StriveHegemong;
using Message.KingFight;
using UnityEngine;

public class SH_ZR_DianFen : UI_SH_ZR_DianFen
{
    private BaseInfo oneinfo;
    private BaseInfo twoinfo;
    private BaseInfo threeinfo;
    public int xiabiao;
    public new static SH_ZR_DianFen CreateInstance()
    {
        return (SH_ZR_DianFen)UI_SH_ZR_DianFen.CreateInstance();
    }
    public void Init(BaseInfo onePetInfo, BaseInfo twoPetInfo, BaseInfo threePetInfo, int i = 0)
    {
        oneinfo = onePetInfo;
        twoinfo = twoPetInfo;
        threeinfo = threePetInfo;
        FillData();
        FillStart();
        xiabiao = i;
    }

    private void FillData()
    {
        int onepre = 0;
        int twopre = 0;
        int threepre = 0;
        if (oneinfo != null)
        {
            m_One.m_dengji.text = oneinfo.petBaseInfo.level.ToString();
            UIGloader.SetUrl(m_One.m_pinjie,UIUtils.GetBorderByQuality(oneinfo.petBaseInfo.color));
            UIGloader.SetUrl(m_One.m_touxiang,UIUtils.GetPetStartIcon(oneinfo.petBaseInfo.id, oneinfo.petBaseInfo.star));
            m_One.m_xuanzhong.visible = false;
            //战力
            m_One_ZL.text = oneinfo.fightPower.ToString();
            onepre = oneinfo.precedeValue;
        }
        else
        {
            m_One.m_dengji.text = "";
            m_One.m_pinjie.visible = false;
            m_One.m_touxiang.visible = false;
            m_One.m_xuanzhong.visible = false;
            onepre = 0;
        }
        m_One.draggable = true;
        m_One.dragBounds = new Rect(90,110,990,480);

        if (twoinfo != null)
        {
            m_Tow.m_dengji.text = twoinfo.petBaseInfo.level.ToString();
            UIGloader.SetUrl(m_Tow.m_pinjie,UIUtils.GetBorderByQuality(twoinfo.petBaseInfo.color));
            UIGloader.SetUrl(m_Tow.m_touxiang,UIUtils.GetPetStartIcon(twoinfo.petBaseInfo.id, twoinfo.petBaseInfo.star));
            m_Tow.m_xuanzhong.visible = false;
            //战力
            m_Two_ZL.text = twoinfo.fightPower.ToString();
            twopre = twoinfo.precedeValue;
        }
        else
        {
            m_Tow.m_dengji.text = "";
            m_Tow.m_pinjie.visible = false;
            m_Tow.m_touxiang.visible = false;
            m_Tow.m_xuanzhong.visible = false;
            twopre = 0;
        }
        m_Tow.draggable = true;
        m_Tow.dragBounds = new Rect(90, 110, 990, 480);

        if (threeinfo != null)
        {
            m_Three.m_dengji.text = threeinfo.petBaseInfo.level.ToString();
            UIGloader.SetUrl(m_Three.m_pinjie,UIUtils.GetBorderByQuality(threeinfo.petBaseInfo.color));
            UIGloader.SetUrl(m_Three.m_touxiang,UIUtils.GetPetStartIcon(threeinfo.petBaseInfo.id, threeinfo.petBaseInfo.star));
            m_Three.m_xuanzhong.visible = false;
            //战力
            m_Three_Zl.text = threeinfo.fightPower.ToString();
            threepre = threeinfo.precedeValue;
        }
        else
        {
            m_Three.m_dengji.text = "";
            m_Three.m_pinjie.visible = false;
            m_Three.m_touxiang.visible = false;
            m_Three.m_xuanzhong.visible = false;
            threepre = 0;
        }
        m_Three.draggable = true;
        m_Three.dragBounds = new Rect(90, 110, 990, 480);

        m_XianShouZhi.text = (onepre + twopre + threepre) + "";
        
    }
    private void FillStart()
    {
        if (oneinfo != null)
        {
            StarList onestart = new StarList((UI_Common.UI_StarList)m_One.m_xingji);
            onestart.SetStar(oneinfo.petBaseInfo.star);
        }
        else
        {
            m_One.m_xingji.visible = false;
        }
        if (twoinfo != null)
        {
            StarList twostart = new StarList((UI_Common.UI_StarList)m_Tow.m_xingji);
            twostart.SetStar(twoinfo.petBaseInfo.star);
        }
        else
        { m_Tow.m_xingji.visible = false; }
        if (threeinfo != null)
        {
            StarList threestart = new StarList((UI_Common.UI_StarList)m_Three.m_xingji);
            threestart.SetStar(threeinfo.petBaseInfo.star);
        }
        else
        { m_Three.m_xingji.visible = false; }
    }
    public override void Dispose()
    {
        oneinfo = null;
        twoinfo = null;
        threeinfo = null;
        base.Dispose();
    }
}
