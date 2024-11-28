/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_LaiYuanWindow : GComponent
	{
		public GGraph m_mask;
		public UI_laiYuanPV m_popupView;

		public const string URL = "ui://4asqm7awh9kv3v";

		public static UI_LaiYuanWindow CreateInstance()
		{
			return (UI_LaiYuanWindow)UIPackage.CreateObject("UI_GeDouJia","LaiYuanWindow");
		}

		public UI_LaiYuanWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_laiYuanPV)this.GetChildAt(1);
		}
	}
}