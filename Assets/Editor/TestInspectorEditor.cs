using UnityEditor;

[CustomEditor(typeof(TestInspector))]//关联之前的脚本
public class TestInspectorEditor : Editor
{

    private SerializedObject test;//序列化
    private SerializedProperty m_type, a_int, b_int;//定义类型，变量a，变量b
    void OnEnable()
    {
        test = new SerializedObject(target);
        m_type = test.FindProperty("m_type");//获取m_type
        a_int = test.FindProperty("a_int");//获取a_int
        b_int = test.FindProperty("b_int");//获取b_int
    }

    public override void OnInspectorGUI()
    {
        test.Update();//更新test
        EditorGUILayout.PropertyField(m_type);
        if (m_type.enumValueIndex == 0)
        {
            //当选择第一个枚举类型
            EditorGUILayout.PropertyField(a_int);
        }
        else if (m_type.enumValueIndex == 1)
        {
            EditorGUILayout.PropertyField(b_int);
        }
        //serializedObject.ApplyModifiedProperties();
        test.ApplyModifiedProperties();//应用
    }

}
