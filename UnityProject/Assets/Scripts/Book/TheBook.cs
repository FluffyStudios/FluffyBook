using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TheBook : MonoBehaviour
{
    public GameObject GuiPanel;
    public Text InputText;
    public string[] KeyWords;
    public Sprite[] VignettesSprites;
    public SpriteRenderer[] Bubbles;
    public Sprite BlankBubble;
    public Transform VignetteContainer;
        
    private string currentInputText;
    private List<string> foundKeyWords;

    private void Awake()
    {
        this.foundKeyWords = new List<string>();
    }

    private void Start()
    {
        
	}

    public void OnInputTextChange()
    {
        this.currentInputText = this.InputText.text;
        string[] words = this.currentInputText.Split(' ');
        this.foundKeyWords.Clear();
        
        for (int i = 0; i < words.Length; ++i)
        {
            if (this.KeyWords.Contains(words[i]) && !this.foundKeyWords.Contains(words[i]))
            {
                this.foundKeyWords.Add(words[i]);
            }
        }

        for (int i = 0; i < this.Bubbles.Length; ++i)
        {
            if (i < this.foundKeyWords.Count)
            {
                int vignetteIndex = Array.IndexOf(this.KeyWords, this.foundKeyWords[i]);
                this.Bubbles[i].sprite = this.VignettesSprites[vignetteIndex];
            }
            else
            {
                this.Bubbles[i].sprite = this.BlankBubble;
            }
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
        
    }
}
