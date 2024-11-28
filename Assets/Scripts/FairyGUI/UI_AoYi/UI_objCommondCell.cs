/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_objCommondCell : GComponent
	{
		public GList m_gridList;
		public GTextField m_txtAoyiName;
		public GButton m_btnOneKeyPlace;
		public GTextField m_objXiangQian;

		public const string URL = "ui://vexa0xrypjwot";

		public static UI_objCommondCell CreateInstance()
		{
			return (UI_objCommondCell)UIPackage.CreateObject("UI_AoYi","objCommondCell");
		}

		public UI_objCommondCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_gridList = (GList)this.GetChildAt(1);
			m_txtAoyiName = (GTextField)this.GetChildAt(2);
			m_btnOneKeyPlace = (GButton)this.GetChildAt(4);
			m_objXiangQian = (GTextField)this.GetChildAt(5);
		}
	}
}