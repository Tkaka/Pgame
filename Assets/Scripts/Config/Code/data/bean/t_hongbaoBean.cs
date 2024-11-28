/**
 * Auto generated, do not edit it
 *
 * t_hongbao
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_hongbaoBean : BaseBin
	{
				private int m_t_id; // id(类型*10 + id) 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // id(类型*10 + id) 
				private int m_t_type; // 类型(1金币 2钻石 3神器之源) 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 类型(1金币 2钻石 3神器之源) 
				private int m_t_cost; // 花费钻石 
				public int t_cost{get{return m_t_cost;} set{m_t_cost = value;}} // 花费钻石 
				private int m_t_vip; // vip等级限制 
				public int t_vip{get{return m_t_vip;} set{m_t_vip = value;}} // vip等级限制 
				private int m_t_itemId; // 红包额度道具id 
				public int t_itemId{get{return m_t_itemId;} set{m_t_itemId = value;}} // 红包额度道具id 
				private int m_t_itemNum; // 红包额度道具数量 
				public int t_itemNum{get{return m_t_itemNum;} set{m_t_itemNum = value;}} // 红包额度道具数量 
				public string t_reward {get; set;}   // 发红包奖励
				private string m_t_name;   // 红包名字
				public string t_name
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_name, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_name;
                        }
                        else
                            return m_t_name;
                    }
                    set { m_t_name = value; }
                } 
				private int m_t_num; // 红包数量 
				public int t_num{get{return m_t_num;} set{m_t_num = value;}} // 红包数量 
				public string t_icon {get; set;}   // 红包图片
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					m_t_cost = XBuffer.ReadInt(data, ref offset);
					m_t_vip = XBuffer.ReadInt(data, ref offset);
					m_t_itemId = XBuffer.ReadInt(data, ref offset);
					m_t_itemNum = XBuffer.ReadInt(data, ref offset);
					t_reward = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_num = XBuffer.ReadInt(data, ref offset);
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


