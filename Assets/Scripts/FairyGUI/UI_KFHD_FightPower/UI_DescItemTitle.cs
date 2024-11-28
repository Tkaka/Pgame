/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_KFHD_FightPower
{
	public partial class UI_DescItemTitle : GComponent
	{
		public GTextField m_title;

		public const string URL = "ui://9kjh5gh09gdui";

		public static UI_DescItemTitle CreateInstance()
		{
			return (UI_DescItemTitle)UIPackage.CreateObject("UI_KFHD_FightPower","DescItemTitle");
		}

		public UI_DescItemTitle()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_title = (GTextField)this.GetChildAt(1);
		}
	}
}