/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DrawCard
{
	public partial class UI_GouMaiBaiCiBtn : GComponent
	{
		public GTextField m_miaoshu;

		public const string URL = "ui://zy7t2yegci3629";

		public static UI_GouMaiBaiCiBtn CreateInstance()
		{
			return (UI_GouMaiBaiCiBtn)UIPackage.CreateObject("UI_DrawCard","GouMaiBaiCiBtn");
		}

		public UI_GouMaiBaiCiBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_miaoshu = (GTextField)this.GetChildAt(1);
		}
	}
}