/**
 * Auto generated, do not edit it
 *
 * t_equip_levelup
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_equip_levelupContainer : BaseContainer
	{
		private List<t_equip_levelupBean> list = new List<t_equip_levelupBean>();
		private Dictionary<int, t_equip_levelupBean> map = new Dictionary<int, t_equip_levelupBean>();

		//public override List<t_equip_levelupBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_equip_levelupBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_equip_levelupBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_equip_levelupBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_equip_levelupBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_equip_levelupBean bean = new t_equip_levelupBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_equip_levelupContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_equip_levelupBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_equip_levelupBean).Name + ".bytes");
			}
		}
	}

}


