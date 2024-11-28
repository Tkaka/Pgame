/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_addAyCell : GComponent
	{
		public GImage m_imgAdd;
		public GImage m_imgRed;
		public GTextField m_txtUnLockDes;
		public GGroup m_objAdd;
		public UI_AoyiCommonItem m_itemIcon;

		public const string URL = "ui://vexa0xryp3poe";

		public static UI_addAyCell CreateInstance()
		{
			return (UI_addAyCell)UIPackage.CreateObject("UI_AoYi","addAyCell");
		}

		public UI_addAyCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgAdd = (GImage)this.GetChildAt(1);
			m_imgRed = (GImage)this.GetChildAt(2);
			m_txtUnLockDes = (GTextField)this.GetChildAt(3);
			m_objAdd = (GGroup)this.GetChildAt(4);
			m_itemIcon = (UI_AoyiCommonItem)this.GetChildAt(5);
		}
	}
}