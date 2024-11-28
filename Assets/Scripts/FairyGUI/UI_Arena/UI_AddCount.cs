/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_AddCount : GComponent
	{
		public GImage m_imgIcom;
		public GTextField m_txtCount;
		public GComponent m_btnAddCount;

		public const string URL = "ui://3xs7lfyxo0de16";

		public static UI_AddCount CreateInstance()
		{
			return (UI_AddCount)UIPackage.CreateObject("UI_Arena","AddCount");
		}

		public UI_AddCount()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgIcom = (GImage)this.GetChildAt(0);
			m_txtCount = (GTextField)this.GetChildAt(1);
			m_btnAddCount = (GComponent)this.GetChildAt(2);
		}
	}
}