/**
 * Auto generated, do not edit it
 *
 * t_recharge
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_rechargeBean : BaseBin
	{
				private int m_t_id; // 充值表 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // 充值表 
				private int m_t_index; // 商品排序 
				public int t_index{get{return m_t_index;} set{m_t_index = value;}} // 商品排序 
				private int m_t_price; // 价格（单位角） 
				public int t_price{get{return m_t_price;} set{m_t_price = value;}} // 价格（单位角） 
				private int m_t_diamond; // 购买获得的钻石数 
				public int t_diamond{get{return m_t_diamond;} set{m_t_diamond = value;}} // 购买获得的钻石数 
				public string t_extra_give_item {get; set;}   // 额外赠送的道具(id+数量)
				private string m_t_extra_give_des;   // 额外获得道具的语言包id
				public string t_extra_give_des
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_extra_give_des, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_extra_give_des;
                        }
                        else
                            return m_t_extra_give_des;
                    }
                    set { m_t_extra_give_des = value; }
                } 
				private int m_t_recommend; // 是否是推荐商品（1是 0不是） 
				public int t_recommend{get{return m_t_recommend;} set{m_t_recommend = value;}} // 是否是推荐商品（1是 0不是） 
				public string t_order {get; set;}   // 订单信息
				public string t_icon {get; set;}   // 商品图标
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_index = XBuffer.ReadInt(data, ref offset);
					m_t_price = XBuffer.ReadInt(data, ref offset);
					m_t_diamond = XBuffer.ReadInt(data, ref offset);
					t_extra_give_item = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_extra_give_des = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					m_t_recommend = XBuffer.ReadInt(data, ref offset);
					t_order = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
		} 

	}
}


