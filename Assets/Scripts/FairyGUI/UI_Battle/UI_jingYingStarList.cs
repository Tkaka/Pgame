/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Battle
{
	public partial class UI_jingYingStarList : GComponent
	{
		public GLoader m_cmpStar;
		public GTextField m_cmpDesc;
		public GLoader m_aliveStar;
		public GTextField m_aliveDesc;
		public GLoader m_comboStar;
		public GTextField m_comboDesc;
		public GGroup m_jingyingGroup;

		public const string URL = "ui://028ppdzhkvqksgp";

		public static UI_jingYingStarList CreateInstance()
		{
			return (UI_jingYingStarList)UIPackage.CreateObject("UI_Battle","jingYingStarList");
		}

		public UI_jingYingStarList()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_cmpStar = (GLoader)this.GetChildAt(0);
			m_cmpDesc = (GTextField)this.GetChildAt(2);
			m_aliveStar = (GLoader)this.GetChildAt(3);
			m_aliveDesc = (GTextField)this.GetChildAt(5);
			m_comboStar = (GLoader)this.GetChildAt(6);
			m_comboDesc = (GTextField)this.GetChildAt(8);
			m_jingyingGroup = (GGroup)this.GetChildAt(9);
		}
	}
}