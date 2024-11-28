/**
 * Auto generated, do not edit it
 *
 * t_tl_equip_box_rank
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_tl_equip_box_rankBean : BaseBin
	{
				private int m_t_id; // id 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id 
				private int m_t_rank_low; // 排名下限 
				public int t_rank_low{get{return m_t_rank_low;} set{m_t_rank_low = value;}} // 排名下限 
				private int m_t_rank_high; // 排名上限 
				public int t_rank_high{get{return m_t_rank_high;} set{m_t_rank_high = value;}} // 排名上限 
				private int m_t_score; // 需求积分 
				public int t_score{get{return m_t_score;} set{m_t_score = value;}} // 需求积分 
				private int m_t_base_award_id; // 积分不满足时取的id 
				public int t_base_award_id{get{return m_t_base_award_id;} set{m_t_base_award_id = value;}} // 积分不满足时取的id 
				public string t_award {get; set;}   // 奖励
				private string m_t_award_show;   // 奖励语言包id
				public string t_award_show
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_award_show, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_award_show;
                        }
                        else
                            return m_t_award_show;
                    }
                    set { m_t_award_show = value; }
                } 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_rank_low = XBuffer.ReadInt(data, ref offset);
					m_t_rank_high = XBuffer.ReadInt(data, ref offset);
					m_t_score = XBuffer.ReadInt(data, ref offset);
					m_t_base_award_id = XBuffer.ReadInt(data, ref offset);
					t_award = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_award_show = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
		} 

	}
}


