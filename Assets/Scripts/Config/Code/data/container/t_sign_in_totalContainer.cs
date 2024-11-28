/**
 * Auto generated, do not edit it
 *
 * t_sign_in_total
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_sign_in_totalContainer : BaseContainer
	{
		private List<t_sign_in_totalBean> list = new List<t_sign_in_totalBean>();
		private Dictionary<int, t_sign_in_totalBean> map = new Dictionary<int, t_sign_in_totalBean>();

		//public override List<t_sign_in_totalBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_sign_in_totalBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_sign_in_totalBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_sign_in_totalBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_sign_in_totalBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_sign_in_totalBean bean = new t_sign_in_totalBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_sign_in_totalContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_sign_in_totalBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_sign_in_totalBean).Name + ".bytes");
			}
		}
	}

}


