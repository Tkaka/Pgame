/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBoss
{
	public partial class UI_passRankItem : GComponent
	{
		public GLoader m_rankLoader;
		public GTextField m_rankLabel;
		public GLoader m_unionIconLoader;
		public GTextField m_unionNameLabel;
		public GTextField m_passTimeLabel;

		public const string URL = "ui://u2d86ulcjg9f2";

		public static UI_passRankItem CreateInstance()
		{
			return (UI_passRankItem)UIPackage.CreateObject("UI_GuildBoss","passRankItem");
		}

		public UI_passRankItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_rankLoader = (GLoader)this.GetChildAt(1);
			m_rankLabel = (GTextField)this.GetChildAt(2);
			m_unionIconLoader = (GLoader)this.GetChildAt(3);
			m_unionNameLabel = (GTextField)this.GetChildAt(4);
			m_passTimeLabel = (GTextField)this.GetChildAt(5);
		}
	}
}