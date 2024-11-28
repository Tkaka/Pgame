/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_gropCheckBox : GComponent
	{
		public GButton m_btnSelect;
		public GLoader m_imgBoss;

		public const string URL = "ui://34cd5b4hjr2f20";

		public static UI_gropCheckBox CreateInstance()
		{
			return (UI_gropCheckBox)UIPackage.CreateObject("UI_SaoDangJieSuan","gropCheckBox");
		}

		public UI_gropCheckBox()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnSelect = (GButton)this.GetChildAt(0);
			m_imgBoss = (GLoader)this.GetChildAt(1);
		}
	}
}