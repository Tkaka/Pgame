/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_JinJiIconCell : GComponent
	{
		public GLoader m_borderLoader;
		public GLoader m_iconLoader;
		public GTextField m_numTxt;
		public GImage m_fragIcon;

		public const string URL = "ui://3xs7lfyxgawd1w";

		public static UI_JinJiIconCell CreateInstance()
		{
			return (UI_JinJiIconCell)UIPackage.CreateObject("UI_Arena","JinJiIconCell");
		}

		public UI_JinJiIconCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_borderLoader = (GLoader)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_numTxt = (GTextField)this.GetChildAt(2);
			m_fragIcon = (GImage)this.GetChildAt(3);
		}
	}
}