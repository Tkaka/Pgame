/**
 * Auto generated, do not edit it
 *
 * t_draw_card
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_draw_cardContainer : BaseContainer
	{
		private List<t_draw_cardBean> list = new List<t_draw_cardBean>();
		private Dictionary<int, t_draw_cardBean> map = new Dictionary<int, t_draw_cardBean>();

		//public override List<t_draw_cardBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_draw_cardBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_draw_cardBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_draw_cardBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_draw_cardBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_draw_cardBean bean = new t_draw_cardBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_draw_cardContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_draw_cardBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_draw_cardBean).Name + ".bytes");
			}
		}
	}

}


