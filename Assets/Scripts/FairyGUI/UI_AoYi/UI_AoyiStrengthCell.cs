/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_AoYi
{
	public partial class UI_AoyiStrengthCell : GComponent
	{
		public UI_AoyiCommonItem m_aoyiItem;
		public GButton m_checkBox;

		public const string URL = "ui://vexa0xrygc7j1j";

		public static UI_AoyiStrengthCell CreateInstance()
		{
			return (UI_AoyiStrengthCell)UIPackage.CreateObject("UI_AoYi","AoyiStrengthCell");
		}

		public UI_AoyiStrengthCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_aoyiItem = (UI_AoyiCommonItem)this.GetChildAt(0);
			m_checkBox = (GButton)this.GetChildAt(1);
		}
	}
}