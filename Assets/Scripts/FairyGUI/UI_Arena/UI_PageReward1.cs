/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Arena
{
	public partial class UI_PageReward1 : GComponent
	{
		public GTextField m_txtRank;
		public GList m_rewrdList;
		public GList m_rankRewardList;

		public const string URL = "ui://3xs7lfyxgawd1u";

		public static UI_PageReward1 CreateInstance()
		{
			return (UI_PageReward1)UIPackage.CreateObject("UI_Arena","PageReward1");
		}

		public UI_PageReward1()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_txtRank = (GTextField)this.GetChildAt(2);
			m_rewrdList = (GList)this.GetChildAt(4);
			m_rankRewardList = (GList)this.GetChildAt(5);
		}
	}
}