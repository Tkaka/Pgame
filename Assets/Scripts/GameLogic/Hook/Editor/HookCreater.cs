using UnityEngine;
using UnityEditor;
using Message.BehaviourHook;
using System.Collections.Generic;
using System.IO;
using System;

public class HookCreater
{
    //[MenuItem("Tools/Game/逻辑拆分MonoBehaviour")]
    static void createHook()
    {
        UnityEngine.Object[] objs = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        if (objs == null)
            return;

        EditorUtility.DisplayProgressBar("提示", "开始拆分", 0);
        EditorUtility.ClearProgressBar();
        int idx = 0;
        float num = objs.Length;
        foreach (var o in objs)
        {
            idx++;
            GameObject obj = o as GameObject;
            if (obj == null)
                continue;

            EditorUtility.DisplayProgressBar("提示", "开始拆分" + obj.name, idx / num);
            CreateObjHook(obj);
        }
        AssetDatabase.Refresh();
        EditorUtility.ClearProgressBar();
        Debug.Log("拆分成功");
    }

    public static void CreateObjHook(GameObject obj)
    {
        if (obj == null)
            return;

        var hook = new Hook();
        hook.hookList.AddRange(actorBehaviourHook(obj));
        hook.hookList.AddRange(skillConfigHook(obj));
        hook.hookList.AddRange(actorSpawnerHook(obj));
        if (hook.hookList.Count > 0)
            saveHook(obj, hook.GetNewMsgData(), hook.GetMsgSize());
        EditorUtility.SetDirty(obj);
        AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);
        EditorUtility.SetDirty(obj);
        AssetDatabase.SaveAssets();
    }

    static List<BaseHook> actorBehaviourHook(GameObject obj)
    {
        var list = new List<BaseHook>();
        var coms = obj.GetComponentsInChildren<ActorBehavior>(true);
        foreach(var com in coms)
        {
            var msg = new HActorBehaviour();
            msg.hangPath.AddRange(getPath(obj.transform, com.transform));

            if (com.headBar != null)
                msg.headBarPath.AddRange(getPath(com.transform, com.headBar));
            if (com.hitPos != null)
                msg.hitPosPath.AddRange(getPath(com.transform, com.hitPos));
            if (com.entranceShot != null)
                msg.entranceShotPath.AddRange(getPath(com.transform, com.entranceShot.transform));
            msg.entranceNameTime = com.entranceDelay;
            list.Add(msg);
        }
        return list;
    }

    static List<BaseHook> skillConfigHook(GameObject obj)
    {
        var list = new List<BaseHook>();
        var coms = obj.GetComponentsInChildren<SkillConfig>(true);
        foreach (var com in coms)
        {
            var msg = new HSkillConfig();
            msg.hangPath.AddRange(getPath(obj.transform, com.transform));

            msg.aniName = (int)com.aniName;
            msg.closeCombat = com.closeCombat;
            msg.skillId = com.skillId;
            msg.skillLevel = com.skillLevel;
            msg.skillType = (int)com.skillType;
            msg.skillTemplate = (int)com.skillTemplate;
            msg.headEffect = com.headEffect;
            msg.attackSound = com.attackSound;
            msg.hitSound = com.hitSound;
            msg.attackEffect = com.attackEffect;
            msg.handEffect = com.handEffect;
            msg.hitEffect = com.hitEffect;
            msg.bulletPrefab = com.bulletPrefab;
            msg.bulletHitEffect = com.bulletHitEffect;
            msg.flySpeed = com.flySpeed;
            msg.bulletDuration = com.bulletDuration;
            msg.bulletInterval = com.bulletInterval;
            msg.hitColor = (com.hitColor.a) << 24 | (com.hitColor.r) << 16 | (com.hitColor.g) << 8 | (com.hitColor.b);
            msg.colorElement = (int)com.colorElement;
            msg.comboFrame = com.comboFrame;
            msg.moveBack = com.moveBack;
            msg.skillTotalTime = com.skillTotalTime;
            msg.standingPoint = (int)com.standingPoint;
            msg.standOffsetX = com.standOffset.x;
            msg.standOffsetY = com.standOffset.y;
            msg.hurtDelay = com.hurtDelay;
            msg.hideOthers = com.hideOthers;
            msg.onlyShowSelfTime = com.onlyShowSelfTime;
            msg.shakeCameraDur = com.shakeCameraDur;
            msg.shakeCameraStrength = com.shakeCameraStrength;
            msg.shakeCameraVibrato = com.shakeCameraVibrato;
            msg.shakeCameraDistance = com.shakeCameraDistance;

            if (com.closeShot != null)
                msg.closeShotPath.AddRange(getPath(obj.transform, com.closeShot.transform));

            if(com.skillFlowList != null)
            {
                HSkillKeyFrame skf = null;
                for (int i = 0; i < com.skillFlowList.Length; ++i)
                {
                    var frame = com.skillFlowList[i];
                    skf = new HSkillKeyFrame();
                    skf.keyFrame = frame.keyFrame;
                    skf.type = (int)frame.type;
                    skf.ease = (int)frame.ease;
                    skf.hurtCount = frame.hurtCount;
                    skf.thunderPos = (int)frame.thunderPos;
                    skf.colMoveTime = frame.ColMoveTime;
                    skf.colMoveDis = frame.ColMoveDis;
                    skf.bulletModel = (int)frame.BulletModel;
                    skf.hurtState = (int)frame.hurtState;
                    skf.FreezeTime = frame.FreezeTime;
                    skf.back = frame.Back;
                    msg.skillFlowList.Add(skf);
                }
            }

            if(com.keyframes != null)
            {
                HSkillKeyFrame skf = null;
                for(int i=0; i<com.keyframes.Length; ++i)
                {
                    var frame = com.keyframes[i];
                    skf = new HSkillKeyFrame();
                    skf.keyFrame = frame.keyFrame;
                    skf.type = (int)frame.type;
                    skf.ease = (int)frame.ease;
                    skf.hurtCount = frame.hurtCount;
                    skf.thunderPos = (int)frame.thunderPos;
                    skf.colMoveTime = frame.ColMoveTime;
                    skf.colMoveDis = frame.ColMoveDis;
                    skf.bulletModel = (int)frame.BulletModel;
                    skf.hurtState = (int)frame.hurtState;
                    skf.FreezeTime = frame.FreezeTime;
                    skf.back = frame.Back;
                    msg.keyframes.Add(skf);
                }
            }

            if(com.otherKeyframes != null)
            {
                HSkillKeyFrame skf = null;
                for (int i = 0; i < com.otherKeyframes.Length; ++i)
                {
                    var frame = com.otherKeyframes[i];
                    skf = new HSkillKeyFrame();
                    skf.keyFrame = frame.keyFrame;
                    skf.type = (int)frame.type;
                    skf.ease = (int)frame.ease;
                    skf.hurtCount = frame.hurtCount;
                    skf.thunderPos = (int)frame.thunderPos;
                    skf.colMoveTime = frame.ColMoveTime;
                    skf.colMoveDis = frame.ColMoveDis;
                    skf.bulletModel = (int)frame.BulletModel;
                    skf.hurtState = (int)frame.hurtState;
                    skf.FreezeTime = frame.FreezeTime;
                    skf.back = frame.Back;
                    msg.otherKeyframes.Add(skf);
                }
            }

            if(com.mountPoints != null)
            {
                HMountPoint mp = null;
                for (int i = 0; i < com.mountPoints.Length; ++i)
                {
                    var mount = com.mountPoints[i];
                    mp = new HMountPoint();

                    mp.type = (int)mount.type;
                    mp.isLocal = mount.isLocal;
                    mp.onlyRotateY = mount.onlyRotateY;
                    if (mount.trans != null)
                        mp.transPath.AddRange(getPath(obj.transform, mount.trans));
                    msg.mountPoints.Add(mp);
                }
            }

            list.Add(msg);
        }
        return list;
    }

    static List<BaseHook> actorSpawnerHook(GameObject obj)
    {
        var list = new List<BaseHook>();
        var coms = obj.GetComponentsInChildren<ActorSpawner>(true);
        foreach(var com in coms)
        {
            var msg = new HActorSpawner();
            msg.hangPath.AddRange(getPath(obj.transform, com.transform));

            msg.actorType = (int)com.Type;
            list.Add(msg);
        }
        return list;
    }

    static void saveHook(GameObject obj, byte[] data, int size)
    {
        if (!Directory.Exists(Application.dataPath + "/hook"))
            Directory.CreateDirectory(Application.dataPath + "/hook");
        var path = Application.dataPath + "/hook/" + obj.name + "@hook.bytes";
        AssetDatabase.Refresh();

        var hook = obj.GetComponent<MonoBehaviourHook>();
        if (hook != null)
        {
            if (hook.original != null)
            {
                var oldPath = AssetDatabase.GetAssetPath(hook.original);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.Refresh();
            }
        }
        else
        {
            hook = obj.AddComponent<MonoBehaviourHook>();
        }

        var file = new FileInfo(path);
        if (file.Exists)
            file.Delete();

        var stream = file.OpenWrite();
        stream.Write(data, 0, size);
        stream.Flush();
        stream.Close();
        stream.Dispose();
        hook.original = AssetDatabase.LoadAssetAtPath<TextAsset>(path.Substring(Application.dataPath.Length - 6));
        AssetDatabase.Refresh();
    }

    static int[] getPath(Transform root, Transform target)
    {
        if (target == root)
            return new int[] { -1 };

        var now = target;
        List<int> list = new List<int>();
        while (now != root)
        {
            list.Insert(0, now.GetSiblingIndex());
            now = now.parent;
            if (now == null)
            {
                Debug.LogError("找不到路径>" + target.name);
                break;
            }
        }
        return list.ToArray();
    }
}
