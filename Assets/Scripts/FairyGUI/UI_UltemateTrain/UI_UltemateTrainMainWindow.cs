/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_UltemateTrainMainWindow : GComponent
	{
		public GComponent m_commonTop;
		public GTextField m_floorNum;
		public GTextField m_integralNum;
		public UI_additionListPanel m_additionListPanel;
		public GLoader m_additionLoader;
		public GTextField m_starNum;
		public GButton m_rankBtn;
		public GButton m_rewardBtn;
		public GButton m_shopBtn;
		public GButton m_ruleBtn;
		public GGraph m_monsterToucher;
		public UI_ultemateFloorTipView m_floorTipView;

		public const string URL = "ui://1wdkrtiuw0hu0";

		public static UI_UltemateTrainMainWindow CreateInstance()
		{
			return (UI_UltemateTrainMainWindow)UIPackage.CreateObject("UI_UltemateTrain","UltemateTrainMainWindow");
		}

		public UI_UltemateTrainMainWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_commonTop = (GComponent)this.GetChildAt(0);
			m_floorNum = (GTextField)this.GetChildAt(2);
			m_integralNum = (GTextField)this.GetChildAt(4);
			m_additionListPanel = (UI_additionListPanel)this.GetChildAt(5);
			m_additionLoader = (GLoader)this.GetChildAt(6);
			m_starNum = (GTextField)this.GetChildAt(8);
			m_rankBtn = (GButton)this.GetChildAt(9);
			m_rewardBtn = (GButton)this.GetChildAt(10);
			m_shopBtn = (GButton)this.GetChildAt(11);
			m_ruleBtn = (GButton)this.GetChildAt(12);
			m_monsterToucher = (GGraph)this.GetChildAt(15);
			m_floorTipView = (UI_ultemateFloorTipView)this.GetChildAt(16);
		}
	}
}