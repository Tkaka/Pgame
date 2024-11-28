/**
 * Auto generated, do not edit it
 *
 * t_vip
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_vipContainer : BaseContainer
	{
		private List<t_vipBean> list = new List<t_vipBean>();
		private Dictionary<int, t_vipBean> map = new Dictionary<int, t_vipBean>();

		//public override List<t_vipBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_vipBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_vipBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_vipBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_vipBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_vipBean bean = new t_vipBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_vipContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_vipBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_vipBean).Name + ".bytes");
			}
		}
	}

}


