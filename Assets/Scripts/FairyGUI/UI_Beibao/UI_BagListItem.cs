/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Beibao
{
	public partial class UI_BagListItem : GButton
	{
		public GLoader m_borderLoader;
		public GLoader m_iconLoader;
		public GTextField m_numTxt;
		public GImage m_fragIcon;
		public GImage m_selectBorder;

		public const string URL = "ui://g5pgln3na9bf17";

		public static UI_BagListItem CreateInstance()
		{
			return (UI_BagListItem)UIPackage.CreateObject("UI_Beibao","BagListItem");
		}

		public UI_BagListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_borderLoader = (GLoader)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_numTxt = (GTextField)this.GetChildAt(2);
			m_fragIcon = (GImage)this.GetChildAt(3);
			m_selectBorder = (GImage)this.GetChildAt(4);
		}
	}
}