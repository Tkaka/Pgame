/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_btnZhaoMu : GComponent
	{
		public GTextField m_txtDes;

		public const string URL = "ui://oe7ras64fde92j";

		public static UI_btnZhaoMu CreateInstance()
		{
			return (UI_btnZhaoMu)UIPackage.CreateObject("UI_Guild","btnZhaoMu");
		}

		public UI_btnZhaoMu()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(1);
		}
	}
}