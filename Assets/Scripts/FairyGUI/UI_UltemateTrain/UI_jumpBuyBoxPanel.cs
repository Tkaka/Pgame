/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_jumpBuyBoxPanel : GComponent
	{
		public GTextField m_topTipLabel;
		public GList m_jumpBoxList;
		public GTextField m_downTipLabel;
		public GList m_buyTimesList;

		public const string URL = "ui://1wdkrtiuw0hu11";

		public static UI_jumpBuyBoxPanel CreateInstance()
		{
			return (UI_jumpBuyBoxPanel)UIPackage.CreateObject("UI_UltemateTrain","jumpBuyBoxPanel");
		}

		public UI_jumpBuyBoxPanel()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_topTipLabel = (GTextField)this.GetChildAt(1);
			m_jumpBoxList = (GList)this.GetChildAt(2);
			m_downTipLabel = (GTextField)this.GetChildAt(3);
			m_buyTimesList = (GList)this.GetChildAt(4);
		}
	}
}