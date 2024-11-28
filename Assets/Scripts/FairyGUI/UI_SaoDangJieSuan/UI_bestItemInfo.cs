/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_bestItemInfo : GComponent
	{
		public GImage m_bestItem;
		public GTextField m_txtGetName;
		public GTextField m_txtGetNum;
		public GButton m_itemIcon;

		public const string URL = "ui://34cd5b4hvf1g2j";

		public static UI_bestItemInfo CreateInstance()
		{
			return (UI_bestItemInfo)UIPackage.CreateObject("UI_SaoDangJieSuan","bestItemInfo");
		}

		public UI_bestItemInfo()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bestItem = (GImage)this.GetChildAt(0);
			m_txtGetName = (GTextField)this.GetChildAt(1);
			m_txtGetNum = (GTextField)this.GetChildAt(3);
			m_itemIcon = (GButton)this.GetChildAt(5);
		}
	}
}