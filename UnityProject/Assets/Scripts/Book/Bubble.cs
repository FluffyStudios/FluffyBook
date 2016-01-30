using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public partial class Bubble : MonoBehaviour
{
    public static string Path = Application.streamingAssetsPath + "/Bubbles";
    public static float BubbleWidth = 288f / 100f;
    public static float BubbleHeight = 255f / 100f;

    public SpriteRenderer Background;

    public string Name
    {
        get;
        set;
    }

    public Vector2 Size
    {
        get;
        set;
    }

    public void Initialize(string name)
    {
        BubbleInfo info = JsonUtility.FromJson<BubbleInfo>(File.ReadAllText(Bubble.Path + "/" + name + ".json"));

        this.Name = info.Name;
        this.Background.sprite = Resources.Load<Sprite>(info.BackgroundImage);
    }

    public class BubbleInfo
    {
        public string Name;
        public string BackgroundImage;
    }
}