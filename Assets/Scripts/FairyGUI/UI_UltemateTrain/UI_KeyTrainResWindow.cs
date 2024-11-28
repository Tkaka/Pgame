/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_KeyTrainResWindow : GComponent
	{
		public GTextField m_boxRewardTipLabel;
		public GTextField m_propertyTipLabel;
		public GTextField m_hideBoxTipLabel;
		public GGroup m_progressTipGroup;
		public GButton m_nextStepBtn;
		public GButton m_buyNextPropertyBtn;
		public GTextField m_remainFloorLabel;
		public GGroup m_buyNextPropertyGroup;
		public UI_jumpBoxRewardPanel m_jumpBoxRewardPanel;
		public UI_jumpBuyPropertyPanel m_jumpBuyPropertyPanel;
		public UI_jumpBuyBoxPanel m_jumpBuyBoxPanel;
		public GButton m_closeBtn;

		public const string URL = "ui://1wdkrtiuw0huj";

		public static UI_KeyTrainResWindow CreateInstance()
		{
			return (UI_KeyTrainResWindow)UIPackage.CreateObject("UI_UltemateTrain","KeyTrainResWindow");
		}

		public UI_KeyTrainResWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boxRewardTipLabel = (GTextField)this.GetChildAt(2);
			m_propertyTipLabel = (GTextField)this.GetChildAt(3);
			m_hideBoxTipLabel = (GTextField)this.GetChildAt(4);
			m_progressTipGroup = (GGroup)this.GetChildAt(7);
			m_nextStepBtn = (GButton)this.GetChildAt(8);
			m_buyNextPropertyBtn = (GButton)this.GetChildAt(9);
			m_remainFloorLabel = (GTextField)this.GetChildAt(10);
			m_buyNextPropertyGroup = (GGroup)this.GetChildAt(11);
			m_jumpBoxRewardPanel = (UI_jumpBoxRewardPanel)this.GetChildAt(12);
			m_jumpBuyPropertyPanel = (UI_jumpBuyPropertyPanel)this.GetChildAt(13);
			m_jumpBuyBoxPanel = (UI_jumpBuyBoxPanel)this.GetChildAt(14);
			m_closeBtn = (GButton)this.GetChildAt(15);
		}
	}
}