/**
 * Auto generated, do not edit it
 *
 * t_draw_card
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_draw_cardBean : BaseBin
	{
				private int m_t_id; // type*10000+t_num 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // type*10000+t_num 
				private int m_t_type; // 类型：1是金币，2是钻石 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 类型：1是金币，2是钻石 
				private int m_t_num; // 购买次数 
				public int t_num{get{return m_t_num;} set{m_t_num = value;}} // 购买次数 
				private int m_t_sum_num; // 可购买次数，-1为不限次数 
				public int t_sum_num{get{return m_t_sum_num;} set{m_t_sum_num = value;}} // 可购买次数，-1为不限次数 
				private int m_t_free_num; // 免费次数 
				public int t_free_num{get{return m_t_free_num;} set{m_t_free_num = value;}} // 免费次数 
				private int m_t_price; // 价格 
				public int t_price{get{return m_t_price;} set{m_t_price = value;}} // 价格 
				private int m_t_level; // 开放等级 
				public int t_level{get{return m_t_level;} set{m_t_level = value;}} // 开放等级 
				private int m_t_ticket; // 代金券道具id 
				public int t_ticket{get{return m_t_ticket;} set{m_t_ticket = value;}} // 代金券道具id 
				private string m_t_ticket_name;   // 代金券名字语言包id
				public string t_ticket_name
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_ticket_name, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_ticket_name;
                        }
                        else
                            return m_t_ticket_name;
                    }
                    set { m_t_ticket_name = value; }
                } 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					m_t_num = XBuffer.ReadInt(data, ref offset);
					m_t_sum_num = XBuffer.ReadInt(data, ref offset);
					m_t_free_num = XBuffer.ReadInt(data, ref offset);
					m_t_price = XBuffer.ReadInt(data, ref offset);
					m_t_level = XBuffer.ReadInt(data, ref offset);
					m_t_ticket = XBuffer.ReadInt(data, ref offset);
					t_ticket_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
		} 

	}
}


