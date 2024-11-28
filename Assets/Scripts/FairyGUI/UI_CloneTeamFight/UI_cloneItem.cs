/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_CloneTeamFight
{
	public partial class UI_cloneItem : GComponent
	{
		public GGraph m_modelPos;
		public GButton m_normalCreateTeamBtn;
		public GButton m_noramlQucikJoinBtn;
		public GGroup m_normalBtnGroup;
		public GButton m_cardCreateTeamBtn;
		public GButton m_cardQucikJoinBtn;
		public GGroup m_cardBtnGroup;
		public GTextField m_indexNum;
		public GTextField m_nameLabel;
		public GTextField m_bubbleLabel;
		public GGroup m_bubbleGroup;

		public const string URL = "ui://y12h0jfmlyhna";

		public static UI_cloneItem CreateInstance()
		{
			return (UI_cloneItem)UIPackage.CreateObject("UI_CloneTeamFight","cloneItem");
		}

		public UI_cloneItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_modelPos = (GGraph)this.GetChildAt(0);
			m_normalCreateTeamBtn = (GButton)this.GetChildAt(2);
			m_noramlQucikJoinBtn = (GButton)this.GetChildAt(3);
			m_normalBtnGroup = (GGroup)this.GetChildAt(4);
			m_cardCreateTeamBtn = (GButton)this.GetChildAt(6);
			m_cardQucikJoinBtn = (GButton)this.GetChildAt(7);
			m_cardBtnGroup = (GGroup)this.GetChildAt(8);
			m_indexNum = (GTextField)this.GetChildAt(9);
			m_nameLabel = (GTextField)this.GetChildAt(11);
			m_bubbleLabel = (GTextField)this.GetChildAt(13);
			m_bubbleGroup = (GGroup)this.GetChildAt(14);
		}
	}
}