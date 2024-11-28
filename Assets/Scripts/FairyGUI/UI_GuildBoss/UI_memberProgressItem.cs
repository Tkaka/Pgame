/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_memberProgressItem : GComponent
	{
		public GLoader m_boardLoader;
		public GLoader m_iconLoader;
		public GTextField m_nameLabel;
		public GTextField m_levelLabel;
		public GTextField m_positionLabel;
		public GTextField m_progressLabel;
		public GProgressBar m_memberProgress;

		public const string URL = "ui://u2d86ulcjg9fb";

		public static UI_memberProgressItem CreateInstance()
		{
			return (UI_memberProgressItem)UIPackage.CreateObject("UI_GuildBoss","memberProgressItem");
		}

		public UI_memberProgressItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_boardLoader = (GLoader)this.GetChildAt(1);
			m_iconLoader = (GLoader)this.GetChildAt(2);
			m_nameLabel = (GTextField)this.GetChildAt(3);
			m_levelLabel = (GTextField)this.GetChildAt(4);
			m_positionLabel = (GTextField)this.GetChildAt(5);
			m_progressLabel = (GTextField)this.GetChildAt(7);
			m_memberProgress = (GProgressBar)this.GetChildAt(8);
		}
	}
}