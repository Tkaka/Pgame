/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_FuncIcon : GButton
	{
		public GLoader m_tipIcon;
		public GTextField m_name;
		public GTextField m_cond;

		public const string URL = "ui://jdfufi06ro1f66";

		public static UI_FuncIcon CreateInstance()
		{
			return (UI_FuncIcon)UIPackage.CreateObject("UI_MainCity","FuncIcon");
		}

		public UI_FuncIcon()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tipIcon = (GLoader)this.GetChildAt(1);
			m_name = (GTextField)this.GetChildAt(2);
			m_cond = (GTextField)this.GetChildAt(3);
		}
	}
}