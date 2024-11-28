/**
 * Auto generated, do not edit it
 *
 * t_dungeon_chapter
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_dungeon_chapterBean : BaseBin
	{
				private int m_t_id; // 章节ID(2位类型*100000+2位序号)51 = 主线；52=精英 90=金币副本 91=经验副本 92=女格斗家副本 93=幻象副本 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 章节ID(2位类型*100000+2位序号)51 = 主线；52=精英 90=金币副本 91=经验副本 92=女格斗家副本 93=幻象副本 
				private string m_t_name_id;   // 章节名称语言包ID
				public string t_name_id
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_name_id, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_name_id;
                        }
                        else
                            return m_t_name_id;
                    }
                    set { m_t_name_id = value; }
                } 
				public string t_act_id {get; set;}   // 关卡ID列表
				private int m_t_open_lv; // 开放需求战队等级 
				public int t_open_lv{get{return m_t_open_lv;} set{m_t_open_lv = value;}} // 开放需求战队等级 
				public string t_box {get; set;}   // 章节宝箱需求星星以及对应宝箱道具
				public string t_bg {get; set;}   // 章节背景图
				public string t_act_pos {get; set;}   // 关卡位置（x+y;x+y）坐标原点在正中心
				public string t_act_box_pos {get; set;}   // 关卡宝箱的位置（x+y;x+y）坐标原点在正中心
				public string t_feiye_img {get; set;}   // 挑战扉页背景图
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_act_id = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_open_lv = XBuffer.ReadInt(data, ref offset);
					t_box = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_bg = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_act_pos = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_act_box_pos = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_feiye_img = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


