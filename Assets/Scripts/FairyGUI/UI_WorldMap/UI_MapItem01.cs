/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_WorldMap
{
	public partial class UI_MapItem01 : GComponent
	{
		public GLoader m_mapLoader;
		public GLoader m_outline01;

		public const string URL = "ui://k1lxoe22m8eda";

		public static UI_MapItem01 CreateInstance()
		{
			return (UI_MapItem01)UIPackage.CreateObject("UI_WorldMap","MapItem01");
		}

		public UI_MapItem01()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mapLoader = (GLoader)this.GetChildAt(0);
			m_outline01 = (GLoader)this.GetChildAt(1);
		}
	}
}