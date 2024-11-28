/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildDrill
{
	public partial class UI_GD_SuiJiQuanBuBtn : GButton
	{
		public GImage m_suijiquanbu;

		public const string URL = "ui://wwlsouxzk46r6";

		public static UI_GD_SuiJiQuanBuBtn CreateInstance()
		{
			return (UI_GD_SuiJiQuanBuBtn)UIPackage.CreateObject("UI_GuildDrill","GD_SuiJiQuanBuBtn");
		}

		public UI_GD_SuiJiQuanBuBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_suijiquanbu = (GImage)this.GetChildAt(0);
		}
	}
}