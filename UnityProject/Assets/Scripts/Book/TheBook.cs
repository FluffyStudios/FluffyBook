using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class TheBook : MonoBehaviour
{    
    private void Awake()
    {
        
    }

    private void Start ()
    {
        
	}

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        Debug.Log("GG !");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
