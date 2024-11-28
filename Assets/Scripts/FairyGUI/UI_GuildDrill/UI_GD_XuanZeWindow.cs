/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildDrill
{
	public partial class UI_GD_XuanZeWindow : GComponent
	{
		public GList m_petlist;
		public GButton m_CloseBtn;

		public const string URL = "ui://wwlsouxzkzeub";

		public static UI_GD_XuanZeWindow CreateInstance()
		{
			return (UI_GD_XuanZeWindow)UIPackage.CreateObject("UI_GuildDrill","GD_XuanZeWindow");
		}

		public UI_GD_XuanZeWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_petlist = (GList)this.GetChildAt(2);
			m_CloseBtn = (GButton)this.GetChildAt(3);
		}
	}
}