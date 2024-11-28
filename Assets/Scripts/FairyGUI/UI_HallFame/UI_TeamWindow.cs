/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_HallFame
{
	public partial class UI_TeamWindow : GComponent
	{
		public Controller m_TeamPet;
		public GImage m_QiPao;
		public GGraph m_MoXing;
		public GTextField m_TeamName;
		public GButton m_Close;
		public UI_HaoGanDuMianBan m_HF_haogandu;
		public GGraph m_last;
		public GGraph m_next;
		public GList m_PetList;
		public GTextField m_qipanyuyan;
		public GTextField m_Name;
		public Transition m_MoXingChuChang;
		public Transition m_jiantou;

		public const string URL = "ui://yo5kunkiux5q3";

		public static UI_TeamWindow CreateInstance()
		{
			return (UI_TeamWindow)UIPackage.CreateObject("UI_HallFame","TeamWindow");
		}

		public UI_TeamWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_TeamPet = this.GetControllerAt(0);
			m_QiPao = (GImage)this.GetChildAt(1);
			m_MoXing = (GGraph)this.GetChildAt(2);
			m_TeamName = (GTextField)this.GetChildAt(4);
			m_Close = (GButton)this.GetChildAt(5);
			m_HF_haogandu = (UI_HaoGanDuMianBan)this.GetChildAt(6);
			m_last = (GGraph)this.GetChildAt(7);
			m_next = (GGraph)this.GetChildAt(8);
			m_PetList = (GList)this.GetChildAt(9);
			m_qipanyuyan = (GTextField)this.GetChildAt(10);
			m_Name = (GTextField)this.GetChildAt(13);
			m_MoXingChuChang = this.GetTransitionAt(0);
			m_jiantou = this.GetTransitionAt(1);
		}
	}
}