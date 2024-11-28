/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_RankCell : GComponent
	{
		public GImage m_imgSelf;
		public GTextField m_txtRank;
		public GTextField m_txtName;
		public GTextField m_txtSheTuan;
		public GTextField m_txtLevel;
		public GTextField m_txtFightPower;
		public GLoader m_imgRank;

		public const string URL = "ui://3xs7lfyxgawd1j";

		public static UI_RankCell CreateInstance()
		{
			return (UI_RankCell)UIPackage.CreateObject("UI_Arena","RankCell");
		}

		public UI_RankCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgSelf = (GImage)this.GetChildAt(1);
			m_txtRank = (GTextField)this.GetChildAt(2);
			m_txtName = (GTextField)this.GetChildAt(3);
			m_txtSheTuan = (GTextField)this.GetChildAt(5);
			m_txtLevel = (GTextField)this.GetChildAt(6);
			m_txtFightPower = (GTextField)this.GetChildAt(7);
			m_imgRank = (GLoader)this.GetChildAt(8);
		}
	}
}