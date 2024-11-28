/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBattle
{
	public partial class UI_baoMingDetailItem : GComponent
	{
		public GComponent m_headIcon;
		public GTextField m_nameLabel;
		public GTextField m_levelLabel;
		public GTextField m_jobPosLabel;
		public GTextField m_yiBaoMingTip;
		public GTextField m_weiBaoMingTip;
		public GButton m_noticeBtn;

		public const string URL = "ui://xj95784rpfl42q";

		public static UI_baoMingDetailItem CreateInstance()
		{
			return (UI_baoMingDetailItem)UIPackage.CreateObject("UI_GuildBattle","baoMingDetailItem");
		}

		public UI_baoMingDetailItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_headIcon = (GComponent)this.GetChildAt(1);
			m_nameLabel = (GTextField)this.GetChildAt(2);
			m_levelLabel = (GTextField)this.GetChildAt(3);
			m_jobPosLabel = (GTextField)this.GetChildAt(4);
			m_yiBaoMingTip = (GTextField)this.GetChildAt(5);
			m_weiBaoMingTip = (GTextField)this.GetChildAt(6);
			m_noticeBtn = (GButton)this.GetChildAt(7);
		}
	}
}