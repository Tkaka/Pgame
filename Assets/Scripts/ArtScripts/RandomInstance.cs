using UnityEngine;

public class RandomInstance : MonoBehaviour
{
    [Header( "必须是当前GameObject的子节点,否则无效" )]
    [SetProperty( "Templet" ), SerializeField]
    private GameObject templet;
    
    public GameObject Templet
    {
        get
        {
            return templet;
        }
        set
        {
            if( !templet.transform.IsChildOf( transform ) )
            {
                Debug.LogError( "templet必须是当前GameObject的子节点" );
                templet = null;
                return;
            }
            templet = value;
        }
    }
    [Header( "随机间隔, 大于0才有效" )]
    public float RandomTime;
    [Header( "重复次数（小于等于0表示无限次数）" )]
    public int Num = 0;
    private int timeNow = 0;
    
    [Header( "角度随机边界值1" )]
    public Vector3 RandomRot1;
    [Header( "角度随机边界值2" )]
    public Vector3 RandomRot2;
    [Header( "角度量化值(结果只会是这个值的倍数)大于0时有效" )]
    public float RotQuater;
    private Vector3 lastRot;


    [Header( "位置随机范围1" )]
    public Vector3 RandomPos1;
    [Header( "位置随机范围2" )]
    public Vector3 RandomPos2;
    [Header( "位置量化值(结果只会是这个值的倍数)大于0时有效" )]
    public float PosQuater;
    private Vector3 lastPos;


    void Start()
    {
        Templet = templet;
        if(templet != null)
        {
            if( RandomTime > 0 && (PosQuater > 0 || RotQuater > 0) )
                Invoke( "randomNew", RandomTime );
        }
    }

    void randomNew()
    {
        GameObject obj = GameObject.Instantiate( templet ) as GameObject;
        obj.transform.SetParent( templet.transform.parent, false );

        if (PosQuater > 0)
        {
            Vector3 vp = new Vector3( RandomPos1.x, RandomPos1.y, RandomPos1.z );
            do
            {
                vp.x = (Random.value - 0.5f) * (RandomPos1.x - RandomPos2.x);
                vp.x = ((int)(vp.x / PosQuater)) * PosQuater;
            } while( vp.x == lastPos.x && Mathf.Abs( RandomPos1.x - RandomPos2.x ) >= PosQuater );

            do
            {
                vp.y = (Random.value - 0.5f) * (RandomPos1.y - RandomPos2.y);
                vp.y = ((int)(vp.y / PosQuater)) * PosQuater;
            } while( vp.y == lastPos.y && Mathf.Abs( RandomPos1.y - RandomPos2.y ) >= PosQuater );

            do
            {
                vp.z = (Random.value - 0.5f) * (RandomPos1.z - RandomPos2.z);
                vp.z = ((int)(vp.z / PosQuater)) * PosQuater;
            } while( vp.z == lastPos.z && Mathf.Abs( RandomPos1.z - RandomPos2.z ) >= PosQuater );
            lastPos.Set( vp.x, vp.y, vp.z );
            obj.transform.localPosition = vp;
        }

        if( RotQuater > 0 )
        {
            Vector3 vr = new Vector3( RandomRot1.x, RandomRot1.y, RandomRot1.z );
            do
            {
                vr.x = (Random.value - 0.5f) * (RandomRot1.x - RandomRot2.x);
                vr.x = ((int)(vr.x / RotQuater)) * RotQuater;
            } while( vr.x == lastRot.x && Mathf.Abs( RandomRot1.x - RandomRot2.x ) >= RotQuater );

            do
            {
                vr.y = (Random.value - 0.5f) * (RandomRot1.y - RandomRot2.y);
                vr.y = ((int)(vr.y / RotQuater)) * RotQuater;
            } while( vr.y == lastRot.y && Mathf.Abs( RandomRot1.y - RandomRot2.y ) >= RotQuater );

            do
            {
                vr.z = (Random.value - 0.5f) * (RandomRot1.z - RandomRot2.z);
                vr.z = ((int)(vr.z / RotQuater)) * RotQuater;
            } while( vr.z == lastRot.z && Mathf.Abs( RandomRot1.z - RandomRot2.z ) >= RotQuater );
            lastRot.Set( vr.x, vr.y, vr.z );
            obj.transform.localRotation = Quaternion.Euler( vr );
        }


        timeNow++;
        if( timeNow < Num || Num <= 0 )
            Invoke( "randomNew", RandomTime );
    }

    void OnDestroy()
    {
        CancelInvoke();
    }
}
