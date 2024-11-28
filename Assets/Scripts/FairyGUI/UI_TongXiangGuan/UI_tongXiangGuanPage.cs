/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TongXiangGuan
{
	public partial class UI_tongXiangGuanPage : GComponent
	{
		public GLoader m_bgLoader;

		public const string URL = "ui://ansp6fm5lni7h";

		public static UI_tongXiangGuanPage CreateInstance()
		{
			return (UI_tongXiangGuanPage)UIPackage.CreateObject("UI_TongXiangGuan","tongXiangGuanPage");
		}

		public UI_tongXiangGuanPage()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bgLoader = (GLoader)this.GetChildAt(0);
		}
	}
}