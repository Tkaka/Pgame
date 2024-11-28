/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_PlayerCell : GComponent
	{
		public GImage m_bg;
		public GRichTextField m_txtRank;
		public GGraph m_modle;
		public GTextField m_txtName;
		public GTextField m_txtMoBaiOrRank;
		public GTextField m_txtCount;
		public GGraph m_imgClick;
		public GTextField m_txtFightPower;
		public GComponent m_btnMoBai;
		public GComponent m_btnSingleChallange;
		public UI_Challange50 m_challange50;
		public GGroup m_mySelf;

		public const string URL = "ui://3xs7lfyxo0de18";

		public static UI_PlayerCell CreateInstance()
		{
			return (UI_PlayerCell)UIPackage.CreateObject("UI_Arena","PlayerCell");
		}

		public UI_PlayerCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bg = (GImage)this.GetChildAt(0);
			m_txtRank = (GRichTextField)this.GetChildAt(1);
			m_modle = (GGraph)this.GetChildAt(2);
			m_txtName = (GTextField)this.GetChildAt(4);
			m_txtMoBaiOrRank = (GTextField)this.GetChildAt(5);
			m_txtCount = (GTextField)this.GetChildAt(6);
			m_imgClick = (GGraph)this.GetChildAt(8);
			m_txtFightPower = (GTextField)this.GetChildAt(9);
			m_btnMoBai = (GComponent)this.GetChildAt(10);
			m_btnSingleChallange = (GComponent)this.GetChildAt(11);
			m_challange50 = (UI_Challange50)this.GetChildAt(14);
			m_mySelf = (GGroup)this.GetChildAt(15);
		}
	}
}