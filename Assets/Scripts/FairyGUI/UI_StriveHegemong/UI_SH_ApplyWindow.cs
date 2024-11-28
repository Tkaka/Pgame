/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_ApplyWindow : GComponent
	{
		public Controller m_shuxing;
		public GList m_PetList;
		public GList m_ShangZhenList;
		public GGraph m_forword;
		public GGraph m_next;
		public GButton m_YiJianShangZhen;
		public GButton m_QueRenTiJiao;
		public GButton m_CloseBtn;

		public const string URL = "ui://yjvxfmwoidnd1b";

		public static UI_SH_ApplyWindow CreateInstance()
		{
			return (UI_SH_ApplyWindow)UIPackage.CreateObject("UI_StriveHegemong","SH_ApplyWindow");
		}

		public UI_SH_ApplyWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_shuxing = this.GetControllerAt(0);
			m_PetList = (GList)this.GetChildAt(4);
			m_ShangZhenList = (GList)this.GetChildAt(9);
			m_forword = (GGraph)this.GetChildAt(10);
			m_next = (GGraph)this.GetChildAt(11);
			m_YiJianShangZhen = (GButton)this.GetChildAt(12);
			m_QueRenTiJiao = (GButton)this.GetChildAt(13);
			m_CloseBtn = (GButton)this.GetChildAt(14);
		}
	}
}