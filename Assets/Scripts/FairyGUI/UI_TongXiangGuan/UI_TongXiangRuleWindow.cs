/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TongXiangGuan
{
	public partial class UI_TongXiangRuleWindow : GComponent
	{
		public GList m_contentList;
		public GButton m_closeBtn;

		public const string URL = "ui://ansp6fm5lni74";

		public static UI_TongXiangRuleWindow CreateInstance()
		{
			return (UI_TongXiangRuleWindow)UIPackage.CreateObject("UI_TongXiangGuan","TongXiangRuleWindow");
		}

		public UI_TongXiangRuleWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_contentList = (GList)this.GetChildAt(3);
			m_closeBtn = (GButton)this.GetChildAt(4);
		}
	}
}