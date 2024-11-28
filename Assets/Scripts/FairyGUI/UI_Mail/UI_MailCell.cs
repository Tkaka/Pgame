/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Mail
{
	public partial class UI_MailCell : GComponent
	{
		public GComponent m_icon;
		public GTextField m_txtMain;
		public GTextField m_txtFrom;
		public GTextField m_txtDate;
		public GComponent m_imgNoRead;
		public GComponent m_imgReaded;
		public GLoader m_imgIcon;
		public GGroup m_objNormal;

		public const string URL = "ui://wgl1vubmyzqv3";

		public static UI_MailCell CreateInstance()
		{
			return (UI_MailCell)UIPackage.CreateObject("UI_Mail","MailCell");
		}

		public UI_MailCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_icon = (GComponent)this.GetChildAt(1);
			m_txtMain = (GTextField)this.GetChildAt(2);
			m_txtFrom = (GTextField)this.GetChildAt(3);
			m_txtDate = (GTextField)this.GetChildAt(4);
			m_imgNoRead = (GComponent)this.GetChildAt(5);
			m_imgReaded = (GComponent)this.GetChildAt(6);
			m_imgIcon = (GLoader)this.GetChildAt(8);
			m_objNormal = (GGroup)this.GetChildAt(9);
		}
	}
}