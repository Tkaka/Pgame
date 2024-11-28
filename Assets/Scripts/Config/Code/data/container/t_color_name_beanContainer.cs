/**
 * Auto generated, do not edit it
 *
 * t_color_name_bean
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_color_name_beanContainer : BaseContainer
	{
		private List<t_color_name_beanBean> list = new List<t_color_name_beanBean>();
		private Dictionary<int, t_color_name_beanBean> map = new Dictionary<int, t_color_name_beanBean>();

		//public override List<t_color_name_beanBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_color_name_beanBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_color_name_beanBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_color_name_beanBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_color_name_beanBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_color_name_beanBean bean = new t_color_name_beanBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_color_name_beanContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_color_name_beanBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_color_name_beanBean).Name + ".bytes");
			}
		}
	}

}


