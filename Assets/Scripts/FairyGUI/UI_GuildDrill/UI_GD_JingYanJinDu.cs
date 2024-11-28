/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildDrill
{
	public partial class UI_GD_JingYanJinDu : GProgressBar
	{
		public GImage m_man;
		public GTextField m_number;

		public const string URL = "ui://wwlsouxzk46r4";

		public static UI_GD_JingYanJinDu CreateInstance()
		{
			return (UI_GD_JingYanJinDu)UIPackage.CreateObject("UI_GuildDrill","GD_JingYanJinDu");
		}

		public UI_GD_JingYanJinDu()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_man = (GImage)this.GetChildAt(2);
			m_number = (GTextField)this.GetChildAt(3);
		}
	}
}