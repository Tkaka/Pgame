/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GER_WoDeHuoDe : GComponent
	{
		public GTextField m_miaoshu;

		public const string URL = "ui://r816m4tmfzr6i";

		public static UI_GER_WoDeHuoDe CreateInstance()
		{
			return (UI_GER_WoDeHuoDe)UIPackage.CreateObject("UI_GuillRedEnvelope","GER_WoDeHuoDe");
		}

		public UI_GER_WoDeHuoDe()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_miaoshu = (GTextField)this.GetChildAt(1);
		}
	}
}