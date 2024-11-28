/**
 * Auto generated, do not edit it
 *
 * t_talent
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_talentContainer : BaseContainer
	{
		private List<t_talentBean> list = new List<t_talentBean>();
		private Dictionary<int, t_talentBean> map = new Dictionary<int, t_talentBean>();

		//public override List<t_talentBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_talentBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_talentBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_talentBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_talentBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_talentBean bean = new t_talentBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_talentContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_talentBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_talentBean).Name + ".bytes");
			}
		}
	}

}


