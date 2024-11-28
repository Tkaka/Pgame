/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_WorldMap
{
	public partial class UI_MapItem02 : GComponent
	{
		public GLoader m_mapLoader;

		public const string URL = "ui://k1lxoe22m8edb";

		public static UI_MapItem02 CreateInstance()
		{
			return (UI_MapItem02)UIPackage.CreateObject("UI_WorldMap","MapItem02");
		}

		public UI_MapItem02()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mapLoader = (GLoader)this.GetChildAt(0);
		}
	}
}