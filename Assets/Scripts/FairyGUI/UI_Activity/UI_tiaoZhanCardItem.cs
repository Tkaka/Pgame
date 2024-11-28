/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Activity
{
	public partial class UI_tiaoZhanCardItem : GComponent
	{
		public GLoader m_bgLoader;
		public GGraph m_toucher;
		public GLoader m_nameLoader;
		public GTextField m_desLabel;
		public GGroup m_activityGroup;
		public GList m_propList;
		public GImage m_lockGroup;
		public Transition m_anim;

		public const string URL = "ui://zwmeip9ukrhb4";

		public static UI_tiaoZhanCardItem CreateInstance()
		{
			return (UI_tiaoZhanCardItem)UIPackage.CreateObject("UI_Activity","tiaoZhanCardItem");
		}

		public UI_tiaoZhanCardItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bgLoader = (GLoader)this.GetChildAt(0);
			m_toucher = (GGraph)this.GetChildAt(1);
			m_nameLoader = (GLoader)this.GetChildAt(2);
			m_desLabel = (GTextField)this.GetChildAt(3);
			m_activityGroup = (GGroup)this.GetChildAt(6);
			m_propList = (GList)this.GetChildAt(7);
			m_lockGroup = (GImage)this.GetChildAt(8);
			m_anim = this.GetTransitionAt(0);
		}
	}
}