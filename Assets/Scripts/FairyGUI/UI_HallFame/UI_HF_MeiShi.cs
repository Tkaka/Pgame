/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_HallFame
{
	public partial class UI_HF_MeiShi : GComponent
	{
		public GImage m_BeiJing;
		public GTextField m_Name;
		public GLoader m_MeiShiIcon;
		public GTextField m_number;

		public const string URL = "ui://yo5kunkik5jic";

		public static UI_HF_MeiShi CreateInstance()
		{
			return (UI_HF_MeiShi)UIPackage.CreateObject("UI_HallFame","HF_MeiShi");
		}

		public UI_HF_MeiShi()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_BeiJing = (GImage)this.GetChildAt(0);
			m_Name = (GTextField)this.GetChildAt(1);
			m_MeiShiIcon = (GLoader)this.GetChildAt(2);
			m_number = (GTextField)this.GetChildAt(3);
		}
	}
}