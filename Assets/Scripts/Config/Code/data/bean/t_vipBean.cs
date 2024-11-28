/**
 * Auto generated, do not edit it
 *
 * t_vip
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_vipBean : BaseBin
	{
				private int m_t_id; // vip等级 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // vip等级 
				private int m_t_exp; // 升下级需要的经验(-1表示为满级) 
				public int t_exp{get{return m_t_exp;} set{m_t_exp = value;}} // 升下级需要的经验(-1表示为满级) 
				public string t_giftBag {get; set;}   // vip礼包
				private int m_t_original_price; // 礼包原价 
				public int t_original_price{get{return m_t_original_price;} set{m_t_original_price = value;}} // 礼包原价 
				private int m_t_cur_price; // 礼包现价 
				public int t_cur_price{get{return m_t_cur_price;} set{m_t_cur_price = value;}} // 礼包现价 
				public string t_vip_des {get; set;}   // vip特权描述（语言包id+语言包id）
				public string t_vip_des_param {get; set;}   // 语言包数值
				private int m_t_tili; // 体力购买次数 
				public int t_tili{get{return m_t_tili;} set{m_t_tili = value;}} // 体力购买次数 
				private int m_t_fb; // 精英副本重置次数 
				public int t_fb{get{return m_t_fb;} set{m_t_fb = value;}} // 精英副本重置次数 
				private int m_t_dj; // 每天可点金次数 
				public int t_dj{get{return m_t_dj;} set{m_t_dj = value;}} // 每天可点金次数 
				public string t_fuli {get; set;}   // 福利每日任务（送100钻石/日，持续30天）
				private int m_t_jnhf; // 技能点回复时间（秒/点） 
				public int t_jnhf{get{return m_t_jnhf;} set{m_t_jnhf = value;}} // 技能点回复时间（秒/点） 
				private int m_t_jngmcs; // 技能点购买次数 
				public int t_jngmcs{get{return m_t_jngmcs;} set{m_t_jngmcs = value;}} // 技能点购买次数 
				private int m_t_zjsl; // 终极试炼是否可跳过战斗（1=是；0=否） 
				public int t_zjsl{get{return m_t_zjsl;} set{m_t_zjsl = value;}} // 终极试炼是否可跳过战斗（1=是；0=否） 
				private int m_t_sd; // 一键扫荡次数 
				public int t_sd{get{return m_t_sd;} set{m_t_sd = value;}} // 一键扫荡次数 
				private int m_t_jjc; // 竞技场是否跳过战斗（1=是；0=否） 
				public int t_jjc{get{return m_t_jjc;} set{m_t_jjc = value;}} // 竞技场是否跳过战斗（1=是；0=否） 
				public string t_tuhao {get; set;}   // 每日任务我是土豪（消耗880钻送钻石）每天一次
				private int m_t_jjczb; // 竞技场战败是否加2点积分（1=是；0=否） 
				public int t_jjczb{get{return m_t_jjczb;} set{m_t_jjczb = value;}} // 竞技场战败是否加2点积分（1=是；0=否） 
				private int m_t_zjslc; // 终极试炼一键爬塔最高层数 
				public int t_zjslc{get{return m_t_zjslc;} set{m_t_zjslc = value;}} // 终极试炼一键爬塔最高层数 
				private int m_t_zjslf; // 终极试炼获得积分增加 
				public int t_zjslf{get{return m_t_zjslf;} set{m_t_zjslf = value;}} // 终极试炼获得积分增加 
				private int m_t_zj; // 传记是否可跳过战斗（1=是；0=否） 
				public int t_zj{get{return m_t_zj;} set{m_t_zj = value;}} // 传记是否可跳过战斗（1=是；0=否） 
				private int m_t_zdhs; // 是否开启小镇所有生产厂资源自动收取（1=是；0=否） 
				public int t_zdhs{get{return m_t_zdhs;} set{m_t_zdhs = value;}} // 是否开启小镇所有生产厂资源自动收取（1=是；0=否） 
				private int m_t_jjclq; // 竞技场冷却时间（秒） 
				public int t_jjclq{get{return m_t_jjclq;} set{m_t_jjclq = value;}} // 竞技场冷却时间（秒） 
				public string t_sqys {get; set;}   // 每日任务神奇钥匙（送装备钥匙）
				private int m_t_sjboss; // 世界BOSS自动战斗是否开启（1=是；0=否） 
				public int t_sjboss{get{return m_t_sjboss;} set{m_t_sjboss = value;}} // 世界BOSS自动战斗是否开启（1=是；0=否） 
				public string t_tfyb {get; set;}   // 每日任务天赋异禀
				private int m_t_jbfb; // 金币副本冷却时间（秒） 
				public int t_jbfb{get{return m_t_jbfb;} set{m_t_jbfb = value;}} // 金币副本冷却时间（秒） 
				public string t_zbyl {get; set;}   // 每日任务装备冶炼（送装备觉醒碎片）
				public string t_wnsp {get; set;}   // 每日任务万能碎片大赠送
				public string t_laba {get; set;}   // 每日任务登录送喇叭
				private int m_t_fhb; // 发红包次数 
				public int t_fhb{get{return m_t_fhb;} set{m_t_fhb = value;}} // 发红包次数 
				private int m_t_shb; // 收红包次数 
				public int t_shb{get{return m_t_shb;} set{m_t_shb = value;}} // 收红包次数 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					m_t_exp = XBuffer.ReadInt(data, ref offset);
					t_giftBag = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_original_price = XBuffer.ReadInt(data, ref offset);
					m_t_cur_price = XBuffer.ReadInt(data, ref offset);
					t_vip_des = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_vip_des_param = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_tili = XBuffer.ReadInt(data, ref offset);
					m_t_fb = XBuffer.ReadInt(data, ref offset);
					m_t_dj = XBuffer.ReadInt(data, ref offset);
					t_fuli = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_jnhf = XBuffer.ReadInt(data, ref offset);
					m_t_jngmcs = XBuffer.ReadInt(data, ref offset);
					m_t_zjsl = XBuffer.ReadInt(data, ref offset);
					m_t_sd = XBuffer.ReadInt(data, ref offset);
					m_t_jjc = XBuffer.ReadInt(data, ref offset);
					t_tuhao = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_jjczb = XBuffer.ReadInt(data, ref offset);
					m_t_zjslc = XBuffer.ReadInt(data, ref offset);
					m_t_zjslf = XBuffer.ReadInt(data, ref offset);
					m_t_zj = XBuffer.ReadInt(data, ref offset);
					m_t_zdhs = XBuffer.ReadInt(data, ref offset);
					m_t_jjclq = XBuffer.ReadInt(data, ref offset);
					t_sqys = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_sjboss = XBuffer.ReadInt(data, ref offset);
					t_tfyb = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_jbfb = XBuffer.ReadInt(data, ref offset);
					t_zbyl = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_wnsp = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					t_laba = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_fhb = XBuffer.ReadInt(data, ref offset);
					m_t_shb = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


