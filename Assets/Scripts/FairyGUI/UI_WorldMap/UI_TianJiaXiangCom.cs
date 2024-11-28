/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_WorldMap
{
	public partial class UI_TianJiaXiangCom : GComponent
	{
		public GTextField m_title;

		public const string URL = "ui://k1lxoe22m8edd";

		public static UI_TianJiaXiangCom CreateInstance()
		{
			return (UI_TianJiaXiangCom)UIPackage.CreateObject("UI_WorldMap","TianJiaXiangCom");
		}

		public UI_TianJiaXiangCom()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_title = (GTextField)this.GetChildAt(2);
		}
	}
}