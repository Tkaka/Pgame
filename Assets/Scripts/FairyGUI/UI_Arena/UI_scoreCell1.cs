/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_scoreCell1 : GComponent
	{
		public GTextField m_txtRank;
		public GGroup m_rewardCell1;

		public const string URL = "ui://3xs7lfyxehrw23";

		public static UI_scoreCell1 CreateInstance()
		{
			return (UI_scoreCell1)UIPackage.CreateObject("UI_Arena","scoreCell1");
		}

		public UI_scoreCell1()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtRank = (GTextField)this.GetChildAt(2);
			m_rewardCell1 = (GGroup)this.GetChildAt(6);
		}
	}
}