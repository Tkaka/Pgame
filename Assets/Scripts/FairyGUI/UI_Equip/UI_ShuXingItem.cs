/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_ShuXingItem : GComponent
	{
		public GTextField m_Type;
		public GTextField m_Data;

		public const string URL = "ui://8u3gv94ngrft1d";

		public static UI_ShuXingItem CreateInstance()
		{
			return (UI_ShuXingItem)UIPackage.CreateObject("UI_Equip","ShuXingItem");
		}

		public UI_ShuXingItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Type = (GTextField)this.GetChildAt(0);
			m_Data = (GTextField)this.GetChildAt(1);
		}
	}
}