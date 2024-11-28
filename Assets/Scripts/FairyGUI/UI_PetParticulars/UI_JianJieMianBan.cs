/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_PetParticulars
{
	public partial class UI_JianJieMianBan : GComponent
	{
		public GImage m_miaoshuditu;
		public GTextField m_taitou;
		public GTextField m_jianjie;

		public const string URL = "ui://rn5o3g4tfzr65";

		public static UI_JianJieMianBan CreateInstance()
		{
			return (UI_JianJieMianBan)UIPackage.CreateObject("UI_PetParticulars","JianJieMianBan");
		}

		public UI_JianJieMianBan()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_miaoshuditu = (GImage)this.GetChildAt(1);
			m_taitou = (GTextField)this.GetChildAt(2);
			m_jianjie = (GTextField)this.GetChildAt(3);
		}
	}
}