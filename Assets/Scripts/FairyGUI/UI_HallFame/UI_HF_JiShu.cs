/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_HallFame
{
	public partial class UI_HF_JiShu : GComponent
	{
		public GTextField m_HF_jibiemingzi;
		public GTextField m_HF_number;

		public const string URL = "ui://yo5kunkik5ji9";

		public static UI_HF_JiShu CreateInstance()
		{
			return (UI_HF_JiShu)UIPackage.CreateObject("UI_HallFame","HF_JiShu");
		}

		public UI_HF_JiShu()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_HF_jibiemingzi = (GTextField)this.GetChildAt(1);
			m_HF_number = (GTextField)this.GetChildAt(2);
		}
	}
}