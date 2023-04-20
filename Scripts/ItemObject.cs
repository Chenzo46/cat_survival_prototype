using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{

    public InventoryItemData referenceItem;
    private Transform player;
    [SerializeField] private float pickupDistance = 1f;
    


    public void OnHandlePickup()
    {
        InventorySystem.current.Add(referenceItem);
        Destroy(gameObject);
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= pickupDistance && Input.GetKeyDown(KeyCode.E))
        {
            OnHandlePickup();
        }
    }

}
