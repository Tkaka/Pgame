/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_nomalStarList : GComponent
	{
		public GImage m_cmpGrayKuang;
		public GImage m_cmdLightKuang;
		public GTextField m_cmpDesc;
		public GImage m_pingFengGrayKuang;
		public GImage m_pingfengLightKuang;
		public GTextField m_comboDesc;
		public GImage m_aliveGrayKuang;
		public GImage m_aliveLightKuang;
		public GTextField m_aliveDesc;
		public GComponent m_cmpStar;
		public GComponent m_pingFenStar;
		public GComponent m_aliveStar;

		public const string URL = "ui://028ppdzhkvqksgk";

		public static UI_nomalStarList CreateInstance()
		{
			return (UI_nomalStarList)UIPackage.CreateObject("UI_Battle","nomalStarList");
		}

		public UI_nomalStarList()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_cmpGrayKuang = (GImage)this.GetChildAt(0);
			m_cmdLightKuang = (GImage)this.GetChildAt(1);
			m_cmpDesc = (GTextField)this.GetChildAt(2);
			m_pingFengGrayKuang = (GImage)this.GetChildAt(3);
			m_pingfengLightKuang = (GImage)this.GetChildAt(4);
			m_comboDesc = (GTextField)this.GetChildAt(5);
			m_aliveGrayKuang = (GImage)this.GetChildAt(6);
			m_aliveLightKuang = (GImage)this.GetChildAt(7);
			m_aliveDesc = (GTextField)this.GetChildAt(8);
			m_cmpStar = (GComponent)this.GetChildAt(9);
			m_pingFenStar = (GComponent)this.GetChildAt(10);
			m_aliveStar = (GComponent)this.GetChildAt(11);
		}
	}
}