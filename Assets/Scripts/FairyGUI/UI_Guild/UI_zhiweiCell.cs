/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_zhiweiCell : GComponent
	{
		public GTextField m_txtDes;

		public const string URL = "ui://oe7ras64105rb3e";

		public static UI_zhiweiCell CreateInstance()
		{
			return (UI_zhiweiCell)UIPackage.CreateObject("UI_Guild","zhiweiCell");
		}

		public UI_zhiweiCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(1);
		}
	}
}