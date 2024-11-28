/**
 * Auto generated, do not edit it
 *
 * t_sign_in_month
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_sign_in_monthContainer : BaseContainer
	{
		private List<t_sign_in_monthBean> list = new List<t_sign_in_monthBean>();
		private Dictionary<int, t_sign_in_monthBean> map = new Dictionary<int, t_sign_in_monthBean>();

		//public override List<t_sign_in_monthBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_sign_in_monthBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_sign_in_monthBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_sign_in_monthBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_sign_in_monthBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_sign_in_monthBean bean = new t_sign_in_monthBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_sign_in_monthContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_sign_in_monthBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_sign_in_monthBean).Name + ".bytes");
			}
		}
	}

}


