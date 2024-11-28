/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TongXiangGuan
{
	public partial class UI_TongXiangItem : GComponent
	{
		public GGraph m_modelLoader;
		public GTextField m_nameLabel;
		public GTextField m_colorNameLabel;
		public GGraph m_toucher;
		public GTextField m_buyTipLabel;

		public const string URL = "ui://ansp6fm5lni7i";

		public static UI_TongXiangItem CreateInstance()
		{
			return (UI_TongXiangItem)UIPackage.CreateObject("UI_TongXiangGuan","TongXiangItem");
		}

		public UI_TongXiangItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_modelLoader = (GGraph)this.GetChildAt(0);
			m_nameLabel = (GTextField)this.GetChildAt(1);
			m_colorNameLabel = (GTextField)this.GetChildAt(2);
			m_toucher = (GGraph)this.GetChildAt(3);
			m_buyTipLabel = (GTextField)this.GetChildAt(4);
		}
	}
}