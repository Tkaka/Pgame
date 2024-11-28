/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnReward : GComponent
	{
		public GImage m_imgRed;

		public const string URL = "ui://oe7ras64105rb3f";

		public static UI_btnReward CreateInstance()
		{
			return (UI_btnReward)UIPackage.CreateObject("UI_Guild","btnReward");
		}

		public UI_btnReward()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgRed = (GImage)this.GetChildAt(2);
		}
	}
}