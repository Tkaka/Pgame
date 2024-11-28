/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_PetParticulars
{
	public partial class UI_DingWeiMianBan : GComponent
	{
		public GImage m_miaoshuditu;
		public GTextField m_taitou;
		public GTextField m_MiaoShu;

		public const string URL = "ui://rn5o3g4tph1wc";

		public static UI_DingWeiMianBan CreateInstance()
		{
			return (UI_DingWeiMianBan)UIPackage.CreateObject("UI_PetParticulars","DingWeiMianBan");
		}

		public UI_DingWeiMianBan()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_miaoshuditu = (GImage)this.GetChildAt(1);
			m_taitou = (GTextField)this.GetChildAt(2);
			m_MiaoShu = (GTextField)this.GetChildAt(3);
		}
	}
}