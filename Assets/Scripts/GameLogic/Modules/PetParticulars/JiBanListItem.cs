using Data.Beans;
using Message.Pet;
using UI_PetParticulars;

public class JiBanListItem : UI_JiBanListItem
{
    private t_fetterBean fetterBean;
    private PetInfo petinfo;
    t_languageBean languageBean;
    int[] weapon;
    t_petBean petBean;
    public new static JiBanListItem CreateInstance()
    {
        return (JiBanListItem)UI_JiBanListItem.CreateInstance();
    }

    public void Init(int jibanid,int petid)
    {
        fetterBean = ConfigBean.GetBean<t_fetterBean, int>(jibanid);
       
        petinfo = PetService.Singleton.GetPetInfo(petid);
        petBean = ConfigBean.GetBean<t_petBean, int>(petid);
        if (petBean == null)
        {
            Logger.err("JiBanListItem:AwakeWeapon:未获得宠物信息" + petinfo.petId);
        }
        //获得装备
        weapon = GTools.splitStringToIntArray(fetterBean.t_condition);
        if (weapon.Length == 0)
        {
            Logger.err("JiBanListItem:AwakeWeapon:羁绊表羁绊条件参数有误，羁绊id为：" + jibanid + "----" + fetterBean.t_condition);
            return;
        }
        if (fetterBean.t_type == 0)
        {
            Logger.err("JiBanListItem:Init:羁绊表中为填写 羁绊类型" + jibanid);
        }
        else
        {
            if (petinfo == null)
            {
                if (fetterBean.t_type == 1)
                    AwakeFirend();
                else if (fetterBean.t_type == 2)
                    AwakeWeapon(false);
                else if (fetterBean.t_type == 3)
                    AwakeBadge(false);
                else if (fetterBean.t_type == 4)
                    AwakeCheats(false);
                else
                {
                    Logger.err("JiBanMianBan:FillData:填入的羁绊类型不属于武器、伙伴、秘籍、徽章中的任意一种！");
                }
            }
            else
            {
                if (fetterBean.t_type == 1)
                    AwakeFirend();
                else if (fetterBean.t_type == 2)
                    AwakeWeapon(true);
                else if (fetterBean.t_type == 3)
                    AwakeBadge(true);
                else if (fetterBean.t_type == 4)
                    AwakeCheats(true);
                else
                {
                    Logger.err("JiBanMianBan:FillData:填入的羁绊类型不属于武器、伙伴、秘籍、徽章中的任意一种！");
                }
            }
        }
        OnWideAndHight();
    }

    /// <summary>
    /// 觉醒类型，武器觉醒
    /// </summary>
    private void AwakeWeapon(bool isown)
    {
        //item = ConfigBean.GetBean<t_itemBean,int>(weapon[0]);
        languageBean = ConfigBean.GetBean<t_languageBean,int>(weapon[0]);
        string name = "";
        if (languageBean == null)
            Logger.err("JiBanListItem:AwakeWeapon:语言包内没有此装备的名字！" );
        else
            name = languageBean.t_content;
        string describe = null;
        string content = null;
        //内容
        describe = fetterBean.t_content;

        //获得装备名字
        if (isown)
        {
            if (petinfo.equipInfo.equips[0].star > 0)
            {
                content = "[color=#FFFF00]" + name + "[/color]";
                m_Name.text = "[color=#FFFF00]" + fetterBean.t_name + "[/color]";
            }
            else
            {
                content = "[color=#808080]" + name + "[/color]";
                m_Name.text = "[color=#808080]" + fetterBean.t_name + "[/color]";
            }
        }
        else
        {
            content = "[color=#808080]" + name + "[/color]";
            m_Name.text = "[color=#808080]" + fetterBean.t_name + "[/color]";
        }
        if (string.IsNullOrEmpty(fetterBean.t_propety_type))
        {
        }
        else
        {
            if (!(string.IsNullOrEmpty(fetterBean.t_propety_type)))
            {
                int[] skillId = GTools.splitStringToIntArray(fetterBean.t_propety_type);
                if (skillId.Length >= 3)
                {
                    t_skillBean oldskillBean = ConfigBean.GetBean<t_skillBean, int>(skillId[1]);
                    t_skillBean newskillBean = ConfigBean.GetBean<t_skillBean, int>(skillId[2]);
                    if (newskillBean == null)
                    {
                        Logger.err("JiBanListItem:AwakeWeapon:技能表没有可替换技能id-----" + ((petBean.t_id) * 10 + 5));
                        return;
                    }
                    m_Content.text = string.Format(describe, content, oldskillBean.t_name, newskillBean.t_name);
                }
            }
        }
    }
    /// <summary>
    /// 宠物伙伴觉醒
    /// </summary>
    /// <returns></returns>
    private void AwakeFirend()
    {
        if (OwnFrind())
            m_Name.text = "[color=#FFFF00]" + fetterBean.t_name + "[/color]";
        else
            m_Name.text = "[color=#808080]" + fetterBean.t_name + "[/color]";

        if (string.IsNullOrEmpty(fetterBean.t_content))
        {
            Logger.err("JiBanListItem:AwakeFirend:羁绊表中没有羁绊语言包id");
            return;
        }
        string describe = fetterBean.t_content;
        string[] frindfetter = new string[weapon.Length];
       
        if (frindfetter == null)
        {
            Logger.err("JiBanListItem:AwakeFirend:羁绊长度不合法-----" + weapon.Length);
        }
        PetInfo pet;
        t_petBean petBean;
        string name = null;
        for (int i = 0; i < weapon.Length; ++i)
        {
            petBean = ConfigBean.GetBean<t_petBean, int>(weapon[i]);
            pet = PetService.Singleton.GetPetInfo(weapon[i]);
            if (petBean == null)
            {
                Logger.err("JiBanListItem:AwakeFirend:宠物表中没有羁绊的宠物id不存在！" + fetterBean.t_id + "--------" + weapon[i]);
                return;
            }
            if (string.IsNullOrEmpty(petBean.t_name) || string.IsNullOrEmpty(petBean.t_star_xingtai))
            {
                return;
            }
            //string[] names = GTools.splitString(petBean.t_name);
            //int[] star_xingji = GTools.splitStringToIntArray(petBean.t_star_xingtai);

            //int star = 0;
            //if (pet != null)
            //    star = pet.basInfo.star;
            //for (int j = 1; j < star_xingji.Length; ++j)
            //{
            //    if (star < star_xingji[j])
            //    {
            //        name = names[j - 1];
            //        break;
            //    }
            //}
            name = UIUtils.GetPetName(petBean);
            if (pet != null)
                frindfetter[i] = "[color=#FFFF00]" + name + "[/color]";
            else
                frindfetter[i] = "[color=#808080]" + name + "[/color]";
          
        }
        string xiaoguo = UIUtils.onXiaoGuo(describe, frindfetter);


        if (string.IsNullOrEmpty(fetterBean.t_content_result))
        {
            Logger.err("JiBanListItem:OwFriend:羁绊表没有填羁绊效果");
        }
        else
            xiaoguo += fetterBean.t_content_result;
        if (string.IsNullOrEmpty(fetterBean.t_propety_type))
            Logger.err("SuMingListItem:Init:羁绊表羁绊类型字段为空-------" + fetterBean.t_id);
        else
        {
            int[] shuzhi = GTools.splitStringToIntArray(fetterBean.t_propety_type);
            xiaoguo += ((float)(shuzhi[1]) / 10000 * 100).ToString() + "%";
        }
        m_Content.text = xiaoguo;
    }
    /// <summary>
    /// 徽章觉醒
    /// </summary>
    /// <returns></returns>
    private void AwakeBadge(bool isown)
    {
        //得到道具名字
        //item = ConfigBean.GetBean<t_itemBean, int>(weapon[0]);
        languageBean = ConfigBean.GetBean<t_languageBean, int>(weapon[0]);
        string name = "";
        if (languageBean == null)
            Logger.err("JiBanListItem:AwakeWeapon:语言包内没有此徽章的名字！");
        else
            name = languageBean.t_content;
        string describe = null;
        string content = null;
        //内容
        if (string.IsNullOrEmpty(fetterBean.t_content))
        {
            Logger.err("JiBanListItem:AwakeBadg:羁绊表羁绊条件字段有误");
            return;
        }
        describe = fetterBean.t_content;
        if (isown)
        {
            if (petinfo.equipInfo.equips[4].star > 0)
            {
                content = "[color=#FFFF00]" + name + "[/color]";
                m_Name.text = "[color=#FFFF00]" + fetterBean.t_name + "[/color]";
            }
            else
            {
                content = "[color=#808080]" + name + "[/color]";
                m_Name.text = "[color=#808080]" + fetterBean.t_name + "[/color]";
            }
        }
        else
        {
            content = "[color=#808080]" + name + "[/color]";
            m_Name.text = "[color=#808080]" + fetterBean.t_name + "[/color]";
        }
        if (string.IsNullOrEmpty(fetterBean.t_content_result))
        {
            Logger.err("JiBanListItem:AwakeBadg:羁绊表羁绊效果字段有误");
            return;
        }
        string xiaoguo = string.Format(describe, content);
        xiaoguo += fetterBean.t_content_result;
        if (string.IsNullOrEmpty(fetterBean.t_propety_type))
            Logger.err("SuMingListItem:Init:羁绊表羁绊类型字段为空-------" + fetterBean.t_id);
        else
        {
            int[] shuzhi = GTools.splitStringToIntArray(fetterBean.t_propety_type);
            xiaoguo += ((float)(shuzhi[1]) / 10000 * 100).ToString() + "%";
        }
        m_Content.text = xiaoguo;
    }
    /// <summary>
    /// 秘籍觉醒
    /// </summary>
    private void AwakeCheats(bool isown)
    {
        //item = ConfigBean.GetBean<t_itemBean, int>(weapon[0]);
        languageBean = ConfigBean.GetBean<t_languageBean,int>(weapon[0]);
        string name = "";
        if (languageBean == null)
            Logger.err("JiBanListItem:AwakeWeapon:语言包内没有此秘籍的名字！");
        else
            name = languageBean.t_content;
        string describe = null;
        string content = null;
        //内容
        describe = fetterBean.t_content;
        if (isown)
        {
            if (petinfo.equipInfo.equips[5].star > 0)
            {
                content = "[color=#FFFF00]" + name + "[/color]";
                m_Name.text = "[color=#FFFF00]" + fetterBean.t_name + "[/color]";
            }
            else
            {
                content = "[color=#808080]" + name + "[/color]";
                m_Name.text = "[color=#808080]" + fetterBean.t_name + "[/color]";
            }
        }
        else
        {
            content = "[color=#808080]" + name + "[/color]";
            m_Name.text = "[color=#808080]" + fetterBean.t_name + "[/color]";
        }
        if (string.IsNullOrEmpty(fetterBean.t_content_result))
        {
            Logger.err("JiBanListItem:AwakeBadg:羁绊表羁绊效果字段有误");
            return;
        }
        string xiaoguo = string.Format(describe, content);
        xiaoguo += fetterBean.t_content_result;
        if (string.IsNullOrEmpty(fetterBean.t_propety_type))
            Logger.err("SuMingListItem:Init:羁绊表羁绊类型字段为空-------" + fetterBean.t_id);
        else
        {
            int[] shuzhi = GTools.splitStringToIntArray(fetterBean.t_propety_type);
            xiaoguo += ((float)(shuzhi[1]) / 10000 * 100).ToString() + "%";
        }
        m_Content.text = xiaoguo;
    }
    /// <summary>
    /// 判断伙伴觉醒条件是否满足，
    /// 用于判定羁绊名字是否变色
    /// </summary>
    /// <returns></returns>
    private bool OwnFrind()
    {
        for (int i = 0; i < weapon.Length; ++i)
        {
            PetInfo pet = PetService.Singleton.GetPetInfo(weapon[i]);
            if (pet == null)
            {
                return false;
            }
            if (pet.basInfo.level <= 0)
                return false;
        }
        return true;
    }
    private void OnWideAndHight()
    {
        height = m_Content.height;
    }

    public override void Dispose()
    {
        fetterBean = null;
        petinfo = null;
        weapon = null;
        petBean = null;
        base.Dispose();

    }

}
