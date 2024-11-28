/**
 * Auto generated, do not edit it
 *
 * t_emoji
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data.Beans;
using UnityEngine;

namespace Data.Containers
{

	public class t_emojiContainer : BaseContainer
	{
		private List<t_emojiBean> list = new List<t_emojiBean>();
		private Dictionary<int, t_emojiBean> map = new Dictionary<int, t_emojiBean>();

		//public override List<t_emojiBean> getList()
		public override IList getList()
		{
			return list;
		}

		//public override Dictionary<int, t_emojiBean> getMap()
		public override IDictionary getMap()
		{
			return map;
		}
		
		public Type BinType = typeof(t_emojiBean);

		public override void loadDataFromBin()
		{    
			list.Clear();
			map.Clear();

#if UNITY_EDITOR
            byte[] data = File.ReadAllBytes(Application.dataPath + "/Bin/" + typeof(t_emojiBean).Name + ".bytes");
#else
            byte[] data = ConfigManager.Singleton.GetData(typeof(t_emojiBean).Name);
#endif
			if(data != null)
			{
				try
				{
					int offset = 0;
					while (data.Length > offset)
					{
						t_emojiBean bean = new t_emojiBean();
						bean.LoadData(data, ref offset);
						list.Add(bean);
						if(!map.ContainsKey(bean.t_id))
							map.Add(bean.t_id, bean);
						else
							Debug.LogError("Exist duplicate Key: " + bean.t_id + " " + typeof(t_emojiContainer).Name);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError("import data error: " + ex.ToString() + typeof(t_emojiBean).Name);
				}
			}
			else
			{
				Debug.LogError("can not find conf data: " + typeof(t_emojiBean).Name + ".bytes");
			}
		}
	}

}


