/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GRE_QiangHongBtn : GButton
	{
		public GTextField m_miaoshu;

		public const string URL = "ui://r816m4tmmf5nn";

		public static UI_GRE_QiangHongBtn CreateInstance()
		{
			return (UI_GRE_QiangHongBtn)UIPackage.CreateObject("UI_GuillRedEnvelope","GRE_QiangHongBtn");
		}

		public UI_GRE_QiangHongBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_miaoshu = (GTextField)this.GetChildAt(1);
		}
	}
}