/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_eliteChapterCell : GComponent
	{
		public GList m_CharpterList;
		public GTextField m_txtZhangJie;
		public GTextField m_txtZhangJieName;

		public const string URL = "ui://34cd5b4hjr2f2b";

		public static UI_eliteChapterCell CreateInstance()
		{
			return (UI_eliteChapterCell)UIPackage.CreateObject("UI_SaoDangJieSuan","eliteChapterCell");
		}

		public UI_eliteChapterCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_CharpterList = (GList)this.GetChildAt(1);
			m_txtZhangJie = (GTextField)this.GetChildAt(3);
			m_txtZhangJieName = (GTextField)this.GetChildAt(7);
		}
	}
}