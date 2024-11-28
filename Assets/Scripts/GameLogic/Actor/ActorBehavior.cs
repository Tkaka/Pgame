using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;

public enum MountPointType
{
    Head,
    Attack,
    Hit,
    Bullet,
    Hand,
}

[System.Serializable]
public class MountPoint
{
    [Tooltip("挂点类型")]
    public MountPointType type;
    public Transform trans;
    [Tooltip("勾选时代表将该特效挂在该节点下")]
    public bool isLocal;
    [Tooltip("勾选时代表将该特效只旋转Y轴")]
    public bool onlyRotateY = false;
}

/// <summary>
/// 实体可能用到的Unity相关接口
/// </summary>

public class ActorBehavior : BaseBehaviour
{

    //public ActorBase Owner {get; set;}

    public long actorId;

    public SimpleAnimation saniComp { get; private set; }

    public Transform mainObj { private set; get; }

    [Tooltip("血条挂点")]
    public Transform headBar;        //头顶条挂点

    [Tooltip("命中挂点")]
    public Transform hitPos;          //受击特效挂点

    public SkillConfig[] SkillConfigs { private set; get; }

    private Dictionary<SkillType, SkillConfig> skillConfigDic = new Dictionary<SkillType, SkillConfig>();

    public Renderer[] renders { get; protected set; }

    [HideInInspector]
    public Transform SkillConfigTrans;

    [Tooltip("捕捉特写镜头")]
    public PlayableDirector catchShot;

    [Tooltip("出场特写镜头")]
    public GameObject entranceShot;

    [Tooltip("出场特写镜头名字延时")]
    public float entranceDelay;

    protected override void Awake()
    {
        base.Awake();
        mainObj = TransformExt.GetChild(0);
        saniComp = mainObj.GetComponent<SimpleAnimation>();
        //saniComp = GetComponent<SimpleAnimation>();
        if (headBar == null)
            Logger.err(mainObj.name + "headBar is null");
        renders = mainObj.GetComponentsInChildren<Renderer>();
        Transform skillTran = TransformExt.Find("skillConfig");
        SkillConfigTrans = skillTran;
        if (skillTran != null)
        {
            SkillConfigs = skillTran.GetComponents<SkillConfig>();
            if (SkillConfigs != null)
            {
                foreach (SkillConfig config in SkillConfigs)
                {
                    if (!skillConfigDic.ContainsKey(config.skillType))
                        skillConfigDic.Add(config.skillType, config);
                    else
                        Logger.err("技能配置错误：" + transform.name);
                }
            }
        }
        if (catchShot != null)
        {
            foreach (PlayableBinding bind in catchShot.playableAsset.outputs)
            {
                if (bind.streamName == "Cinemachine Track")
                {
                    catchShot.SetGenericBinding(bind.sourceObject, Camera.main.GetComponent<Cinemachine.CinemachineBrain>());
                }
            }
        }
        if (entranceShot != null)
            entranceShot.SetActive(false);
    }

    public SkillConfig AddSkillConfig()
    {
        if (SkillConfigTrans != null)
        {
            return SkillConfigTrans.gameObject.AddComponent<SkillConfig>();
        }
        else
        {
            Logger.err(name + " 未能找到skillconfig");
        }
        return null;
    }

    public SkillConfig GetSkillConfig(SkillType type)
    {
        if (skillConfigDic.ContainsKey(type))
        {
            return skillConfigDic[type];
        }
        Logger.err("找不到" + type + "类型的技能配置:" + name);
        return null;
    }

    private Color originColor;
    protected long hitCoroId;
    public void OnHit(Color color, float duration=0.2f)
    {
        if (hitCoroId > 0)
            stopCoroutine(hitCoroId);
        if (renders != null && renders.Length > 0)
        {
            foreach (Renderer render in renders)
            {
                if (render.GetComponent<ParticleSystem>() != null)
                    continue;
                if (hitCoroId <= 0)
                    originColor = render.material.GetColor("_MainColor");
                render.material.SetFloat("_IsHit", 1);
                render.material.SetColor("_MainColor", color);
            }
            hitCoroId = delayCall(duration, hitReset);
        }
    }

    private void hitReset()
    {
        hitCoroId = 0;
        foreach (var render in renders)
        {
            render.material.SetFloat("_IsHit", 0);
            render.material.SetColor("_MainColor", originColor);
        }
    }

    public Vector3 headBarPos
    {
        get
        {
            if (headBar != null)
                return headBar.position;
            else
                return Vector3.zero;
        }
    }

    /// <summary>
    /// 释放内部引用
    /// </summary>
    protected override void OnDestroy()
    {
        renders = null;
        actorId = 0;
        base.OnDestroy();
    }
    
}
