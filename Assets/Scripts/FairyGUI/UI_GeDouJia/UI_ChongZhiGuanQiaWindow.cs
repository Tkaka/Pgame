/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_ChongZhiGuanQiaWindow : GComponent
	{
		public GButton m_Close;

		public const string URL = "ui://4asqm7awfps84v";

		public static UI_ChongZhiGuanQiaWindow CreateInstance()
		{
			return (UI_ChongZhiGuanQiaWindow)UIPackage.CreateObject("UI_GeDouJia","ChongZhiGuanQiaWindow");
		}

		public UI_ChongZhiGuanQiaWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Close = (GButton)this.GetChildAt(1);
		}
	}
}