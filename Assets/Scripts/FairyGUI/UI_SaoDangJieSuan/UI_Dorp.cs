/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_Dorp : GComponent
	{
		public GList m_List;

		public const string URL = "ui://34cd5b4hkgjx10";

		public static UI_Dorp CreateInstance()
		{
			return (UI_Dorp)UIPackage.CreateObject("UI_SaoDangJieSuan","Dorp");
		}

		public UI_Dorp()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_List = (GList)this.GetChildAt(0);
		}
	}
}