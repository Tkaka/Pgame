/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_ayIocnList : GComponent
	{
		public GList m_iconList;

		public const string URL = "ui://vexa0xryp3pof";

		public static UI_ayIocnList CreateInstance()
		{
			return (UI_ayIocnList)UIPackage.CreateObject("UI_AoYi","ayIocnList");
		}

		public UI_ayIocnList()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_iconList = (GList)this.GetChildAt(0);
		}
	}
}