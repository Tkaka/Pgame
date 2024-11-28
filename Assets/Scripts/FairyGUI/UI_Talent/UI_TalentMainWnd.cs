/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Talent
{
	public partial class UI_TalentMainWnd : GComponent
	{
		public Controller m_c1;
		public UI_windowCloseBtn m_btnClose;
		public UI_btnJiDou m_btnJiDou;
		public UI_btnZhenXing m_btnZhenXing;
		public UI_btnJinXiu m_btnJinXiu;
		public UI_btnShenZao m_btnShenZao;
		public UI_talentCell1 m_t4;
		public UI_talentCell2 m_t2;
		public UI_talentCell2 m_t3;
		public UI_talentCell2 m_t5;
		public UI_talentCell2 m_t6;
		public UI_talentCell2 m_t7;
		public UI_talentCell2 m_t8;
		public UI_talentCell2 m_t9;
		public UI_talentCell2 m_t10;
		public UI_talentCell2 m_t1;
		public GList m_talentList;
		public GTextField m_txtTalentDes;
		public GTextField m_txtUnLockDes;
		public GComponent m_btnReset;
		public GComponent m_btnFrom;
		public GTextField m_txtTalentNum;
		public GLoader m_imgType;

		public const string URL = "ui://erk5lfvwm6aa0";

		public static UI_TalentMainWnd CreateInstance()
		{
			return (UI_TalentMainWnd)UIPackage.CreateObject("UI_Talent","TalentMainWnd");
		}

		public UI_TalentMainWnd()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_c1 = this.GetControllerAt(0);
			m_btnClose = (UI_windowCloseBtn)this.GetChildAt(1);
			m_btnJiDou = (UI_btnJiDou)this.GetChildAt(3);
			m_btnZhenXing = (UI_btnZhenXing)this.GetChildAt(4);
			m_btnJinXiu = (UI_btnJinXiu)this.GetChildAt(5);
			m_btnShenZao = (UI_btnShenZao)this.GetChildAt(6);
			m_t4 = (UI_talentCell1)this.GetChildAt(8);
			m_t2 = (UI_talentCell2)this.GetChildAt(9);
			m_t3 = (UI_talentCell2)this.GetChildAt(10);
			m_t5 = (UI_talentCell2)this.GetChildAt(11);
			m_t6 = (UI_talentCell2)this.GetChildAt(12);
			m_t7 = (UI_talentCell2)this.GetChildAt(13);
			m_t8 = (UI_talentCell2)this.GetChildAt(14);
			m_t9 = (UI_talentCell2)this.GetChildAt(15);
			m_t10 = (UI_talentCell2)this.GetChildAt(16);
			m_t1 = (UI_talentCell2)this.GetChildAt(17);
			m_talentList = (GList)this.GetChildAt(19);
			m_txtTalentDes = (GTextField)this.GetChildAt(20);
			m_txtUnLockDes = (GTextField)this.GetChildAt(21);
			m_btnReset = (GComponent)this.GetChildAt(22);
			m_btnFrom = (GComponent)this.GetChildAt(23);
			m_txtTalentNum = (GTextField)this.GetChildAt(26);
			m_imgType = (GLoader)this.GetChildAt(28);
		}
	}
}