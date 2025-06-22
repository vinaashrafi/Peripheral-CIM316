using UnityEngine;

public class InteractableBox : MonoBehaviour, IInteractable
{
    public string boxText = "";

    public void Interact()
    {
        if (boxText == "")
        {
            Debug.Log(gameObject.name + "'s text has not been set yet. Please set a text to ensure that the Interactable Box component is working as intended");
        }
        else
        {
            Debug.Log(gameObject.name + " says... " + boxText);
        }
    }
}
