/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_WorldMap
{
	public partial class UI_MapItem03 : GComponent
	{
		public GLoader m_mapLoader;

		public const string URL = "ui://k1lxoe22m8edc";

		public static UI_MapItem03 CreateInstance()
		{
			return (UI_MapItem03)UIPackage.CreateObject("UI_WorldMap","MapItem03");
		}

		public UI_MapItem03()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mapLoader = (GLoader)this.GetChildAt(0);
		}
	}
}