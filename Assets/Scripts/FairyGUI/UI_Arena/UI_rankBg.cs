/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_rankBg : GComponent
	{
		public GLoader m_bgLoader;

		public const string URL = "ui://3xs7lfyxgawd1m";

		public static UI_rankBg CreateInstance()
		{
			return (UI_rankBg)UIPackage.CreateObject("UI_Arena","rankBg");
		}

		public UI_rankBg()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bgLoader = (GLoader)this.GetChildAt(0);
		}
	}
}