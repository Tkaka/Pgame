/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildDrill
{
	public partial class UI_GD_SuiJiYiCiBtn : GButton
	{
		public GImage m_suijiyiciBtn;

		public const string URL = "ui://wwlsouxzk46r5";

		public static UI_GD_SuiJiYiCiBtn CreateInstance()
		{
			return (UI_GD_SuiJiYiCiBtn)UIPackage.CreateObject("UI_GuildDrill","GD_SuiJiYiCiBtn");
		}

		public UI_GD_SuiJiYiCiBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_suijiyiciBtn = (GImage)this.GetChildAt(0);
		}
	}
}