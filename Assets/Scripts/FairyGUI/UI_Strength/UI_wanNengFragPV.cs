/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Strength
{
	public partial class UI_wanNengFragPV : GComponent
	{
		public GButton m_closeBtn;
		public GSlider m_slider;
		public GButton m_wnIcon;
		public GButton m_targetIcon;
		public GButton m_transBtn;
		public GButton m_addBtn;
		public GButton m_reduceBtn;

		public const string URL = "ui://qnd9tp35lxvz4n";

		public static UI_wanNengFragPV CreateInstance()
		{
			return (UI_wanNengFragPV)UIPackage.CreateObject("UI_Strength","wanNengFragPV");
		}

		public UI_wanNengFragPV()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_closeBtn = (GButton)this.GetChildAt(5);
			m_slider = (GSlider)this.GetChildAt(7);
			m_wnIcon = (GButton)this.GetChildAt(8);
			m_targetIcon = (GButton)this.GetChildAt(9);
			m_transBtn = (GButton)this.GetChildAt(11);
			m_addBtn = (GButton)this.GetChildAt(12);
			m_reduceBtn = (GButton)this.GetChildAt(13);
		}
	}
}