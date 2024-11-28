/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GRE_RobBtn : GButton
	{
		public GTextField m_miaoshu;

		public const string URL = "ui://r816m4tmfzr61";

		public static UI_GRE_RobBtn CreateInstance()
		{
			return (UI_GRE_RobBtn)UIPackage.CreateObject("UI_GuillRedEnvelope","GRE_RobBtn");
		}

		public UI_GRE_RobBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_miaoshu = (GTextField)this.GetChildAt(2);
		}
	}
}