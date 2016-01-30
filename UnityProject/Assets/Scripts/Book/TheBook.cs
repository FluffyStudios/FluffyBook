using System;
using System.Text;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class TheBook : MonoBehaviour
{
    public GameObject GuiPanel;
    public CameraManager CameraManager;
    public Text InputText;
    public GameObject InputField;
    public Text StoryLabel;
    public GameObject ScreenshotButton;
    public string[] KeyWords;
    public Sprite[] VignettesSprites;
    public SpriteRenderer[] Bubbles;
    public Sprite BlankBubble;
    public Transform VignetteContainer;
        
    private string currentInputText;
    private List<string> foundKeyWords;
    private Vignette[] vignettes;

    private void Awake()
    {
        this.foundKeyWords = new List<string>();
    }

    private void Start()
    {
        this.vignettes = this.VignetteContainer.GetComponentsInChildren<Vignette>();
        this.ScreenshotButton.SetActive(false);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            this.OnInputTextChange();
        }
    }

    public void OnInputTextChange()
    {
        this.ScreenshotButton.SetActive(false);
        this.StoryLabel.text = this.InputText.text;
        this.currentInputText = this.InputText.text.Replace(",", string.Empty);
        string[] words = this.currentInputText.Split(' ');
        this.foundKeyWords.Clear();
        
        for (int i = 0; i < words.Length; ++i)
        {
            string word = words[i].ToLower();
            if (this.KeyWords.Contains(word) && !this.foundKeyWords.Contains(word))
            {
                this.foundKeyWords.Add(word);
            }
        }

        if (this.foundKeyWords.Count <= 6)
        {
            // Fill Right Bubbles
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

            //Disable left vignettes
            for (int i = 0; i < this.vignettes.Length; ++i)
            {
                this.vignettes[i].gameObject.SetActive(!this.foundKeyWords.Contains(this.vignettes[i].KeyWord));
            }
        }
        
        if (this.foundKeyWords.Count >= 6)
        {
            this.ScreenshotButton.SetActive(true);
            foreach (Vignette vignette in this.vignettes)
            {
                vignette.gameObject.SetActive(false);
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

    public void TakeScreenshot()
    {
        this.StartCoroutine(this.TakeScreenshotCoroutine());
    }

    private IEnumerator TakeScreenshotCoroutine()
    {
        this.InputField.gameObject.SetActive(false);
        this.ScreenshotButton.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.1f);

        Application.CaptureScreenshot(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Boris.png");
        this.StartCoroutine(this.CameraManager.FlashPhoto());

        yield return new WaitForSeconds(0.8f);

        this.InputField.gameObject.SetActive(true);
        this.ScreenshotButton.gameObject.SetActive(true);
    }
}
