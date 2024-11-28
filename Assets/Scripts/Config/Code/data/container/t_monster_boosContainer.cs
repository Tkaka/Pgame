/**
 * Auto generated, do not edit it
 *
 * t_monster_boos
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_monster_boosContainer : BaseContainer
	{
		private List<t_monster_boosBean> list = new List<t_monster_boosBean>();
		private Dictionary<int, t_monster_boosBean> map = new Dictionary<int, t_monster_boosBean>();

		//public override List<t_monster_boosBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_monster_boosBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_monster_boosBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_monster_boosBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_monster_boosBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_monster_boosBean bean = new t_monster_boosBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_monster_boosContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_monster_boosBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_monster_boosBean).Name + ".bytes");
			}
		}
	}

}


