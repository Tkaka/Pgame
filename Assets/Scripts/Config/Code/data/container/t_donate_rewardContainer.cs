/**
 * Auto generated, do not edit it
 *
 * t_donate_reward
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_donate_rewardContainer : BaseContainer
	{
		private List<t_donate_rewardBean> list = new List<t_donate_rewardBean>();
		private Dictionary<int, t_donate_rewardBean> map = new Dictionary<int, t_donate_rewardBean>();

		//public override List<t_donate_rewardBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_donate_rewardBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_donate_rewardBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_donate_rewardBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_donate_rewardBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_donate_rewardBean bean = new t_donate_rewardBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_donate_rewardContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_donate_rewardBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_donate_rewardBean).Name + ".bytes");
			}
		}
	}

}


