/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_XunLianJia
{
	public partial class UI_ChengJiuBtn : GButton
	{
		public GTextField m_lockLabel;
		public GGroup m_lockGroup;

		public const string URL = "ui://27xc27ake2gre";

		public static UI_ChengJiuBtn CreateInstance()
		{
			return (UI_ChengJiuBtn)UIPackage.CreateObject("UI_XunLianJia","ChengJiuBtn");
		}

		public UI_ChengJiuBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_lockLabel = (GTextField)this.GetChildAt(0);
			m_lockGroup = (GGroup)this.GetChildAt(2);
		}
	}
}