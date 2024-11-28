/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DrawCard
{
	public partial class UI_GouMaiYiCiBtn : GComponent
	{
		public GTextField m_miaoshu;

		public const string URL = "ui://zy7t2yegci3627";

		public static UI_GouMaiYiCiBtn CreateInstance()
		{
			return (UI_GouMaiYiCiBtn)UIPackage.CreateObject("UI_DrawCard","GouMaiYiCiBtn");
		}

		public UI_GouMaiYiCiBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_miaoshu = (GTextField)this.GetChildAt(1);
		}
	}
}