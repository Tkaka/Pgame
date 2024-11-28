/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Chat
{
	public partial class UI_objFriendCell : GComponent
	{
		public UI_objTouXiang m_imgIocn;
		public GTextField m_txtName;
		public GTextField m_txtTime;
		public GTextField m_txtLevel;
		public UI_btnGift m_btnGift;

		public const string URL = "ui://51gazvjdqknig2c";

		public static UI_objFriendCell CreateInstance()
		{
			return (UI_objFriendCell)UIPackage.CreateObject("UI_Chat","objFriendCell");
		}

		public UI_objFriendCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgIocn = (UI_objTouXiang)this.GetChildAt(1);
			m_txtName = (GTextField)this.GetChildAt(2);
			m_txtTime = (GTextField)this.GetChildAt(3);
			m_txtLevel = (GTextField)this.GetChildAt(4);
			m_btnGift = (UI_btnGift)this.GetChildAt(5);
		}
	}
}