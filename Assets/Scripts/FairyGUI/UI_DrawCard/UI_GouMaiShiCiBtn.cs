/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DrawCard
{
	public partial class UI_GouMaiShiCiBtn : GComponent
	{
		public GTextField m_miaoshu;

		public const string URL = "ui://zy7t2yegci3628";

		public static UI_GouMaiShiCiBtn CreateInstance()
		{
			return (UI_GouMaiShiCiBtn)UIPackage.CreateObject("UI_DrawCard","GouMaiShiCiBtn");
		}

		public UI_GouMaiShiCiBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_miaoshu = (GTextField)this.GetChildAt(1);
		}
	}
}