/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_GMWindow : GComponent
	{
		public GButton m_okBtn;
		public GButton m_cancelBtn;
		public GTextInput m_cmdTxt;
		public UI_Dropdown m_dropDown;
		public GTextInput m_txtCmdInput;

		public const string URL = "ui://jdfufi06ht5d23";

		public static UI_GMWindow CreateInstance()
		{
			return (UI_GMWindow)UIPackage.CreateObject("UI_MainCity","GMWindow");
		}

		public UI_GMWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_okBtn = (GButton)this.GetChildAt(2);
			m_cancelBtn = (GButton)this.GetChildAt(3);
			m_cmdTxt = (GTextInput)this.GetChildAt(4);
			m_dropDown = (UI_Dropdown)this.GetChildAt(5);
			m_txtCmdInput = (GTextInput)this.GetChildAt(6);
		}
	}
}