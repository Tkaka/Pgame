/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Talent
{
	public partial class UI_btnJinXiu : GButton
	{
		public GImage m_imgLock;
		public GTextField m_txtDescribe;

		public const string URL = "ui://erk5lfvwm6aaf";

		public static UI_btnJinXiu CreateInstance()
		{
			return (UI_btnJinXiu)UIPackage.CreateObject("UI_Talent","btnJinXiu");
		}

		public UI_btnJinXiu()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgLock = (GImage)this.GetChildAt(2);
			m_txtDescribe = (GTextField)this.GetChildAt(3);
		}
	}
}