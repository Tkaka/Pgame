/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_attributeChangeList : GComponent
	{
		public GList m_attributeList;

		public const string URL = "ui://8u3gv94nqwou1e";

		public static UI_attributeChangeList CreateInstance()
		{
			return (UI_attributeChangeList)UIPackage.CreateObject("UI_Equip","attributeChangeList");
		}

		public UI_attributeChangeList()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_attributeList = (GList)this.GetChildAt(0);
		}
	}
}