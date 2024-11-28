/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_objArenaTypeCell : GComponent
	{
		public GLoader m_imgBg;
		public GList m_RewardList;
		public GTextField m_txtName;
		public GImage m_imgRed;

		public const string URL = "ui://3xs7lfyxfa342l";

		public static UI_objArenaTypeCell CreateInstance()
		{
			return (UI_objArenaTypeCell)UIPackage.CreateObject("UI_Arena","objArenaTypeCell");
		}

		public UI_objArenaTypeCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgBg = (GLoader)this.GetChildAt(0);
			m_RewardList = (GList)this.GetChildAt(1);
			m_txtName = (GTextField)this.GetChildAt(2);
			m_imgRed = (GImage)this.GetChildAt(3);
		}
	}
}