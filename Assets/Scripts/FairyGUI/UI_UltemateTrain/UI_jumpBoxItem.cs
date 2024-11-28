/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_jumpBoxItem : GComponent
	{
		public GTextField m_floorNumLabel;
		public GTextField m_remainTimesLabel;
		public GGraph m_toucher;

		public const string URL = "ui://1wdkrtiuw0hu10";

		public static UI_jumpBoxItem CreateInstance()
		{
			return (UI_jumpBoxItem)UIPackage.CreateObject("UI_UltemateTrain","jumpBoxItem");
		}

		public UI_jumpBoxItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_floorNumLabel = (GTextField)this.GetChildAt(2);
			m_remainTimesLabel = (GTextField)this.GetChildAt(3);
			m_toucher = (GGraph)this.GetChildAt(4);
		}
	}
}