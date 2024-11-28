/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_SaoDangJieSuan
{
	public partial class UI_WanChenPic : GComponent
	{
		public GImage m_WanChengPic;

		public const string URL = "ui://34cd5b4hkgjxz";

		public static UI_WanChenPic CreateInstance()
		{
			return (UI_WanChenPic)UIPackage.CreateObject("UI_SaoDangJieSuan","WanChenPic");
		}

		public UI_WanChenPic()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_WanChengPic = (GImage)this.GetChildAt(0);
		}
	}
}