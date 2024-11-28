/**
 * Auto generated, do not edit it
 *
 * t_tl_equip_box_rank
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_tl_equip_box_rankContainer : BaseContainer
	{
		private List<t_tl_equip_box_rankBean> list = new List<t_tl_equip_box_rankBean>();
		private Dictionary<int, t_tl_equip_box_rankBean> map = new Dictionary<int, t_tl_equip_box_rankBean>();

		//public override List<t_tl_equip_box_rankBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_tl_equip_box_rankBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_tl_equip_box_rankBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_tl_equip_box_rankBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_tl_equip_box_rankBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_tl_equip_box_rankBean bean = new t_tl_equip_box_rankBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_tl_equip_box_rankContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_tl_equip_box_rankBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_tl_equip_box_rankBean).Name + ".bytes");
			}
		}
	}

}


