/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_floorTipItem : GComponent
	{
		public GLoader m_cirleLoader;
		public GTextField m_floorNum;
		public GTextField m_typeLabel;

		public const string URL = "ui://1wdkrtiuw0hu8";

		public static UI_floorTipItem CreateInstance()
		{
			return (UI_floorTipItem)UIPackage.CreateObject("UI_UltemateTrain","floorTipItem");
		}

		public UI_floorTipItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_cirleLoader = (GLoader)this.GetChildAt(0);
			m_floorNum = (GTextField)this.GetChildAt(1);
			m_typeLabel = (GTextField)this.GetChildAt(2);
		}
	}
}