/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_changeAyCell : GComponent
	{
		public GGroup m_objBg;
		public UI_AoyiCommonItem m_itemIcon;
		public GTextField m_txtUnLockDes;
		public GTextField m_txtUse;
		public GComponent m_toucher;

		public const string URL = "ui://vexa0xryh9lhh";

		public static UI_changeAyCell CreateInstance()
		{
			return (UI_changeAyCell)UIPackage.CreateObject("UI_AoYi","changeAyCell");
		}

		public UI_changeAyCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_objBg = (GGroup)this.GetChildAt(1);
			m_itemIcon = (UI_AoyiCommonItem)this.GetChildAt(2);
			m_txtUnLockDes = (GTextField)this.GetChildAt(3);
			m_txtUse = (GTextField)this.GetChildAt(4);
			m_toucher = (GComponent)this.GetChildAt(5);
		}
	}
}