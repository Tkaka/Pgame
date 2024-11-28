/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Activity
{
	public partial class UI_activityPanel : GComponent
	{
		public GLoader m_bgLoader;
		public GLoader m_typeLoader;
		public GList m_propList;
		public GButton m_canJiaBtn;
		public GTextField m_remainTimesLabel;
		public GGroup m_remainTimesGroup;
		public GTextField m_unOpenTipLabel;
		public GButton m_ruleDetailBtn;
		public GTextField m_remianTimeLabel;
		public GGroup m_remainTimeGroup;
		public GGroup m_unOpenGroup;

		public const string URL = "ui://zwmeip9ukrhbl";

		public static UI_activityPanel CreateInstance()
		{
			return (UI_activityPanel)UIPackage.CreateObject("UI_Activity","activityPanel");
		}

		public UI_activityPanel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bgLoader = (GLoader)this.GetChildAt(0);
			m_typeLoader = (GLoader)this.GetChildAt(1);
			m_propList = (GList)this.GetChildAt(4);
			m_canJiaBtn = (GButton)this.GetChildAt(5);
			m_remainTimesLabel = (GTextField)this.GetChildAt(7);
			m_remainTimesGroup = (GGroup)this.GetChildAt(8);
			m_unOpenTipLabel = (GTextField)this.GetChildAt(9);
			m_ruleDetailBtn = (GButton)this.GetChildAt(10);
			m_remianTimeLabel = (GTextField)this.GetChildAt(11);
			m_remainTimeGroup = (GGroup)this.GetChildAt(13);
			m_unOpenGroup = (GGroup)this.GetChildAt(16);
		}
	}
}