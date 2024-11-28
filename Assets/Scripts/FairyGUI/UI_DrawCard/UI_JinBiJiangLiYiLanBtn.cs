/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DrawCard
{
	public partial class UI_JinBiJiangLiYiLanBtn : GButton
	{
		public GImage m_JinBiJiangLiYiLan_BeiJing;

		public const string URL = "ui://zy7t2yegvy0p2b";

		public static UI_JinBiJiangLiYiLanBtn CreateInstance()
		{
			return (UI_JinBiJiangLiYiLanBtn)UIPackage.CreateObject("UI_DrawCard","JinBiJiangLiYiLanBtn");
		}

		public UI_JinBiJiangLiYiLanBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_JinBiJiangLiYiLan_BeiJing = (GImage)this.GetChildAt(0);
		}
	}
}