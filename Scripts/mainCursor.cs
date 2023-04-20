using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCursor : MonoBehaviour
{
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void LateUpdate()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void Update()
    {
        
    }
}
