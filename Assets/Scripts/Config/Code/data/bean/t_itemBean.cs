/**
 * Auto generated, do not edit it
 *
 * t_item
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_itemBean : BaseBin
	{
				private int m_t_id; // Id（大类1位）+（小类2位）+(3位标识种类)（id小于100=代币）


 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // Id（大类1位）+（小类2位）+(3位标识种类)（id小于100=代币）


 
				private string m_t_name;   // 名字
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
				private string m_t_describe;   // 道具描述
				public string t_describe
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_describe, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_describe;
                        }
                        else
                            return m_t_describe;
                    }
                    set { m_t_describe = value; }
                } 
				public string t_shop_source {get; set;}   // 来源ID+来源语言ID
				private int m_t_use_jump_ly; // 使用跳转界面                          10=酒吧界面11=拳皇争霸商店界面12=公会战商店界面13=名人堂界面14=竞技场15=金币副本16=工会17=装备觉醒宝箱18=杂货商店19=荣誉商店20=试炼商店21=社团商店22=精英关卡23=终极试炼24=经验挑战25=幻想挑战26=拳皇争霸27=噩梦副本28=成就29=铜像馆30=奥义抽奖
 
				public int t_use_jump_ly{get{return m_t_use_jump_ly;} set{m_t_use_jump_ly = value;}} // 使用跳转界面                          10=酒吧界面11=拳皇争霸商店界面12=公会战商店界面13=名人堂界面14=竞技场15=金币副本16=工会17=装备觉醒宝箱18=杂货商店19=荣誉商店20=试炼商店21=社团商店22=精英关卡23=终极试炼24=经验挑战25=幻想挑战26=拳皇争霸27=噩梦副本28=成就29=铜像馆30=奥义抽奖
 
				public string t_value {get; set;}   // 道具参数
				private int m_t_star; // 抽卡星级 
				public int t_star{get{return m_t_star;} set{m_t_star = value;}} // 抽卡星级 
				private int m_t_tab; // 道具类型  0=特殊     1=装备2=材料     3=碎片     4=消耗品 5=代币6=奥义石 
				public int t_tab{get{return m_t_tab;} set{m_t_tab = value;}} // 道具类型  0=特殊     1=装备2=材料     3=碎片     4=消耗品 5=代币6=奥义石 
				private int m_t_type; // 道具类型      -1=金币       -2=钻石       -3=红钻       -4=体力       -5=觉醒碎片       -6=荣誉币     -7=试练币     -8=社团币      -9=训练师经验-10天赋点     -11=奥义精华          1=宝箱                            2=钥匙                                  3=体力药剂                           4=升品道具                           5=宝贝碎片                         6=觉醒石             7=经验药水                     8=装备升品卷轴               9=万能碎片  10=经验秘籍           11=经验勋章 12=装备升星道具       13=装备升星道具碎片14=玩法系统道具20=徽章21=秘籍          22=战魂经验 23=宠物整卡24=食物30=抽卡用代金券31=能量之源32=喇叭 33=金币出售物 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 道具类型      -1=金币       -2=钻石       -3=红钻       -4=体力       -5=觉醒碎片       -6=荣誉币     -7=试练币     -8=社团币      -9=训练师经验-10天赋点     -11=奥义精华          1=宝箱                            2=钥匙                                  3=体力药剂                           4=升品道具                           5=宝贝碎片                         6=觉醒石             7=经验药水                     8=装备升品卷轴               9=万能碎片  10=经验秘籍           11=经验勋章 12=装备升星道具       13=装备升星道具碎片14=玩法系统道具20=徽章21=秘籍          22=战魂经验 23=宠物整卡24=食物30=抽卡用代金券31=能量之源32=喇叭 33=金币出售物 
				public string t_icon {get; set;}   // 道具图标
				private int m_t_script_id; // 脚本id（宝箱=101001 获取=101002 整卡=101003） 
				public int t_script_id{get{return m_t_script_id;} set{m_t_script_id = value;}} // 脚本id（宝箱=101001 获取=101002 整卡=101003） 
				public string t_quality {get; set;}   // 品质    0=白色 1=绿色 2=蓝色 3=紫色 4=橙色 5=红色（卷轴特殊处理跟宠物品质一样）
				private int m_t_max; // 最大叠加数          0=不可叠加 
				public int t_max{get{return m_t_max;} set{m_t_max = value;}} // 最大叠加数          0=不可叠加 
				private int m_t_use; // 使用状态      0=不可使用1=可使用 2=拿到自动使用     3=打开背包自动使用 
				public int t_use{get{return m_t_use;} set{m_t_use = value;}} // 使用状态      0=不可使用1=可使用 2=拿到自动使用     3=打开背包自动使用 
				private int m_t_use_jump; // 使用跳转界面                          1=宝贝升品界面2=宝贝升级界面3=装备强化界面4=装备觉醒界面5=战魂强化界面6=神器界面7=装备宝箱抽卡界面8=宝贝升星界面9=宝贝管理界面10=酒吧界面11=拳皇争霸商店界面12=公会战商店界面13=名人堂界面14=竞技场15=金币副本16=工会17=装备觉醒宝箱18=杂货商店19=荣誉商店20=试炼商店21=社团商店22=精英关卡23=终极试炼24=经验挑战25=幻想挑战26=拳皇争霸27=噩梦副本28=成就29=铜像馆30=奥义抽奖
 
				public int t_use_jump{get{return m_t_use_jump;} set{m_t_use_jump = value;}} // 使用跳转界面                          1=宝贝升品界面2=宝贝升级界面3=装备强化界面4=装备觉醒界面5=战魂强化界面6=神器界面7=装备宝箱抽卡界面8=宝贝升星界面9=宝贝管理界面10=酒吧界面11=拳皇争霸商店界面12=公会战商店界面13=名人堂界面14=竞技场15=金币副本16=工会17=装备觉醒宝箱18=杂货商店19=荣誉商店20=试炼商店21=社团商店22=精英关卡23=终极试炼24=经验挑战25=幻想挑战26=拳皇争霸27=噩梦副本28=成就29=铜像馆30=奥义抽奖
 
				private int m_t_par; // 跳转参数 
				public int t_par{get{return m_t_par;} set{m_t_par = value;}} // 跳转参数 
				private int m_t_limit; // 使用等级限制 
				public int t_limit{get{return m_t_limit;} set{m_t_limit = value;}} // 使用等级限制 
				private int m_t_sell; // 出售状态 0=不可出售 1=自动出售 2=手动出售 
				public int t_sell{get{return m_t_sell;} set{m_t_sell = value;}} // 出售状态 0=不可出售 1=自动出售 2=手动出售 
				private int m_t_sell_price; // 出售价格 
				public int t_sell_price{get{return m_t_sell_price;} set{m_t_sell_price = value;}} // 出售价格 
				public string t_source {get; set;}   // 来源关卡id（-1最近通关的主线关卡）
				private int m_t_soul_exp; // 战魂经验 
				public int t_soul_exp{get{return m_t_soul_exp;} set{m_t_soul_exp = value;}} // 战魂经验 
				private int m_t_buy_price; // 购买价格（钻石） 
				public int t_buy_price{get{return m_t_buy_price;} set{m_t_buy_price = value;}} // 购买价格（钻石） 
				public string t_compose_arg {get; set;}   // 合成参数（合成该道具需要的材料ID和数量）
				private int m_t_compose_target; // 合成目标（可合成的道具ID） 
				public int t_compose_target{get{return m_t_compose_target;} set{m_t_compose_target = value;}} // 合成目标（可合成的道具ID） 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_describe = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_shop_source = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_use_jump_ly = XBuffer.ReadInt(data, ref offset);
					t_value = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_star = XBuffer.ReadInt(data, ref offset);
					m_t_tab = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					t_icon = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_script_id = XBuffer.ReadInt(data, ref offset);
					t_quality = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_max = XBuffer.ReadInt(data, ref offset);
					m_t_use = XBuffer.ReadInt(data, ref offset);
					m_t_use_jump = XBuffer.ReadInt(data, ref offset);
					m_t_par = XBuffer.ReadInt(data, ref offset);
					m_t_limit = XBuffer.ReadInt(data, ref offset);
					m_t_sell = XBuffer.ReadInt(data, ref offset);
					m_t_sell_price = XBuffer.ReadInt(data, ref offset);
					t_source = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_soul_exp = XBuffer.ReadInt(data, ref offset);
					m_t_buy_price = XBuffer.ReadInt(data, ref offset);
					t_compose_arg = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_compose_target = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


