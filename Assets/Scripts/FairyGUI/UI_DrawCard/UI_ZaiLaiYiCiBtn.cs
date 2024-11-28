/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DrawCard
{
	public partial class UI_ZaiLaiYiCiBtn : GButton
	{
		public GTextField m_miaoshu;

		public const string URL = "ui://zy7t2yegeo851v";

		public static UI_ZaiLaiYiCiBtn CreateInstance()
		{
			return (UI_ZaiLaiYiCiBtn)UIPackage.CreateObject("UI_DrawCard","ZaiLaiYiCiBtn");
		}

		public UI_ZaiLaiYiCiBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_miaoshu = (GTextField)this.GetChildAt(1);
		}
	}
}