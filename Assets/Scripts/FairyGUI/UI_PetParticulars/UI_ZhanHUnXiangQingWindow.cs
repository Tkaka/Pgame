/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_PetParticulars
{
	public partial class UI_ZhanHUnXiangQingWindow : GComponent
	{
		public GImage m_beijing;
		public GLoader m_toixaing;
		public GButton m_closeBtn;
		public GTextField m_name;
		public GTextField m_level;
		public GImage m_miaoshubeijing;
		public GTextField m_MiaoShu;

		public const string URL = "ui://rn5o3g4tkv1shp";

		public static UI_ZhanHUnXiangQingWindow CreateInstance()
		{
			return (UI_ZhanHUnXiangQingWindow)UIPackage.CreateObject("UI_PetParticulars","ZhanHUnXiangQingWindow");
		}

		public UI_ZhanHUnXiangQingWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_beijing = (GImage)this.GetChildAt(1);
			m_toixaing = (GLoader)this.GetChildAt(3);
			m_closeBtn = (GButton)this.GetChildAt(4);
			m_name = (GTextField)this.GetChildAt(5);
			m_level = (GTextField)this.GetChildAt(6);
			m_miaoshubeijing = (GImage)this.GetChildAt(7);
			m_MiaoShu = (GTextField)this.GetChildAt(8);
		}
	}
}