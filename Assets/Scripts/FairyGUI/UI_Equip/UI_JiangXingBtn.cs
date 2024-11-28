/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Equip
{
	public partial class UI_JiangXingBtn : GButton
	{
		public GImage m_JiangXing;

		public const string URL = "ui://8u3gv94ntwti1b";

		public static UI_JiangXingBtn CreateInstance()
		{
			return (UI_JiangXingBtn)UIPackage.CreateObject("UI_Equip","JiangXingBtn");
		}

		public UI_JiangXingBtn()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_JiangXing = (GImage)this.GetChildAt(0);
		}
	}
}