using FairyGUI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillEditor : MonoBehaviour
{
    [Tooltip("攻击者")]
    public ActorBehavior attacker;

    [Tooltip("防御者")]
    public ActorBehavior defender;

    [Tooltip("当前选中的列")]
    public int selectCol;

    [Tooltip("前排是否还有人")]
    public bool hasFrontRow= true;

    public List<ActorBehavior> normalDefenderList;

    public List<ActorBehavior> smallDefenderList;

    public List<ActorBehavior> masterDefenderList;

    private List<EditorActor> defenders = new List<EditorActor>();

    //private EditorActor editorActor;

    public Button normalSkillBtn;

    public Button smallSkillBtn;

    public Button masterSkillBtn;

    private void Awake()
    {
        Logger.CurLevel = Logger.Level.None;
        WinMgr.Singleton.Init();
        UIPackage.AddPackage(WinEnum.BasePath + WinEnum.UI_Common);
        UIPackage.AddPackage(WinEnum.BasePath + WinEnum.UI_Battle);
        // 窗口注册
        new RegisterWindowCmd().Excute();
        SpawnerHelper.Singleton.Init(SpawnerManager.Singleton);
    }

    private void Update()
    {
        ActorManager.Singleton.Update();
    }

    private void Start()
    {
        if (normalSkillBtn != null)
            normalSkillBtn.onClick.AddListener(OnNormalSkill);
        if (smallSkillBtn != null)
            smallSkillBtn.onClick.AddListener(OnSmallSkill);
        if (masterSkillBtn != null)
            masterSkillBtn.onClick.AddListener(OnMasterSkill);
        //editorActor = new EditorActor(999, ActorType.Pet, ActorCamp.CampFriend, 0);
        //editorActor.Init(attacker);
    }

    private EditorActor GetAttackerActor()
    {
        EditorActor editorActor = new EditorActor(999, ActorType.Pet, ActorCamp.CampFriend, 0);
        editorActor.Init(attacker);
        return editorActor;
    }

    private EditorSkill normalSkill;
    private void OnNormalSkill()
    {
        SkillConfig config = attacker.GetSkillConfig(SkillType.NormalAttack);
        if (config != null)
        {
            if(normalSkill == null)
                normalSkill = new EditorSkill();
            normalSkill.Init(config, GetAttackerActor());
            normalSkill.skillPos = GetSkillPos(config);
            normalSkill.skillDir = attacker.transform.forward;
            normalSkill.targetCol = selectCol;
            normalSkill.target = defender;
            normalSkill.defenderList = normalDefenderList;
            normalSkill.onEnter();
        }
        else
        {
            Debug.Log("没有普通攻击");
        }
    }

    private EditorSkill smallSkill;
    private void OnSmallSkill()
    {
        SkillConfig config = attacker.GetSkillConfig(SkillType.SmallSkill);
        if (config != null)
        {
            if(smallSkill == null)
               smallSkill = new EditorSkill();
            smallSkill.Init(config, GetAttackerActor());
            smallSkill.skillPos = GetSkillPos(config);
            smallSkill.skillDir = attacker.transform.forward;
            smallSkill.targetCol = selectCol;
            smallSkill.target = defender;
            smallSkill.defenderList = smallDefenderList;
            smallSkill.onEnter();
        }
        else
        {
            Debug.Log("没有小技能");
        }
    }

    private EditorSkill masterSkill;
    private void OnMasterSkill()
    {
        SkillConfig config = attacker.GetSkillConfig(SkillType.MasterSkill);
        if (config != null)
        {
            if(masterSkill == null)
                masterSkill = new EditorSkill();
            masterSkill.Init(config, GetAttackerActor());
            masterSkill.skillPos = GetSkillPos(config);
            masterSkill.skillDir = attacker.transform.forward;
            masterSkill.targetCol = selectCol;
            masterSkill.target = defender;
            masterSkill.defenderList = masterDefenderList;
            masterSkill.onEnter();
        }
        else
        {
            Debug.Log("没有大招");
        }
    }

    public Vector3 GetSkillPos(SkillConfig skillConfig)
    {
        Vector3 skillDir = Vector3.zero;
        Vector3 skillPos = SpawnerHelper.Singleton.GetAssist(0).position;
        switch (skillConfig.standingPoint)
        {
            case StandingPoint.SingleFrontRow:
            case StandingPoint.SingleBackRow:
                skillPos = defender.TransformExt.position;
                skillPos += defender.TransformExt.forward * 1.2f;
                skillDir = -defender.TransformExt.forward;
                break;
            case StandingPoint.AssistMid:
            case StandingPoint.BulletMid:
                Transform transMid = SpawnerHelper.Singleton.GetAssist(1);
                if (transMid != null)
                {
                    if (hasFrontRow)
                    {
                        skillDir = transMid.forward;
                        skillPos = transMid.position;
                    }
                    else
                    {
                        skillDir = transMid.forward;
                        if (skillConfig.standingPoint == StandingPoint.BulletMid)
                            skillPos = transMid.position + skillDir;
                        else
                            skillPos = transMid.position + 2 * skillDir;
                    }
                }
                break;
            case StandingPoint.AssistCol:
                //获取目标所在列
                Transform trans1 = SpawnerHelper.Singleton.GetAssist(selectCol);
                if (trans1 != null)
                {
                    skillDir = trans1.forward;
                    skillPos = trans1.position;
                }
                break;
            case StandingPoint.BulletCol:
                //获取目标所在列
                Transform btrans1 = SpawnerHelper.Singleton.GetAssist(selectCol);
                if (btrans1 != null)
                {
                    if (hasFrontRow)
                    {
                        skillDir = btrans1.forward;
                        skillPos = btrans1.position - skillDir;
                    }
                    else
                    {
                        skillDir = btrans1.forward;
                        skillPos = btrans1.position + skillDir;
                    }
                }
                break;
            case StandingPoint.Original:
                skillDir = attacker.TransformExt.forward;
                skillPos = attacker.TransformExt.position;
                break;
            case StandingPoint.OppositeCenter:
                skillDir = attacker.TransformExt.forward;
                skillPos = SpawnerHelper.Singleton.GetColCenter(ActorCamp.CampEnemy, GridEnum.Col1);
                break;
        }

        return skillPos;
    }


}
