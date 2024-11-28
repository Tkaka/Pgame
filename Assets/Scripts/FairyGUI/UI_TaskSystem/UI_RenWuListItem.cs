/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TaskSystem
{
	public partial class UI_RenWuListItem : GButton
	{
		public GGraph m_WanCheng;
		public GLoader m_BeiJing;
		public GLoader m_RenWuBeiJing;
		public GLoader m_RenWuIcon;
		public GTextField m_Name;
		public GTextField m_Content;
		public UI_QianWangBtn m_QianWang;
		public GTextField m_AccomplishNumber;
		public GTextField m_Time;
		public GGroup m_DaoJiShi;
		public UI_YiWanChengBtn m_YiWanCheng;
		public GRichTextField m_OneAward;
		public GRichTextField m_TwoAward;
		public GGroup m_XingYunJiaoBiao;
		public GGroup m_VipJiaoBiao;
		public GTextField m_WanChenIcon;

		public const string URL = "ui://zswzat1en9yq4";

		public static UI_RenWuListItem CreateInstance()
		{
			return (UI_RenWuListItem)UIPackage.CreateObject("UI_TaskSystem","RenWuListItem");
		}

		public UI_RenWuListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_WanCheng = (GGraph)this.GetChildAt(1);
			m_BeiJing = (GLoader)this.GetChildAt(2);
			m_RenWuBeiJing = (GLoader)this.GetChildAt(3);
			m_RenWuIcon = (GLoader)this.GetChildAt(4);
			m_Name = (GTextField)this.GetChildAt(5);
			m_Content = (GTextField)this.GetChildAt(6);
			m_QianWang = (UI_QianWangBtn)this.GetChildAt(8);
			m_AccomplishNumber = (GTextField)this.GetChildAt(9);
			m_Time = (GTextField)this.GetChildAt(10);
			m_DaoJiShi = (GGroup)this.GetChildAt(12);
			m_YiWanCheng = (UI_YiWanChengBtn)this.GetChildAt(13);
			m_OneAward = (GRichTextField)this.GetChildAt(14);
			m_TwoAward = (GRichTextField)this.GetChildAt(15);
			m_XingYunJiaoBiao = (GGroup)this.GetChildAt(18);
			m_VipJiaoBiao = (GGroup)this.GetChildAt(21);
			m_WanChenIcon = (GTextField)this.GetChildAt(22);
		}
	}
}