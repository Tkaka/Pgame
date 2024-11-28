/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_JuXingCaiLiao : GComponent
	{
		public GLoader m_PinJie;
		public GLoader m_TouXiang;
		public GTextField m_number;
		public GImage m_Jiaaotubiao;
		public GGroup m_Jiaao;

		public const string URL = "ui://8u3gv94nvds0s";

		public static UI_JuXingCaiLiao CreateInstance()
		{
			return (UI_JuXingCaiLiao)UIPackage.CreateObject("UI_Equip","JuXingCaiLiao");
		}

		public UI_JuXingCaiLiao()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_PinJie = (GLoader)this.GetChildAt(0);
			m_TouXiang = (GLoader)this.GetChildAt(1);
			m_number = (GTextField)this.GetChildAt(2);
			m_Jiaaotubiao = (GImage)this.GetChildAt(4);
			m_Jiaao = (GGroup)this.GetChildAt(5);
		}
	}
}