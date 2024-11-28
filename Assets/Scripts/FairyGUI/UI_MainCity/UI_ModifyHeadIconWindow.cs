/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_ModifyHeadIconWindow : GComponent
	{
		public GGraph m_mask;
		public GButton m_closeBtn;
		public GList m_headIconList;

		public const string URL = "ui://jdfufi06kho74u";

		public static UI_ModifyHeadIconWindow CreateInstance()
		{
			return (UI_ModifyHeadIconWindow)UIPackage.CreateObject("UI_MainCity","ModifyHeadIconWindow");
		}

		public UI_ModifyHeadIconWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_closeBtn = (GButton)this.GetChildAt(2);
			m_headIconList = (GList)this.GetChildAt(3);
		}
	}
}