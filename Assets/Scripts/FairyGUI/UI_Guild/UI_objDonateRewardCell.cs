/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_objDonateRewardCell : GComponent
	{
		public GLoader m_imgBg;
		public GTextField m_txtExp;
		public GTextField m_txtGuildCoin;
		public UI_btnDonate m_btnDonate;
		public UI_btnDonateAll m_btnDonateAll;
		public UI_btnDonate m_btnDonate2;
		public GGroup m_btnGroup;
		public GTextField m_txtCoinNum;
		public GLoader m_imgCoin;

		public const string URL = "ui://oe7ras64lcbob44";

		public static UI_objDonateRewardCell CreateInstance()
		{
			return (UI_objDonateRewardCell)UIPackage.CreateObject("UI_Guild","objDonateRewardCell");
		}

		public UI_objDonateRewardCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgBg = (GLoader)this.GetChildAt(1);
			m_txtExp = (GTextField)this.GetChildAt(5);
			m_txtGuildCoin = (GTextField)this.GetChildAt(6);
			m_btnDonate = (UI_btnDonate)this.GetChildAt(7);
			m_btnDonateAll = (UI_btnDonateAll)this.GetChildAt(8);
			m_btnDonate2 = (UI_btnDonate)this.GetChildAt(9);
			m_btnGroup = (GGroup)this.GetChildAt(10);
			m_txtCoinNum = (GTextField)this.GetChildAt(12);
			m_imgCoin = (GLoader)this.GetChildAt(13);
		}
	}
}