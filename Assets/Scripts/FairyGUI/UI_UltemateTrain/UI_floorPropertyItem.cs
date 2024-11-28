/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_floorPropertyItem : GComponent
	{
		public GLoader m_typeIcon;
		public GTextField m_valueLabel;
		public GTextField m_starNumLabel;
		public GTextField m_decriptLabel;
		public GImage m_buyIcon;
		public GLoader m_nameLoader;
		public GGraph m_toucher;

		public const string URL = "ui://1wdkrtiuw0hux";

		public static UI_floorPropertyItem CreateInstance()
		{
			return (UI_floorPropertyItem)UIPackage.CreateObject("UI_UltemateTrain","floorPropertyItem");
		}

		public UI_floorPropertyItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_typeIcon = (GLoader)this.GetChildAt(1);
			m_valueLabel = (GTextField)this.GetChildAt(3);
			m_starNumLabel = (GTextField)this.GetChildAt(6);
			m_decriptLabel = (GTextField)this.GetChildAt(7);
			m_buyIcon = (GImage)this.GetChildAt(8);
			m_nameLoader = (GLoader)this.GetChildAt(9);
			m_toucher = (GGraph)this.GetChildAt(10);
		}
	}
}