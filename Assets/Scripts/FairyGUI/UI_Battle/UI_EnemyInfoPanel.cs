/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_EnemyInfoPanel : GComponent
	{
		public GImage m_hpBar;
		public GTextField m_name;
		public GTextField m_xianShouVal;

		public const string URL = "ui://028ppdzhgrm16f";

		public static UI_EnemyInfoPanel CreateInstance()
		{
			return (UI_EnemyInfoPanel)UIPackage.CreateObject("UI_Battle","EnemyInfoPanel");
		}

		public UI_EnemyInfoPanel()
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