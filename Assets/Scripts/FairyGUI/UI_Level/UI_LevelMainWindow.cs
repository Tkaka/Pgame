/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_LevelMainWindow : GComponent
	{
		public GList m_levelList;
		public GComponent m_commonTop;
		public GProgressBar m_starProgress;
		public UI_starBoxItem m_starBox1;
		public UI_starBoxItem m_starBox2;
		public UI_starBoxItem m_starBox3;
		public UI_mainLevelBtn m_mainLevelBtn;
		public UI_eiteLevelBtn m_eliteLevelBtn;
		public GTextField m_starNumLabel;
		public GLoader m_chapterNameBg;
		public GLoader m_chapterType;
		public GTextField m_chapterName;
		public GButton m_leftChapterBtn;
		public GButton m_leftDoubleArrowBtn;
		public GButton m_rightChapterBtn;
		public GButton m_rightDoubleArrowBtn;
		public GButton m_btnSupperSweep;
		public GButton m_quickJumpBtn;
		public UI_keyReceiveBtn m_keyReceiveBtn;
		public GButton m_zhenRongBtn;
		public GComponent m_buZhenColumn;

		public const string URL = "ui://z04ymz0emyik0";

		public static UI_LevelMainWindow CreateInstance()
		{
			return (UI_LevelMainWindow)UIPackage.CreateObject("UI_Level","LevelMainWindow");
		}

		public UI_LevelMainWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_levelList = (GList)this.GetChildAt(0);
			m_commonTop = (GComponent)this.GetChildAt(1);
			m_starProgress = (GProgressBar)this.GetChildAt(3);
			m_starBox1 = (UI_starBoxItem)this.GetChildAt(4);
			m_starBox2 = (UI_starBoxItem)this.GetChildAt(5);
			m_starBox3 = (UI_starBoxItem)this.GetChildAt(6);
			m_mainLevelBtn = (UI_mainLevelBtn)this.GetChildAt(9);
			m_eliteLevelBtn = (UI_eiteLevelBtn)this.GetChildAt(10);
			m_starNumLabel = (GTextField)this.GetChildAt(14);
			m_chapterNameBg = (GLoader)this.GetChildAt(16);
			m_chapterType = (GLoader)this.GetChildAt(17);
			m_chapterName = (GTextField)this.GetChildAt(18);
			m_leftChapterBtn = (GButton)this.GetChildAt(20);
			m_leftDoubleArrowBtn = (GButton)this.GetChildAt(21);
			m_rightChapterBtn = (GButton)this.GetChildAt(22);
			m_rightDoubleArrowBtn = (GButton)this.GetChildAt(23);
			m_btnSupperSweep = (GButton)this.GetChildAt(25);
			m_quickJumpBtn = (GButton)this.GetChildAt(26);
			m_keyReceiveBtn = (UI_keyReceiveBtn)this.GetChildAt(28);
			m_zhenRongBtn = (GButton)this.GetChildAt(29);
			m_buZhenColumn = (GComponent)this.GetChildAt(30);
		}
	}
}