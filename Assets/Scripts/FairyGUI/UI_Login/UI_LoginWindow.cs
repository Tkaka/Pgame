/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Login
{
	public partial class UI_LoginWindow : GComponent
	{
		public GTextInput m_userName;
		public GButton m_loginBtn;
		public GButton m_serverBtn;
		public UI_loginLogoIcon m_loginLogo;
		public GGraph m_texiao;
		public GLoader m_movie;
		public GLoader m_logoLoader;
		public GGroup m_logoGroup;
		public Transition m_logoAnim;

		public const string URL = "ui://hg19ijpap1l10";

		public static UI_LoginWindow CreateInstance()
		{
			return (UI_LoginWindow)UIPackage.CreateObject("UI_Login","LoginWindow");
		}

		public UI_LoginWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_userName = (GTextInput)this.GetChildAt(4);
			m_loginBtn = (GButton)this.GetChildAt(5);
			m_serverBtn = (GButton)this.GetChildAt(6);
			m_loginLogo = (UI_loginLogoIcon)this.GetChildAt(8);
			m_texiao = (GGraph)this.GetChildAt(10);
			m_movie = (GLoader)this.GetChildAt(11);
			m_logoLoader = (GLoader)this.GetChildAt(13);
			m_logoGroup = (GGroup)this.GetChildAt(14);
			m_logoAnim = this.GetTransitionAt(0);
		}
	}
}