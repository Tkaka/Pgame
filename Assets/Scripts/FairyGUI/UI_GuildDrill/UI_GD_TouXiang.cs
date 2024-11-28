/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace UI_GuildDrill
{
	public partial class UI_GD_TouXiang : GComponent
	{
		public GLoader m_pinjie;
		public GLoader m_touxiang;
		public GComponent m_xingji;

		public const string URL = "ui://wwlsouxzkzeud";

		public static UI_GD_TouXiang CreateInstance()
		{
			return (UI_GD_TouXiang)UIPackage.CreateObject("UI_GuildDrill","GD_TouXiang");
		}

		public UI_GD_TouXiang()
		{
		}

		public override void ConstructFromXML(XML xml)
		{
			base.ConstructFromXML(xml);

			m_pinjie = (GLoader)this.GetChildAt(0);
			m_touxiang = (GLoader)this.GetChildAt(1);
			m_xingji = (GComponent)this.GetChildAt(2);
		}
	}
}