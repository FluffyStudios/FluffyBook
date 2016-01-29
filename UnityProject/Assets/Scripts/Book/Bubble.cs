using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public partial class Bubble : MonoBehaviour
{
    public static string bubblesPath = Application.streamingAssetsPath + "/Bubbles/";

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

    public Bubble(string bubbleName)
    {
        BubbleInfo info = JsonUtility.FromJson<BubbleInfo>(File.ReadAllText(bubblesPath + bubbleName + ".json"));

        this.Name = info.Name;
        this.Size = new Vector2(info.Width, info.Height);
    }

    public class BubbleInfo
    {
        public string Name;
        public float Width;
        public float Height; 
    }
}