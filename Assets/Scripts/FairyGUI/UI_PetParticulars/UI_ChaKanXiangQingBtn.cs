/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_PetParticulars
{
	public partial class UI_ChaKanXiangQingBtn : GButton
	{
		public GTextField m_miaoshu;

		public const string URL = "ui://rn5o3g4tw5s4f";

		public static UI_ChaKanXiangQingBtn CreateInstance()
		{
			return (UI_ChaKanXiangQingBtn)UIPackage.CreateObject("UI_PetParticulars","ChaKanXiangQingBtn");
		}

		public UI_ChaKanXiangQingBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_miaoshu = (GTextField)this.GetChildAt(1);
		}
	}
}