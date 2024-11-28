/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_CloneTeamFight
{
	public partial class UI_cloneWinBoxItem : GComponent
	{
		public GTextField m_costDiamondLabel;
		public GGroup m_costDiamondGroup;
		public GLoader m_boxIcon;
		public GGraph m_toucher;

		public const string URL = "ui://y12h0jfmlyhnq";

		public static UI_cloneWinBoxItem CreateInstance()
		{
			return (UI_cloneWinBoxItem)UIPackage.CreateObject("UI_CloneTeamFight","cloneWinBoxItem");
		}

		public UI_cloneWinBoxItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_costDiamondLabel = (GTextField)this.GetChildAt(1);
			m_costDiamondGroup = (GGroup)this.GetChildAt(2);
			m_boxIcon = (GLoader)this.GetChildAt(3);
			m_toucher = (GGraph)this.GetChildAt(4);
		}
	}
}