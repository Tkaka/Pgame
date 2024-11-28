/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_ShenQi
{
	public partial class UI_IntroduceWindow : GComponent
	{
		public GGraph m_mask;
		public GList m_contentList;
		public GButton m_closeBtn;

		public const string URL = "ui://bi2nkn43fd9im";

		public static UI_IntroduceWindow CreateInstance()
		{
			return (UI_IntroduceWindow)UIPackage.CreateObject("UI_ShenQi","IntroduceWindow");
		}

		public UI_IntroduceWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_contentList = (GList)this.GetChildAt(4);
			m_closeBtn = (GButton)this.GetChildAt(5);
		}
	}
}