/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_RwardCell1 : GComponent
	{
		public GLoader m_bg;
		public GTextField m_txtQuJian;
		public GList m_rewardList;
		public GGroup m_gYiLing;
		public GGroup m_gKeLing;

		public const string URL = "ui://3xs7lfyxgawd1v";

		public static UI_RwardCell1 CreateInstance()
		{
			return (UI_RwardCell1)UIPackage.CreateObject("UI_Arena","RwardCell1");
		}

		public UI_RwardCell1()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bg = (GLoader)this.GetChildAt(0);
			m_txtQuJian = (GTextField)this.GetChildAt(2);
			m_rewardList = (GList)this.GetChildAt(4);
			m_gYiLing = (GGroup)this.GetChildAt(7);
			m_gKeLing = (GGroup)this.GetChildAt(10);
		}
	}
}