/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_DiamondBuyMaterialWindow : GComponent
	{
		public GGraph m_mask;
		public GRichTextField m_tipLabel;
		public GButton m_confirmBtn;

		public const string URL = "ui://8u3gv94nmbxb16";

		public static UI_DiamondBuyMaterialWindow CreateInstance()
		{
			return (UI_DiamondBuyMaterialWindow)UIPackage.CreateObject("UI_Equip","DiamondBuyMaterialWindow");
		}

		public UI_DiamondBuyMaterialWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_tipLabel = (GRichTextField)this.GetChildAt(7);
			m_confirmBtn = (GButton)this.GetChildAt(8);
		}
	}
}