using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class TextureExtension
{
    public static Sprite toSprite(this Texture2D tex, Vector2 pivot)
    {
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), pivot);
    }

    public static Sprite toSprite(this Texture2D tex)
    {
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
    }
}
