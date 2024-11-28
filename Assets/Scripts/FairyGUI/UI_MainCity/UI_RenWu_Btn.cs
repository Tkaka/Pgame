/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_MainCity
{
	public partial class UI_RenWu_Btn : GButton
	{
		public GImage m_Task_HongDian;

		public const string URL = "ui://jdfufi06ro1f6g";

		public static UI_RenWu_Btn CreateInstance()
		{
			return (UI_RenWu_Btn)UIPackage.CreateObject("UI_MainCity","RenWu_Btn");
		}

		public UI_RenWu_Btn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Task_HongDian = (GImage)this.GetChildAt(1);
		}
	}
}