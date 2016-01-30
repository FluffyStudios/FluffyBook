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
    public Transform FirstChoiceButton;
    public Transform SecondChoiceButton;

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

    public bool IsValidated
    {
        get;
        set;
    }

    public void Initialize(string name)
    {
        BubbleInfo info = JsonUtility.FromJson<BubbleInfo>(File.ReadAllText(Bubble.Path + "/" + name + ".json"));

        this.Name = info.Name;
        this.Background.sprite = Resources.Load<Sprite>(info.BackgroundImage);

        this.FirstChoiceButton.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(info.FirstChoiceImage);
        this.SecondChoiceButton.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(info.SecondChoiceImage);
        string[] serializedFirstPosition = info.FirstChoicePosition.Split(',');
        string[] serializedSecondPosition = info.SecondChoicePosition.Split(',');
        this.FirstChoiceButton.localPosition = new Vector2(float.Parse(serializedFirstPosition[0]) / 100f, float.Parse(serializedFirstPosition[1]) / 100f);
        this.SecondChoiceButton.localPosition = new Vector2(float.Parse(serializedSecondPosition[0]) / 100f, float.Parse(serializedSecondPosition[1]) / 100f);
    }

    public void ChooseItem(int choice)
    {
        this.IsValidated = true;

    }

    public class BubbleInfo
    {
        public string Name;
        public string BackgroundImage;
        public string FirstChoiceImage;
        public string FirstChoicePosition;
        public string SecondChoiceImage;
        public string SecondChoicePosition;
    }
}