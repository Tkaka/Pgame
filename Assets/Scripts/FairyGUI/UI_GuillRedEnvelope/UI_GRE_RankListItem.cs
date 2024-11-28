/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuillRedEnvelope
{
	public partial class UI_GRE_RankListItem : GComponent
	{
		public GLoader m_paiming_icon;
		public GTextField m_paiming_number;
		public GLoader m_touxiang_icon;
		public GTextField m_name;
		public GTextField m_geshu;
		public GTextField m_Fa_zongliang;
		public GTextField m_de_shuliang;

		public const string URL = "ui://r816m4tmmf5nm";

		public static UI_GRE_RankListItem CreateInstance()
		{
			return (UI_GRE_RankListItem)UIPackage.CreateObject("UI_GuillRedEnvelope","GRE_RankListItem");
		}

		public UI_GRE_RankListItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_paiming_icon = (GLoader)this.GetChildAt(1);
			m_paiming_number = (GTextField)this.GetChildAt(2);
			m_touxiang_icon = (GLoader)this.GetChildAt(4);
			m_name = (GTextField)this.GetChildAt(5);
			m_geshu = (GTextField)this.GetChildAt(6);
			m_Fa_zongliang = (GTextField)this.GetChildAt(7);
			m_de_shuliang = (GTextField)this.GetChildAt(8);
		}
	}
}