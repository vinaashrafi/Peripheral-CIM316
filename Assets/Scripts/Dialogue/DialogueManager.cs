using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueBox;
    public 
    
    public delegate void DisplayDialogue(string dialogueText);
    public event DisplayDialogue DisplayDialogueEvent;
    

}
