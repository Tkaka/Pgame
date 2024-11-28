/**
 * Auto generated, do not edit it
 *
 * t_mail
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_mailContainer : BaseContainer
	{
		private List<t_mailBean> list = new List<t_mailBean>();
		private Dictionary<int, t_mailBean> map = new Dictionary<int, t_mailBean>();

		//public override List<t_mailBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_mailBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_mailBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_mailBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_mailBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_mailBean bean = new t_mailBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_mailContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_mailBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_mailBean).Name + ".bytes");
			}
		}
	}

}


