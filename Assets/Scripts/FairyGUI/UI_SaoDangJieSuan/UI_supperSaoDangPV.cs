/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_supperSaoDangPV : GComponent
	{
		public GButton m_btnClose;
		public GList m_mainList;

		public const string URL = "ui://34cd5b4hlxvz2o";

		public static UI_supperSaoDangPV CreateInstance()
		{
			return (UI_supperSaoDangPV)UIPackage.CreateObject("UI_SaoDangJieSuan","supperSaoDangPV");
		}

		public UI_supperSaoDangPV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnClose = (GButton)this.GetChildAt(2);
			m_mainList = (GList)this.GetChildAt(3);
		}
	}
}