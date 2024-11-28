/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Activity
{
	public partial class UI_selectDifficultyPV : GComponent
	{
		public GButton m_forkBtn;
		public GList m_difficultList;

		public const string URL = "ui://zwmeip9up1r81n";

		public static UI_selectDifficultyPV CreateInstance()
		{
			return (UI_selectDifficultyPV)UIPackage.CreateObject("UI_Activity","selectDifficultyPV");
		}

		public UI_selectDifficultyPV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_forkBtn = (GButton)this.GetChildAt(3);
			m_difficultList = (GList)this.GetChildAt(4);
		}
	}
}