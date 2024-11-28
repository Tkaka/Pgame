/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DailyActivity
{
	public partial class UI_WndDuiHuanNum : GComponent
	{
		public GGraph m_mask;
		public UI_itemUsePV m_popupView;

		public const string URL = "ui://0n5r1ymrl73xe";

		public static UI_WndDuiHuanNum CreateInstance()
		{
			return (UI_WndDuiHuanNum)UIPackage.CreateObject("UI_DailyActivity","WndDuiHuanNum");
		}

		public UI_WndDuiHuanNum()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_popupView = (UI_itemUsePV)this.GetChildAt(1);
		}
	}
}