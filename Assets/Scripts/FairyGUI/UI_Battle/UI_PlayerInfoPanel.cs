/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_PlayerInfoPanel : GComponent
	{
		public GImage m_hpBar;
		public GTextField m_name;
		public GTextField m_xianShouVal;

		public const string URL = "ui://028ppdzhgrm16e";

		public static UI_PlayerInfoPanel CreateInstance()
		{
			return (UI_PlayerInfoPanel)UIPackage.CreateObject("UI_Battle","PlayerInfoPanel");
		}

		public UI_PlayerInfoPanel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_hpBar = (GImage)this.GetChildAt(2);
			m_name = (GTextField)this.GetChildAt(6);
			m_xianShouVal = (GTextField)this.GetChildAt(7);
		}
	}
}