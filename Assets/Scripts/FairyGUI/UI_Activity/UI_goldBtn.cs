/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Activity
{
	public partial class UI_goldBtn : GButton
	{
		public GGroup m_activityGroup;
		public GImage m_openTipLoader;
		public GTextField m_openTipLabel;
		public GGroup m_unOpenGroup;
		public GGroup m_finishGroup;

		public const string URL = "ui://zwmeip9ukrhbh";

		public static UI_goldBtn CreateInstance()
		{
			return (UI_goldBtn)UIPackage.CreateObject("UI_Activity","goldBtn");
		}

		public UI_goldBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_activityGroup = (GGroup)this.GetChildAt(6);
			m_openTipLoader = (GImage)this.GetChildAt(7);
			m_openTipLabel = (GTextField)this.GetChildAt(8);
			m_unOpenGroup = (GGroup)this.GetChildAt(9);
			m_finishGroup = (GGroup)this.GetChildAt(12);
		}
	}
}