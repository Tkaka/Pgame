/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_GuanKaCell : GComponent
	{
		public GLoader m_imgBoss;
		public GTextField m_txtGuanQiaName;
		public GList m_RewardList;
		public GButton m_btnSaoDang10;
		public GButton m_btnSaoDang50;

		public const string URL = "ui://34cd5b4hdqky1q";

		public static UI_GuanKaCell CreateInstance()
		{
			return (UI_GuanKaCell)UIPackage.CreateObject("UI_SaoDangJieSuan","GuanKaCell");
		}

		public UI_GuanKaCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgBoss = (GLoader)this.GetChildAt(1);
			m_txtGuanQiaName = (GTextField)this.GetChildAt(2);
			m_RewardList = (GList)this.GetChildAt(3);
			m_btnSaoDang10 = (GButton)this.GetChildAt(4);
			m_btnSaoDang50 = (GButton)this.GetChildAt(5);
		}
	}
}