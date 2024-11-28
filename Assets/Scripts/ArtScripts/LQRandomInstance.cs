using System.Collections.Generic;
using UnityEngine;

public class LQRandomInstance : MonoBehaviour
{
    [Header("必须是当前GameObject的子节点,否则无效")]
    [SetProperty("Templet"), SerializeField]
    private GameObject templet;

    public GameObject Templet
    {
        get
        {
            return templet;
        }
        set
        {
            if (!templet.transform.IsChildOf(transform))
            {
                Debug.LogError("templet必须是当前GameObject的子节点");
                templet = null;
                return;
            }
            templet = value;
        }
    }

    [Header("随机间隔, 大于0才有效")]
    public float RandomTime;
    [Header("重复次数（建议10次以内）")]
    public int Num = 0;
    private int timeNow = 0;
    [Header("椭圆轴:a>b则椭圆左右长")]
    public float a;
    [Header("椭圆轴:a<b则椭圆前后长")]
    public float b;
    [Header("推荐值[0,360]决定随机点范围")]
    public Vector2 angleRange = new Vector2(0, 360);
    [Header("推荐值[0,360]决定Y轴随机旋转范围")]
    public Vector2 rotRange = new Vector2(0, 360);

    private Vector3[] posList = null;
    private void Start()
    {
        Templet = templet;
        if (templet != null)
        {
            posList = GetRandomPosInEllipse(Num, a, b, angleRange.x, angleRange.y);
            System.Random random = new System.Random();
            for (int i = 1; i < posList.Length; i++)
            {
                Swap(posList, i, random.Next(0, i));
            }
            if (RandomTime > 0)
                Invoke("randomNew", RandomTime);
        }
    }

    private void Swap<T>(T[] array, int indexA, int indexB)
    {
        T temp = array[indexA];
        array[indexA] = array[indexB];
        array[indexB] = temp;
    }

    void randomNew()
    {
        GameObject obj = GameObject.Instantiate(templet) as GameObject;
        obj.transform.SetParent(templet.transform.parent, false);

        obj.transform.localPosition += posList[timeNow];

        Vector3 vr = new Vector3(0, Random.Range(rotRange.x, rotRange.y), 0);
        obj.transform.localRotation = Quaternion.Euler(vr);

        timeNow++;
        if (timeNow < Num || Num <= 0)
            Invoke("randomNew", RandomTime);
    }

    private Vector3[] GetRandomPosInEllipse(int num, float a, float b, float startAngle, float endAngle)
    {
        Vector3[] res = new Vector3[num];
        float part = (endAngle - startAngle) / num;
        Vector3 vec = Vector3.zero;
        float nowAngle = 0;
        for (int i = 0; i < num; i++)
        {
            //x=acosθ,  y=bsinθ
            nowAngle = startAngle + part * i;
            if (a > b)
            {
                vec.x = a * Mathf.Cos(nowAngle) * UnityEngine.Random.value;
                vec.z = b * Mathf.Sin(nowAngle) * UnityEngine.Random.value;
            }
            else
            {
                vec.z = b * Mathf.Cos(nowAngle) * UnityEngine.Random.value;
                vec.x = a * Mathf.Sin(nowAngle) * UnityEngine.Random.value;
            }
            res[i] = vec;
        }
        return res;
    }

    void OnDestroy()
    {
        CancelInvoke();
    }

}