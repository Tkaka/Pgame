using UnityEngine;
using Data.Beans;
using System.Collections.Generic;
using Message.Pet;
using System;


public enum FacadeType
{
    Name,
    Icon,
    CityPrefab,
    BattlePrefab,
}

public class UIUtils
{

    public static string ColorToHex(Color color)
    {
        string hex = string.Format("{0:X2}{1:X2}{2:X2}", (int)color.r, (int)color.g, (int)color.b);
        return hex;
    }

    public Color HexToColor(string hex)
    {
        byte br = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte bg = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte bb = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        byte cc = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        float r = br / 255f;
        float g = bg / 255f;
        float b = bb / 255f;
        float a = cc / 255f;
        return new Color(r, g, b, a);
    }

    //设置文本垂直方向渐变
    public static void SetTextVerGradient(FairyGUI.GTextField txt, Color up, Color down)
    {
        if(txt != null && txt.textFormat != null)
        {
            if(txt.textFormat.gradientColor != null && txt.textFormat.gradientColor.Length == 4)
            {
                txt.textFormat.gradientColor[0] = up;
                txt.textFormat.gradientColor[1] = down;
                txt.textFormat.gradientColor[2] = up;
                txt.textFormat.gradientColor[3] = down;
            }else
            {
                txt.textFormat.gradientColor = new Color32[] { up, down, up, down };
            }
        }
    }

    //品质（0：白色 1：绿色 2：蓝色 3：紫色 4：橙色 5：红色
    /// <summary>
    /// 获得道具的边框
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    private static string GetItemBorderByQuality(int quality)
    {
        string res;
        switch (quality)
        {
            case 0:
                res = "kuang01";
                break;
            case 1:
                res = "kuang02";
                break;
            case 2:
                res = "kuang03";
                break;
            case 3:
                res = "kuang04";
                break;
            case 4:
                res = "kuang05";
                break;
            case 5:
                res = "kuang06";
                break;
            default:
                res = "kuang01";
                Logger.err("UIUtils:GetBorderByQuality:无法识别的道具品质");
                break;
        }
        return res;
    }
    /// <summary>
    /// 获得道具代币的品质
    /// </summary>
    public static int GetDaiBiQulity(int num, int itemID)
    {
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemID);
        if (itemBean == null)
        {
            return 1;
        }
        int[] qualityArr = GTools.splitStringToIntArray(itemBean.t_quality);
        if (qualityArr.Length > 0)
        {
            int length = qualityArr.Length;
            if (!string.IsNullOrEmpty(itemBean.t_icon))
            {
                string[] iconArr = itemBean.t_icon.Split(';');
                if (num == 0)
                {
                    // 默认代币显示最高品质的
                    return qualityArr[qualityArr.Length - 1];
                }
                int needNum;
                for (int i = 0; i < length; i++)
                {
                    if (iconArr.Length > i)
                    {
                        string[] iconInfo = iconArr[iconArr.Length - 1 - i].Split('+');
                        if (iconInfo.Length >= 2)
                        {
                            needNum = int.Parse(iconInfo[1]);
                            if (num >= needNum)
                            {
                                return qualityArr[length - 1 - i];
                            }
                        }
                        else
                            Debug.LogError("代币配置异常" + itemID);
 
                    }
                }
            }
            return qualityArr[0];
        }

        return 1;
    }

    public static string GetItemBorder(int itemID, int num = 0)
    {
        if (itemID == 0)
            return GetLoaderUrl(WinEnum.UI_Common, GetItemBorderByQuality(0));

        t_itemBean bean = ConfigBean.GetBean<t_itemBean, int>(itemID);
        if (bean != null)
        {
            int quality = 0;
            if (bean.t_type < 0)
                quality = GetDaiBiQulity(num, itemID);
            else
                int.TryParse(bean.t_quality, out quality);
            string iconName = "";
            if (bean.t_type == (int)ItemType.EquipShenPinJuanZhou)
                iconName = GetBorderByQuality(quality);
            else
                iconName = GetItemBorderByQuality(quality);
            
            return "ui://" + WinEnum.UI_Common + "/" + iconName;
        }
        return GetLoaderUrl(WinEnum.UI_Common, GetItemBorderByQuality(0));
    }


    public static string GetIocnBorderByQuility(int quility)
    {
        return "ui://" + WinEnum.UI_Common + "/" + GetItemBorderByQuality(quility);
    }
    /// <summary>
    /// 根据宠物id返回宠物当前星级形态对应头像
    /// 未获得则返回最低形态头像
    /// </summary>
    /// <param name="petid"></param>
    /// <returns></returns>
    public static string GetPetStartIcon(int petid,int star = 0)
    {
        string icon = "";
        t_petBean petBean = ConfigBean.GetBean<t_petBean,int>(petid);
        if (petBean == null)
        {
            Logger.err("UIUtils:GetPetStartIcon:未能从宠物表获取到宠物信息，请检查当前要加载的宠物id是否正确---" + petid);
            return icon;
        }
        string[] icons = GTools.splitString(petBean.t_icon);
        if (star != 0)
        {
            int[] starticon = GTools.splitStringToIntArray(petBean.t_star_xingtai);
            for (int i = 0; i < starticon.Length - 1; ++i)
            {
                if (starticon[i] <= star && star < starticon[i + 1])
                {
                    icon = icons[i];
                    break;
                }
            }
            if (star >= starticon[starticon.Length - 1])
            {
                icon = icons[icons.Length - 1];
            }
            else if (star < starticon[0])
            {
                icon = icons[0];
            }
        }
        else
        {
            PetInfo petInfo = PetService.Singleton.GetPetInfo(petid);
            if (petInfo == null)
            { icon = icons[0]; }
            else
            {
                int start = petInfo.basInfo.star;
                int[] starticon = GTools.splitStringToIntArray(petBean.t_star_xingtai);
                for (int i = 0; i < starticon.Length - 1; ++i)
                {
                    if (starticon[i] <= start && start < starticon[i + 1])
                    {
                        icon = icons[i];
                        break;
                    }
                }
                if (icon == "")
                {
                    icon = icons[icons.Length - 1];
                }
            }
        }
        return icon;
    }
    /// <summary>
    /// 返回星级对应模型
    /// </summary>
    /// <param name="petid"></param>
    /// <returns></returns>
    public static string GetPetStartModel(int petid)
    {
        string model = "";
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petid);
        if (petBean == null)
        {
            Logger.err("UIUtils:GetPetStartIcon:未能从宠物表获取到宠物信息，请检查当前要加载的宠物id是否正确---" + petid);
            return model;
        }
        string[] models = GTools.splitString(petBean.t_battle_prefab);
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petid);
        if (petInfo == null)
        { model = models[0]; }
        else
        {
            int start = petInfo.basInfo.star;
            int[] starticon = GTools.splitStringToIntArray(petBean.t_star_xingtai);
            for (int i = 0; i < starticon.Length - 1; ++i)
            {
                if (starticon[i] <= start && start < starticon[i + 1])
                {
                    model = models[i];
                    break;
                }
            }
            if (model == "")
            {
                model = models[models.Length - 1];
            }
        }
        return model;
    }
    /// <summary>
    /// 获取道具图标
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public static string GetItemIcon(int itemID, int num=0)
    {
        string res = "";
        t_itemBean bean = ConfigBean.GetBean<t_itemBean, int>(itemID);
        if (bean == null || string.IsNullOrEmpty(bean.t_icon))
        {
            return res;
        }
        //代币 
        if (bean.t_type < 0)
        {
            string[] strArr = GTools.splitString(bean.t_icon,';');
            for (int i = strArr.Length - 1; i >= 0; i--)
            {
                string[] itemArr = GTools.splitString(strArr[i]);
                if (itemArr.Length < 2)
                {
                    Logger.log("UIUtils:GetItemIcon:道具图标参数不正确" + bean.t_icon);
                    return "";
                }
                if (num == 0)
                    return itemArr[0];

                int needNum;
                int.TryParse(itemArr[1], out needNum);
                if (num >= needNum)
                {
                    res = itemArr[0];
                    return res;
                }
                else
                {
                    if (i == 0)
                        return itemArr[0];
                }
            }
        }
        return bean.t_icon;
    }
    /// <summary>
    /// 获得购买的价格代币图标
    /// </summary>
    /// <param name="itemID"></param>
    /// <param name="num"></param>
    public static string GetBuyGoodsPriceIcon(int itemID, int num = 0)
    {
        if (itemID == (int)ItemType.Gold)
            return GetLoaderUrl(WinEnum.UI_Common, "jinbi01");

        return GetItemIcon(itemID, num);
    }
    /// <summary>
    /// 分割字符串到long数组中
    /// </summary>
    /// <param name="src"></param>
    /// <param name="sign"></param>
    /// <returns></returns>
    public static long[] splitStringToLongArray(string src, char sign = '+')
    {
        if (string.IsNullOrEmpty(src))
        {
            return new long[0];
        }
        else
        {
            string[] strs = src.Split(sign);
            long[] ret = new long[strs.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                if (!long.TryParse(strs[i], out ret[i]))
                {
                    Logger.err("字符串转long出错！");
                    continue;
                }
            }
            return ret;
        }
    }


    /// <summary>
    /// 根据品阶获得对应的边框(宠物和装备, 道具中的卷轴)
    /// </summary>
    /// <returns></returns>
    public static string GetBorderByQuality(int petQuality)
    {
        t_color_name_beanBean petQualityBean = ConfigBean.GetBean<t_color_name_beanBean, int>(petQuality);
        if(petQualityBean != null)
            return petQualityBean.t_border;
        return "";
    }

    public static string GetBorderUrl(int quality)
    {
        return "ui://" + WinEnum.UI_Common + "/" + GetBorderByQuality(quality);
    }


    public static string GetPetTypeUrl(int type)
    {
        string res;
        switch (type)
        {
            case 1:
                res = "gong01";
                break;
            case 2:
                res = "fang01";
                break;
            case 3:
                res = "ji01";
                break;
            default:
                res = "gong01";
                break;
        }
        return res;
    }
    /// <summary>
    /// 根据关卡类型获得关卡边框图片
    /// </summary>
    /// <param name="levelType"></param>
    /// <returns></returns>
    public static string GetLevelBoradIcon(int levelType)
    {
        string icon = "";
        switch (levelType)
        {
            case 0:
                icon = "putongkuang";
                break;
            case 1:
                icon = "xjingyingkuang";
                break;
            case 2:
                icon = "xjingyingkuang";
                break;
            case 3:
                icon = "jingyingkuang";
                break;
            case 4:
                icon = "bosskuang";
                break;
            default:
                break;
        }

        return icon;
    }


    public static string GetLoaderUrl(string pkgName, string resName)
    {
        return "ui://" + pkgName + "/" + resName;
    }
    /// <summary>
    /// 获得品质对应的颜色（只使用宠物和装备）
    /// </summary>
    /// <param name="quality"></param>
    /// <returns></returns>
    public static Color GetColorByQuality(int quality)
    {
        //Logger.log("ssssss" + quality);
        t_color_name_beanBean petQualityBean = ConfigBean.GetBean<t_color_name_beanBean, int>(quality);
        if (petQualityBean == null)
        {
            Logger.err("UIUtils:GetColorByQuaLity:未获得颜色数据");
            return Color.white;
        }
        return GetColor(petQualityBean.t_color -1);
    }

    public static string GetZiZhiStr(int ziZhi)
    {
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(3);
        if (globalBean != null)
        {
            int[] ziZhiIDs = GTools.splitStringToIntArray(globalBean.t_string_param);
            if (ziZhiIDs.Length > 1)
            {
                // 11 最差的一个资质
                int index = ziZhi - 11;
                if(ziZhiIDs.Length > index)
                {
                    int languaeID = ziZhiIDs[index];
                    return GetStrByLanguageID(languaeID);
                }
            }
        }

        return "";
    }

    /// <summary>
    /// 根据道具id返回道具品质颜色
    /// </summary>
    /// <param name="itenid"></param>
    /// <returns></returns>
    public static Color GetItemColor(int itemid, int num = 0)
    {
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean,int>(itemid);
        if (string.IsNullOrEmpty(itemBean.t_quality))
        {
            Logger.err("UIUlils:GetItemColor:未能在道具表中获得颜色数据！---" + itemBean.t_id);
            return new Color(1,1,1,1);
        }

        int quality = 0;
        if (itemBean.t_type >= 0)
        {
            if (itemBean.t_type == (int)ItemType.EquipShenPinJuanZhou)
                return GetColorByQuality(int.Parse(itemBean.t_quality));
            else
                quality = int.Parse(itemBean.t_quality);
        }
        else
            quality = GetDaiBiQulity(num, itemid);

        return GetColor(quality);
    }

    private static Color GetColor(int quality)
    {
        Color color = new Color(255, 255, 255);
        switch (quality)
        {
            case 0:
                color = new Color(228, 228, 228);
                break;
            case 1:
                color = new Color(66, 222, 39);
                break;
            case 2:
                color = new Color(92, 204, 249);
                break;
            case 3:
                color = new Color(254, 93, 254);
                break;
            case 4:
                color = new Color(255, 186, 66);
                break;
            case 5:
                color = new Color(250, 97, 82);
                break;
            default:
                break;
        }
        color /= 255f;
        color.a = 1;
        return color;
    }

    public static Color GetColorValueByQuility(int quility)
    {
        return GetColor(quility);
    }

    /// <summary>
    /// 根据宠物品阶和ID返回拼接后的宠物名字
    /// </summary>
    /// <param name="petQuality"></param>
    /// <returns></returns>
    public static string GetPingJiePetName(int petID, int petQuality, int star)
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petID);
        t_color_name_beanBean colorNameBean = ConfigBean.GetBean<t_color_name_beanBean, int>(petQuality);
        if (colorNameBean == null)
            return "";

        string numStr = colorNameBean.t_num == 0 ? "" : string.Format("+{0}", colorNameBean.t_num);

        return GetPetName(petBean, star) + numStr;
    }
    /// <summary>
    /// 获得拼接后的装备名字
    /// </summary>
    /// <param name="petID"></param>
    /// <param name="equipID"></param>
    /// <param name="equipType"></param>
    /// <returns></returns>
    public static string GetPingJieEquipName(int petID, int equipIndex, int star, int color)
    {
        string nameLanguageID = "";
    
        PetInfo petInfo = PetService.Singleton.GetPetByID(petID);
        PetEquip petEquip = petInfo.equipInfo.equips[equipIndex];
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petID);
        if (petBean == null)
        {
            return nameLanguageID;
        }

        nameLanguageID = star > 0 ? petBean.t_awaken_names.Split('+')[equipIndex] : petBean.t_normal_names.Split('+')[equipIndex];

        t_color_name_beanBean colorNameBean = ConfigBean.GetBean<t_color_name_beanBean, int>(color);
        if (colorNameBean == null)
            return "";

        string numStr = colorNameBean.t_num == 0 ? "" : string.Format("+{0}", colorNameBean.t_num);

        return GetStrByLanguageID(int.Parse(nameLanguageID)) + numStr;
    }

    /// <summary>
    /// 获得装备的图标
    /// </summary>
    /// <returns></returns>
    public static string GetEquipIcon(int petID, int equipIndex, int star)
    {
        string url = "";
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petID);
        if (petBean == null)
            return url;
        if (string.IsNullOrEmpty(petBean.t_awaken_icons) || string.IsNullOrEmpty(petBean.t_normal_icons))
        {
            Logger.err("头像数据为空");
            return url;
        }

        url = star > 0 ? petBean.t_awaken_icons.Split('+')[equipIndex] : petBean.t_normal_icons.Split('+')[equipIndex];
        return url;
    }
    /// <summary>
    /// 获得箱子的图片
    /// </summary>
    /// <param name="levelModel">关卡模式</param>
    /// <param name="boxType">箱子类型</param>
    /// <param name="boxStatus">箱子状态</param>
    /// <param name="index">箱子下标, 只有星级宝箱才有</param>
    public static string GetBoxIcon(LevelModel levelModel, BoxType boxType, int boxStatus, int index = 0)
    {
        string iconStr = "";
        // 全局表id
        // 主线：19010，精英19020
        // 箱子类型：关卡：9，章节8
        int globalID = 0;
        switch (levelModel)
        {
            case LevelModel.Main:
                globalID += 19010;
                break;
            case LevelModel.Elite:
                globalID += 19020;
                break;
            case LevelModel.Nightmare:
                break;
            case LevelModel.Cycle:
                break;
            default:
                break;
        }

        switch (boxType)
        {
            case BoxType.Act:
                globalID += 9;
                break;
            case BoxType.Star:
                globalID += 8;
                break;
            default:
                break;
        }

        // 箱子图片在字符串中的索引下标 
        // 箱子索引*3 + （箱子状态： -1 ：1，      0：2，     1：0）
        int iconIndex = index * 3;
        switch (boxStatus)
        {
            case -1:
                iconIndex += 1;
                break;
            case 0:
                iconIndex += 1;
                break;
            case 1:
                iconIndex += 0;
                break;
            default:
                break;
        }

        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(globalID);
        if (globalBean == null)
            return iconStr;

        iconStr = globalBean.t_string_param;
        if(!string.IsNullOrEmpty(iconStr))
        {
            string[] iconArr = iconStr.Split('+');
            if (iconArr.Length > iconIndex)
            {
                iconStr = iconArr[iconIndex];
            }
        }

        return iconStr;
    }

    /// <summary>
    /// 获取形态数量
    /// </summary>
    /// <returns></returns>
    public static int GetFacadeCount(int petId)
    {
        t_petBean bean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (bean != null && !string.IsNullOrEmpty(bean.t_star_xingtai))
        {
            string[] strArr = GTools.splitString(bean.t_star_xingtai);
            if (strArr != null && strArr.Length > 0)
            {
                return strArr.Length;
            }
        }
        return 0;
    }

    public static int[] GetFacadeStar(int petId)
    {
        t_petBean bean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (bean != null && !string.IsNullOrEmpty(bean.t_star_xingtai))
        {
            int[] strArr = GTools.splitStringToIntArray(bean.t_star_xingtai);
            if (strArr != null && strArr.Length > 0)
            {
                return strArr;
            }
        }
        return null;
    }


    public static string GetPetName(t_petBean bean, int star = -1)
    {
        if (star < 0)
            star = bean.t_hecheng_star;
        return GetFacadeByStar(FacadeType.Name, bean, star);
    }

    public static string GetIconPath(t_petBean bean, int star=-1)
    {
        if (star < 0)
            star = bean.t_hecheng_star;
        return GetFacadeByStar(FacadeType.Icon, bean, star);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bean"></param>
    /// <param name="star">默认值为初始星级</param>
    /// <returns></returns>
    public static string GetCityPrefab(t_petBean bean, int star = -1)
    {
        if (star < 0)
            star = bean.t_hecheng_star;
        return GetFacadeByStar(FacadeType.CityPrefab, bean, star);
    }

    public static string GetBattlePrefab(t_petBean bean, int star = -1)
    {
        if (star < 0)
            star = bean.t_hecheng_star;
        return GetFacadeByStar(FacadeType.BattlePrefab, bean, star);
    }

    public static int GetUnlockFacade(int petId, int star)
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (petBean != null)
        {
            int[] starArr = GTools.splitStringToIntArray(petBean.t_star_xingtai);
            for (int i = 0; i < starArr.Length; i++)
            {
                if (star >= starArr[i])
                {
                    return ++i;
                }
            }
        }
        return 1;
    }
    /// <summary>
    /// 将带分号和逗号的字符串分割到二维数组中
    /// </summary>
    /// <returns></returns>
    public static int[,] splitStringTotwodimensionArry(string src)
    {
        if (string.IsNullOrEmpty(src))
            return null;
        string[] strs = src.Split(';');
        int[,] mubiao = new int[strs.Length,2];
        for (int i = 0; i < strs.Length; ++i)
        {
            int[] chaifen = GTools.splitStringToIntArray(strs[i]);
            for (int j = 0; j < 2; ++j)
            {
                mubiao[i, j] = chaifen[j];
            }
        }
        return mubiao;
    }
    /// <summary>
    /// 将字符串分割为单个字符（汉字）
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string[] OnSplitStringToStringArry(string str)
    {
        string[] strTemp = new string[str.Length];

        for (int i = 0; i < str.Length; i++)
        {
            strTemp[i] = str[i].ToString();
        }
        return strTemp;
    }

    /// <summary>
    /// 根据宠物id和羁绊id返回羁绊状态
    /// </summary>
    public static bool OnFetterState(int petid,int fetterid)
    {
        PetInfo pet = PetService.Singleton.GetPetInfo(petid);
        if (pet == null)
        {
            Logger.err("UIUtils:OnFetterState():未在服务器发来的宠物列表中找到对应宠物数据-----" + petid);
            return false;
        }
        //得到羁绊类型
        t_fetterBean fetterBean = ConfigBean.GetBean<t_fetterBean,int>(fetterid);
        if(fetterBean == null)
        {
            Logger.err("UIUtils:OnFetterState():未在羁绊表中找到对应羁绊id的数据-----" + petid);
            return false;
        }
        int type = fetterBean.t_type;
        if (string.IsNullOrEmpty(fetterBean.t_condition))
        {
            Logger.err("UIUtils:OnFetterState():未在羁绊表中羁绊条件参数id-----" + fetterBean.t_id);
            return false;
        }
        int[] id = GTools.splitStringToIntArray(fetterBean.t_condition);
        switch (type)
        {
            case 1:
                {
                    PetInfo petinfo;
                    for (int i = 0; i < id.Length; ++i)
                    {
                        petinfo = PetService.Singleton.GetPetInfo(id[i]);
                        if (petinfo == null)
                            return false;
                        if (petinfo.basInfo.level <= 0)
                            return false;
                    }
                    return true;
                }
            case 2:
                {
                    if (pet.equipInfo == null || pet.equipInfo.equips == null || pet.equipInfo.equips.Count == 0)
                    {
                        Logger.err("UIUiils:OnFetterState:本地没有宠物装备信息");
                        return false;
                    }
                    if (pet.equipInfo.equips[0].star > 0)
                        return true;
                    else
                        return false;
                }
            case 3:
                {
                    if (pet.equipInfo == null || pet.equipInfo.equips == null || pet.equipInfo.equips.Count == 0)
                    {
                        Logger.err("UIUiils:OnFetterState:本地没有宠物装备信息");
                        return false;
                    }
                    if (pet.equipInfo.equips[4].star > 0)
                        return true;
                    else
                        return false;
                }
            case 4:
                {
                    if (pet.equipInfo == null || pet.equipInfo.equips == null || pet.equipInfo.equips.Count == 0)
                    {
                        Logger.err("UIUiils:OnFetterState:本地没有宠物装备信息");
                        return false;
                    }
                    if (pet.equipInfo.equips[5].star > 0)
                        return true;
                    else
                        return false;
                }
        }
        return false;
    }
    /// <summary>
    /// 根据条件个数返回标准长度文本内的替换
    /// </summary>
    /// <param name="describe"></param>
    /// <param name="fetter"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    public static string onXiaoGuo(string describe, string[] fetter)
    {
        int number = fetter.Length;
        string xiaoguo = null;
        for (int i = fetter.Length; i < 5; ++i)
        {
            string oldStr = "、{" + i + "}";
            describe = describe.Replace(oldStr, "");
        }
        switch (number)
        {
            case 1: { xiaoguo = string.Format(describe, fetter[0]); }break;
            case 2:{xiaoguo = string.Format(describe, fetter[0], fetter[1]);  }break;
            case 3: { xiaoguo = string.Format(describe, fetter[0], fetter[1], fetter[2]);  } break;
            case 4: {xiaoguo = string.Format(describe, fetter[0], fetter[1], fetter[2], fetter[3]); } break;
            case 5: {xiaoguo = string.Format(describe, fetter[0], fetter[1], fetter[2], fetter[3], fetter[4]);  }break;
        }
        return xiaoguo;
    }


    public static string GetFacadeByStar(FacadeType faceType, t_petBean bean, int star)
    {
        string[] prefabArr = null;
        if (faceType == FacadeType.Name)
            prefabArr = GTools.splitString(bean.t_name);
        else if (faceType == FacadeType.Icon)
            prefabArr = GTools.splitString(bean.t_icon);
        else if (faceType == FacadeType.CityPrefab)
            prefabArr = GTools.splitString(bean.t_city_prefab);
        else if (faceType == FacadeType.BattlePrefab)
            prefabArr = GTools.splitString(bean.t_battle_prefab);
        else
            Logger.err("UIUtils:GetFacadeByStar:未知的外观类型");

        int[] starArr = GTools.splitStringToIntArray(bean.t_star_xingtai);
        if (prefabArr == null || starArr == null)
        {
            Logger.err("UIUtils:GetPrefabByStar:形态对应星级数不正确");
            return null;
        }

        if (prefabArr.Length != starArr.Length)
        {
            if(faceType == FacadeType.Name)
                Logger.err("UIUtils:GetPrefabByStar:形态对应星级数不正确" + prefabArr.Length + "_" + starArr.Length + "_petid:" + bean.t_name);
            else if (faceType == FacadeType.Icon)
                Logger.err("UIUtils:GetPrefabByStar:形态对应星级数不正确" + prefabArr.Length + "_" + starArr.Length + "_petid:" + bean.t_icon);
            else if (faceType == FacadeType.CityPrefab)
                Logger.err("UIUtils:GetPrefabByStar:形态对应星级数不正确" + prefabArr.Length + "_" + starArr.Length + "_petid:" + bean.t_city_prefab);
            else if (faceType == FacadeType.BattlePrefab)
                Logger.err("UIUtils:GetPrefabByStar:形态对应星级数不正确" + prefabArr.Length + "_" + starArr.Length + "_petid:" + bean.t_battle_prefab);
            return null;
        }

        //TODO:判断是否开启八门
        int index = 0;
        for (int i = 0; i < starArr.Length; i++)
        {
            if (star >= starArr[i])
            {
                index = i;
            }
        }
        return prefabArr[index];
    }
    /// <summary>
    /// 获得品阶对应的颜色值
    /// </summary>
    /// <returns></returns>
    public static string GetColorName(int color)
    {
        t_color_name_beanBean petQualityBean = ConfigBean.GetBean<t_color_name_beanBean, int>(color);
        string colorName = "";
        switch (petQualityBean.t_color)
        {
            case 1:
                colorName = "白";
                break;
            case 2:
                colorName = "绿";
                break;
            case 3:
                colorName = "蓝";
                break;
            case 4:
                colorName = "紫";
                break;
            case 5:
                colorName = "橙";
                break;
            case 6:
                colorName = "红";
                break;
            default:
                colorName = "白";
                Logger.err("UIUtils:GetBorderByQuality:无法识别的道具品质");
                break;
        }

        string numStr = petQualityBean.t_num == 0 ? "" : string.Format("+{0}", petQualityBean.t_num);
        return colorName + numStr;
    }

    /// <summary>
    /// 根据宠物id获取升星消耗碎片数量
    /// </summary>
    /// <param name="petId"></param>
    /// <returns></returns>
    public static TwoParam<int, int> GetStarUpCount(int petId, int star)
    {
        TwoParam<int, int> res =  new TwoParam<int, int>();
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (petBean != null)
        {
            int xiaoHaoKey = star * 100 + petBean.t_starUp_type;
            t_pet_starup_costBean xiaoHaoBean = ConfigBean.GetBean<t_pet_starup_costBean, int>(xiaoHaoKey);
            if (xiaoHaoBean != null)
            {
                res.value1 = xiaoHaoBean.t_gold;
                int xiuZhengKey = petId * 100 + star;
                Dictionary<int, t_pet_starup_costBean>  map  = ConfigBean.GetBeanMap<t_pet_starup_costBean, int>();
                if (map.ContainsKey(xiuZhengKey))
                {
                    t_pet_starup_costBean xiuZhengBean = ConfigBean.GetBean<t_pet_starup_costBean, int>(xiuZhengKey);
                    if (xiuZhengBean != null)
                    {
                        res.value2 = xiaoHaoBean.t_num + xiuZhengBean.t_num;
                        return res;
                    }
                }
                res.value2 = xiaoHaoBean.t_num;
                return res;
            }
        }
        res.value1 = int.MaxValue;
        res.value2 = int.MaxValue;
        return res;
    }
    /// <summary>
    /// 获得铜像的材质
    /// </summary>
    /// <param name="tongXiangID"></param>
    /// <returns></returns>
    public static int GetTongXiangMaterial(int tongXiangID)
    {
        return tongXiangID % 100 / 10; 
    }
    /// <summary>
    /// 获得铜像材质对应的名字
    /// </summary>
    /// <param name="material"></param>
    /// <returns></returns>
    public static string GetTongXiangMaterialName(TongXiangMaterial material)
    {
        // TODO： 换成语言ID
        string colorName = "";
        switch (material)
        {
            case TongXiangMaterial.QingTong:
                colorName = "青铜像";
                break;
            case TongXiangMaterial.BaiYing:
                colorName = "白银像";
                break;
            case TongXiangMaterial.DuJin:
                colorName = "镀金像";
                break;
            case TongXiangMaterial.LiuJin:
                colorName = "鎏金像";
                break;
            case TongXiangMaterial.ChunJin:
                colorName = "纯金像";
                break;
            case TongXiangMaterial.BaiJin:
                colorName = "白金像";
                break;
            default:
                colorName = "青铜像";
                break;
        }

        return colorName;
    }
    /// <summary>
    /// 获得铜像等级
    /// </summary>
    /// <param name="statueID"></param>
    /// <returns></returns>
    public static int GetTongXiangRank(int statueID)
    {
        return statueID % 10;
    }
    /// <summary>
    /// 获得铜像等级对应的名字
    /// </summary>
    /// <param name="rank"></param>
    /// <returns></returns>
    public static string GetTongXiangRankName(TongXiangRank rank)
    {
        string rankStr = "";

        switch (rank)
        {
            case TongXiangRank.XueTu:
                rankStr += "学徒级";
                break;
            case TongXiangRank.GaoShou:
                rankStr += "高手级";
                break;
            case TongXiangRank.DaShi:
                rankStr += "大师级";
                break;
            case TongXiangRank.GongJian:
                rankStr += "工匠级";
                break;
            default:
                break;
        }

        return rankStr;
    }

    public static Color GetTongXiangRankColor(TongXiangRank rank)
    {
        Color color = new Color(255, 255, 255);

        switch (rank)
        {
            case TongXiangRank.XueTu:
                color = Color.white;
                break;
            case TongXiangRank.GaoShou:
                color = Color.green;
                break;
            case TongXiangRank.DaShi:
                color = Color.blue;
                break;
            case TongXiangRank.GongJian:
                color = Color.red;
                break;
            default:
                break;
        }

        return color;
    }
    /// <summary>
    /// 获得来源的关卡ID
    /// </summary>
    /// <returns></returns>
    public static int GetLaiYuanActID(int actID)
    {
        if (actID == -1)
            return LevelService.Singleton.GetCurrPassActID(LevelModel.Main);

        return actID;
    }
    /// <summary>
    /// 通过属性ID，获得对应的文本
    /// </summary>
    /// <returns></returns>
    public static string GetTextByAttributeID(int id)
    {
        t_attr_nameBean attrNameBean = ConfigBean.GetBean<t_attr_nameBean, int>(id);
        if (attrNameBean == null)
            return "";
        return attrNameBean.t_name_id;
    }
    public static string GetStrByLanguageID(int languageID)
    {
        t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(languageID);
        if (languageBean == null)
            return "违规的名字";
        return languageBean.t_content;
    }
    /// <summary>
    /// 矩形碰撞
    /// </summary>
    /// <param name="one"></param>
    /// <param name="two"></param>
    /// <returns></returns>
    public static bool isRectCrash(Rect one, Rect two)
    {

        if (one.x + one.width < two.x)
            return false;
        else if (one.x > two.x + two.width)
            return false;
        else if (one.y + one.height < two.y)
            return false;
        else if (one.y > two.y + two.width)
            return false;

        return true;
    }
    /// <summary>
    /// 二维点是否与矩形碰撞
    /// </summary>
    public static bool isSpotToRect(Vector2 vector, Rect rect)
    {
        if (vector.x < rect.x)
            return false;
        else if (vector.x > rect.x + rect.width)
            return false;
        else if (vector.y < rect.y)
            return false;
        else if (vector.y > rect.y + rect.height)
            return false;

        return true;
    }

    //获得默认的道具品质
    public static int GetDefaultItemQuality(int itemId)
    {
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemId);
        if (itemBean == null)
            return -1;

        int[] arr = GTools.splitStringToIntArray(itemBean.t_quality);
        if (arr.Length > 0)
            return arr[0];
        else
            Debug.Log("道具的品质配置异常  " + itemId);
        return -1;
    }

    //获得头像框
    public static string GetHeadIcon(int id)
    {
        t_headBean iconBean = ConfigBean.GetBean<t_headBean, int>(id);
        if (iconBean == null)
            return "";
        return iconBean.t_icon;
    }

    public static SoulType GetSoulType(string soulStr)
    {
        if (!string.IsNullOrEmpty(soulStr))
        {
            int[] arr = GTools.splitStringToIntArray(soulStr);
            if (arr != null && arr.Length > 0)
            {
                t_pet_soulBean soulBean = ConfigBean.GetBean<t_pet_soulBean, int>(arr[0]);
                if (soulBean != null)
                {
                    if (System.Enum.IsDefined(typeof(SoulType), soulBean.t_type))
                    {
                        return (SoulType)soulBean.t_type;
                    }
                }
            }
        }
        Logger.log("魂参数错误:" + soulStr);
        return SoulType.Hu;
    }



    public static string GetItemName(int itemId)
    {
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemId);
        if (itemBean == null)
            return "";

        Color color = GetItemColor(itemId);
        string rgb = ColorUtility.ToHtmlStringRGB(color);

        return string.Format("[color=#{0}]{1}[/color]", rgb, itemBean.t_name);
    }

    #region   id规则 -------------------------------------------------------------------------------------------------
    /************技能Id规则**************/

    /// <summary>
    /// 普攻技能id（仅用于战斗）
    /// </summary>
    /// <param name="petId"></param>
    /// <returns></returns>
    public static int GetNormalAttackSkillID(int petId)
    {
        return 4990;
        //return petId * 10;
    }

    /// <summary>
    /// 小技能id
    /// </summary>
    /// <param name="petId">宠物id</param>
    /// <returns></returns>
    public static int GetSmallSkillID(int petId)
    {
        return petId * 10 + 1;
    }

    /// <summary>
    /// 必杀技能id
    /// </summary>
    /// <param name="petId"></param>
    /// <returns></returns>
    public static int GetMasterSkillID(int petId)
    {
        return petId * 10 + 2;
    }


    /// <summary>
    /// 成长技能id
    /// </summary>
    /// <param name="petId"></param>
    /// <returns></returns>
    public static int GetGrowSkillID(int petId)
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (petBean != null)
        {
            return petBean.t_bd_grow_skill_id; ;
        }
        return 0;
    }

    /// <summary>
    /// 被动成长技能效果id
    /// </summary>
    /// <param name="petId"></param>
    /// <returns></returns>
    public static int GetGrowSkillEffectId(int petId)
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (petBean != null)
        {
            int dbGrowSkillId = petBean.t_bd_grow_skill_id;
            t_skillBean skillBean = ConfigBean.GetBean<t_skillBean, int>(dbGrowSkillId);
            if (!string.IsNullOrEmpty(skillBean.t_bd_effect_id))
            {
                int res = 0;
                int.TryParse(skillBean.t_bd_effect_id, out res);
                return res;
            }
        }
        return 0;
    }
    
    public static void SetWrapperMask(FairyGUI.GoWrapper wrapper, bool supportMask = true)
    {
        if(wrapper == null || wrapper.wrapTarget == null)
        {
            Logger.err("设置wrapper mask失败，wrapper为空或者wrap对应GameObject为空");
            return;
        }

        wrapper.supportStencil = supportMask;
        var renders = wrapper.wrapTarget.GetComponentsInChildren<Renderer>(true);
        for(int i=0, len = renders.Length; i<len; ++i)
        {
            var mats = renders[i].materials;
            for (int j = 0; j < mats.Length; ++j)
            {
                Material mat = mats[j];
                if (mat == null)
                    continue;
                switch (mat.shader.name)
                {
                    case "PGame/ActorOptSimple":
                    case "PGame/ActorOpt":
                        if (supportMask)
                        {
                            string maskShader = mat.shader.name + "_Mask";
                            mat.shader = Shader.Find(maskShader);
                        }
                        break;
                    case "PGame/ActorOptSimple_Mask":
                    case "PGame/ActorOpt_Mask":
                        if (!supportMask)
                        {
                            string normalShader = mat.shader.name.Substring(0, mat.shader.name.Length - 5);
                            mat.shader = Shader.Find(normalShader);
                        }
                        break;
                }
            }
        }
        wrapper.CacheRenderers();
    }

    /// <summary>
    /// 核心技能id
    /// </summary>
    /// <param name="petId"></param>
    /// <returns></returns>
    public static int GetCoreSkillID(int petId)
    {
        return petId * 10 + 4;
    }
    /// <summary>
    /// 获得角色升品属性和表的ID
    /// </summary>
    /// <returns></returns>
    public static int GetPetColorUpAttrSum(int petID, int color)
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petID);
        if(petBean != null)
        {
            return color * 10 + petBean.t_type;
        }

        return -1;
    }
    /// <summary>
    /// 获得雕塑ID 
    /// </summary>
    /// <returns></returns>
    public static int GetStatueID(int petID, int material, int rank)
    {
        return petID * 100 + material * 10 + rank;
    }
    /// <summary>
    /// 获得铜像ID
    /// </summary>
    /// <returns></returns>
    public static int GetTongXiangID(int petID, int materialIndex)
    {
        return petID * 10 + materialIndex;
    }
    /// <summary>
    /// 获得铜像价格ID 
    /// </summary>
    /// <returns></returns>
    public static int GetTongXiangPriceID(int material, int rank)
    {
        return material * 10 + rank;
    }
    /************技能Id规则**************/
    #endregion


}
