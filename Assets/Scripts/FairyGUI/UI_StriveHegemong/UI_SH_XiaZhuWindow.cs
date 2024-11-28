/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_XiaZhuWindow : GComponent
	{
		public GList m_XiaZhuList;
		public GButton m_CloseBtn;

		public const string URL = "ui://yjvxfmwon7xzv";

		public static UI_SH_XiaZhuWindow CreateInstance()
		{
			return (UI_SH_XiaZhuWindow)UIPackage.CreateObject("UI_StriveHegemong","SH_XiaZhuWindow");
		}

		public UI_SH_XiaZhuWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_XiaZhuList = (GList)this.GetChildAt(2);
			m_CloseBtn = (GButton)this.GetChildAt(7);
		}
	}
}