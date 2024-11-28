/**
 * Auto generated, do not edit it
 *
 * t_exchange
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_exchangeContainer : BaseContainer
	{
		private List<t_exchangeBean> list = new List<t_exchangeBean>();
		private Dictionary<int, t_exchangeBean> map = new Dictionary<int, t_exchangeBean>();

		//public override List<t_exchangeBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_exchangeBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_exchangeBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_exchangeBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_exchangeBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_exchangeBean bean = new t_exchangeBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_exchangeContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_exchangeBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_exchangeBean).Name + ".bytes");
			}
		}
	}

}


