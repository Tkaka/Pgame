/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_DrawCard
{
	public partial class UI_XianShiLieBiao : GComponent
	{
		public GList m_JiangLiList;
		public GGraph m_LieBiaoZheZhao;

		public const string URL = "ui://zy7t2yegux5q1y";

		public static UI_XianShiLieBiao CreateInstance()
		{
			return (UI_XianShiLieBiao)UIPackage.CreateObject("UI_DrawCard","XianShiLieBiao");
		}

		public UI_XianShiLieBiao()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_JiangLiList = (GList)this.GetChildAt(0);
			m_LieBiaoZheZhao = (GGraph)this.GetChildAt(1);
		}
	}
}