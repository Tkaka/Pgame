/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_ShenQi
{
	public partial class UI_shenQiItem : GComponent
	{
		public GImage m_showBoardIcon;
		public GImage m_wenHaoIcon;
		public GLoader m_shenQiIconLoader;
		public GImage m_selectIcon;
		public GImage m_lockIcon;
		public GGraph m_toucher;

		public const string URL = "ui://bi2nkn43fd9i9";

		public static UI_shenQiItem CreateInstance()
		{
			return (UI_shenQiItem)UIPackage.CreateObject("UI_ShenQi","shenQiItem");
		}

		public UI_shenQiItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_showBoardIcon = (GImage)this.GetChildAt(1);
			m_wenHaoIcon = (GImage)this.GetChildAt(2);
			m_shenQiIconLoader = (GLoader)this.GetChildAt(3);
			m_selectIcon = (GImage)this.GetChildAt(4);
			m_lockIcon = (GImage)this.GetChildAt(5);
			m_toucher = (GGraph)this.GetChildAt(6);
		}
	}
}