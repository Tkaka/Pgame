/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_buyTimesGroup : GComponent
	{
		public GButton m_buyTimesBtn;
		public GTextField m_diamondNumLabel;

		public const string URL = "ui://1wdkrtiuw0hun";

		public static UI_buyTimesGroup CreateInstance()
		{
			return (UI_buyTimesGroup)UIPackage.CreateObject("UI_UltemateTrain","buyTimesGroup");
		}

		public UI_buyTimesGroup()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_buyTimesBtn = (GButton)this.GetChildAt(0);
			m_diamondNumLabel = (GTextField)this.GetChildAt(2);
		}
	}
}