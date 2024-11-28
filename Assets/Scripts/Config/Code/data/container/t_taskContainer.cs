/**
 * Auto generated, do not edit it
 *
 * t_task
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_taskContainer : BaseContainer
	{
		private List<t_taskBean> list = new List<t_taskBean>();
		private Dictionary<int, t_taskBean> map = new Dictionary<int, t_taskBean>();

		//public override List<t_taskBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_taskBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_taskBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_taskBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_taskBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_taskBean bean = new t_taskBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_taskContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_taskBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_taskBean).Name + ".bytes");
			}
		}
	}

}


