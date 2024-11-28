/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Activity
{
	public partial class UI_activityPropItem : GComponent
	{
		public GGraph m_toucher;
		public GLoader m_boardLoader;
		public GLoader m_iconLoader;

		public const string URL = "ui://zwmeip9uh3m9w";

		public static UI_activityPropItem CreateInstance()
		{
			return (UI_activityPropItem)UIPackage.CreateObject("UI_Activity","activityPropItem");
		}

		public UI_activityPropItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_toucher = (GGraph)this.GetChildAt(0);
			m_boardLoader = (GLoader)this.GetChildAt(1);
			m_iconLoader = (GLoader)this.GetChildAt(2);
		}
	}
}