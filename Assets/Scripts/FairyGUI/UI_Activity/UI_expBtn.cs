/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Activity
{
	public partial class UI_expBtn : GButton
	{
		public GImage m_openTipLoader;
		public GTextField m_openTipLabel;
		public GGroup m_unOpenGroup;
		public GGroup m_finishGroup;
		public GGroup m_activityGroup;

		public const string URL = "ui://zwmeip9unbbx17";

		public static UI_expBtn CreateInstance()
		{
			return (UI_expBtn)UIPackage.CreateObject("UI_Activity","expBtn");
		}

		public UI_expBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_openTipLoader = (GImage)this.GetChildAt(4);
			m_openTipLabel = (GTextField)this.GetChildAt(5);
			m_unOpenGroup = (GGroup)this.GetChildAt(6);
			m_finishGroup = (GGroup)this.GetChildAt(9);
			m_activityGroup = (GGroup)this.GetChildAt(12);
		}
	}
}