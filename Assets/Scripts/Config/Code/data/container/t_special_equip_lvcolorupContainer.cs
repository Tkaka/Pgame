/**
 * Auto generated, do not edit it
 *
 * t_special_equip_lvcolorup
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_special_equip_lvcolorupContainer : BaseContainer
	{
		private List<t_special_equip_lvcolorupBean> list = new List<t_special_equip_lvcolorupBean>();
		private Dictionary<int, t_special_equip_lvcolorupBean> map = new Dictionary<int, t_special_equip_lvcolorupBean>();

		//public override List<t_special_equip_lvcolorupBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_special_equip_lvcolorupBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_special_equip_lvcolorupBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_special_equip_lvcolorupBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_special_equip_lvcolorupBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_special_equip_lvcolorupBean bean = new t_special_equip_lvcolorupBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_special_equip_lvcolorupContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_special_equip_lvcolorupBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_special_equip_lvcolorupBean).Name + ".bytes");
			}
		}
	}

}


