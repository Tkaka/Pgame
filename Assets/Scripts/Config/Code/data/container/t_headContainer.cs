/**
 * Auto generated, do not edit it
 *
 * t_head
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_headContainer : BaseContainer
	{
		private List<t_headBean> list = new List<t_headBean>();
		private Dictionary<int, t_headBean> map = new Dictionary<int, t_headBean>();

		//public override List<t_headBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_headBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_headBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_headBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_headBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_headBean bean = new t_headBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_headContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_headBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_headBean).Name + ".bytes");
			}
		}
	}

}


