/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_AoyiDrawWnd : GComponent
	{
		public GComponent m_commonTop;
		public GTextField m_;
		public UI_objCoin m_objCoin;
		public UI_objDiamond m_objDiamond;
		public UI_objCoinDraw m_objCoinDraw;
		public UI_objDiamondDraw m_objDiamondDraw;

		public const string URL = "ui://vexa0xrynxtq1k";

		public static UI_AoyiDrawWnd CreateInstance()
		{
			return (UI_AoyiDrawWnd)UIPackage.CreateObject("UI_AoYi","AoyiDrawWnd");
		}

		public UI_AoyiDrawWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_commonTop = (GComponent)this.GetChildAt(1);
			m_ = (GTextField)this.GetChildAt(2);
			m_objCoin = (UI_objCoin)this.GetChildAt(3);
			m_objDiamond = (UI_objDiamond)this.GetChildAt(4);
			m_objCoinDraw = (UI_objCoinDraw)this.GetChildAt(5);
			m_objDiamondDraw = (UI_objDiamondDraw)this.GetChildAt(6);
		}
	}
}