/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_jumpBuyPropertyPanel : GComponent
	{
		public GList m_propertyList;
		public GTextField m_floorNumLabel;
		public GTextField m_remainStarNumLabel;
		public UI_additionListPanel m_additionListPanel;
		public GImage m_additionLoader;

		public const string URL = "ui://1wdkrtiuw0huz";

		public static UI_jumpBuyPropertyPanel CreateInstance()
		{
			return (UI_jumpBuyPropertyPanel)UIPackage.CreateObject("UI_UltemateTrain","jumpBuyPropertyPanel");
		}

		public UI_jumpBuyPropertyPanel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_propertyList = (GList)this.GetChildAt(7);
			m_floorNumLabel = (GTextField)this.GetChildAt(8);
			m_remainStarNumLabel = (GTextField)this.GetChildAt(11);
			m_additionListPanel = (UI_additionListPanel)this.GetChildAt(12);
			m_additionLoader = (GImage)this.GetChildAt(13);
		}
	}
}