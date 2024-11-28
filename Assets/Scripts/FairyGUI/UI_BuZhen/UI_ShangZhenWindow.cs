/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_BuZhen
{
	public partial class UI_ShangZhenWindow : GComponent
	{
		public GList m_ShanZhenList;
		public GComponent m_taitou;
		public GGraph m_Close;

		public const string URL = "ui://z0csav43mwyx1e";

		public static UI_ShangZhenWindow CreateInstance()
		{
			return (UI_ShangZhenWindow)UIPackage.CreateObject("UI_BuZhen","ShangZhenWindow");
		}

		public UI_ShangZhenWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ShanZhenList = (GList)this.GetChildAt(3);
			m_taitou = (GComponent)this.GetChildAt(5);
			m_Close = (GGraph)this.GetChildAt(6);
		}
	}
}