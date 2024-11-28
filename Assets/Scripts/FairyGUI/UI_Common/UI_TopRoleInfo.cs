/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_TopRoleInfo : GComponent
	{
		public UI_topRoleInfoGroup m_topRoleGroup;

		public const string URL = "ui://42sthz3teazaxnh";

		public static UI_TopRoleInfo CreateInstance()
		{
			return (UI_TopRoleInfo)UIPackage.CreateObject("UI_Common","TopRoleInfo");
		}

		public UI_TopRoleInfo()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_topRoleGroup = (UI_topRoleInfoGroup)this.GetChildAt(0);
		}
	}
}