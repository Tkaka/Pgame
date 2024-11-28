/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_XunLianJia
{
	public partial class UI_btnPanel : GComponent
	{
		public UI_mingRenTanBtn m_mingRenTanBtn;
		public UI_tongXianGuanBtn m_tongXiangGuan;

		public const string URL = "ui://27xc27aks03s7";

		public static UI_btnPanel CreateInstance()
		{
			return (UI_btnPanel)UIPackage.CreateObject("UI_XunLianJia","btnPanel");
		}

		public UI_btnPanel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mingRenTanBtn = (UI_mingRenTanBtn)this.GetChildAt(0);
			m_tongXiangGuan = (UI_tongXianGuanBtn)this.GetChildAt(1);
		}
	}
}