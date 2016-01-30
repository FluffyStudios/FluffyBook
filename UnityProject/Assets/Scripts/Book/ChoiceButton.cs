using UnityEngine;
using System.Collections;

public class ChoiceButton : MonoBehaviour
{
    public Bubble Bubble;
    public int choiceNumber = 0;
	
    private void OnMouseDown()
    {
        this.Bubble.ChooseItem(this.choiceNumber);
    }
}
