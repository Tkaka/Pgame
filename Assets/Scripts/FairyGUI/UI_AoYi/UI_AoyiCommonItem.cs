/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_AoyiCommonItem : GComponent
	{
		public GLoader m_imgBorder;
		public GTextField m_txtType;
		public GLoader m_imgIcon;
		public GTextField m_txtLevel;
		public GImage m_imgSelect;
		public GGroup m_objAdd;

		public const string URL = "ui://vexa0xrydys1z";

		public static UI_AoyiCommonItem CreateInstance()
		{
			return (UI_AoyiCommonItem)UIPackage.CreateObject("UI_AoYi","AoyiCommonItem");
		}

		public UI_AoyiCommonItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgBorder = (GLoader)this.GetChildAt(0);
			m_txtType = (GTextField)this.GetChildAt(1);
			m_imgIcon = (GLoader)this.GetChildAt(2);
			m_txtLevel = (GTextField)this.GetChildAt(3);
			m_imgSelect = (GImage)this.GetChildAt(4);
			m_objAdd = (GGroup)this.GetChildAt(5);
		}
	}
}