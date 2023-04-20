using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class itemUI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TMP_Text amount;
    [SerializeField] private Image Back;
    [SerializeField] private Sprite normalBack, hotBack;

    private InventoryItem repItem;

    public void set(InventoryItem item)
    {
        repItem = item;

        itemSprite.sprite = item.data.icon;
        amount.text = "x" + item.stackSize.ToString();
        if(item.stackSize <= 1)
        {
            amount.gameObject.SetActive(false);
        }
        else
        {
            amount.gameObject.SetActive(true);
        }

    }

    private void Update()
    {
        if (repItem.selected)
        {
            Back.sprite = hotBack;
        }
        else
        {
            Back.sprite = normalBack;
        }
    }

    public void ToggleHot()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!repItem.hotSelected && !hotBar.instance.hotFull())
            {
                hotBar.instance.AddItem(repItem);
                repItem.hotSelected = true;
            }
            else if (repItem.hotSelected)
            {
                hotBar.instance.RemoveItem(repItem);
                repItem.hotSelected = false;
            }
        }
        
    }
}
