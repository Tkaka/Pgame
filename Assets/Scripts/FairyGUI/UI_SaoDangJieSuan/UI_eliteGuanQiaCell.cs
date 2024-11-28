/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_eliteGuanQiaCell : GComponent
	{
		public GLoader m_imgBoss;
		public GTextField m_txtBossName;
		public GButton m_btnSaoDang3;
		public UI_gropCheckBox m_gropSelect;
		public GButton m_btnRest;

		public const string URL = "ui://34cd5b4hjr2f1y";

		public static UI_eliteGuanQiaCell CreateInstance()
		{
			return (UI_eliteGuanQiaCell)UIPackage.CreateObject("UI_SaoDangJieSuan","eliteGuanQiaCell");
		}

		public UI_eliteGuanQiaCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgBoss = (GLoader)this.GetChildAt(1);
			m_txtBossName = (GTextField)this.GetChildAt(2);
			m_btnSaoDang3 = (GButton)this.GetChildAt(3);
			m_gropSelect = (UI_gropCheckBox)this.GetChildAt(4);
			m_btnRest = (GButton)this.GetChildAt(5);
		}
	}
}