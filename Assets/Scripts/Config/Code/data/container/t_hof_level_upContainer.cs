/**
 * Auto generated, do not edit it
 *
 * t_hof_level_up
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_hof_level_upContainer : BaseContainer
	{
		private List<t_hof_level_upBean> list = new List<t_hof_level_upBean>();
		private Dictionary<int, t_hof_level_upBean> map = new Dictionary<int, t_hof_level_upBean>();

		//public override List<t_hof_level_upBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_hof_level_upBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_hof_level_upBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_hof_level_upBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_hof_level_upBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_hof_level_upBean bean = new t_hof_level_upBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_hof_level_upContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_hof_level_upBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_hof_level_upBean).Name + ".bytes");
			}
		}
	}

}


