/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DrawCard
{
	public partial class UI_JieSuoDengJiFenGeXian : GComponent
	{
		public GTextField m_JieSuo;

		public const string URL = "ui://zy7t2yegux5q1x";

		public static UI_JieSuoDengJiFenGeXian CreateInstance()
		{
			return (UI_JieSuoDengJiFenGeXian)UIPackage.CreateObject("UI_DrawCard","JieSuoDengJiFenGeXian");
		}

		public UI_JieSuoDengJiFenGeXian()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_JieSuo = (GTextField)this.GetChildAt(1);
		}
	}
}