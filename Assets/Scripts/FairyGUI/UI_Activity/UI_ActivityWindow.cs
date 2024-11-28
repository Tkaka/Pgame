/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Activity
{
	public partial class UI_ActivityWindow : GComponent
	{
		public UI_activityPanel m_activityPanel;
		public UI_activityBtnGroup m_activityGroup;
		public GComponent m_commonTop;

		public const string URL = "ui://zwmeip9ukrhb6";

		public static UI_ActivityWindow CreateInstance()
		{
			return (UI_ActivityWindow)UIPackage.CreateObject("UI_Activity","ActivityWindow");
		}

		public UI_ActivityWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_activityPanel = (UI_activityPanel)this.GetChildAt(4);
			m_activityGroup = (UI_activityBtnGroup)this.GetChildAt(6);
			m_commonTop = (GComponent)this.GetChildAt(8);
		}
	}
}