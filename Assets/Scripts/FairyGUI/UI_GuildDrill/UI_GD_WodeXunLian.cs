/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildDrill
{
	public partial class UI_GD_WodeXunLian : GComponent
	{
		public Controller m_type;
		public GList m_XunLianWeiList;
		public GGraph m_XuanLianWeiZheZhao;

		public const string URL = "ui://wwlsouxzp5h4k";

		public static UI_GD_WodeXunLian CreateInstance()
		{
			return (UI_GD_WodeXunLian)UIPackage.CreateObject("UI_GuildDrill","GD_WodeXunLian");
		}

		public UI_GD_WodeXunLian()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_type = this.GetControllerAt(0);
			m_XunLianWeiList = (GList)this.GetChildAt(0);
			m_XuanLianWeiZheZhao = (GGraph)this.GetChildAt(1);
		}
	}
}