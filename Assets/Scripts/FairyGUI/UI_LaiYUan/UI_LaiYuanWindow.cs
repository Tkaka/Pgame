/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_LaiYUan
{
	public partial class UI_LaiYuanWindow : GComponent
	{
		public GButton m_Close;

		public const string URL = "ui://3cw5ouevh9kv0";

		public static UI_LaiYuanWindow CreateInstance()
		{
			return (UI_LaiYuanWindow)UIPackage.CreateObject("UI_LaiYUan","LaiYuanWindow");
		}

		public UI_LaiYuanWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Close = (GButton)this.GetChildAt(1);
		}
	}
}