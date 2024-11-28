/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Common
{
	public partial class UI_BoxReceiveWidow : GComponent
	{
		public GGraph m_mask;
		public GList m_itemList;
		public GTextField m_tipLabel;
		public GButton m_confirmBtn;
		public UI_xingxing_ain_l m_leftStarAnim;
		public UI_xingxing_ain_r m_rightStarAnim;
		public GGraph m_touchMask;
		public Transition m_t0;

		public const string URL = "ui://42sthz3tw0huxkt";

		public static UI_BoxReceiveWidow CreateInstance()
		{
			return (UI_BoxReceiveWidow)UIPackage.CreateObject("UI_Common","BoxReceiveWidow");
		}

		public UI_BoxReceiveWidow()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_mask = (GGraph)this.GetChildAt(0);
			m_itemList = (GList)this.GetChildAt(2);
			m_tipLabel = (GTextField)this.GetChildAt(3);
			m_confirmBtn = (GButton)this.GetChildAt(4);
			m_leftStarAnim = (UI_xingxing_ain_l)this.GetChildAt(6);
			m_rightStarAnim = (UI_xingxing_ain_r)this.GetChildAt(7);
			m_touchMask = (GGraph)this.GetChildAt(9);
			m_t0 = this.GetTransitionAt(0);
		}
	}
}