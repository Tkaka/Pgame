/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_ResetTime : GComponent
	{
		public GComponent m_btnReset;
		public GTextField m_txtRemainTime;
		public GTextField m_txtNum;

		public const string URL = "ui://3xs7lfyxo0dez";

		public static UI_ResetTime CreateInstance()
		{
			return (UI_ResetTime)UIPackage.CreateObject("UI_Arena","ResetTime");
		}

		public UI_ResetTime()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_btnReset = (GComponent)this.GetChildAt(0);
			m_txtRemainTime = (GTextField)this.GetChildAt(2);
			m_txtNum = (GTextField)this.GetChildAt(3);
		}
	}
}