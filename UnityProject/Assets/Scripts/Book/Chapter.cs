using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class Chapter : MonoBehaviour
{
    public static string chaptersPath = Application.streamingAssetsPath + "/Chapters/";
        
    private List<Bubble> bubbles = new List<Bubble>();

    public string Name
    {
        get;
        set;
    }

    public Chapter(string chapterName)
    {
        ChapterInfo info = JsonUtility.FromJson<ChapterInfo>(File.ReadAllText(chaptersPath + chapterName + ".json"));

        string[] bubbleNames = info.Bubbles.Split(',');
        for (int i = 0; i < bubbleNames.Length; ++i)
        {
            this.bubbles.Add(new Bubble(bubbleNames[i]));
        }
    }

    public class ChapterInfo
    {
        public string Name;
        public string Bubbles;
    }
}