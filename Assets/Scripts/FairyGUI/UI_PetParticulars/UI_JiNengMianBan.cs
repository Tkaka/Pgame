/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_PetParticulars
{
	public partial class UI_JiNengMianBan : GComponent
	{
		public GList m_JiNengList;

		public const string URL = "ui://rn5o3g4tfzr67";

		public static UI_JiNengMianBan CreateInstance()
		{
			return (UI_JiNengMianBan)UIPackage.CreateObject("UI_PetParticulars","JiNengMianBan");
		}

		public UI_JiNengMianBan()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_JiNengList = (GList)this.GetChildAt(2);
		}
	}
}