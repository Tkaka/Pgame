/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_BuZhen
{
	public partial class UI_YiShangZhen : GComponent
	{
		public UI_YiShangZhenBtn m_TouXiang;
		public GLoader m_ShuXing;
		public GTextField m_Name;
		public GButton m_ShangZhenBtn;
		public GTextField m_fightPowerLable;
		public GGroup m_fightPowerGroup;
		public GImage m_levelNotEnoughIcon;
		public GProgressBar m_hpProgress;
		public GProgressBar m_energyProgress;
		public GGroup m_progressGroup;
		public GButton m_selectBtn;
		public GTextField m_zhenWanIcon;

		public const string URL = "ui://z0csav43rxblf3c";

		public static UI_YiShangZhen CreateInstance()
		{
			return (UI_YiShangZhen)UIPackage.CreateObject("UI_BuZhen","YiShangZhen");
		}

		public UI_YiShangZhen()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_TouXiang = (UI_YiShangZhenBtn)this.GetChildAt(1);
			m_ShuXing = (GLoader)this.GetChildAt(2);
			m_Name = (GTextField)this.GetChildAt(3);
			m_ShangZhenBtn = (GButton)this.GetChildAt(4);
			m_fightPowerLable = (GTextField)this.GetChildAt(6);
			m_fightPowerGroup = (GGroup)this.GetChildAt(7);
			m_levelNotEnoughIcon = (GImage)this.GetChildAt(8);
			m_hpProgress = (GProgressBar)this.GetChildAt(9);
			m_energyProgress = (GProgressBar)this.GetChildAt(10);
			m_progressGroup = (GGroup)this.GetChildAt(11);
			m_selectBtn = (GButton)this.GetChildAt(12);
			m_zhenWanIcon = (GTextField)this.GetChildAt(13);
		}
	}
}