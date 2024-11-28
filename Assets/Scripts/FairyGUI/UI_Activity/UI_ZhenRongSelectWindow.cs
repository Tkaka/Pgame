/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Activity
{
	public partial class UI_ZhenRongSelectWindow : GComponent
	{
		public GImage m_bgLoader;
		public GComponent m_commonTop;
		public GList m_propList;
		public GList m_huanXiangList;
		public GGroup m_huanXiangGroup;
		public GLoader m_activityTypeLoader;
		public GComponent m_buZhenColumn;
		public GTextField m_specialRuleLabel;
		public GGroup m_specialRuleGroup;

		public const string URL = "ui://zwmeip9uh3m9v";

		public static UI_ZhenRongSelectWindow CreateInstance()
		{
			return (UI_ZhenRongSelectWindow)UIPackage.CreateObject("UI_Activity","ZhenRongSelectWindow");
		}

		public UI_ZhenRongSelectWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bgLoader = (GImage)this.GetChildAt(0);
			m_commonTop = (GComponent)this.GetChildAt(2);
			m_propList = (GList)this.GetChildAt(5);
			m_huanXiangList = (GList)this.GetChildAt(7);
			m_huanXiangGroup = (GGroup)this.GetChildAt(8);
			m_activityTypeLoader = (GLoader)this.GetChildAt(9);
			m_buZhenColumn = (GComponent)this.GetChildAt(10);
			m_specialRuleLabel = (GTextField)this.GetChildAt(13);
			m_specialRuleGroup = (GGroup)this.GetChildAt(14);
		}
	}
}