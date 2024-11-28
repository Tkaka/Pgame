/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_shengPingNormalBtn : GButton
	{
		public GImage m_redPoint;

		public const string URL = "ui://qnd9tp35kiq139";

		public static UI_shengPingNormalBtn CreateInstance()
		{
			return (UI_shengPingNormalBtn)UIPackage.CreateObject("UI_Strength","shengPingNormalBtn");
		}

		public UI_shengPingNormalBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_redPoint = (GImage)this.GetChildAt(2);
		}
	}
}