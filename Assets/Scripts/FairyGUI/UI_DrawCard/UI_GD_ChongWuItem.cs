/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DrawCard
{
	public partial class UI_GD_ChongWuItem : GComponent
	{
		public GList m_petList;

		public const string URL = "ui://zy7t2yegp5h421";

		public static UI_GD_ChongWuItem CreateInstance()
		{
			return (UI_GD_ChongWuItem)UIPackage.CreateObject("UI_DrawCard","GD_ChongWuItem");
		}

		public UI_GD_ChongWuItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_petList = (GList)this.GetChildAt(0);
		}
	}
}