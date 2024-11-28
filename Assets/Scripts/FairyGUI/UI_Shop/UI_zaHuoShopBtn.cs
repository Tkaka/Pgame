/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_zaHuoShopBtn : GButton
	{
		public GComponent m_lockGroup;
		public GTextField m_title1;

		public const string URL = "ui://w9mypx6jb48q1d";

		public static UI_zaHuoShopBtn CreateInstance()
		{
			return (UI_zaHuoShopBtn)UIPackage.CreateObject("UI_Shop","zaHuoShopBtn");
		}

		public UI_zaHuoShopBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_lockGroup = (GComponent)this.GetChildAt(3);
			m_title1 = (GTextField)this.GetChildAt(4);
		}
	}
}