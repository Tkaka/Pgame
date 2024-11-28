/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Guild
{
	public partial class UI_objDonateBigTypeCell : GComponent
	{
		public GLoader m_imgBg;
		public GImage m_imgLock;
		public GTextField m_txtName;

		public const string URL = "ui://oe7ras64lcbob42";

		public static UI_objDonateBigTypeCell CreateInstance()
		{
			return (UI_objDonateBigTypeCell)UIPackage.CreateObject("UI_Guild","objDonateBigTypeCell");
		}

		public UI_objDonateBigTypeCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgBg = (GLoader)this.GetChildAt(1);
			m_imgLock = (GImage)this.GetChildAt(2);
			m_txtName = (GTextField)this.GetChildAt(3);
		}
	}
}