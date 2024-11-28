/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_MainCityWindow : GComponent
	{
		public UI_Joystick m_joystick;
		public GGraph m_touchHold;
		public GButton m_gmBtn;
		public UI_Battle_Btn m_battleBtn;
		public UI_bottomColumn m_bottomColumn;
		public GButton m_btnRecharge;
		public GButton m_qianDaoBtn;
		public GButton m_PaiHangBangBtn;
		public GGroup m_ShouNa_Left;
		public GGroup m_ShouNa_Right;
		public UI_email_Btn m_btnEmail;
		public GButton m_liaoTianBtn;
		public GTextField m_timeTxt;
		public GButton m_SH_baomingkuaijie;
		public GButton m_SH_GuanZhanBtn;
		public GButton m_CeShiChuangKouBtn;
		public GButton m_MysteriousShopBtn;
		public UI_FuncIcon m_funcIcon;
		public UI_roleInfo m_roleInfo;

		public const string URL = "ui://jdfufi06ro1f5x";

		public static UI_MainCityWindow CreateInstance()
		{
			return (UI_MainCityWindow)UIPackage.CreateObject("UI_MainCity","MainCityWindow");
		}

		public UI_MainCityWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_joystick = (UI_Joystick)this.GetChildAt(0);
			m_touchHold = (GGraph)this.GetChildAt(1);
			m_gmBtn = (GButton)this.GetChildAt(2);
			m_battleBtn = (UI_Battle_Btn)this.GetChildAt(3);
			m_bottomColumn = (UI_bottomColumn)this.GetChildAt(4);
			m_btnRecharge = (GButton)this.GetChildAt(5);
			m_qianDaoBtn = (GButton)this.GetChildAt(8);
			m_PaiHangBangBtn = (GButton)this.GetChildAt(9);
			m_ShouNa_Left = (GGroup)this.GetChildAt(14);
			m_ShouNa_Right = (GGroup)this.GetChildAt(18);
			m_btnEmail = (UI_email_Btn)this.GetChildAt(19);
			m_liaoTianBtn = (GButton)this.GetChildAt(20);
			m_timeTxt = (GTextField)this.GetChildAt(21);
			m_SH_baomingkuaijie = (GButton)this.GetChildAt(22);
			m_SH_GuanZhanBtn = (GButton)this.GetChildAt(23);
			m_CeShiChuangKouBtn = (GButton)this.GetChildAt(24);
			m_MysteriousShopBtn = (GButton)this.GetChildAt(25);
			m_funcIcon = (UI_FuncIcon)this.GetChildAt(26);
			m_roleInfo = (UI_roleInfo)this.GetChildAt(27);
		}
	}
}