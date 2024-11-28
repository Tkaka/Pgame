/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_TipFuncView : GComponent
	{
		public GLoader m_icon;
		public GTextField m_name;
		public GTextField m_desc1;
		public GTextField m_desc2;
		public GButton m_close;

		public const string URL = "ui://jdfufi06h2kp4q";

		public static UI_TipFuncView CreateInstance()
		{
			return (UI_TipFuncView)UIPackage.CreateObject("UI_MainCity","TipFuncView");
		}

		public UI_TipFuncView()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_icon = (GLoader)this.GetChildAt(1);
			m_name = (GTextField)this.GetChildAt(2);
			m_desc1 = (GTextField)this.GetChildAt(3);
			m_desc2 = (GTextField)this.GetChildAt(4);
			m_close = (GButton)this.GetChildAt(5);
		}
	}
}