/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_JueXingShuXingItem : GComponent
	{
		public GTextField m_Type;
		public GTextField m_oldnumber;
		public GTextField m_newnumber;

		public const string URL = "ui://8u3gv94nhr711f";

		public static UI_JueXingShuXingItem CreateInstance()
		{
			return (UI_JueXingShuXingItem)UIPackage.CreateObject("UI_Equip","JueXingShuXingItem");
		}

		public UI_JueXingShuXingItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Type = (GTextField)this.GetChildAt(0);
			m_oldnumber = (GTextField)this.GetChildAt(1);
			m_newnumber = (GTextField)this.GetChildAt(2);
		}
	}
}