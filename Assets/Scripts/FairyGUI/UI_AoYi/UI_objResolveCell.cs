/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_objResolveCell : GComponent
	{
		public UI_AoyiCommonItem m_icon;
		public GTextField m_txtName;
		public GTextField m_txtProperty;
		public GButton m_checkBox;

		public const string URL = "ui://vexa0xrydys1y";

		public static UI_objResolveCell CreateInstance()
		{
			return (UI_objResolveCell)UIPackage.CreateObject("UI_AoYi","objResolveCell");
		}

		public UI_objResolveCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_icon = (UI_AoyiCommonItem)this.GetChildAt(1);
			m_txtName = (GTextField)this.GetChildAt(2);
			m_txtProperty = (GTextField)this.GetChildAt(3);
			m_checkBox = (GButton)this.GetChildAt(4);
		}
	}
}