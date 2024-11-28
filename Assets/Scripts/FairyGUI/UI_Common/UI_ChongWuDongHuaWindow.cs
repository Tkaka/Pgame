/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_ChongWuDongHuaWindow : GComponent
	{
		public GGraph m_TeXiao;
		public GGraph m_CloseBtn;
		public GGraph m_moxing;
		public GTextField m_miaoshu;
		public GButton m_close;
		public GTextField m_DingWei;
		public GTextField m_juejimiaoshu;
		public GTextField m_jueJiName;
		public GLoader m_juejiIcon;
		public GTextField m_petName;
		public GTextField m_zizhi;

		public const string URL = "ui://42sthz3tvhppxni";

		public static UI_ChongWuDongHuaWindow CreateInstance()
		{
			return (UI_ChongWuDongHuaWindow)UIPackage.CreateObject("UI_Common","ChongWuDongHuaWindow");
		}

		public UI_ChongWuDongHuaWindow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_TeXiao = (GGraph)this.GetChildAt(1);
			m_CloseBtn = (GGraph)this.GetChildAt(2);
			m_moxing = (GGraph)this.GetChildAt(3);
			m_miaoshu = (GTextField)this.GetChildAt(4);
			m_close = (GButton)this.GetChildAt(5);
			m_DingWei = (GTextField)this.GetChildAt(10);
			m_juejimiaoshu = (GTextField)this.GetChildAt(13);
			m_jueJiName = (GTextField)this.GetChildAt(14);
			m_juejiIcon = (GLoader)this.GetChildAt(15);
			m_petName = (GTextField)this.GetChildAt(16);
			m_zizhi = (GTextField)this.GetChildAt(17);
		}
	}
}