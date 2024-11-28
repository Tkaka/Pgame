/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildBattle
{
	public partial class UI_zhanKuanTipItem : GComponent
	{
		public GTextField m_tipLabel;

		public const string URL = "ui://xj95784riouo2x";

		public static UI_zhanKuanTipItem CreateInstance()
		{
			return (UI_zhanKuanTipItem)UIPackage.CreateObject("UI_GuildBattle","zhanKuanTipItem");
		}

		public UI_zhanKuanTipItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_tipLabel = (GTextField)this.GetChildAt(1);
		}
	}
}