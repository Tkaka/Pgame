/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_HallFame
{
	public partial class UI_HF_HaoGanDengJi : GComponent
	{
		public GImage m_HF_BeiJing;
		public GLoader m_HF_HaoGanDuIcon;
		public GTextField m_name;

		public const string URL = "ui://yo5kunkik5ji8";

		public static UI_HF_HaoGanDengJi CreateInstance()
		{
			return (UI_HF_HaoGanDengJi)UIPackage.CreateObject("UI_HallFame","HF_HaoGanDengJi");
		}

		public UI_HF_HaoGanDengJi()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_HF_BeiJing = (GImage)this.GetChildAt(0);
			m_HF_HaoGanDuIcon = (GLoader)this.GetChildAt(1);
			m_name = (GTextField)this.GetChildAt(2);
		}
	}
}