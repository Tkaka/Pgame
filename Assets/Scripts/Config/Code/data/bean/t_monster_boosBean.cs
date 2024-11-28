/**
 * Auto generated, do not edit it
 *
 * t_monster_boos
 */
using Data.Containers;

namespace Data.Beans
{
	public class t_monster_boosBean : BaseBin
	{
				private int m_t_id; // BossID 
				public int t_id{get{return m_t_id;} set{m_t_id = value;}} // BossID 
				private string m_t_name;   // boss名字
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
				private string m_t_dingwei;   // boss定位语言id
				public string t_dingwei
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_dingwei, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_dingwei;
                        }
                        else
                            return m_t_dingwei;
                    }
                    set { m_t_dingwei = value; }
                } 
				private string m_t_desc;   // boss描述语言id
				public string t_desc
                {
                    get           
                    {
                        int ret;
                        bool flag = int.TryParse(m_t_desc, out ret);
                        if(flag) 
                        {
							t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(ret);
							if (lanBean != null)
								return lanBean.t_content;
							else
								return m_t_desc;
                        }
                        else
                            return m_t_desc;
                    }
                    set { m_t_desc = value; }
                } 
				public string t_prefab {get; set;}   // 战斗预制件
				private int m_t_xiao_jineng; // 小技能ID 
				public int t_xiao_jineng{get{return m_t_xiao_jineng;} set{m_t_xiao_jineng = value;}} // 小技能ID 
				private int m_t_da_jineng; // 大招ID 
				public int t_da_jineng{get{return m_t_da_jineng;} set{m_t_da_jineng = value;}} // 大招ID 
				private int m_t_beidong; // 核心被动ID 
				public int t_beidong{get{return m_t_beidong;} set{m_t_beidong = value;}} // 核心被动ID 
				private int m_t_dengji; // 等级 
				public int t_dengji{get{return m_t_dengji;} set{m_t_dengji = value;}} // 等级 
				private int m_t_zizhi; // 资质 
				public int t_zizhi{get{return m_t_zizhi;} set{m_t_zizhi = value;}} // 资质 
				private int m_t_type; // 类别 
				public int t_type{get{return m_t_type;} set{m_t_type = value;}} // 类别 
				public string t_race {get; set;}   // 种族
				private int m_t_if_boss; // 是否boss(1=boss,2=非boss) 
				public int t_if_boss{get{return m_t_if_boss;} set{m_t_if_boss = value;}} // 是否boss(1=boss,2=非boss) 
				private int m_t_pet_id; // 对应宝贝ID 
				public int t_pet_id{get{return m_t_pet_id;} set{m_t_pet_id = value;}} // 对应宝贝ID 
				private int m_t_soul_detail_type; // 战魂大类 
				public int t_soul_detail_type{get{return m_t_soul_detail_type;} set{m_t_soul_detail_type = value;}} // 战魂大类 
				private int m_t_max_mp; // 能量上限 
				public int t_max_mp{get{return m_t_max_mp;} set{m_t_max_mp = value;}} // 能量上限 
				private int m_t_atk; // 攻击 
				public int t_atk{get{return m_t_atk;} set{m_t_atk = value;}} // 攻击 
				private int m_t_def; // 防御 
				public int t_def{get{return m_t_def;} set{m_t_def = value;}} // 防御 
				private int m_t_hp; // 生命 
				public int t_hp{get{return m_t_hp;} set{m_t_hp = value;}} // 生命 
				private int m_t_baoji_lv; // 暴击率 
				public int t_baoji_lv{get{return m_t_baoji_lv;} set{m_t_baoji_lv = value;}} // 暴击率 
				private int m_t_kangbaolv; // 抗爆率 
				public int t_kangbaolv{get{return m_t_kangbaolv;} set{m_t_kangbaolv = value;}} // 抗爆率 
				private int m_t_baoji_qiangdu; // 暴击强度 
				public int t_baoji_qiangdu{get{return m_t_baoji_qiangdu;} set{m_t_baoji_qiangdu = value;}} // 暴击强度 
				private int m_t_gedang; // 格挡率 
				public int t_gedang{get{return m_t_gedang;} set{m_t_gedang = value;}} // 格挡率 
				private int m_t_poji_lv; // 破击率 
				public int t_poji_lv{get{return m_t_poji_lv;} set{m_t_poji_lv = value;}} // 破击率 
				private int m_t_gedang_qiangdu; // 格挡强度 
				public int t_gedang_qiangdu{get{return m_t_gedang_qiangdu;} set{m_t_gedang_qiangdu = value;}} // 格挡强度 
				private int m_t_shanghai_lv; // 伤害率 
				public int t_shanghai_lv{get{return m_t_shanghai_lv;} set{m_t_shanghai_lv = value;}} // 伤害率 
				private int m_t_mianshang_lv; // 免伤率 
				public int t_mianshang_lv{get{return m_t_mianshang_lv;} set{m_t_mianshang_lv = value;}} // 免伤率 
				private int m_t_xixue_lv; // 吸血率 
				public int t_xixue_lv{get{return m_t_xixue_lv;} set{m_t_xixue_lv = value;}} // 吸血率 
				private int m_t_zhiliao_lv; // 治疗率 
				public int t_zhiliao_lv{get{return m_t_zhiliao_lv;} set{m_t_zhiliao_lv = value;}} // 治疗率 
				private int m_t_zhiliao; // 治疗效果 
				public int t_zhiliao{get{return m_t_zhiliao;} set{m_t_zhiliao = value;}} // 治疗效果 
				private int m_t_kongzhi_lv; // 控制率 
				public int t_kongzhi_lv{get{return m_t_kongzhi_lv;} set{m_t_kongzhi_lv = value;}} // 控制率 
				private int m_t_miankong_lv; // 免控率 
				public int t_miankong_lv{get{return m_t_miankong_lv;} set{m_t_miankong_lv = value;}} // 免控率 
				private int m_t_jueji_shanghai_lv; // 绝技伤害率 
				public int t_jueji_shanghai_lv{get{return m_t_jueji_shanghai_lv;} set{m_t_jueji_shanghai_lv = value;}} // 绝技伤害率 
				private int m_t_jueji_fangyu_lv; // 绝技防御率 
				public int t_jueji_fangyu_lv{get{return m_t_jueji_fangyu_lv;} set{m_t_jueji_fangyu_lv = value;}} // 绝技防御率 
				private int m_t_gongji_lv; // 攻击率 
				public int t_gongji_lv{get{return m_t_gongji_lv;} set{m_t_gongji_lv = value;}} // 攻击率 
				private int m_t_fangyu_lv; // 防御率 
				public int t_fangyu_lv{get{return m_t_fangyu_lv;} set{m_t_fangyu_lv = value;}} // 防御率 
				private int m_t_shanghai_fantan_lv; // 伤害反弹率 
				public int t_shanghai_fantan_lv{get{return m_t_shanghai_fantan_lv;} set{m_t_shanghai_fantan_lv = value;}} // 伤害反弹率 
				private int m_t_duigong_shanghai_lv; // 对攻伤害率 
				public int t_duigong_shanghai_lv{get{return m_t_duigong_shanghai_lv;} set{m_t_duigong_shanghai_lv = value;}} // 对攻伤害率 
				private int m_t_duiji_shanghai_lv; // 对技伤害率 
				public int t_duiji_shanghai_lv{get{return m_t_duiji_shanghai_lv;} set{m_t_duiji_shanghai_lv = value;}} // 对技伤害率 
				private int m_t_duifang_shanghai_lv; // 对防伤害率 
				public int t_duifang_shanghai_lv{get{return m_t_duifang_shanghai_lv;} set{m_t_duifang_shanghai_lv = value;}} // 对防伤害率 
				private int m_t_duigong_mianshang_lv; // 对攻免伤率 
				public int t_duigong_mianshang_lv{get{return m_t_duigong_mianshang_lv;} set{m_t_duigong_mianshang_lv = value;}} // 对攻免伤率 
				private int m_t_duiji_mianshang_lv; // 对技免伤率 
				public int t_duiji_mianshang_lv{get{return m_t_duiji_mianshang_lv;} set{m_t_duiji_mianshang_lv = value;}} // 对技免伤率 
				private int m_t_duifang_mianshang_lv; // 对防免伤率 
				public int t_duifang_mianshang_lv{get{return m_t_duifang_mianshang_lv;} set{m_t_duifang_mianshang_lv = value;}} // 对防免伤率 
				private int m_t_nengliang_speed; // 能量回复速度 
				public int t_nengliang_speed{get{return m_t_nengliang_speed;} set{m_t_nengliang_speed = value;}} // 能量回复速度 
				private int m_t_dikang_lv; // 抵抗率 
				public int t_dikang_lv{get{return m_t_dikang_lv;} set{m_t_dikang_lv = value;}} // 抵抗率 
				private int m_t_xueliangxing_dikang_lv; // 血量型抵抗率 
				public int t_xueliangxing_dikang_lv{get{return m_t_xueliangxing_dikang_lv;} set{m_t_xueliangxing_dikang_lv = value;}} // 血量型抵抗率 
				private int m_t_debuff_mianyi_lv; // Debuff免疫概率 
				public int t_debuff_mianyi_lv{get{return m_t_debuff_mianyi_lv;} set{m_t_debuff_mianyi_lv = value;}} // Debuff免疫概率 
				private int m_t_xiaojineng_gailv_jiacheng; // 小技能概率加成 
				public int t_xiaojineng_gailv_jiacheng{get{return m_t_xiaojineng_gailv_jiacheng;} set{m_t_xiaojineng_gailv_jiacheng = value;}} // 小技能概率加成 
				private int m_t_mp; // 初始能量增加 
				public int t_mp{get{return m_t_mp;} set{m_t_mp = value;}} // 初始能量增加 
				private int m_hit_energy_add; // 每次受击额外获得能量 
				public int hit_energy_add{get{return m_hit_energy_add;} set{m_hit_energy_add = value;}} // 每次受击额外获得能量 
				private int m_att_energy_add; // 每次攻击额外获得能量 
				public int att_energy_add{get{return m_att_energy_add;} set{m_att_energy_add = value;}} // 每次攻击额外获得能量 
				private int m_fdead_energy_add; // 每有队友死亡时获得能量 
				public int fdead_energy_add{get{return m_fdead_energy_add;} set{m_fdead_energy_add = value;}} // 每有队友死亡时获得能量 
				private int m_enemydead_energy_add; // 敌方死亡时获得 
				public int enemydead_energy_add{get{return m_enemydead_energy_add;} set{m_enemydead_energy_add = value;}} // 敌方死亡时获得 
				private int m_turn_energy_add; // 每回合回复能量 
				public int turn_energy_add{get{return m_turn_energy_add;} set{m_turn_energy_add = value;}} // 每回合回复能量 
		
		public void LoadData(byte[] data, ref int offset)
		{
					m_t_id = XBuffer.ReadInt(data, ref offset);
					t_name = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_dingwei = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_desc = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n");
					t_prefab = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_xiao_jineng = XBuffer.ReadInt(data, ref offset);
					m_t_da_jineng = XBuffer.ReadInt(data, ref offset);
					m_t_beidong = XBuffer.ReadInt(data, ref offset);
					m_t_dengji = XBuffer.ReadInt(data, ref offset);
					m_t_zizhi = XBuffer.ReadInt(data, ref offset);
					m_t_type = XBuffer.ReadInt(data, ref offset);
					t_race = XBuffer.ReadString(data, ref offset).Replace(@"\n", "\n"); 
					m_t_if_boss = XBuffer.ReadInt(data, ref offset);
					m_t_pet_id = XBuffer.ReadInt(data, ref offset);
					m_t_soul_detail_type = XBuffer.ReadInt(data, ref offset);
					m_t_max_mp = XBuffer.ReadInt(data, ref offset);
					m_t_atk = XBuffer.ReadInt(data, ref offset);
					m_t_def = XBuffer.ReadInt(data, ref offset);
					m_t_hp = XBuffer.ReadInt(data, ref offset);
					m_t_baoji_lv = XBuffer.ReadInt(data, ref offset);
					m_t_kangbaolv = XBuffer.ReadInt(data, ref offset);
					m_t_baoji_qiangdu = XBuffer.ReadInt(data, ref offset);
					m_t_gedang = XBuffer.ReadInt(data, ref offset);
					m_t_poji_lv = XBuffer.ReadInt(data, ref offset);
					m_t_gedang_qiangdu = XBuffer.ReadInt(data, ref offset);
					m_t_shanghai_lv = XBuffer.ReadInt(data, ref offset);
					m_t_mianshang_lv = XBuffer.ReadInt(data, ref offset);
					m_t_xixue_lv = XBuffer.ReadInt(data, ref offset);
					m_t_zhiliao_lv = XBuffer.ReadInt(data, ref offset);
					m_t_zhiliao = XBuffer.ReadInt(data, ref offset);
					m_t_kongzhi_lv = XBuffer.ReadInt(data, ref offset);
					m_t_miankong_lv = XBuffer.ReadInt(data, ref offset);
					m_t_jueji_shanghai_lv = XBuffer.ReadInt(data, ref offset);
					m_t_jueji_fangyu_lv = XBuffer.ReadInt(data, ref offset);
					m_t_gongji_lv = XBuffer.ReadInt(data, ref offset);
					m_t_fangyu_lv = XBuffer.ReadInt(data, ref offset);
					m_t_shanghai_fantan_lv = XBuffer.ReadInt(data, ref offset);
					m_t_duigong_shanghai_lv = XBuffer.ReadInt(data, ref offset);
					m_t_duiji_shanghai_lv = XBuffer.ReadInt(data, ref offset);
					m_t_duifang_shanghai_lv = XBuffer.ReadInt(data, ref offset);
					m_t_duigong_mianshang_lv = XBuffer.ReadInt(data, ref offset);
					m_t_duiji_mianshang_lv = XBuffer.ReadInt(data, ref offset);
					m_t_duifang_mianshang_lv = XBuffer.ReadInt(data, ref offset);
					m_t_nengliang_speed = XBuffer.ReadInt(data, ref offset);
					m_t_dikang_lv = XBuffer.ReadInt(data, ref offset);
					m_t_xueliangxing_dikang_lv = XBuffer.ReadInt(data, ref offset);
					m_t_debuff_mianyi_lv = XBuffer.ReadInt(data, ref offset);
					m_t_xiaojineng_gailv_jiacheng = XBuffer.ReadInt(data, ref offset);
					m_t_mp = XBuffer.ReadInt(data, ref offset);
					m_hit_energy_add = XBuffer.ReadInt(data, ref offset);
					m_att_energy_add = XBuffer.ReadInt(data, ref offset);
					m_fdead_energy_add = XBuffer.ReadInt(data, ref offset);
					m_enemydead_energy_add = XBuffer.ReadInt(data, ref offset);
					m_turn_energy_add = XBuffer.ReadInt(data, ref offset);
		} 

	}
}


