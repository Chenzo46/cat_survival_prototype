using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hotBar : MonoBehaviour
{
    public delegate void hotBarEvent();

    public static hotBarEvent OnSelectionChange;

    public static hotBar instance;

    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private float scrollTime = 0.05f;

    public InventoryItem selected;
    private int selectedIndex = 0;

    public List<InventoryItem> hotItems = new List<InventoryItem>(0);

    private bool canScroll = true;

    private void Awake()
    {
        instance = this;

        InventorySystem.OnInventoryChanged += updateHotBar;
    }

    public bool hotFull()
    {
        return hotItems.Count >= 5;
    }


    private void Update()
    {
        float Scroll = -Input.mouseScrollDelta.y;

        if (canScroll && Scroll != 0 && hotItems.Count != 0)
        {
            selectedIndex += (int)Scroll;
            
            if(selectedIndex >= hotItems.Count)
            {
                selectedIndex = hotItems.Count-1;
            }
            else if (selectedIndex < 0)
            {
                selectedIndex = 0;
            }

            StartCoroutine(scrollRoutine());
        }
        else if (hotItems.Count < 1)
        {
            selected = null;
        }

        setSelected();

        //Debug.Log(selectedIndex);
    }

    private void setSelected()
    {
        for (int i = 0; i < hotItems.Count; i++)
        {
            if (i != selectedIndex)
            {
                hotItems[i].selected = false;
            }
            else
            {
                hotItems[i].selected = true;
                selected = hotItems[i];
            }
        }
        OnSelectionChange.Invoke();
    }

    public void AddItem(InventoryItem item)
    {

        if(hotItems.Count < 5)
        {
            hotItems.Add(item);
        }
        else
        {
            hotItems.RemoveAt(0);
            hotItems.Add(item);
        }

        updateHotBar();
        OnSelectionChange.Invoke();
    }

    public void RemoveItem(InventoryItem item)
    {
        hotItems.Remove(item);
        item.selected = false;
        updateHotBar();
    }

    public void updateHotBar()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        foreach(InventoryItem item in hotItems)
        {
            GameObject sl = Instantiate(slotPrefab, transform);
            sl.GetComponent<itemUI>().set(item);
            
        }
    }

    private IEnumerator scrollRoutine()
    {
        canScroll = false;
        yield return new WaitForSeconds(scrollTime);
        canScroll = true;
    }
}
