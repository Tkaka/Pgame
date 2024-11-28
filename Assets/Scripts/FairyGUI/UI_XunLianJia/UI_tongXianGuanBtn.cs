/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_XunLianJia
{
	public partial class UI_tongXianGuanBtn : GButton
	{
		public GTextField m_lockLabel;
		public GGroup m_lockGroup;

		public const string URL = "ui://27xc27aks03s6";

		public static UI_tongXianGuanBtn CreateInstance()
		{
			return (UI_tongXianGuanBtn)UIPackage.CreateObject("UI_XunLianJia","tongXianGuanBtn");
		}

		public UI_tongXianGuanBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_lockLabel = (GTextField)this.GetChildAt(1);
			m_lockGroup = (GGroup)this.GetChildAt(3);
		}
	}
}