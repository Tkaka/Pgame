/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_StriveHegemong
{
	public partial class UI_SH_ZS_NO_role : GComponent
	{
		public GGraph m_One;
		public GGraph m_Two;
		public GGraph m_Three;
		public GLoader m_PaiMingIcon;
		public GTextField m_PaiMingText;
		public GTextField m_PlayerName;
		public GTextField m_SheTuanName;

		public const string URL = "ui://yjvxfmwon7xz8";

		public static UI_SH_ZS_NO_role CreateInstance()
		{
			return (UI_SH_ZS_NO_role)UIPackage.CreateObject("UI_StriveHegemong","SH_ZS_NO_role");
		}

		public UI_SH_ZS_NO_role()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_One = (GGraph)this.GetChildAt(1);
			m_Two = (GGraph)this.GetChildAt(2);
			m_Three = (GGraph)this.GetChildAt(3);
			m_PaiMingIcon = (GLoader)this.GetChildAt(4);
			m_PaiMingText = (GTextField)this.GetChildAt(5);
			m_PlayerName = (GTextField)this.GetChildAt(6);
			m_SheTuanName = (GTextField)this.GetChildAt(7);
		}
	}
}