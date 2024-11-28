/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_JingYan : GComponent
	{
		public GTextField m_JingYanNumber;
		public GTextField m_JinBiNumber;
		public GTextField m_JingYanNumber_2;

		public const string URL = "ui://34cd5b4hkgjxy";

		public static UI_JingYan CreateInstance()
		{
			return (UI_JingYan)UIPackage.CreateObject("UI_SaoDangJieSuan","JingYan");
		}

		public UI_JingYan()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_JingYanNumber = (GTextField)this.GetChildAt(0);
			m_JinBiNumber = (GTextField)this.GetChildAt(1);
			m_JingYanNumber_2 = (GTextField)this.GetChildAt(3);
		}
	}
}