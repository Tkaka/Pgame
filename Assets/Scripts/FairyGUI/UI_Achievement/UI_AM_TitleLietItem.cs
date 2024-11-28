/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Achievement
{
	public partial class UI_AM_TitleLietItem : GComponent
	{
		public GLoader m_TouXiang;
		public GTextField m_Name;
		public GTextField m_number;
		public GGroup m_vaule;

		public const string URL = "ui://xpd8f6j0qkz1k";

		public static UI_AM_TitleLietItem CreateInstance()
		{
			return (UI_AM_TitleLietItem)UIPackage.CreateObject("UI_Achievement","AM_TitleLietItem");
		}

		public UI_AM_TitleLietItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_TouXiang = (GLoader)this.GetChildAt(1);
			m_Name = (GTextField)this.GetChildAt(2);
			m_number = (GTextField)this.GetChildAt(5);
			m_vaule = (GGroup)this.GetChildAt(8);
		}
	}
}