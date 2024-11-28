/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_SelectOpponentWindow : GComponent
	{
		public GGraph m_mask;
		public GButton m_keyShangZhenBtn;
		public GTextField m_fightPowerLabel;
		public GList m_shanZhenList;
		public GList m_opponentList;
		public GButton m_backBtn;

		public const string URL = "ui://1wdkrtiuw0hu17";

		public static UI_SelectOpponentWindow CreateInstance()
		{
			return (UI_SelectOpponentWindow)UIPackage.CreateObject("UI_UltemateTrain","SelectOpponentWindow");
		}

		public UI_SelectOpponentWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_keyShangZhenBtn = (GButton)this.GetChildAt(2);
			m_fightPowerLabel = (GTextField)this.GetChildAt(4);
			m_shanZhenList = (GList)this.GetChildAt(5);
			m_opponentList = (GList)this.GetChildAt(8);
			m_backBtn = (GButton)this.GetChildAt(10);
		}
	}
}