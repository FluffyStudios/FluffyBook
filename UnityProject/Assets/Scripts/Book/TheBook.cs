using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TheBook : MonoBehaviour
{
    public Text ChapterTitleLabel;

    private List<Chapter> ActiveChapters;

    private void Awake()
    {
        this.ActiveChapters = new List<Chapter>();
    }

    private void Start ()
    {
	
	}

    public void StartStory()
    {

    }

    public void StartDay()
    {
        this.ActiveChapters.Clear();        
    }

    public void AddChapter()
    {

    }
}
