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
    public AudioSource KeyStrikeSound;
    public AudioSource VictorySound;
    public AudioSource FlashSound;
    public AudioSource DrawingSound;
    public CameraManager CameraManager;
    public GameObject GuiPanel;
    public GameObject InputField;
    public GameObject ScreenshotButton;
    public Text InputText;
    public Text StoryLabel;
    public Transform VignetteContainer;
    public Sprite BlankBubble;
    public SpriteRenderer[] Bubbles;
    public string[] KeyWords;
    public Sprite[] VignettesSprites;


    private string currentInputText;
    private List<string> foundKeyWords;
    private Vignette[] vignettes;
    private bool win;

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
        if (Input.anyKey)
        {
            this.OnInputTextChange();
        }
    }

    public void SoundKeyboard()
    {
        this.KeyStrikeSound.Play();
    }

    public void OnInputTextChange()
    {
        this.currentInputText = this.InputText.text.Replace(",", string.Empty).Replace(".", string.Empty);
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

                    if (this.Bubbles[i].color.a != 1)
                    {
                        this.Bubbles[i].color = new Color(this.Bubbles[i].color.r, this.Bubbles[i].color.g, this.Bubbles[i].color.b, 0);
                        this.Bubbles[i].sprite = this.VignettesSprites[vignetteIndex];
                        this.Bubbles[i].SendMessage("Show", false, SendMessageOptions.DontRequireReceiver);
                        //this.DrawingSound.Play();
                    }
                    else
                    {
                        this.Bubbles[i].sprite = this.VignettesSprites[vignetteIndex];
                        this.Bubbles[i].SendMessage("Show", true, SendMessageOptions.DontRequireReceiver);
                        //this.DrawingSound.Play();
                    }
                }
                else
                {
                    this.Bubbles[i].SendMessage("Hide", false, SendMessageOptions.DontRequireReceiver);
                }
            }

            //Disable left vignettes
            for (int i = 0; i < this.vignettes.Length; ++i)
            {
                if (this.foundKeyWords.Contains(this.vignettes[i].KeyWord))
                {
                    this.vignettes[i].gameObject.SendMessage("Hide", false, SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    this.vignettes[i].gameObject.SendMessage("Show", false, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

        if (this.foundKeyWords.Count < 6)
        {
            if (this.win)
            {
                this.win = false;
                this.ScreenshotButton.SetActive(false);
            }
        }        

        if (this.foundKeyWords.Count >= 6)
        {            
            if (!this.win)
            {
                this.win = true;
                this.VictorySound.Play();
                this.ScreenshotButton.SetActive(true);
            }

            foreach (Vignette vignette in this.vignettes)
            {
                vignette.gameObject.SendMessage("Hide", true, SendMessageOptions.DontRequireReceiver);
            }
        }
        
        // Coloring keywords
        this.StoryLabel.text = this.InputText.text;
        for (int i = 0; i < this.foundKeyWords.Count; ++i)
        {
            this.StoryLabel.text = this.StoryLabel.text.Replace(this.foundKeyWords[i], "<color='#BC001C'>" + this.foundKeyWords[i] + "</color>");
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

    public void TakeScreenshot()
    {
        this.StartCoroutine(this.TakeScreenshotCoroutine());
    }

    private IEnumerator TakeScreenshotCoroutine()
    {
        this.InputField.gameObject.SetActive(false);
        this.ScreenshotButton.gameObject.SetActive(false);
        this.FlashSound.Play();

        yield return new WaitForSeconds(0.25f);

        Application.CaptureScreenshot(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Boris.png");
        this.StartCoroutine(this.CameraManager.FlashPhoto());

        yield return new WaitForSeconds(0.75f);

        this.InputField.gameObject.SetActive(true);
        this.ScreenshotButton.gameObject.SetActive(true);
    }
}
