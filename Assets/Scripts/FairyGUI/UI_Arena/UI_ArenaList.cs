/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_ArenaList : GComponent
	{
		public GList m_mainList;

		public const string URL = "ui://3xs7lfyxh53e2j";

		public static UI_ArenaList CreateInstance()
		{
			return (UI_ArenaList)UIPackage.CreateObject("UI_Arena","ArenaList");
		}

		public UI_ArenaList()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mainList = (GList)this.GetChildAt(0);
		}
	}
}