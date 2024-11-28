/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Achievement
{
	public partial class UI_AM_List_Item : GComponent
	{
		public GLoader m_AM_Icon;
		public GList m_AM_Start_List;
		public GTextField m_AM_Name;
		public GTextField m_AM_MiaoShu;
		public GGraph m_AM_prize_icon;
		public GTextField m_AM_XianShouZhi;
		public GGroup m_AM_WeiWnaCheng;
		public GTextField m_bukelingqu;
		public GButton m_kelingquBtn;
		public UI_AM_ChengJiuJinDu m_jianglijindu;
		public GGroup m_AM_yiwancheng;

		public const string URL = "ui://xpd8f6j0entef";

		public static UI_AM_List_Item CreateInstance()
		{
			return (UI_AM_List_Item)UIPackage.CreateObject("UI_Achievement","AM_List_Item");
		}

		public UI_AM_List_Item()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_AM_Icon = (GLoader)this.GetChildAt(1);
			m_AM_Start_List = (GList)this.GetChildAt(2);
			m_AM_Name = (GTextField)this.GetChildAt(3);
			m_AM_MiaoShu = (GTextField)this.GetChildAt(4);
			m_AM_prize_icon = (GGraph)this.GetChildAt(5);
			m_AM_XianShouZhi = (GTextField)this.GetChildAt(6);
			m_AM_WeiWnaCheng = (GGroup)this.GetChildAt(7);
			m_bukelingqu = (GTextField)this.GetChildAt(8);
			m_kelingquBtn = (GButton)this.GetChildAt(9);
			m_jianglijindu = (UI_AM_ChengJiuJinDu)this.GetChildAt(10);
			m_AM_yiwancheng = (GGroup)this.GetChildAt(13);
		}
	}
}