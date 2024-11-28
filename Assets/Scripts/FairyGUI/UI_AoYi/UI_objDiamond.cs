/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_objDiamond : GComponent
	{
		public GTextField m_txtWDiamondNum;
		public GLoader m_imgWDiamond;
		public GImage m_imgWDiamondRed;
		public GGroup m_objDiamond;

		public const string URL = "ui://vexa0xrynxtq1o";

		public static UI_objDiamond CreateInstance()
		{
			return (UI_objDiamond)UIPackage.CreateObject("UI_AoYi","objDiamond");
		}

		public UI_objDiamond()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtWDiamondNum = (GTextField)this.GetChildAt(9);
			m_imgWDiamond = (GLoader)this.GetChildAt(10);
			m_imgWDiamondRed = (GImage)this.GetChildAt(12);
			m_objDiamond = (GGroup)this.GetChildAt(13);
		}
	}
}