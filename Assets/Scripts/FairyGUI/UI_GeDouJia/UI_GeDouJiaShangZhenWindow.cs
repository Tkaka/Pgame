/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_GeDouJiaShangZhenWindow : GComponent
	{
		public GButton m_Close;

		public const string URL = "ui://4asqm7awmwyx57";

		public static UI_GeDouJiaShangZhenWindow CreateInstance()
		{
			return (UI_GeDouJiaShangZhenWindow)UIPackage.CreateObject("UI_GeDouJia","GeDouJiaShangZhenWindow");
		}

		public UI_GeDouJiaShangZhenWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Close = (GButton)this.GetChildAt(2);
		}
	}
}