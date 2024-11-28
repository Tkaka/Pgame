/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_TaskSystem
{
	public partial class UI_TaskWindow : GComponent
	{
		public UI_taskToggleGroup m_taskToggleGroup;
		public GList m_RenWuList;
		public GComponent m_YiJianLingQu;
		public GComponent m_commonTop;

		public const string URL = "ui://zswzat1en9yq0";

		public static UI_TaskWindow CreateInstance()
		{
			return (UI_TaskWindow)UIPackage.CreateObject("UI_TaskSystem","TaskWindow");
		}

		public UI_TaskWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_taskToggleGroup = (UI_taskToggleGroup)this.GetChildAt(1);
			m_RenWuList = (GList)this.GetChildAt(3);
			m_YiJianLingQu = (GComponent)this.GetChildAt(4);
			m_commonTop = (GComponent)this.GetChildAt(6);
		}
	}
}