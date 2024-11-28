/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_commonPropItem : GComponent
	{
		public GLoader m_framLoader;
		public GLoader m_iconLoader;
		public GTextField m_numLabel;
		public GImage m_fragmentIcon;
		public GGraph m_toucher;

		public const string URL = "ui://1wdkrtiusi7j1r";

		public static UI_commonPropItem CreateInstance()
		{
			return (UI_commonPropItem)UIPackage.CreateObject("UI_UltemateTrain","commonPropItem");
		}

		public UI_commonPropItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_framLoader = (GLoader)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_numLabel = (GTextField)this.GetChildAt(2);
			m_fragmentIcon = (GImage)this.GetChildAt(3);
			m_toucher = (GGraph)this.GetChildAt(4);
		}
	}
}