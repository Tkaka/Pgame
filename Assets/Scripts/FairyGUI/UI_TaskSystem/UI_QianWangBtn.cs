/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TaskSystem
{
	public partial class UI_QianWangBtn : GButton
	{
		public GTextField m_QianWang;

		public const string URL = "ui://zswzat1eetrta";

		public static UI_QianWangBtn CreateInstance()
		{
			return (UI_QianWangBtn)UIPackage.CreateObject("UI_TaskSystem","QianWangBtn");
		}

		public UI_QianWangBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_QianWang = (GTextField)this.GetChildAt(1);
		}
	}
}