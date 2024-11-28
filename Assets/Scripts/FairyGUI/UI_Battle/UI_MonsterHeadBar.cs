/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_MonsterHeadBar : GComponent
	{
		public GImage m_hpBar;
		public GImage m_hpBarGreen;

		public const string URL = "ui://028ppdzhq2pd1";

		public static UI_MonsterHeadBar CreateInstance()
		{
			return (UI_MonsterHeadBar)UIPackage.CreateObject("UI_Battle","MonsterHeadBar");
		}

		public UI_MonsterHeadBar()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_hpBar = (GImage)this.GetChildAt(1);
			m_hpBarGreen = (GImage)this.GetChildAt(2);
		}
	}
}