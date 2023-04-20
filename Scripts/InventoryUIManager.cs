using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] private GameObject InventoryGrid;
    [SerializeField] private GameObject slotPrefab;

    private bool inventoryOpen = false;

    private GridLayout gp;

    private void Awake()
    {
        InventorySystem.OnInventoryChanged += OnUpdateInventory;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryOpen = !inventoryOpen;
            InventoryGrid.GetComponent<Animator>().SetBool("open", inventoryOpen);
        }
    }

    private void OnUpdateInventory()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        DrawInventory();
    }

    public void DrawInventory()
    {
        foreach (InventoryItem item in InventorySystem.current.inventory)
        {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventoryItem item)
    {
        GameObject slot = Instantiate(slotPrefab, InventoryGrid.transform);
        slot.GetComponent<itemUI>().set(item);
    }
}
