/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_UltimateSucessWindow : GComponent
	{
		public GComponent m_imgBg;
		public GTextField m_objTitle;
		public GTextField m_txtStarNum;
		public GTextField m_txtbaseScore;
		public GTextField m_txtRate;
		public GTextField m_txtTotalScore;
		public GImage m_star1;
		public GImage m_star2;
		public GImage m_star3;

		public const string URL = "ui://028ppdzhumw7ez";

		public static UI_UltimateSucessWindow CreateInstance()
		{
			return (UI_UltimateSucessWindow)UIPackage.CreateObject("UI_Battle","UltimateSucessWindow");
		}

		public UI_UltimateSucessWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_imgBg = (GComponent)this.GetChildAt(1);
			m_objTitle = (GTextField)this.GetChildAt(4);
			m_txtStarNum = (GTextField)this.GetChildAt(6);
			m_txtbaseScore = (GTextField)this.GetChildAt(8);
			m_txtRate = (GTextField)this.GetChildAt(10);
			m_txtTotalScore = (GTextField)this.GetChildAt(11);
			m_star1 = (GImage)this.GetChildAt(13);
			m_star2 = (GImage)this.GetChildAt(14);
			m_star3 = (GImage)this.GetChildAt(15);
		}
	}
}