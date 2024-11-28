/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GeDouJia
{
	public partial class UI_GeDouJiaWindow : GComponent
	{
		public Controller m_Type;
		public GComponent m_BeiJing;
		public GList m_PetList;

		public const string URL = "ui://4asqm7awg4x326";

		public static UI_GeDouJiaWindow CreateInstance()
		{
			return (UI_GeDouJiaWindow)UIPackage.CreateObject("UI_GeDouJia","GeDouJiaWindow");
		}

		public UI_GeDouJiaWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_Type = this.GetControllerAt(0);
			m_BeiJing = (GComponent)this.GetChildAt(5);
			m_PetList = (GList)this.GetChildAt(6);
		}
	}
}