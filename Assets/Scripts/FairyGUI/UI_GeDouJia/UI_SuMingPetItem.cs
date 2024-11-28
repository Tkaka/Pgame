/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_SuMingPetItem : GComponent
	{
		public GLoader m_TouXiang;
		public GImage m_JiaHao;

		public const string URL = "ui://4asqm7awn0x05o";

		public static UI_SuMingPetItem CreateInstance()
		{
			return (UI_SuMingPetItem)UIPackage.CreateObject("UI_GeDouJia","SuMingPetItem");
		}

		public UI_SuMingPetItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_TouXiang = (GLoader)this.GetChildAt(1);
			m_JiaHao = (GImage)this.GetChildAt(2);
		}
	}
}