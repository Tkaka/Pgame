/**
 * Auto generated, do not edit it
 *
 * t_dungeon_chapter
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_dungeon_chapterContainer : BaseContainer
	{
		private List<t_dungeon_chapterBean> list = new List<t_dungeon_chapterBean>();
		private Dictionary<int, t_dungeon_chapterBean> map = new Dictionary<int, t_dungeon_chapterBean>();

		//public override List<t_dungeon_chapterBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_dungeon_chapterBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_dungeon_chapterBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_dungeon_chapterBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_dungeon_chapterBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_dungeon_chapterBean bean = new t_dungeon_chapterBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_dungeon_chapterContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_dungeon_chapterBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_dungeon_chapterBean).Name + ".bytes");
			}
		}
	}

}


