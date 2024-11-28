/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_ManXingShuXingItem : GComponent
	{
		public GTextField m_Name;
		public GTextField m_Number;

		public const string URL = "ui://8u3gv94necma1i";

		public static UI_ManXingShuXingItem CreateInstance()
		{
			return (UI_ManXingShuXingItem)UIPackage.CreateObject("UI_Equip","ManXingShuXingItem");
		}

		public UI_ManXingShuXingItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Name = (GTextField)this.GetChildAt(0);
			m_Number = (GTextField)this.GetChildAt(1);
		}
	}
}