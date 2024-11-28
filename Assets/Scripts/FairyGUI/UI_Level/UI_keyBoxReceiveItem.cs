/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_keyBoxReceiveItem : GComponent
	{
		public GImage m_bg;
		public GTextField m_chapterName;
		public GList m_boxList;

		public const string URL = "ui://z04ymz0e97kmw";

		public static UI_keyBoxReceiveItem CreateInstance()
		{
			return (UI_keyBoxReceiveItem)UIPackage.CreateObject("UI_Level","keyBoxReceiveItem");
		}

		public UI_keyBoxReceiveItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bg = (GImage)this.GetChildAt(0);
			m_chapterName = (GTextField)this.GetChildAt(2);
			m_boxList = (GList)this.GetChildAt(3);
		}
	}
}