/**
 * Auto generated, do not edit it
 *
 * t_trial_buff
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_trial_buffContainer : BaseContainer
	{
		private List<t_trial_buffBean> list = new List<t_trial_buffBean>();
		private Dictionary<int, t_trial_buffBean> map = new Dictionary<int, t_trial_buffBean>();

		//public override List<t_trial_buffBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_trial_buffBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_trial_buffBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_trial_buffBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_trial_buffBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_trial_buffBean bean = new t_trial_buffBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_trial_buffContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_trial_buffBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_trial_buffBean).Name + ".bytes");
			}
		}
	}

}


