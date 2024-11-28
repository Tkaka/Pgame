
public class SingletonWindow<T> : BaseWindow where T : BaseWindow, new()
{

    protected static T mSingleton = null;

    public static T Singleton
    {
        get
        {
            //窗口打开了才应该有这个实例
            /*if (mSingleton == null)
            {
                mSingleton = new T();
            }*/
            return mSingleton;
        }
    }

    public SingletonWindow()
    {
        mSingleton = this as T;
    }

    protected override void OnClose()
    {
        base.OnClose();
        mSingleton = null;
    }

    public virtual void Open()
    {
        
    }

}