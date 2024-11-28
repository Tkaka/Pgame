using UnityEngine;

public class StageCameraView : MonoBehaviour
{
    private static Camera mCamera;
    private static Camera mOrtCamera;
    private void Awake()
    {
        mCamera = GetComponent<Camera>();
    }
    
    public static void ChangeCamera(bool orthographicCamera = false)
    {
        getCamera();
        if (mCamera != null)
            mCamera.enabled = !orthographicCamera;
        if (mOrtCamera != null)
            mOrtCamera.enabled = orthographicCamera;
    }

    private static void getCamera()
    {
        if (mCamera == null)
        {
            var view = FindObjectOfType<StageCameraView>();
            if (view != null)
                mCamera = view.GetComponent<Camera>();
        }

        if (mOrtCamera == null)
        {
            var mono = FindObjectOfType<FairyGUI.StageCamera>();
            if (mono != null)
                mOrtCamera = mono.GetComponent<Camera>();
        }
    }

    public static void Apply()
    {
        getCamera();
        if (mCamera == null)
            return;

        //相机禁用后需要重新打开一次
        //否则事件会出错
        if(!mOrtCamera.enabled)
        {
            ChangeCamera(true);
            ChangeCamera(false);
        }

        //ui相机动态Size
        //int screenWidth = Screen.width;
        //int screenHeight = Screen.height;

        //ui相机固定Size
        int screenHeight = 500;
        int screenWidth = (int)(500 * ((float)Screen.width / Screen.height));
        float far = screenHeight / 100f / Mathf.Tan((mCamera.fieldOfView / 2f * Mathf.PI) / 180f);
        mCamera.transform.localPosition = new Vector3((screenHeight / 100f) * screenWidth / screenHeight, -screenHeight / 100f, -far);
        mCamera.orthographic = false;
    }
}