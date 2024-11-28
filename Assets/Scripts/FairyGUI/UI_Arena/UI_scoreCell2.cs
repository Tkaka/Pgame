/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_scoreCell2 : GComponent
	{
		public GTextField m_txtScore;
		public GList m_rewardList;
		public GGroup m_gWeiWangCheng;
		public GGroup m_gKeLingQu;
		public GGroup m_gComplete;

		public const string URL = "ui://3xs7lfyxehrw25";

		public static UI_scoreCell2 CreateInstance()
		{
			return (UI_scoreCell2)UIPackage.CreateObject("UI_Arena","scoreCell2");
		}

		public UI_scoreCell2()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtScore = (GTextField)this.GetChildAt(2);
			m_rewardList = (GList)this.GetChildAt(3);
			m_gWeiWangCheng = (GGroup)this.GetChildAt(6);
			m_gKeLingQu = (GGroup)this.GetChildAt(9);
			m_gComplete = (GGroup)this.GetChildAt(12);
		}
	}
}