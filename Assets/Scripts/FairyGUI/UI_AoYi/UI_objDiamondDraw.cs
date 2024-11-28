/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_objDiamondDraw : GComponent
	{
		public GTextField m_txtDiamondFreeCount;
		public GTextField m_txtSingleDiamondRemain;
		public GTextField m_txtDiamondTenRemain;
		public GButton m_btnSingleDiamond;
		public GButton m_btnDiamondTen;
		public GImage m_imgDiamondRed;
		public GTextField m_txtSingleDiamondNum;
		public GLoader m_imgSingleDiamond;
		public GTextField m_txtDiamondTenNum;
		public GLoader m_imgDiamondTen;
		public GGroup m_objDiamondDraw;

		public const string URL = "ui://vexa0xrynxtq1q";

		public static UI_objDiamondDraw CreateInstance()
		{
			return (UI_objDiamondDraw)UIPackage.CreateObject("UI_AoYi","objDiamondDraw");
		}

		public UI_objDiamondDraw()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDiamondFreeCount = (GTextField)this.GetChildAt(1);
			m_txtSingleDiamondRemain = (GTextField)this.GetChildAt(4);
			m_txtDiamondTenRemain = (GTextField)this.GetChildAt(5);
			m_btnSingleDiamond = (GButton)this.GetChildAt(6);
			m_btnDiamondTen = (GButton)this.GetChildAt(7);
			m_imgDiamondRed = (GImage)this.GetChildAt(8);
			m_txtSingleDiamondNum = (GTextField)this.GetChildAt(10);
			m_imgSingleDiamond = (GLoader)this.GetChildAt(11);
			m_txtDiamondTenNum = (GTextField)this.GetChildAt(13);
			m_imgDiamondTen = (GLoader)this.GetChildAt(14);
			m_objDiamondDraw = (GGroup)this.GetChildAt(15);
		}
	}
}