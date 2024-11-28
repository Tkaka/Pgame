/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_ChnageNameWindow : GComponent
	{
		public GGraph m_mask;
		public GButton m_closeBtn;
		public GTextInput m_name;
		public GTextField m_firstFreeTip;
		public GButton m_comfirmBtn;
		public GTextField m_normalTip;
		public GTextField m_costDiamond;
		public GGroup m_noramlTip;
		public GButton m_randomNameBtn;

		public const string URL = "ui://jdfufi06wjf86q";

		public static UI_ChnageNameWindow CreateInstance()
		{
			return (UI_ChnageNameWindow)UIPackage.CreateObject("UI_MainCity","ChnageNameWindow");
		}

		public UI_ChnageNameWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_closeBtn = (GButton)this.GetChildAt(2);
			m_name = (GTextInput)this.GetChildAt(5);
			m_firstFreeTip = (GTextField)this.GetChildAt(6);
			m_comfirmBtn = (GButton)this.GetChildAt(7);
			m_normalTip = (GTextField)this.GetChildAt(8);
			m_costDiamond = (GTextField)this.GetChildAt(10);
			m_noramlTip = (GGroup)this.GetChildAt(11);
			m_randomNameBtn = (GButton)this.GetChildAt(13);
		}
	}
}