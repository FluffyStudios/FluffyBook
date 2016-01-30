using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class Chapter : MonoBehaviour
{
    public static string Path = Application.streamingAssetsPath + "/Chapters";
    public static float BubbleMargin = 8f / 100f;

    public Transform BubblePrefab;
    public Transform BubbleContainer;

    private List<Bubble> bubbles = new List<Bubble>();

    public string Name
    {
        get;
        set;
    }

    public void Initialize(string name)
    {
        ChapterInfo info = JsonUtility.FromJson<ChapterInfo>(File.ReadAllText(Chapter.Path + "/" + name + ".json"));

        this.Name = info.Name;

        string[] bubbleNames = info.Bubbles.Split(',');
        for (int i = 0; i < bubbleNames.Length; ++i)
        {
            int col  = i % 2;
            int line = Mathf.FloorToInt(i / 2f);
            float x = col * Bubble.BubbleWidth + col * Chapter.BubbleMargin + Bubble.BubbleWidth / 2;
            float y = (line * Bubble.BubbleHeight + line * Chapter.BubbleMargin + Bubble.BubbleHeight / 2) * -1f;

            Transform gameObject = GameObject.Instantiate(this.BubblePrefab);
            Bubble newBubble = gameObject.GetComponent<Bubble>();
            newBubble.transform.parent = this.BubbleContainer.transform;
            newBubble.transform.localPosition = new Vector2(x, y);
            newBubble.Initialize(bubbleNames[i]);
            this.bubbles.Add(newBubble);
        }
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public class ChapterInfo
    {
        public string Name;
        public string Bubbles;
    }
}