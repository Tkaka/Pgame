/**
 * Auto generated, do not edit it
 *
 * t_equip_lib
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_equip_libContainer : BaseContainer
	{
		private List<t_equip_libBean> list = new List<t_equip_libBean>();
		private Dictionary<int, t_equip_libBean> map = new Dictionary<int, t_equip_libBean>();

		//public override List<t_equip_libBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_equip_libBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_equip_libBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_equip_libBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_equip_libBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_equip_libBean bean = new t_equip_libBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_equip_libContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_equip_libBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_equip_libBean).Name + ".bytes");
			}
		}
	}

}


