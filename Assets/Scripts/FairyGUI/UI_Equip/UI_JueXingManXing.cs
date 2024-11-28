/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_JueXingManXing : GComponent
	{
		public GGraph m_DiZuo;
		public GGraph m_MoXing;
		public GList m_ManXingShuXing;
		public UI_JiangXingBtn m_JiangXingBtn;

		public const string URL = "ui://8u3gv94necma1h";

		public static UI_JueXingManXing CreateInstance()
		{
			return (UI_JueXingManXing)UIPackage.CreateObject("UI_Equip","JueXingManXing");
		}

		public UI_JueXingManXing()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_DiZuo = (GGraph)this.GetChildAt(1);
			m_MoXing = (GGraph)this.GetChildAt(2);
			m_ManXingShuXing = (GList)this.GetChildAt(3);
			m_JiangXingBtn = (UI_JiangXingBtn)this.GetChildAt(8);
		}
	}
}