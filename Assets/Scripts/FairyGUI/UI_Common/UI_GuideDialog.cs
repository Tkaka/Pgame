/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_GuideDialog : GComponent
	{
		public GImage m_rightBg;
		public GTextField m_txtRightName;
		public GImage m_leftBg;
		public GTextField m_txtLeftName;
		public GRichTextField m_txtContent;

		public const string URL = "ui://42sthz3tp9j2xqy";

		public static UI_GuideDialog CreateInstance()
		{
			return (UI_GuideDialog)UIPackage.CreateObject("UI_Common","GuideDialog");
		}

		public UI_GuideDialog()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_rightBg = (GImage)this.GetChildAt(1);
			m_txtRightName = (GTextField)this.GetChildAt(2);
			m_leftBg = (GImage)this.GetChildAt(3);
			m_txtLeftName = (GTextField)this.GetChildAt(4);
			m_txtContent = (GRichTextField)this.GetChildAt(6);
		}
	}
}