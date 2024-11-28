/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_objDesItem : GComponent
	{
		public GTextField m_txtDes;

		public const string URL = "ui://028ppdzhcrhidr";

		public static UI_objDesItem CreateInstance()
		{
			return (UI_objDesItem)UIPackage.CreateObject("UI_Battle","objDesItem");
		}

		public UI_objDesItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtDes = (GTextField)this.GetChildAt(0);
		}
	}
}