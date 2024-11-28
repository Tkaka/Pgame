/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Level
{
	public partial class UI_quickJumpChapterItem : GComponent
	{
		public GTextField m_chapterNumLabel;
		public GTextField m_chapterName;
		public GList m_actList;

		public const string URL = "ui://z04ymz0ekb9x1l";

		public static UI_quickJumpChapterItem CreateInstance()
		{
			return (UI_quickJumpChapterItem)UIPackage.CreateObject("UI_Level","quickJumpChapterItem");
		}

		public UI_quickJumpChapterItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_chapterNumLabel = (GTextField)this.GetChildAt(2);
			m_chapterName = (GTextField)this.GetChildAt(4);
			m_actList = (GList)this.GetChildAt(5);
		}
	}
}