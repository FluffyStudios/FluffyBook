using UnityEngine;
using System.Collections;

public class FlowManager : MonoBehaviour
{
    public CameraManager CameraManager;
    public TheBook TheBook;
    public GameObject MainMenu;

    private void Start()
    {
        this.TheBook.Hide();
    }

    public void Play()
    {
        this.StartCoroutine(this.PlayCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        yield return this.StartCoroutine(this.CameraManager.FadeOut());

        this.MainMenu.SetActive(false);
        this.TheBook.Show();

        yield return this.StartCoroutine(this.CameraManager.FadeIn());
    }
}
