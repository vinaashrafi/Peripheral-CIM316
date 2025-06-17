using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    public int count = 1;
    public ItemScriptable item;
    public Image image;
    public TextMeshProUGUI countText;
    public GameObject itemObject;
    
    public void InitialiseItem(ItemScriptable newItem)
    {
        image.sprite = newItem.image;
    }
    
    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public GameObject ReturnItemObject()
    {
        return itemObject;
    }

    public void Deselected()
    {
        itemObject.SetActive(false);
    }

    public void Killyourself()
    {
        Destroy(gameObject);
    }
    public void Selected()
    {
        itemObject.SetActive(true);
    }
}
