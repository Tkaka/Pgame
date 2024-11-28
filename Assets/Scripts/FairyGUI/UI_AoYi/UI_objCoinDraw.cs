/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_objCoinDraw : GComponent
	{
		public GTextField m_txtCoinFree;
		public GTextField m_txtSingleCoinNum;
		public GTextField m_txtCoinRemainCount;
		public GTextField m_txtCoinTenNum;
		public GTextField m_txtCoinTenRemainNum;
		public GButton m_btnCoinBuyOnce;
		public GButton m_btnCoinTen;
		public GImage m_imgCoinSingleRed;
		public GGroup m_objCoinDraw;

		public const string URL = "ui://vexa0xrynxtq1p";

		public static UI_objCoinDraw CreateInstance()
		{
			return (UI_objCoinDraw)UIPackage.CreateObject("UI_AoYi","objCoinDraw");
		}

		public UI_objCoinDraw()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtCoinFree = (GTextField)this.GetChildAt(1);
			m_txtSingleCoinNum = (GTextField)this.GetChildAt(5);
			m_txtCoinRemainCount = (GTextField)this.GetChildAt(7);
			m_txtCoinTenNum = (GTextField)this.GetChildAt(9);
			m_txtCoinTenRemainNum = (GTextField)this.GetChildAt(11);
			m_btnCoinBuyOnce = (GButton)this.GetChildAt(12);
			m_btnCoinTen = (GButton)this.GetChildAt(13);
			m_imgCoinSingleRed = (GImage)this.GetChildAt(14);
			m_objCoinDraw = (GGroup)this.GetChildAt(15);
		}
	}
}