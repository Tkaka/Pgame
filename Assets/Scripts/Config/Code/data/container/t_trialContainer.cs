/**
 * Auto generated, do not edit it
 *
 * t_trial
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_trialContainer : BaseContainer
	{
		private List<t_trialBean> list = new List<t_trialBean>();
		private Dictionary<int, t_trialBean> map = new Dictionary<int, t_trialBean>();

		//public override List<t_trialBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_trialBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_trialBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_trialBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_trialBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_trialBean bean = new t_trialBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_trialContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_trialBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_trialBean).Name + ".bytes");
			}
		}
	}

}


