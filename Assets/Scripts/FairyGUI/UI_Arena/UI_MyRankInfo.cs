/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_MyRankInfo : GComponent
	{
		public GTextField m_txtRank;
		public GTextField m_txtFightPower;

		public const string URL = "ui://3xs7lfyxo0det";

		public static UI_MyRankInfo CreateInstance()
		{
			return (UI_MyRankInfo)UIPackage.CreateObject("UI_Arena","MyRankInfo");
		}

		public UI_MyRankInfo()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtRank = (GTextField)this.GetChildAt(2);
			m_txtFightPower = (GTextField)this.GetChildAt(4);
		}
	}
}