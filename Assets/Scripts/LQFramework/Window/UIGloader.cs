using FairyGUI;
using UnityEngine;
using System.Collections.Generic;

public class UIGloader : GLoader
{
    public UIGloader()
    {

    }

    public UIGloader(bool fromWWW)
    {
        isFromWWW = fromWWW;
    }

    private bool isFromWWW;
    private string tosetUrl;
    private Texture2D abTexture;
    private Texture2D wwwTexture;
    private Dictionary<string, int> refPkgMap = new Dictionary<string, int>();
    private ResPack resPacker = new ResPack("UIGloader");

    /// <summary>
    /// 设置图片
    /// </summary>
    public void SetUrl(string targetUrl, bool async = true)
    {
        if (isDisposed)
            return;
        if (targetUrl == null)
            return;
        if (targetUrl == url)
            return;

        if (isFromWWW && targetUrl.StartsWith("http"))
        {
            //网络图片，只能异步
            tosetUrl = targetUrl;
            UIResMgr.Singleton.LoadWWWTexture(targetUrl, (tex) =>
            {
                if (!isDisposed && tosetUrl == targetUrl)
                {
                    wwwTexture = tex;
                    isFromWWW = true;
                    this.url = targetUrl;
                }
            });
            return;
        }

        if (isFromWWW)
        {
            Debug.LogError("www类型UIGloader.url格式不正确>" + targetUrl);
            return;
        }

        tosetUrl = targetUrl;
        if (targetUrl.StartsWith("ui://"))
        {
            //package图片
            int pos1 = targetUrl.IndexOf("//");
            int pos2 = targetUrl.IndexOf('/', pos1 + 2);
            if (pos2 >= 0)
            {
                string pkgName = targetUrl.Substring(pos1 + 2, pos2 - pos1 - 2);
                if (UIPackage.GetByName(pkgName) == null)
                {
                    Debuger.Err("设置了不在内存中的包，请引起注意>" + this.name + ">" + targetUrl);
                    if (async)
                    {
                        UIResMgr.Singleton.PreloadTexturePackage(WinEnum.BasePath + pkgName, () =>
                        {
                            if (!isDisposed && tosetUrl == targetUrl)
                            {
                                //添加计数
                                addPkgRef(pkgName);
                                WinMgr.AddPackage(pkgName);
                                this.url = tosetUrl;
                            }
                        }, false);
                    }
                    else
                    {
                        //添加计数
                        addPkgRef(pkgName);
                        WinMgr.AddPackage(pkgName);
                        this.url = tosetUrl;
                    }
                }
                else
                {
                    //添加计数
                    addPkgRef(pkgName);
                    this.url = tosetUrl;
                }
            }
            else
            {
                Debug.LogWarning("不识别的url>" + targetUrl);
                this.url = tosetUrl;
            }
        }
        else
        {
            //外部路径
            if (targetUrl.Contains("/"))
            {
                var arr = targetUrl.Split('/');
                if (async)
                {
                    resPacker.Request(arr[0], arr[1], typeof(Texture2D), (a, b, t) =>
                    {
                        if (!isDisposed && tosetUrl == targetUrl)
                        {
                            abTexture = resPacker.GetObject(a, b) as Texture2D;
                            this.url = tosetUrl;
                        }
                    });
                }
                else
                {
                    abTexture = resPacker.GetObject(arr[0], arr[1], typeof(Texture2D)) as Texture2D;
                    this.url = tosetUrl;
                }
            }
            else
            {
                this.url = tosetUrl;
            }

            //Reources图片
            /*if (async)
            {
                //异步
                UIResMgr.Singleton.PreloadTexturePackage(targetUrl, () => {
                    if (!isDisposed && tosetUrl == targetUrl)
                    {
                        this.url = tosetUrl;
                    }
                }, true);
            }
            else
            {
                //同步 
                this.url = targetUrl;
            }*/
        }
    }
    
    private Texture targetTexture;
    public void SetTexture(Texture texture)
    {
        if(texture != null) 
        {
            targetTexture = texture;
            this.url = "__set_outside_" + texture.GetHashCode();
        }
    }


    private bool isDisposed;
    protected override void LoadExternal()
    {
        if(targetTexture != null)
        {
            onExternalLoadSuccess(new NTexture(targetTexture));
            return;
        }

        if(isFromWWW)
        {
            //网络图片
            if (wwwTexture != null)
            {
                UIResMgr.Singleton.LoadIcon(wwwTexture);
                onExternalLoadSuccess(new NTexture(wwwTexture));
            }
            else
            {
                Debuger.Err("UIGloader set url failed >" + url);
                onExternalLoadFailed();
            }
        }else
        {
            if (abTexture != null)
            {
                onExternalLoadSuccess(new NTexture(abTexture));
            }
            else
            {
                //外部设置都异步
                if (tosetUrl != url && url.Contains("/"))
                {
                    string targetUrl = url;
                    var arr = url.Split('/');
                    resPacker.Request(arr[0], arr[1], typeof(Texture2D), (a, b, t) =>
                    {
                        if (!isDisposed && url == targetUrl)
                        {
                            abTexture = resPacker.GetObject(a, b) as Texture2D;
                            if (abTexture != null)
                            {
                                onExternalLoadSuccess(new NTexture(abTexture));
                            }
                            else
                            {
                                Debuger.Err("UIGloader set url failed >" + url);
                                onExternalLoadFailed();
                            }
                        }
                    });
                    return;
                }

                Debuger.Err("UIGloader set url failed >" + url);
                onExternalLoadFailed();
            }

            //Resource图片
            /*Texture2D tex = Resources.Load<Texture2D>(url);
            if (tex != null)
            {
                UIResMgr.Singleton.LoadIcon(tex);
                onExternalLoadSuccess(new NTexture(tex));
            }
            else
            {
                onExternalLoadFailed();
            }*/
        }
    }

    protected override void FreeExternal(NTexture texture)
    {
        //http图片
        if (texture != null && wwwTexture != null)
            UIResMgr.Singleton.ReleaseIcon(texture.nativeTexture);
        Object.DestroyImmediate(targetTexture);
        targetTexture = null;
    }

    public override void Dispose()
    {
        base.Dispose();
        isDisposed = true;
        wwwTexture = null;
        resPacker.ReleaseAllRes();

        //移除引用计数
        var enu = refPkgMap.GetEnumerator();
        while(enu.MoveNext())
        {
            var key = enu.Current.Key;
            var value = enu.Current.Value;
            while(value >0 )
            {
                value--;
                UIResMgr.Singleton.RemovePkgRef(key);
            }
        }
        enu.Dispose();
        refPkgMap.Clear();
    }

    private void addPkgRef(string pkgName)
    {
        //添加引用计数
        if (refPkgMap.ContainsKey(pkgName))
            refPkgMap[pkgName] = refPkgMap[pkgName] + 1;
        else
            refPkgMap[pkgName] = 1;
        UIResMgr.Singleton.AddPkgRef(pkgName);
    }

    public static void SetUrl(GLoader loader, string url, bool async = true)
    {
        UIGloader load = loader as UIGloader;
        if (load != null)
        {
            load.SetUrl(url, async);
        }
        else if(loader != null)
        {
            Debuger.Wrn("GLoader不是一个UIGLoader" + loader.name);
            loader.url = url;
        }
    }
}