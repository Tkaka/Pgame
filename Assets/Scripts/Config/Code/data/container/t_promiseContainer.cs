/**
 * Auto generated, do not edit it
 *
 * t_promise
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_promiseContainer : BaseContainer
	{
		private List<t_promiseBean> list = new List<t_promiseBean>();
		private Dictionary<int, t_promiseBean> map = new Dictionary<int, t_promiseBean>();

		//public override List<t_promiseBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_promiseBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_promiseBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_promiseBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_promiseBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_promiseBean bean = new t_promiseBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_promiseContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_promiseBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_promiseBean).Name + ".bytes");
			}
		}
	}

}


