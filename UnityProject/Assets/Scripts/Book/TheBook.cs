using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TheBook : MonoBehaviour
{
    public Transform ChapterPrefab;
    public GameObject GuiPanel;
    public Transform ChapterContainer;

    private List<string> chapterPool;
    private List<string> availableChapterPool;
    private List<Chapter> currentChapters;

    public int CurrentDay
    {
        get
        {
            return this.currentChapters.Count;
        }
    }

    public int ActiveChapterIndex
    {
        get;
        set;
    }

    public Chapter ActiveChapter
    {
        get
        {
            if (this.ActiveChapterIndex >= this.currentChapters.Count)
            {
                return null;
            }

            return this.currentChapters[this.ActiveChapterIndex];
        }
    }        

    private void Awake()
    {
        this.currentChapters = new List<Chapter>();
        this.chapterPool = new List<string>();
        this.availableChapterPool = new List<string>();
        if (Directory.Exists(Chapter.Path))
        {
            foreach (string file in Directory.GetFiles(Chapter.Path, "*.json"))
            {
                this.chapterPool.Add(Path.GetFileNameWithoutExtension(file));
            }
        }

    }

    private void Start ()
    {
        this.StartStory();
	}

    public void StartStory()
    {
        this.currentChapters.Clear();
        this.availableChapterPool.Clear();
        this.ActiveChapterIndex = 0;

        foreach (string chapterPath in this.chapterPool)
        {
            
            this.availableChapterPool.Add(chapterPath);
        }

        this.StartNewDay();
    }

    public void StartNewDay()
    {
        this.AddChapter();
        this.ActiveChapterIndex = 0;
        this.ActiveChapter.Show();
    }

    public void AddChapter()
    {
        if (this.availableChapterPool.Count > 0)
        {
            int random = Random.Range(0, this.availableChapterPool.Count);
            Transform gameObject = GameObject.Instantiate(this.ChapterPrefab);
            Chapter newChapter = gameObject.GetComponent<Chapter>();
            newChapter.transform.parent = this.ChapterContainer.transform;
            newChapter.Initialize(this.availableChapterPool[random]);
            this.currentChapters.Add(newChapter);
            this.availableChapterPool.RemoveAt(random);
        }
        else
        {
            Debug.Log("No more available chapter");
        }
    }

    public void NextChapter()
    {
        this.ActiveChapter.Hide();
        this.ActiveChapterIndex++;

        if (this.ActiveChapter != null)
        {
            this.ActiveChapter.Show();
        }
        else
        {
            this.GameOver();
        }
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
        this.GuiPanel.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
        this.GuiPanel.SetActive(false);
    }

    public void GameOver()
    {
        Debug.Log("GG !");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
