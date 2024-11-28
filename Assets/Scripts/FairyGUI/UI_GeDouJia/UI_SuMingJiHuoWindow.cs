/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_SuMingJiHuoWindow : GComponent
	{
		public GButton m_CloseBtn;
		public GList m_SuMingList;
		public GTextField m_number;

		public const string URL = "ui://4asqm7awn0x05m";

		public static UI_SuMingJiHuoWindow CreateInstance()
		{
			return (UI_SuMingJiHuoWindow)UIPackage.CreateObject("UI_GeDouJia","SuMingJiHuoWindow");
		}

		public UI_SuMingJiHuoWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_CloseBtn = (GButton)this.GetChildAt(2);
			m_SuMingList = (GList)this.GetChildAt(3);
			m_number = (GTextField)this.GetChildAt(5);
		}
	}
}