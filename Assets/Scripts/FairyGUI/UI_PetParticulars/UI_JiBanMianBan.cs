/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_PetParticulars
{
	public partial class UI_JiBanMianBan : GComponent
	{
		public GImage m_bg;
		public GList m_SuMingList;
		public GTextField m_name;

		public const string URL = "ui://rn5o3g4tfzr66";

		public static UI_JiBanMianBan CreateInstance()
		{
			return (UI_JiBanMianBan)UIPackage.CreateObject("UI_PetParticulars","JiBanMianBan");
		}

		public UI_JiBanMianBan()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_bg = (GImage)this.GetChildAt(0);
			m_SuMingList = (GList)this.GetChildAt(2);
			m_name = (GTextField)this.GetChildAt(3);
		}
	}
}