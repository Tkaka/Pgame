/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_RwardCell2 : GComponent
	{
		public GLoader m_bg;
		public GTextField m_txtQuJian;
		public GList m_rewardList;

		public const string URL = "ui://3xs7lfyxgawd1z";

		public static UI_RwardCell2 CreateInstance()
		{
			return (UI_RwardCell2)UIPackage.CreateObject("UI_Arena","RwardCell2");
		}

		public UI_RwardCell2()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bg = (GLoader)this.GetChildAt(0);
			m_txtQuJian = (GTextField)this.GetChildAt(2);
			m_rewardList = (GList)this.GetChildAt(4);
		}
	}
}