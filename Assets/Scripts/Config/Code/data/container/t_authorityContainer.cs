/**
 * Auto generated, do not edit it
 *
 * t_authority
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_authorityContainer : BaseContainer
	{
		private List<t_authorityBean> list = new List<t_authorityBean>();
		private Dictionary<int, t_authorityBean> map = new Dictionary<int, t_authorityBean>();

		//public override List<t_authorityBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_authorityBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_authorityBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_authorityBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_authorityBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_authorityBean bean = new t_authorityBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_authorityContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_authorityBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_authorityBean).Name + ".bytes");
			}
		}
	}

}


