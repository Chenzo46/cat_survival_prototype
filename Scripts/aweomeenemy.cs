using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aweomeenemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Transform plr;

    // Start is called before the first frame update
    void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = plr.position - transform.position;
        rb.velocity = dir * Time.fixedDeltaTime * 100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}
