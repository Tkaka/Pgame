/**
 * Auto generated, do not edit it
 *
 * t_tl_activity_draw_price
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_tl_activity_draw_priceContainer : BaseContainer
	{
		private List<t_tl_activity_draw_priceBean> list = new List<t_tl_activity_draw_priceBean>();
		private Dictionary<int, t_tl_activity_draw_priceBean> map = new Dictionary<int, t_tl_activity_draw_priceBean>();

		//public override List<t_tl_activity_draw_priceBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_tl_activity_draw_priceBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_tl_activity_draw_priceBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_tl_activity_draw_priceBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_tl_activity_draw_priceBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_tl_activity_draw_priceBean bean = new t_tl_activity_draw_priceBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_tl_activity_draw_priceContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_tl_activity_draw_priceBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_tl_activity_draw_priceBean).Name + ".bytes");
			}
		}
	}

}


