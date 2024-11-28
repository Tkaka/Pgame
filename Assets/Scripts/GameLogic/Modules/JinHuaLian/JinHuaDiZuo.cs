using FairyGUI;
using Message.Pet;
using UI_JinHuaLian;
using Data.Beans;

public class JinHuaDiZuo : UI_JinHuaDiZuo
{
    private UIResPack resPack;
    private ActorUI actor;
    private t_petBean petBean;
    private int start;//宠物的当前星级
    private int index;//当前加载模型在宠物表形态对应星级下标
    private float[] suofang = { 0.8f, 1.0f,1.2f,1.4f,1.4f};
    public new static JinHuaDiZuo CreateInstance()
    {
        return (JinHuaDiZuo)UI_JinHuaDiZuo.CreateInstance();
    }
    public void Init(int petId, int xiabiao)
    {
        m_dianjixiangying.onClick.Add(Ondonghua);
        petBean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (petBean == null)
        {
            Logger.err("宠物表没有对应宠物");
            return;
        }

        if (resPack == null)
        {
            resPack = new UIResPack(this);
        }
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petId);
        if (petInfo == null)
        {
            start = -1;
        }
        else
            start = petInfo.basInfo.star;
        index = xiabiao;
        //是否解锁，是的卷加载名字，不是的话加载几星解锁
        //加载模型
        //加载箭头是否点亮

        m_all.y -= index * 40;
        //m_DiZuo.SetScale((index + 1) * 1.01f, (index + 1) * 1.01f);
        FillData();
    }
    //加载是否解锁
    private void FillData()
    {
        string miaoshu = "宠物{0}星解锁";
        if (string.IsNullOrEmpty(petBean.t_star_xingtai))
        {
            Logger.err("模型星级字段有误");
            return;
        }
        int[] starts = GTools.splitStringToIntArray(petBean.t_star_xingtai);
        if (starts.Length < index)
        {
            Logger.err("下标大于形态对应星级数组的长度，无法加载模型");
            return;
        }
        if (start < 0)
        {
            m_YiJieSuo.visible = false;
            m_WeiJieSuo.visible = true;
            m_JieSuoTiaoJian.text = string.Format(miaoshu, starts[index]);
        }
        else
        {
            if (starts[index] <= start)
            {
                //已解锁
                m_YiJieSuo.visible = true;
                m_WeiJieSuo.visible = false;
                m_name.text = UIUtils.GetPetName(petBean, starts[index]);
            }
            else
            {
                m_YiJieSuo.visible = false;
                m_WeiJieSuo.visible = true;
                //未解锁
                if (starts[index] == 10)
                    m_JieSuoTiaoJian.text = "Z结晶开启第一阶段";
                else
                    m_JieSuoTiaoJian.text = string.Format(miaoshu, starts[index]);
            }
        }
        if (starts.Length < suofang.Length)
        {
            int num = starts.Length;
            int num2 = suofang.Length;
            for (int i = 0; i < num; ++i)
            {
                suofang[i] = suofang[i + (num2 - num)];
            }
        }
        OnShowModel(starts[index]);
    }
    public void OnChange(int xiabiao)
    {
        index = xiabiao;
        FillData();
    }
    private void OnShowModel(int star)
    {
        resPack.CacheWrapper(m_model);
        var wapper = new GoWrapper();
        m_model.SetNativeObject(wapper);
        actor = resPack.NewActorUI(petBean.t_id, star,ActorType.Pet, wapper,true,false);
        actor.SetTransform(new UnityEngine.Vector3(0, 0, 100), 100 * (suofang[index]), new UnityEngine.Vector3(0, 180, 0));
        actor.MouseRotate(m_dianjixiangying);
    }
    private void Ondonghua()
    {
        actor.PlayRandomAni();
    }
    public override void Dispose()
    {
        if (resPack != null)
        {
            resPack.ReleaseAllRes();
            resPack = null;
        }
        actor = null;
        base.Dispose();
    }
}