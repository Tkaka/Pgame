/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_XunLianJia
{
	public partial class UI_shenQiBtn : GButton
	{
		public GTextField m_lockLabel;
		public GGroup m_lockGroup;

		public const string URL = "ui://27xc27akiqjog";

		public static UI_shenQiBtn CreateInstance()
		{
			return (UI_shenQiBtn)UIPackage.CreateObject("UI_XunLianJia","shenQiBtn");
		}

		public UI_shenQiBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_lockLabel = (GTextField)this.GetChildAt(1);
			m_lockGroup = (GGroup)this.GetChildAt(3);
		}
	}
}