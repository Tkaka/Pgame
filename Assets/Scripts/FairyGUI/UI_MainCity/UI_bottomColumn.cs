/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_bottomColumn : GComponent
	{
		public GButton m_teamBtn;
		public GButton m_GeDouJia;
		public GButton m_ZhaoHuan;
		public UI_ShangDian_Btn m_btnShop;
		public GButton m_BagBtn;
		public UI_RenWu_Btn m_TaskBtn;
		public UI_SheTuan_Btn m_guild;
		public UI_jingJi_Btn m_jingJiBtn;
		public GButton m_tiaoZhanBtn;
		public Transition m_anim;

		public const string URL = "ui://jdfufi06nt4g7h";

		public static UI_bottomColumn CreateInstance()
		{
			return (UI_bottomColumn)UIPackage.CreateObject("UI_MainCity","bottomColumn");
		}

		public UI_bottomColumn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_teamBtn = (GButton)this.GetChildAt(1);
			m_GeDouJia = (GButton)this.GetChildAt(2);
			m_ZhaoHuan = (GButton)this.GetChildAt(3);
			m_btnShop = (UI_ShangDian_Btn)this.GetChildAt(4);
			m_BagBtn = (GButton)this.GetChildAt(5);
			m_TaskBtn = (UI_RenWu_Btn)this.GetChildAt(6);
			m_guild = (UI_SheTuan_Btn)this.GetChildAt(7);
			m_jingJiBtn = (UI_jingJi_Btn)this.GetChildAt(8);
			m_tiaoZhanBtn = (GButton)this.GetChildAt(9);
			m_anim = this.GetTransitionAt(0);
		}
	}
}