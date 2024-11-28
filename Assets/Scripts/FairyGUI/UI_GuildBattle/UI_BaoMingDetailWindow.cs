/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBattle
{
	public partial class UI_BaoMingDetailWindow : GComponent
	{
		public GList m_detailList;

		public const string URL = "ui://xj95784rpfl42p";

		public static UI_BaoMingDetailWindow CreateInstance()
		{
			return (UI_BaoMingDetailWindow)UIPackage.CreateObject("UI_GuildBattle","BaoMingDetailWindow");
		}

		public UI_BaoMingDetailWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_detailList = (GList)this.GetChildAt(5);
		}
	}
}