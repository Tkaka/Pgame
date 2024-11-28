/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_XunLianJia
{
	public partial class UI_mingRenTanBtn : GButton
	{
		public GTextField m_lockLabel;
		public GGroup m_lockGroup;

		public const string URL = "ui://27xc27aks03s8";

		public static UI_mingRenTanBtn CreateInstance()
		{
			return (UI_mingRenTanBtn)UIPackage.CreateObject("UI_XunLianJia","mingRenTanBtn");
		}

		public UI_mingRenTanBtn()
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