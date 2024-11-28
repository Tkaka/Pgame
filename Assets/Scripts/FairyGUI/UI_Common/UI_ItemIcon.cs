/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_ItemIcon : GButton
	{
		public GLoader m_borderLoader;
		public GLoader m_iconLoader;
		public GTextField m_numTxt;
		public GImage m_fragIcon;
		public GTextField m_nameLabel;
		public GGraph m_toucher;
		public GImage m_imgSelect;
		public GTextField m_junZhouQualityNum;
		public GGroup m_junZhouGroup;

		public const string URL = "ui://42sthz3tn1c0je";

		public static UI_ItemIcon CreateInstance()
		{
			return (UI_ItemIcon)UIPackage.CreateObject("UI_Common","ItemIcon");
		}

		public UI_ItemIcon()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_borderLoader = (GLoader)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_numTxt = (GTextField)this.GetChildAt(2);
			m_fragIcon = (GImage)this.GetChildAt(3);
			m_nameLabel = (GTextField)this.GetChildAt(4);
			m_toucher = (GGraph)this.GetChildAt(5);
			m_imgSelect = (GImage)this.GetChildAt(6);
			m_junZhouQualityNum = (GTextField)this.GetChildAt(8);
			m_junZhouGroup = (GGroup)this.GetChildAt(9);
		}
	}
}