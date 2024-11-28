/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_objCoin : GComponent
	{
		public GTextField m_txtWCoin;
		public GImage m_imgWCoinRed;
		public GGroup m_objCoin;

		public const string URL = "ui://vexa0xrynxtq1n";

		public static UI_objCoin CreateInstance()
		{
			return (UI_objCoin)UIPackage.CreateObject("UI_AoYi","objCoin");
		}

		public UI_objCoin()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtWCoin = (GTextField)this.GetChildAt(9);
			m_imgWCoinRed = (GImage)this.GetChildAt(11);
			m_objCoin = (GGroup)this.GetChildAt(12);
		}
	}
}