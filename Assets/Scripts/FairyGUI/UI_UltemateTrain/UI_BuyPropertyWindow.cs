/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_BuyPropertyWindow : GComponent
	{
		public GGraph m_mask;
		public GList m_propertyList;
		public GTextField m_remainStarNumLabel;
		public GButton m_backBtn;

		public const string URL = "ui://1wdkrtiuw0hu13";

		public static UI_BuyPropertyWindow CreateInstance()
		{
			return (UI_BuyPropertyWindow)UIPackage.CreateObject("UI_UltemateTrain","BuyPropertyWindow");
		}

		public UI_BuyPropertyWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_propertyList = (GList)this.GetChildAt(8);
			m_remainStarNumLabel = (GTextField)this.GetChildAt(12);
			m_backBtn = (GButton)this.GetChildAt(13);
		}
	}
}