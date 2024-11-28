/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_shopToggleGroup : GComponent
	{
		public Controller m_ctrl;
		public UI_sheTuanShopBtn m_sheTuanShopBtn;
		public UI_shiLianShpBtn m_shiLianShopBtn;
		public UI_rongYuBoxBtn m_rongYuBoxBtn;
		public UI_zaHuoShopBtn m_zaHuoShopBtn;
		public UI_equipAwakeBtn m_equipAwakeBtn;
		public Transition m_anim;

		public const string URL = "ui://w9mypx6jvjwx1l";

		public static UI_shopToggleGroup CreateInstance()
		{
			return (UI_shopToggleGroup)UIPackage.CreateObject("UI_Shop","shopToggleGroup");
		}

		public UI_shopToggleGroup()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_ctrl = this.GetControllerAt(0);
			m_sheTuanShopBtn = (UI_sheTuanShopBtn)this.GetChildAt(0);
			m_shiLianShopBtn = (UI_shiLianShpBtn)this.GetChildAt(1);
			m_rongYuBoxBtn = (UI_rongYuBoxBtn)this.GetChildAt(2);
			m_zaHuoShopBtn = (UI_zaHuoShopBtn)this.GetChildAt(3);
			m_equipAwakeBtn = (UI_equipAwakeBtn)this.GetChildAt(5);
			m_anim = this.GetTransitionAt(0);
		}
	}
}