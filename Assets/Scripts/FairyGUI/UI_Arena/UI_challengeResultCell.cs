/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_challengeResultCell : GComponent
	{
		public GTextField m_imgSucess;
		public GTextField m_imgFailed;
		public GTextField m_txtHuiHe;
		public GTextField m_txtDes;

		public const string URL = "ui://3xs7lfyxehrw29";

		public static UI_challengeResultCell CreateInstance()
		{
			return (UI_challengeResultCell)UIPackage.CreateObject("UI_Arena","challengeResultCell");
		}

		public UI_challengeResultCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgSucess = (GTextField)this.GetChildAt(1);
			m_imgFailed = (GTextField)this.GetChildAt(2);
			m_txtHuiHe = (GTextField)this.GetChildAt(5);
			m_txtDes = (GTextField)this.GetChildAt(7);
		}
	}
}