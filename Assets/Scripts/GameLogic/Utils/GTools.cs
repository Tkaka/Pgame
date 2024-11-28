using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using UnityEngine.AI;

public class GTools
{

    /// <summary>
    /// 毫秒到秒的转换
    /// </summary>
    /// <param name="ms"></param>
    /// <returns></returns>
    public static float MsToSce(int ms)
    {
        return ms * 0.001f;
    }


    /// <summary>
    /// 判断给定的点是否在圆内（非圆上）
    /// </summary>
    /// <param name="position">给定点的坐标</param>
    /// <param name="center">圆心</param>
    /// <param name="radius">半径</param>
    /// <returns></returns>
    public static bool isInCircle(Vector3 position, Vector3 center, float radius)
    {
        //return Tools.distanceIgnoreY(position, center) <= radius;
        //return (position - center).sqrMagnitude <= radius * radius;
        return GTools.sqrMagnitudeIgnoreY(position, center) <= radius * radius;
    }

    /// <summary>
    /// 判断给定的点是否在圆内（非圆上）, 并依次返回最优的逃跑点
    /// </summary>
    /// <param name="position"></param>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <param name="escapePoints"></param>
    /// <returns></returns>
    public static bool isInCircle(Vector3 position, Vector3 center, float radius, float escapeDis, out List<Vector3> escapePoints)
    {
        Vector3 direction = position - center;
        direction.Normalize();
        Vector3 pos = position + direction * (radius + escapeDis);
        escapePoints = new List<Vector3>();
        escapePoints.Add(pos);
        return (position - center).sqrMagnitude <= radius * radius;
    }

    /// <summary>
    /// 判断给定的点是否在矩形范围内
    /// </summary>
    /// <param name="position"></param>
    /// <param name="center"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static bool isInRectangle(Vector3 position, Transform transform, float xDelta, float zDelta, Vector3 center, bool ignoreY = true)
    {
        if (ignoreY)
        {
            position.y = 0;
            center.y = 0;
        }
        Vector3 direction = position - center;
        float wProjectLen = Vector3.Project(direction, transform.right).magnitude;
        float hProjectLen = Vector3.Project(direction, transform.forward).magnitude;
        return (wProjectLen <= xDelta / 2 && hProjectLen <= zDelta / 2);
    }

    /// <summary>
    /// 计算夹角 0 ~ 360
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static float angleWithDirection(Vector2 from, Vector2 to)
    {
        float angle = Vector3.Angle(from, to);
        if ((from - to).y > 0)
            return -angle;
        return angle;
    }

    public static float angleWithDirection(Vector3 from, Vector3 to)
    {
        Vector3 res = Vector3.Cross(from, to);
        if (res.y > 0)
            return Vector3.Angle(from, to);
        else
            return 360 - Vector3.Angle(from, to);
    }

    /// <summary>
    /// 是否在矩形内并获取逃跑点
    /// </summary>
    /// <param name="position"></param>
    /// <param name="transform"></param>
    /// <param name="xDelta"></param>
    /// <param name="zDelta"></param>
    /// <param name="escapeDis"></param>
    /// <param name="escapePoints"></param>
    /// <param name="offset"></param>
    /// <returns></returns>
    public static bool isInRectangle(Vector3 position, Transform transform, float xDelta, float zDelta, float escapeDis, out List<Vector3> escapePoints, Vector3 center)
    {
        center.y = 0;
        Vector3 topRight = center + transform.forward * (xDelta / 2) + transform.right * (zDelta / 2);
        Vector3 topLeft = center + transform.forward * (xDelta / 2) - transform.right * (zDelta / 2);
        Vector3 btmRight = center - transform.forward * (xDelta / 2) + transform.right * (zDelta / 2);
        Vector3 btmLeft = center - transform.forward * (xDelta / 2) - transform.right * (zDelta / 2);

        Vector3 direction = position - center;
        float wProjectLen = Vector3.Project(direction, transform.right).magnitude;
        float hProjectLen = Vector3.Project(direction, transform.forward).magnitude;

        //在矩形范围内
        if (wProjectLen <= xDelta / 2 && hProjectLen <= zDelta / 2)        //quadrant
        {
            Vector3 pos = Vector3.zero;
            float angle = angleWithDirection(direction, transform.right);
            if (angle > 0 && angle <= 90)    //第一象限
            {
                //if (hProjectLen > wProjectLen)
                //    pos = position + transform.forward * (zDelta / 2 - hProjectLen + escapeDis);     //往上侧逃跑  
                //else
                //    pos = position + transform.right * (xDelta / 2 - wProjectLen + escapeDis);     //往右侧逃跑
                pos = position + transform.right * (xDelta / 2 - wProjectLen + escapeDis);     //往右侧逃跑 
            }
            else if (angle > 90 && angle <= 180)    //第二象限
            {
                //if (hProjectLen > wProjectLen)
                //    pos = position + transform.forward * (zDelta / 2 - hProjectLen + escapeDis);     //往上侧逃跑  
                //else
                //    pos = position - transform.right * (xDelta / 2 - wProjectLen + escapeDis);     //往左侧逃跑
                pos = position - transform.right * (xDelta / 2 - wProjectLen + escapeDis);     //往左侧逃跑
            }
            else if (angle > 180 && angle <= 270)    //第三象限
            {
                //if (hProjectLen > wProjectLen)
                //    pos = position - transform.forward * (zDelta / 2 - hProjectLen + escapeDis);     //往下侧逃跑  
                //else
                //    pos = position - transform.right * (xDelta / 2 - wProjectLen + escapeDis);     //往左侧逃跑
                pos = position - transform.right * (xDelta / 2 - wProjectLen + escapeDis);     //往左侧逃跑
            }
            else if (angle > 270 && angle <= 360)      //第四象限
            {
                //if (hProjectLen > wProjectLen)
                //    pos = position - transform.forward * (zDelta / 2 - hProjectLen + escapeDis);     //往上侧逃跑  
                //else
                //    pos = position + transform.right * (xDelta / 2 - wProjectLen + escapeDis);     //往左侧逃跑
                pos = position + transform.right * (xDelta / 2 - wProjectLen + escapeDis);     //往左侧逃跑
            }
            escapePoints = new List<Vector3>();
            escapePoints.Add(pos);
            return true;
        }
        else
        {
            escapePoints = null;
            return false;
        }
    }


    /// <summary>
    /// 判断给定的点是否在扇形范围内
    /// </summary>
    /// <param name="position"></param>
    /// <param name="center"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static bool isInSector(Vector3 position, Transform transform, float radius, float degree, Vector3 center)
    {
        if ((center - position).sqrMagnitude > radius * radius)
            return false;
        Vector3 transDirection = transform.forward;
        Vector3 lineDirection = position - center;
        return (Vector3.Angle(transDirection, lineDirection)) <= degree / 2;
    }

    public static bool isInSector(Vector3 position, Transform transform, float radius, float degree, Vector3 center, float escapeDis, out List<Vector3> escapPoint)
    {
        if ((center - position).sqrMagnitude > radius * radius)
        {
            escapPoint = null;
            return false;
        }

        Vector3 direction = position - center;
        float angleAbs = Vector3.Angle(direction, transform.forward);
        if (angleAbs <= degree / 2)      //在扇形范围内
        {
            float angle = angleWithDirection(direction, transform.forward);
            Vector3 pos = Vector3.zero;
            float delta = degree / 2 - angleAbs;
            if (angle < 90)      //第一象限
            {
                pos = Quaternion.Euler(0, -delta, 0) * direction;
                pos += transform.right * escapeDis;
            }
            else
            {
                pos = Quaternion.Euler(0, -delta, 0) * direction;
                pos -= transform.right * escapeDis;
            }
            escapPoint = new List<Vector3>();
            escapPoint.Add(pos);
            return true;
        }
        else
        {
            escapPoint = null;
            return false;
        }
    }

    /// <summary>
    /// 判断浮点数相等
    /// </summary>
    /// <param name="f1"></param>
    /// <param name="f2"></param>
    /// <returns></returns>
    public static bool isFloatEqual(float f1, float f2, float threshold = 0.000001f)
    {
        return Mathf.Abs(f1 - f2) < threshold;
    }

    /// <summary>
    /// 判断两个Vector3是否相等 
    /// </summary>
    /// <param name="vec1"></param>
    /// <param name="vec2"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    public static bool isVector3Equal(Vector3 vec1, Vector3 vec2, float threshold = 0.01f)
    {
        return isFloatEqual(vec1.x, vec2.x, threshold) && isFloatEqual(vec1.y, vec2.y, threshold) && isFloatEqual(vec1.z, vec2.z, threshold);
    }


    /// <summary>
    /// 将string转换为byte数组
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static byte[] stringToBytes(string str)
    {
        return System.Text.Encoding.UTF8.GetBytes(str);
    }

    /// <summary>
    /// 将byte数组转换为string
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string bytesToString(byte[] bytes)
    {
        return System.Text.Encoding.UTF8.GetString(bytes);
    }


    public static string getMd5(string str)
    {
        byte[] md5 = new MD5CryptoServiceProvider().ComputeHash(System.Text.Encoding.Default.GetBytes(str));
        return BitConverter.ToString(md5);
    }

    /// <summary>
    /// 计算MD5
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string getMd5(byte[] bytes)
    {
        byte[] md5 = new MD5CryptoServiceProvider().ComputeHash(bytes);
        return BitConverter.ToString(md5).Replace("-", "");
    }

    public static byte[] getMd5Byte(byte[] bytes)
    {
        return new MD5CryptoServiceProvider().ComputeHash(bytes);
    }

    /// <summary>
    /// 计算文件MD5，需要外部关闭流
    /// </summary>
    /// <param name="fs"></param>
    /// <returns></returns>
    public static string getMd5(FileStream fs)
    {
        string ret = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(fs));
        return ret.Replace("-", "");
    }

    /// <summary>
    /// 忽略大小写比较
    /// </summary>
    /// <param name="str1"></param>
    /// <param name="str2"></param>
    /// <returns></returns>
    public static bool equalsIgnoreCase(string str1, string str2)
    {
        return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 返回一个String作为Key
    /// </summary>
    private static StringBuilder strBuilder = new StringBuilder();
    public static string getStringKey(long param1, long param2, string param3)
    {
        strBuilder.Length = 0;
        strBuilder.Append(param1).Append("_").Append(param2).Append("_").Append(param3);
        return strBuilder.ToString();
    }

    /// <summary>
    /// 分割字符串
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    public static string[] splitString(string src, char sign = '+')
    {
        if (string.IsNullOrEmpty(src))
        {
            //Logger.dbg("进行split操作的字符串为空串");
            return null;
        }
        return src.Split(sign);
    }

    /// <summary>
    /// 分割字符串到int数组中
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    public static int[] splitStringToIntArray(string src, char sign = '+')
    {
        if (string.IsNullOrEmpty(src))
        {
            return new int[0];
        }
        else
        {
            string[] strs = src.Split(sign);
            int[] ret = new int[strs.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                if (!int.TryParse(strs[i], out ret[i]))
                {
                    Logger.err("字符串转int出错！");
                    continue;
                }
            }
            return ret;
        }
    }

    /// <summary>
    /// 分割字符串到float数组中
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    public static float[] splitStringToFloatArray(string src, char sign = '+')
    {
        if (String.IsNullOrEmpty(src))
        {
            return new float[3] { 0, 0, 0 };
        }
        else
        {
            string[] strs = src.Split(sign);
            float[] ret = new float[strs.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                if (!float.TryParse(strs[i], out ret[i]))
                {
                    Logger.err("字符串转float出错！");
                    continue;
                }
            }
            return ret;
        }
    }

    /// <summary>
    /// 分割字符串到int数组中
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    public static List<int> splitStringToIntList(string src, char sign = '+')
    {
        if (String.IsNullOrEmpty(src))
        {
            return null;
        }
        else
        {
            string[] strs = src.Split(sign);
            List<int> ret = new List<int>();
            for (int i = 0; i < strs.Length; i++)
            {
                int tmp;
                if (!int.TryParse(strs[i], out tmp))
                {
                    Logger.err("字符串转int出错！");
                    continue;
                }
                ret.Add(tmp);
            }
            return ret;
        }
    }

    public static string intArrayToString(int[] array, char sign = '+')
    {
        string res = "";
        foreach (var tmp in array)
        {
            res += tmp + "+";
        }

        return res.TrimEnd('+');
    }

    /// <summary>
    /// 某个概率是否发生
    /// </summary>
    /// <param name="prob">概率(为0既是不可能发生)</param>
    /// <param name="maxNum">基数</param>
    /// <param name="minNum">开始数</param>
    /// <returns></returns>
    public static bool WillOccur(int prob, int maxNum, out int res, int minNum = 1)
    {
        res = UnityEngine.Random.Range(minNum, maxNum + 1);
        if (res <= prob)
            return true;
        else
            return false;
    }

    public static bool WillOccur(int prob, int maxNum=10000, int minNum = 1)
    {
        int res = UnityEngine.Random.Range(minNum, maxNum + 1);
        if (res <= prob)
            return true;
        else
            return false;
    }

    public static bool WillOccurL(LNumber prob, int maxNum = 10000, int minNum = 1)
    {
        int res = UnityEngine.Random.Range(minNum, maxNum + 1);
        if (res <= prob)
            return true;
        else
            return false;
    }

    public static bool WillOccurF(float prob, int maxNum, int minNum = 1)
    {
        int result = UnityEngine.Random.Range(minNum, maxNum+1);
        if (result <= prob)
            return true;
        else
            return false;
    }

    public static bool WillOccurD(double prob, int maxNum=10000, int minNum = 1)
    {
        int result = UnityEngine.Random.Range(minNum, maxNum+1);
        if (result <= prob)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 万分比比率转换
    /// </summary>
    /// <param name="rate"></param>
    /// <param name="baseNum"></param>
    /// <returns></returns>
    public static float rateConvert(int rate, int baseNum = 10000)
    {
        return (float)rate / (float)baseNum;
    }

    /// <summary>
    /// 乘法操作
    /// </summary>
    /// <param name="old"></param>
    /// <param name="rate"></param>
    /// <param name="flag"></param>
    /// <param name="baseNum"></param>
    /// <returns></returns>
    public static float mulOperate(float old, int rate, bool flag = false, int baseNum = 10000)
    {
        float res = 0.0f;
        if (flag)
            res = old * (1 + rateConvert(rate, baseNum));
        else
            res = old * rateConvert(rate, baseNum);
        return res;
    }

    /// <summary>
    /// 减法操作
    /// </summary>
    /// <param name="old"></param>
    /// <param name="rate"></param>
    /// <param name="flag"></param>
    /// <param name="baseNum"></param>
    /// <returns></returns>
    public static float subOrAdd(float old, int rate, int flag = 1, int baseNum = 10000)
    {
        return old + rateConvert(rate, baseNum) * flag;
    }

    /// <summary>
    /// 无y轴方向
    /// </summary>
    /// <param name="src"></param>
    /// <param name="dst"></param>
    /// <returns></returns>
    public static Vector3 vecSub(Vector3 dst, Vector3 src)
    {
        Vector3 ret = dst - src;
        ret.y = 0;
        return ret;
    }


    /// <summary>
    /// 计算距离（忽略Y轴）
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="src"></param>
    /// <returns></returns>
    public static float distanceIgnoreY(Vector3 dst, Vector3 src)
    {
        dst.y = 0;
        src.y = 0;
        return Vector3.Distance(dst, src);
    }

    public static float angleIgnoreY(Vector3 dst, Vector3 src)
    {
        dst.y = 0;
        src.y = 0;
        return Vector3.Angle(dst, src);
    }

    public static Vector3 dirIgnoreY(Vector3 dst, Vector3 src)
    {
        dst.y = 0;
        src.y = 0;
        return (dst - src);
    }


    public static float sqrMagnitudeIgnoreY(Vector3 dst, Vector3 src)
    {
        dst.y = 0;
        src.y = 0;
        return (dst - src).sqrMagnitude;
    }


    public static float scaleInt2float(int origin, float scaleRate = 0.0001f)
    {
        return origin * scaleRate;
    }

    public static double ScaleInt2Double(int origin, float scaleRate = 0.0001f)
    {
        return origin * scaleRate;
    }

    public static LNumber ScaleInt2LNumber(int origin, int scaleRate = 10000)
    {
        LNumber num = LNumber.Create(origin, 0);
        return num / scaleRate;
        //LNumber x = 12345 / (LNumber)10000; 
        //return (LNumber)origin / scaleRate;
    }

    /// <summary>
    /// 解析配置表中连续列表的参数 （如：AAA+BBB*2+CCC  --> 将返回{AAA,BBB,BBB,CCC}）
    /// </summary>
    /// <param name="str"></param>
    /// <param name="sign"></param>
    /// <returns></returns>
    public static List<string> parseContinuousParam(string str, char sign = '+')
    {
        List<string> list = new List<string>();
        string[] baseArr = GTools.splitString(str, sign);
        if (baseArr == null)
            return null;
        foreach (string s in baseArr)
        {
            if (s.Contains("*"))
                list.AddRange(parseStrWithMutil(s));
            else
                list.Add(s);
        }
        return list;
    }

    /// <summary>
    /// 解析带有*的参数（如AAA*3 -> 将返回{AAA,AAA,AAA}）
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    public static List<string> parseStrWithMutil(string src)
    {
        int index = src.IndexOf('*');
        List<string> arrRes = new List<string>();
        if (index > 0)
        {
            int count = Convert.ToInt32(src[index + 1] + "");
            string newStr = src.Substring(0, index);
            for (int i = 0; i < count; i++)
            {
                arrRes.Add(newStr);
            }
        }
        return arrRes;
    }

    /// <summary>
    /// 将字符串数组转化为int数组
    /// </summary>
    /// <param name="strArr"></param>
    /// <returns></returns>
    public int[] strArrToIntArr(string[] strArr)
    {
        return Array.ConvertAll<string, int>(strArr, s => int.Parse(s));
    }


    /// <summary>
    /// 将字符转list转化为整型list
    /// </summary>
    /// <param name="strList"></param>
    /// <returns></returns>
    public static List<int> strListToIntList(List<string> strList)
    {
        List<int> resList = new List<int>();
        foreach (string str in strList)
        {
            resList.Add(int.Parse(str));
        }
        return resList;
    }

    /// <summary>
    /// 字符串list转换为整型数组
    /// </summary>
    /// <param name="strList"></param>
    /// <returns></returns>
    public static int[] strListToIntArr(List<string> strList)
    {
        int[] resArr = new int[strList.Count];
        for (int i = 0; i < resArr.Length; i++)
        {
            resArr[i] = int.Parse(strList[i]);
        }
        return resArr;
    }

    /// <summary>
    /// 以center为中心，半径为radius的圆中生成随机点
    /// </summary>
    /// <param name="center"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static Vector3 randomVector3InCircle(Vector3 center, float radius)
    {
        float ang = UnityEngine.Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y; // center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }


    /// <summary>
    /// 在navmesh上随机采样n个点，可能出现采样点重复
    /// </summary>
    /// <param name="source"></param>
    /// <param name="maxDistance"></param>
    /// <param name="allowedMask"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public static List<Vector3> getRandomPositionInNavmesh(Vector3 source, float maxDistance, int allowedMask, int num)
    {

        List<Vector3> ret = new List<Vector3>();
        Vector3 lastPosition = source;
        NavMeshHit hit;

        for (int a = 0; a < num; ++a)
        {
            Vector3 pos = randomVector3InCircle(source, maxDistance);
            if (NavMesh.SamplePosition(pos, out hit, maxDistance, allowedMask))
            {
                ret.Add(hit.position);
                lastPosition = hit.position;
            }
            else
            {
                // 避免采样失败
                ret.Add(lastPosition);
            }
        }
        return ret;
    }

    /// <summary>
    /// 根据帧的长度获取时间
    /// </summary>
    /// <param name="frameCount">帧数量</param>
    /// <param name="fps">帧率</param>
    /// <returns></returns>
    public static float getTimeByFrameCount(int frameCount, int fps = 30)
    {
        return (frameCount / fps);
    }




    /// <summary>
    /// Image 变灰开关
    /// </summary>
    /// <param name="target"></param>
    /// <param name="flag"></param>
    public const string UIGreyMat = "UIGreyMat";
    public static void ImageGreyToggle(Image target, bool flag)
    {
        //if (flag)
        //    target.color = Color.gray;
        //else
        //    target.color = Color.white;
        //return;

        if (target == null)
            return;

        if (target.material != null && target.material.name.Contains(UIGreyMat) && flag)
            return;

        if ((target.material == null || !target.material.name.Contains(UIGreyMat)) && !flag)
            return;

        Material mat;
        if (flag)
        {
            mat = new Material(Shader.Find("UI/Transparent Colored Gray Stencil"));
            mat.name = UIGreyMat;
            target.material = mat;
        }
        else
        {
            mat = target.material;
            target.material = null;
            GameObject.Destroy(mat);
        }
    }

    public static void GraphicGreyToggle(Graphic target, bool flag)
    {
        //if (flag)
        //    target.color = Color.gray;
        //else
        //    target.color = Color.white;
        //return;

        if (target == null)
            return;

        if (target.material != null && target.material.name.Contains(UIGreyMat) && flag)
            return;

        if ((target.material == null || !target.material.name.Contains(UIGreyMat)) && !flag)
            return;

        Material mat;
        if (flag)
        {
            mat = new Material(Shader.Find("UI/Transparent Colored Gray Stencil"));
            mat.name = UIGreyMat;
            target.material = mat;
        }
        else
        {
            mat = target.material;
            target.material = null;
            GameObject.Destroy(mat);
        }
    }

    /// <summary>
    /// 将一个数组的内容随机交换
    /// </summary>
    /// <param name="array"></param>
    public static void randomArr<T>(T[] array)
    {
        int length = array.Length;
        if (array == null || length <= 0)
            return;
        int index;
        int value;
        T median;
        for (index = 0; index < length; index++)
        {
            value = UnityEngine.Random.Range(0, length - 1);
            median = array[index];
            array[index] = array[value];
            array[value] = median;
        }
    }

    /// <summary>
    /// 将一个list的内容随机交换
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void RandomList<T>(List<T> list)
    {
        int length = list.Count;
        if (list == null || length <= 0)
            return;
        int index;
        int value;
        T median;
        for (index = 0; index < length; index++)
        {
            value = UnityEngine.Random.Range(0, length - 1);
            median = list[index];
            list[index] = list[value];
            list[value] = median;
        }
    }

    public static string UrlEncode(string str)
    {
        StringBuilder sb = new StringBuilder();
        byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
        for (int i = 0; i < byStr.Length; i++)
        {
            sb.Append(@"%" + Convert.ToString(byStr[i], 16));
        }

        return (sb.ToString());
    }

    public static T parseValueToEnum<T>(int val)
    {
        return (T)Enum.Parse(typeof(T), val + "", true);
    }

    public static T parseValueToEnum<T>(string val)
    {
        return (T)Enum.Parse(typeof(T), val, true);
    }

    private static bool EqualStrArray(string[] strList1, string[] strList2)
    {
        if (strList1 == null || strList2 == null)
            return false;

        if (strList1.Length != strList2.Length)
            return false;

        for (int i = 0; i < strList1.Length; ++i)
        {
            if (strList1[i] != strList2[i])
                return false;
        }

        return true;
    }

    public static System.Text.Encoding checkFormat(byte[] bytes)
    {
        System.Text.Encoding enc = null;
        byte[] bom = new byte[4]; // Get the byte-order mark, if there is one 
        Array.Copy(bytes, bom, 4);
        if ((bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) || // utf-8 
            (bom[0] == 0xff && bom[1] == 0xfe) || // ucs-2le, ucs-4le, and ucs-16le 
            (bom[0] == 0xfe && bom[1] == 0xff) || // utf-16 and ucs-2 
            (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff)) // ucs-4 
        {
            enc = System.Text.Encoding.Unicode;
        }
        else
        {
            enc = System.Text.Encoding.ASCII;
        }
        return enc;
    }

    public static string wwwBytesToString(byte[] bytes)
    {
        string json = "";
        if (bytes != null)
        {
            System.Text.Encoding enc = GTools.checkFormat(bytes);
            if (enc == null)
            {
                Logger.err("无法识别的编码格式");
                return json;
            }
            if (enc == System.Text.Encoding.Unicode)
            {
                json = System.Text.Encoding.Unicode.GetString(bytes);
            }
            else if (enc == System.Text.Encoding.ASCII)
            {
                json = System.Text.Encoding.ASCII.GetString(bytes);
            }
        }
        return json;
    }

    /// <summary>
    /// 获取倒计时
    /// </summary>
    /// <param name="endTime">结束时间</param>
    /// <returns></returns>
    public static string getCountDown(long endTime)
    {
        var time = endTime - TimeUtils.currentMilliseconds();
        return getTimeStr(time);
    }

    public static string getTimeStr(long value, string format = "{0:D2}:{1:D2}:{2:D2}")
    {
        var time = value / 1000;
        if (time < 0)
            time = 0;
        var shi = time / 3600;
        var fen = time % 3600 / 60;
        var miao = time % 60;
        return string.Format(format, shi, fen, miao);
    }

    public static void Shuffle<T>(T[] array)
    {
        if (array == null)
            return;
        System.Random random = new System.Random();
        for (int i = 1; i < array.Length; i++)
        {
            //从i位置拿出数据，和i位置之前的数据进行数据交换
            Swap<T>(array, i, random.Next(0, i));
        }
    }

    public static void Swap<T>(T[] array, int indexA, int indexB)
    {
        T temp = array[indexA];
        array[indexA] = array[indexB];
        array[indexB] = temp;
    }

    /// <summary>
    /// 判断是否在可视范围之类
    /// </summary>
    /// <param name="attackTrans">攻击者</param>
    /// <param name="targetTrans">防御者</param>
    /// <param name="fieldViewAngle">视角</param>
    /// <param name="viewMagnitude">视距</param>
    /// <returns></returns>
    public static bool withinSightSector(Transform attackTrans, Transform targetTrans, float fieldViewAngle, float viewMagnitude, out float far)
    {
        Vector3 pos1 = attackTrans.position;
        Vector3 pos2 = targetTrans.position;
        pos1.y = 0;
        pos2.y = 0;
        Vector3 fw = attackTrans.forward;
        fw.y = 0;
        far = Vector3.Distance(pos1, pos2);
        return Vector3.Angle(pos2 - pos1, fw) < fieldViewAngle * 0.5f && far <= viewMagnitude;
    }

    public static List<Vector3> GetRandomPosInEllipse(int num, float a, float b)
    {
        List<Vector3> res = new List<Vector3>();
        List<float> angles = new List<float>();
        float part = 360 / num;
        Vector3 vec = Vector3.zero;
        for (int i = 0; i < num; i++)
        {
            //x=acosθ,  y=bsinθ
            if (a > b)
            {
                vec.x = a * Mathf.Cos(part * i) * UnityEngine.Random.value;
                vec.z = b * Mathf.Sin(part * i) * UnityEngine.Random.value;
            }
            else
            {
                vec.x = b * Mathf.Cos(part * i) * UnityEngine.Random.value;
                vec.z = a * Mathf.Sin(part * i) * UnityEngine.Random.value;
            }
            res.Add(vec);
        }
        return res;
    }


    public static void ScaleParticleSystem(GameObject gameObj, float scale)
    {
        var hasParticleObj = false;
        var particles = gameObj.GetComponentsInChildren<ParticleSystem>(true);
        var max = particles.Length;
        for (int idx = 0; idx < max; idx++)
        {
            var particle = particles[idx];
            if (particle == null) continue;
            hasParticleObj = true;
            particle.startSize *= scale;
            particle.startSpeed *= scale;
            particle.startRotation *= scale;
            particle.transform.localScale *= scale;
        }
        if (hasParticleObj)
        {
            gameObj.transform.localScale = new Vector3(scale, scale, 1);
        }
    }

    /// <summary>
    /// 判断点是否在多边形内
    /// </summary>
    /// <param name="p"></param>
    /// <param name="areaPoints"></param>
    /// <returns></returns>
    public static bool IsInPolygon(Vector2 p, Vector2[] areaPoints)
    {
        // 交点个数
        int nCross = 0;
        for (int i = 0; i<areaPoints.Length; i++)
        {
            Vector2 p1 = areaPoints[i];
            Vector2 p2 = areaPoints[(i + 1) % areaPoints.Length];// 最后一个点与第一个点连线
            if (p1.y == p2.y )
                continue;
            if (p.y<Mathf.Min(p1.y, p2.y) )
                continue;
            if (p.y >= Mathf.Max(p1.y, p2.y) )
                continue;
            // 求交点的x坐标
            float x = (p.y - p1.y) *  (p2.x - p1.x) / (p2.y - p1.y) + p1.x;
            // 只统计p1p2与p向右射线的交点
            if (x > p.x )
            {
                nCross++;
            }
        }
        // 交点为偶数，点在多边形之外
        return (nCross % 2 == 1);
    }


    public static int[] DevideNum(int target, int count)
    {
        if (count <= 0)
            return new int[1]{ target };

        int[] res = new int[count];
        int average = target / count;
        int left = target % count;
        for (int i = 0; i < count; i++)
        {
            res[i] = average;
        }
        res[count - 1] += left;
        return res;
    }

    public static Queue<long> DevideNum(long target, int count)
    {
        Queue<long> res = new Queue<long>();
        if (count <= 0)
        {
            res.Enqueue(target);
            return res;
        }

        //如果数字 小于拆分数量
        if (target <= count)
        {
            for (int i = 0; i < target; i++)
                res.Enqueue(1);
        }

        long average = target / count;
        long left = target % count;
        for (int i = 0; i < count; i++)
        {
            if(i == count - 1)
                res.Enqueue(average + left);
            else
                res.Enqueue(average);
        }
        return res;
    }

    public static int Time2Frame(float time, float frameRate =30)
    {
        return Mathf.CeilToInt(time * frameRate);
    }

    public static float Frame2Time(int keyframe, float frameRate = 30)
    {
        return keyframe / frameRate;
    }

}