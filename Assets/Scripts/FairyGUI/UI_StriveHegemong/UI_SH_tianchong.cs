/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_tianchong : GComponent
	{
		public GGraph m_tianchong;

		public const string URL = "ui://yjvxfmwoe8xk1n";

		public static UI_SH_tianchong CreateInstance()
		{
			return (UI_SH_tianchong)UIPackage.CreateObject("UI_StriveHegemong","SH_tianchong");
		}

		public UI_SH_tianchong()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tianchong = (GGraph)this.GetChildAt(0);
		}
	}
}