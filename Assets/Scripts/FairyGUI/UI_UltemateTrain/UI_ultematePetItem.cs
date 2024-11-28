/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_ultematePetItem : GComponent
	{
		public GLoader m_boardLoader;
		public GLoader m_iconLoader;
		public GComponent m_starList;
		public GTextField m_levelLabel;
		public GProgressBar m_hpProgress;
		public GProgressBar m_energyProgress;
		public GGroup m_progressGroup;
		public GGroup m_petGroup;
		public GGroup m_noPetGroup;
		public GGraph m_toucher;

		public const string URL = "ui://1wdkrtiuw0hu1a";

		public static UI_ultematePetItem CreateInstance()
		{
			return (UI_ultematePetItem)UIPackage.CreateObject("UI_UltemateTrain","ultematePetItem");
		}

		public UI_ultematePetItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boardLoader = (GLoader)this.GetChildAt(0);
			m_iconLoader = (GLoader)this.GetChildAt(1);
			m_starList = (GComponent)this.GetChildAt(2);
			m_levelLabel = (GTextField)this.GetChildAt(3);
			m_hpProgress = (GProgressBar)this.GetChildAt(4);
			m_energyProgress = (GProgressBar)this.GetChildAt(5);
			m_progressGroup = (GGroup)this.GetChildAt(6);
			m_petGroup = (GGroup)this.GetChildAt(7);
			m_noPetGroup = (GGroup)this.GetChildAt(10);
			m_toucher = (GGraph)this.GetChildAt(11);
		}
	}
}