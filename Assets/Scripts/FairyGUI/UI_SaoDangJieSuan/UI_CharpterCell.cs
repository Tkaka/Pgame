/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_CharpterCell : GComponent
	{
		public GList m_CharpterList;
		public GTextField m_txtZhangJieName;
		public GTextField m_txtZhangJie;

		public const string URL = "ui://34cd5b4hdqky1n";

		public static UI_CharpterCell CreateInstance()
		{
			return (UI_CharpterCell)UIPackage.CreateObject("UI_SaoDangJieSuan","CharpterCell");
		}

		public UI_CharpterCell()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_CharpterList = (GList)this.GetChildAt(2);
			m_txtZhangJieName = (GTextField)this.GetChildAt(4);
			m_txtZhangJie = (GTextField)this.GetChildAt(8);
		}
	}
}