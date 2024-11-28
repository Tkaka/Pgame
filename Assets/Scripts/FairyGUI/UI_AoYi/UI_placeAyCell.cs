/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_placeAyCell : GComponent
	{
		public GLoader m_imgDic;
		public GGroup m_objBg;
		public UI_AoyiCommonItem m_itemIcon;

		public const string URL = "ui://vexa0xrypjwou";

		public static UI_placeAyCell CreateInstance()
		{
			return (UI_placeAyCell)UIPackage.CreateObject("UI_AoYi","placeAyCell");
		}

		public UI_placeAyCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgDic = (GLoader)this.GetChildAt(1);
			m_objBg = (GGroup)this.GetChildAt(2);
			m_itemIcon = (UI_AoyiCommonItem)this.GetChildAt(3);
		}
	}
}