using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEditorInternal;
using Cinemachine.Editor;

[CustomEditor(typeof(SkillConfig))]
public class SkillConfigEditor : BaseEditor<SkillConfig>
{
    private SerializedProperty aniName, moveBack, comboFrame, standingPoint, standOffset, hitColor, colorElement;
    private SerializedProperty skillId, skillLevel, skillType, skillTemplate, headEffect, attackSound, hitSound, skillTotalTime;
    private SerializedProperty bulletPrefab, bulletHitEffect, flySpeed, bulletDuration, bulletInterval;
    private SerializedProperty attackEffect, hitEffect, handEffect, closeShot, closeCombat, hurtDelay, hideOthers, onlyShowSelfTime;
    private SerializedProperty mountPoints, keyframes, otherKeyframes, hurtCount, skillFlows;

    private SerializedProperty shakeDur, shakeStr, shakeVib, shakeDis;

    private ReorderableList keyframeList, mountPointList, otherKeyframeList, skillFlowOrderList;

    private void OnEnable()
    {
        comboFrame = serializedObject.FindProperty("comboFrame");
        skillId = serializedObject.FindProperty("skillId");
        skillLevel = serializedObject.FindProperty("skillLevel");
        aniName = serializedObject.FindProperty("aniName");
        skillType = serializedObject.FindProperty("skillType");
        skillTemplate = serializedObject.FindProperty("skillTemplate");
        moveBack = serializedObject.FindProperty("moveBack");
        closeCombat = serializedObject.FindProperty("closeCombat");
        standingPoint = serializedObject.FindProperty("standingPoint");
        standOffset = serializedObject.FindProperty("standOffset");
        skillTotalTime = serializedObject.FindProperty("skillTotalTime");
        hitColor = serializedObject.FindProperty("hitColor");
        colorElement = serializedObject.FindProperty("colorElement");
        hideOthers = serializedObject.FindProperty("hideOthers"); 
        onlyShowSelfTime = serializedObject.FindProperty("onlyShowSelfTime");

        //头部
        attackSound = serializedObject.FindProperty("attackSound");
        hitSound = serializedObject.FindProperty("hitSound");
        headEffect = serializedObject.FindProperty("headEffect");
        //bullet
        //bulletType = serializedObject.FindProperty("bulletType");
        bulletPrefab = serializedObject.FindProperty("bulletPrefab");
        bulletHitEffect = serializedObject.FindProperty("bulletHitEffect");
        flySpeed = serializedObject.FindProperty("flySpeed");
        bulletDuration = serializedObject.FindProperty("bulletDuration");
        bulletInterval = serializedObject.FindProperty("bulletInterval");
        //normal skill
        attackEffect = serializedObject.FindProperty("attackEffect");
        handEffect = serializedObject.FindProperty("handEffect");
        hitEffect = serializedObject.FindProperty("hitEffect");
        //镜头特写
        closeShot = serializedObject.FindProperty("closeShot");
        //mount points
        mountPoints = serializedObject.FindProperty("mountPoints");
        keyframes = serializedObject.FindProperty("keyframes");
        otherKeyframes = serializedObject.FindProperty("otherKeyframes");
        skillFlows = serializedObject.FindProperty("skillFlows");
        hurtDelay = serializedObject.FindProperty("hurtDelay");
        
        shakeDur = serializedObject.FindProperty("shakeCameraDur");
        shakeStr = serializedObject.FindProperty("shakeCameraStrength");
        shakeVib = serializedObject.FindProperty("shakeCameraVibrato");
        shakeDis = serializedObject.FindProperty("shakeCameraDistance");
    }

    public override void OnInspectorGUI()
    {
        BeginInspector();

        EditorGUI.BeginChangeCheck();

        GUI.enabled = false;
        EditorGUILayout.PropertyField(skillId);
        EditorGUILayout.PropertyField(skillLevel);
        GUI.enabled = true;
        EditorGUILayout.PropertyField(skillType);
        EditorGUILayout.PropertyField(aniName);
        EditorGUILayout.PropertyField(skillTotalTime);
        //EditorGUILayout.PropertyField(closeCombat);
        EditorGUILayout.PropertyField(standingPoint);
        EditorGUILayout.PropertyField(standOffset);
        EditorGUILayout.PropertyField(hurtDelay);
        EditorGUILayout.PropertyField(hideOthers);
        EditorGUILayout.PropertyField(onlyShowSelfTime);

        if (GUI.changed)
        {
            skillTotalTime.intValue = GetAniLen();
        }

        if (skillFlowOrderList == null)
            SetupSkillFlowOrderList();
        if (skillFlowOrderList.index >= skillFlowOrderList.count)
            skillFlowOrderList.index = skillFlowOrderList.count - 1;
        skillFlowOrderList.DoLayoutList();

        if (keyframeList == null)
            SetupKeyframeList();
        if (keyframeList.index >= keyframeList.count)
            keyframeList.index = keyframeList.count - 1;
        keyframeList.DoLayoutList();

        if (otherKeyframeList == null)
            SetupOtherKeyframeList();
        if (otherKeyframeList.index >= otherKeyframeList.count)
            otherKeyframeList.index = otherKeyframeList.count - 1;
        otherKeyframeList.DoLayoutList();

        //EditorGUILayout.PropertyField(comboFrame);
        float oldWid = EditorGUIUtility.labelWidth;
        GUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 60;
        EditorGUILayout.PropertyField(colorElement, new GUIContent("HitColor"));
        EditorGUILayout.PropertyField(hitColor, new GUIContent(""));
        GUILayout.EndHorizontal();
        EditorGUIUtility.labelWidth = oldWid;
        //hitColor.colorValue = new Color(1, 1, 1);
        if (GUI.changed)
        {
            hitColor.colorValue = AttackUtils.GetHitColor((ColorElement)colorElement.enumValueIndex);
        }

        ShowSerializedProperty(attackSound, SoundPath);
        ShowSerializedProperty(hitSound, SoundPath);
        EditorGUILayout.PropertyField(skillTemplate);

        if (HasBullet())
        {
            //EditorGUILayout.PropertyField(bulletType);
            ShowSerializedProperty(bulletPrefab);
            if (HasTimeBullet())
            {
                //EditorGUILayout.PropertyField(bulletDuration);
                //EditorGUILayout.PropertyField(bulletInterval);
            }
            else
            {
                ShowSerializedProperty(bulletHitEffect);
                EditorGUILayout.PropertyField(flySpeed);
            }
        }

        if (HasOffset())
        {
            //EditorGUILayout.PropertyField(moveBack);
        }

        ShowSerializedProperty(attackEffect);
        ShowSerializedProperty(handEffect);
        ShowSerializedProperty(headEffect);
        ShowSerializedProperty(hitEffect);

        EditorGUILayout.PropertyField(closeShot);
        //EditorGUILayout.PropertyField(mountPoints, true);

        if (mountPointList == null)
            SetupMountPointList();
        if (mountPointList.index >= mountPointList.count)
            mountPointList.index = mountPointList.count - 1;
        mountPointList.DoLayoutList();
        
        if(HasShake())
        {
            ShowSerializedProperty(shakeDur);
            ShowSerializedProperty(shakeStr);
            ShowSerializedProperty(shakeVib);
            ShowSerializedProperty(shakeDis);
        }

        //if (Event.current.commandName == "ObjectSelectorUpdated")
        if (Event.current.commandName == "ObjectSelectorClosed")
        {
            Object selectObj = EditorGUIUtility.GetObjectPickerObject();
            if (selectObj != null)
            {
                string path = AssetDatabase.GetAssetPath(selectObj);
                if (string.IsNullOrEmpty(path) || !path.Contains("/ArtResources/OutPut"))
                {
                    Debug.Log("非法的路径");
                }
                else
                {
                    if (curProperty != null)
                    {
                        //Assets/Resources/_Prefabs/Effect/Attack/eff_pet_BaoLiLong_skill01_blue.prefab
                        path = path.Replace(CurPath, "");
                        //去除后缀
                        path = path.Replace(".prefab", "");
                        path = path.Replace(".ogg", "");
                        //curProperty.stringValue = path.Replace(CurPath, "");
                        curProperty.stringValue = selectObj.name;
                        serializedObject.ApplyModifiedProperties();
                    }
                }
            }
        }

        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
    }

    private const string EffectPath = "Assets/ArtResources/OutPut/Effect/";
    private const string SoundPath = "Assets/ArtResources/OutPut/Other/Sound/";
    private string CurPath;    
    private SerializedProperty curProperty;
    private void ShowSerializedProperty(SerializedProperty property, string curPath= EffectPath)
    {
        GUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(property);
        //EditorGUILayout.TextField(property.stringValue);
        //if (GUILayout.Button(EditorGUIUtility.FindTexture("PrefabNormal Icon")))
        if (GUILayout.Button("", GUI.skin.GetStyle("AssetLabel Icon")))
        {
            int controlID = GUIUtility.GetControlID(FocusType.Passive);
            curProperty = property;
            CurPath = curPath;
            if(CurPath == EffectPath)
                EditorGUIUtility.ShowObjectPicker<GameObject>(null, false, null, controlID);
            else
                EditorGUIUtility.ShowObjectPicker<AudioClip>(null, false, null, controlID);
        }
        GUILayout.EndHorizontal();
    }

    private int GetAniLen()
    {
        Transform parent = Target.transform.parent;
        if (parent != null)
        {
            SimpleAnimation sani = parent.GetComponentInChildren<SimpleAnimation>();
            if (sani != null)
            {
                string stateName = System.Enum.GetName(typeof(AniName), aniName.enumValueIndex);
                SimpleAnimation.EditorState[] states = sani.GetEditorStates;
                if (states != null && states.Length > 0)
                {
                    foreach (SimpleAnimation.EditorState es in states)
                    {
                        if (es.name == stateName)
                        {
                            return (int)(es.clip.length * 1000);
                        }
                    }
                }
                else
                {
                    Debug.LogError("SimpleAnimationCtrl:GetClip:Can not find state: " + stateName);
                }
            }
        }
        return 0;
    }

    void SetupSkillFlowOrderList()
    {
        skillFlowOrderList = new ReorderableList(
                serializedObject, FindProperty(x => x.skillFlowList),
                true, true, true, true);

        skillFlowOrderList.drawHeaderCallback = (Rect rect) =>
        { EditorGUI.LabelField(rect, "技能流程"); };

        skillFlowOrderList.drawElementCallback
            = (Rect rect, int index, bool isActive, bool isFocused) =>
            { DrawSkillFlowOrderEditor(rect, index); };
    }

    void DrawSkillFlowOrderEditor(Rect rect, int index)
    {
        SkillKeyFrame def = new SkillKeyFrame();
        SerializedProperty element = skillFlowOrderList.serializedProperty.GetArrayElementAtIndex(index);

        float originWidth = EditorGUIUtility.labelWidth;

        Rect r = new Rect(rect.position, new Vector2(150, rect.height));
        EditorGUIUtility.labelWidth = 45;
        EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.type), new GUIContent("步骤" + index));
        EditorGUIUtility.labelWidth = originWidth;

        if (Target.skillFlowList[index].type == SkillKeyFrame.Type.Hurt)
        {
            float oldx = r.x + r.width + 10;
            r = new Rect(rect.position, new Vector2(60, rect.height));
            r.x = oldx;
            EditorGUIUtility.labelWidth = 30;
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.hurtCount), new GUIContent("次数"));
            EditorGUIUtility.labelWidth = originWidth;
        }
        else if (Target.skillFlowList[index].type == SkillKeyFrame.Type.MoveForward)
        {
            float oldx = r.x + r.width + 10;
            r = new Rect(rect.position, new Vector2(100, rect.height));
            r.x = oldx;
            EditorGUIUtility.labelWidth = 30;
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.ease), new GUIContent("缓动"));
            EditorGUIUtility.labelWidth = originWidth;
        }
        else if (Target.skillFlowList[index].type == SkillKeyFrame.Type.Thunder)
        {
            float oldx = r.x + r.width + 10;
            r = new Rect(rect.position, new Vector2(100, rect.height));
            r.x = oldx;
            EditorGUIUtility.labelWidth = 40;
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.thunderPos), new GUIContent("落雷点"));
            EditorGUIUtility.labelWidth = originWidth;
        }
        else if (Target.skillFlowList[index].type == SkillKeyFrame.Type.ColMove)
        {
            float oldx = r.x + r.width + 10;
            r = new Rect(rect.position, new Vector2(100, rect.height));
            r.x = oldx;
            EditorGUIUtility.labelWidth = 40;
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.ColMoveTime), new GUIContent("时间"));
            oldx = r.x + r.width + 10;
            r = new Rect(rect.position, new Vector2(100, rect.height));
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.ColMoveDis), new GUIContent("距离"));
            EditorGUIUtility.labelWidth = originWidth;
        }
        else if (Target.skillFlowList[index].type == SkillKeyFrame.Type.MoveBackward)
        {
            float oldx = r.x + r.width + 10;
            r = new Rect(rect.position, new Vector2(100, rect.height));
            r.x = oldx;
            EditorGUIUtility.labelWidth = 30;
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.ease), new GUIContent("缓动"));
            EditorGUIUtility.labelWidth = originWidth;
        }
    }

    void SetupKeyframeList()
    {
        keyframeList = new ReorderableList(
                serializedObject, FindProperty(x => x.keyframes),
                true, true, true, true);

        keyframeList.drawHeaderCallback = (Rect rect) =>
        { EditorGUI.LabelField(rect, "伤害关键帧"); };

        keyframeList.drawElementCallback
            = (Rect rect, int index, bool isActive, bool isFocused) =>
            { DrawKeyframeEditor(rect, index); };

        //mWaypointList.onAddCallback = (ReorderableList l) =>
        //{ InsertWaypointAtIndex(l.index); };
    }


    void DrawKeyframeEditor(Rect rect, int index)
    {
        SkillKeyFrame def = new SkillKeyFrame();
        SerializedProperty element = keyframeList.serializedProperty.GetArrayElementAtIndex(index);

        float originWidth = EditorGUIUtility.labelWidth;

        float hSpace = 30;
        Rect r = new Rect(rect.position, new Vector2(75, rect.height));
        EditorGUIUtility.labelWidth = 45;
        EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.keyFrame), new GUIContent("frame"));

        r.x += r.width + hSpace;
        r.width = 80;
        EditorGUIUtility.labelWidth = 40;
        EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.type), new GUIContent(""));
        EditorGUIUtility.labelWidth = originWidth;

        if (Target.keyframes[index].type == SkillKeyFrame.Type.Hurt || Target.keyframes[index].type == SkillKeyFrame.Type.BulletHurt)
        {
            float oldx = r.x + r.width + 10;
            r = new Rect(rect.position, new Vector2(60, rect.height));
            r.x = oldx;
            EditorGUIUtility.labelWidth = 30;
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.hurtState), new GUIContent(""));
            r.x += r.width + 10;
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.hurtCount), new GUIContent("次数"));
            EditorGUIUtility.labelWidth = originWidth;
        }
        else if (Target.keyframes[index].type == SkillKeyFrame.Type.MoveForward)
        {
            float oldx = r.x + r.width + 10;
            r = new Rect(rect.position, new Vector2(100, rect.height));
            r.x = oldx;
            EditorGUIUtility.labelWidth = 30;
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.ease), new GUIContent("缓动"));
            EditorGUIUtility.labelWidth = originWidth;
        }

        //GUILayout.BeginHorizontal();
        //EditorGUILayout.PropertyField(element.FindPropertyRelative(() => def.keyFrame), new GUIContent("frame"));
        //EditorGUILayout.PropertyField(element.FindPropertyRelative(() => def.type), new GUIContent("Type"));
        //GUILayout.EndHorizontal();
    }

    void SetupOtherKeyframeList()
    {
        otherKeyframeList = new ReorderableList(
                serializedObject, FindProperty(x => x.otherKeyframes),
                true, true, true, true);

        otherKeyframeList.drawHeaderCallback = (Rect rect) =>
        { EditorGUI.LabelField(rect, "其他关键帧"); };

        otherKeyframeList.drawElementCallback
            = (Rect rect, int index, bool isActive, bool isFocused) =>
            { DrawOtherKeyframeEditor(rect, index); };
    }


    void DrawOtherKeyframeEditor(Rect rect, int index)
    {
        SkillKeyFrame def = new SkillKeyFrame();
        SerializedProperty element = otherKeyframeList.serializedProperty.GetArrayElementAtIndex(index);

        float originWidth = EditorGUIUtility.labelWidth;

        float hSpace = 30;
        Rect r = new Rect(rect.position, new Vector2(75, rect.height));
        EditorGUIUtility.labelWidth = 45;
        EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.keyFrame), new GUIContent("frame"));

        r.x += r.width + hSpace;
        r.width = 80;
        EditorGUIUtility.labelWidth = 40;
        EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.type), new GUIContent(""));
        EditorGUIUtility.labelWidth = originWidth;

        if (Target.otherKeyframes[index].type == SkillKeyFrame.Type.Hurt)
        {
            float oldx = r.x + r.width + 10;
            r = new Rect(rect.position, new Vector2(60, rect.height));
            r.x = oldx;
            EditorGUIUtility.labelWidth = 30;
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.hurtCount), new GUIContent("次数"));
            EditorGUIUtility.labelWidth = originWidth;
        }
        else if (Target.otherKeyframes[index].type == SkillKeyFrame.Type.MoveForward)
        {
            float oldx = r.x + r.width + 10;
            r = new Rect(rect.position, new Vector2(100, rect.height));
            r.x = oldx;
            EditorGUIUtility.labelWidth = 30;
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.ease), new GUIContent("缓动"));
            EditorGUIUtility.labelWidth = originWidth;
        }
        else if (Target.otherKeyframes[index].type == SkillKeyFrame.Type.Thunder)
        {
            float oldx = r.x + r.width + 10;
            r = new Rect(rect.position, new Vector2(100, rect.height));
            r.x = oldx;
            EditorGUIUtility.labelWidth = 40;
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.thunderPos), new GUIContent("落雷点"));
            EditorGUIUtility.labelWidth = originWidth;
        }
        else if (Target.otherKeyframes[index].type == SkillKeyFrame.Type.ColMove)
        {
            float oldx = r.x + r.width + 10;
            r = new Rect(rect.position, new Vector2(70, rect.height));
            r.x = oldx;
            EditorGUIUtility.labelWidth = 30;
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.ColMoveTime), new GUIContent("时间"));
            r.x += r.width + 10;
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.ColMoveDis), new GUIContent("距离"));
            r.x += r.width + 10;
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.Back), new GUIContent("回来"));
            EditorGUIUtility.labelWidth = originWidth;
        }
        else if (Target.otherKeyframes[index].type == SkillKeyFrame.Type.Bullet)
        {
            float oldx = r.x + r.width + 10;
            r = new Rect(rect.position, new Vector2(100, rect.height));
            r.x = oldx;
            EditorGUIUtility.labelWidth = 30;
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.BulletModel), new GUIContent("模式"));
            //r.x += r.width + 10;
            //EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.ColMoveDis), new GUIContent("距离"));
            EditorGUIUtility.labelWidth = originWidth;
        }
        else if (Target.otherKeyframes[index].type == SkillKeyFrame.Type.FreezeFrame)
        {
            float oldx = r.x + r.width + 10;
            r = new Rect(rect.position, new Vector2(100, rect.height));
            r.x = oldx;
            EditorGUIUtility.labelWidth = 30;
            EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.FreezeTime), new GUIContent("时间"));
            EditorGUIUtility.labelWidth = originWidth;
        }
    }



    private void SetupMountPointList()
    {
        mountPointList = new ReorderableList(
                serializedObject, FindProperty(x => x.mountPoints),
                true, true, true, true);

        mountPointList.drawHeaderCallback = (Rect rect) =>
        { EditorGUI.LabelField(rect, "特效挂点"); };

        mountPointList.drawElementCallback
            = (Rect rect, int index, bool isActive, bool isFocused) =>
            { DrawMountPointEditor(rect, index); };

        //mWaypointList.onAddCallback = (ReorderableList l) =>
        //{ InsertWaypointAtIndex(l.index); };
    }

    void DrawMountPointEditor(Rect rect, int index)
    {
        MountPoint def = new MountPoint();
        SerializedProperty element = mountPointList.serializedProperty.GetArrayElementAtIndex(index);

        float originWidth = EditorGUIUtility.labelWidth;

        float hSpace = 1;
        Rect r = new Rect(rect.position, new Vector2(80, EditorGUIUtility.singleLineHeight));
        r.y += 2;
        EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.type), GUIContent.none);

        r.x += r.width + hSpace;
        r.width = 100;
        EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.trans), GUIContent.none);

        r.x += r.width + hSpace;
        r.width = 70;
        EditorGUIUtility.labelWidth = 55;
        EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.isLocal),  new GUIContent("IsLocal"));

        r.x += r.width + hSpace;
        r.width = 65;
        EditorGUIUtility.labelWidth = 45;
        EditorGUI.PropertyField(r, element.FindPropertyRelative(() => def.onlyRotateY), new GUIContent("OnlyY"));

        EditorGUIUtility.labelWidth = originWidth;
    }

    private bool HasBullet()
    {
        if (Target.otherKeyframes == null || Target.otherKeyframes.Length <= 0)
            return false;
        foreach (SkillKeyFrame keyframe in Target.otherKeyframes)
        {
            if (keyframe.type == SkillKeyFrame.Type.Bullet || keyframe.type == SkillKeyFrame.Type.Thunder)
            {
                return true;
            }
        }
        return false;
    }

    private bool HasShake()
    {
        if (Target.otherKeyframes == null || Target.otherKeyframes.Length <= 0)
            return false;
        foreach (SkillKeyFrame keyframe in Target.otherKeyframes)
        {
            if (keyframe.type == SkillKeyFrame.Type.ShakeCamera)
            {
                return true;
            }
        }
        return false;
    }

    private bool HasTimeBullet()
    {
        if (Target.otherKeyframes == null || Target.otherKeyframes.Length <= 0)
            return false;
        foreach (SkillKeyFrame keyframe in Target.otherKeyframes)
        {
            if (keyframe.type == SkillKeyFrame.Type.Thunder)
            {
                return true;
            }
        }
        return false;
    }

    private bool HasOffset()
    {
        if (Target.keyframes == null || Target.keyframes.Length <= 0)
            return false;
        foreach (SkillKeyFrame keyframe in Target.keyframes)
        {
            if (keyframe.type == SkillKeyFrame.Type.MoveForward)
            {
                return true;
            }
        }
        return false;
    }

}
