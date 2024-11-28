/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_Shop
{
	public partial class UI_EquipBoxItem : GComponent
	{
		public GComponent m_objBox;
		public GComponent m_btnRewardShow;
		public GTextField m_txtNum;
		public GLoader m_imgComsume;
		public UI_objOpen1 m_objOpen1;
		public UI_objOpen10 m_objOpen10;
		public GComponent m_btnDuiHuan;
		public GList m_rewardList;

		public const string URL = "ui://w9mypx6jasm2t";

		public static UI_EquipBoxItem CreateInstance()
		{
			return (UI_EquipBoxItem)UIPackage.CreateObject("UI_Shop","EquipBoxItem");
		}

		public UI_EquipBoxItem()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_objBox = (GComponent)this.GetChildAt(2);
			m_btnRewardShow = (GComponent)this.GetChildAt(7);
			m_txtNum = (GTextField)this.GetChildAt(10);
			m_imgComsume = (GLoader)this.GetChildAt(11);
			m_objOpen1 = (UI_objOpen1)this.GetChildAt(13);
			m_objOpen10 = (UI_objOpen10)this.GetChildAt(14);
			m_btnDuiHuan = (GComponent)this.GetChildAt(15);
			m_rewardList = (GList)this.GetChildAt(17);
		}
	}
}