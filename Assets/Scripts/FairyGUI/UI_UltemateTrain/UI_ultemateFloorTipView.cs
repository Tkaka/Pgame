/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_UltemateTrain
{
	public partial class UI_ultemateFloorTipView : GComponent
	{
		public GGroup m_firstFloorGroup;
		public GGroup m_lastFloorGroup;
		public GList m_floorTipList;
		public Transition m_boxAnim;

		public const string URL = "ui://1wdkrtiucf2o1y";

		public static UI_ultemateFloorTipView CreateInstance()
		{
			return (UI_ultemateFloorTipView)UIPackage.CreateObject("UI_UltemateTrain","ultemateFloorTipView");
		}

		public UI_ultemateFloorTipView()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_firstFloorGroup = (GGroup)this.GetChildAt(4);
			m_lastFloorGroup = (GGroup)this.GetChildAt(7);
			m_floorTipList = (GList)this.GetChildAt(8);
			m_boxAnim = this.GetTransitionAt(0);
		}
	}
}