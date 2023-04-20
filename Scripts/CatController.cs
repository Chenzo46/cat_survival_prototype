using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spr;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject dust;
    [SerializeField] private Transform dustPos;
    [SerializeField] private float dustRate = 0.2f;
    [SerializeField] private Transform hand;
    [SerializeField] private Vector2 handClamp = new Vector2(1,1);
    [SerializeField] private float handSmooth = 0.1f;

    private float dustTime;

    private Vector2 moveInput;

    //Reference variables
    private Vector3 handRef;
    
    void Awake()
    {
        dustTime = 0;

        hotBar.OnSelectionChange += setSelectedItem;
    }

    private void Update()
    {
        spawnDust();
        setHandPosition();
    }

    private void setSelectedItem()
    {
        if (hotBar.instance.selected != null)
        {
            foreach(Transform t in hand)
            {
                Destroy(t.gameObject);
            }

            GameObject sel = Instantiate(hotBar.instance.selected.data.HoldPrefab, hand);
            sel.transform.localPosition = Vector2.zero + Vector2.up/5;
        }
        else
        {
            foreach (Transform t in hand)
            {
                Destroy(t.gameObject);
            }
        }
    }

    private void spawnDust()
    {
        if (moveInput != Vector2.zero)
        {
            if (dustTime <= 0)
            {
                Instantiate(dust, dustPos.position, dust.transform.rotation);
                dustTime = dustRate;
            }
            else
            {
                dustTime -= Time.deltaTime;
            }
        }
        else
        {
            if (dustTime != 0)
            {
                dustTime = dustRate;
            }
        }
    }

    private void setHandPosition()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        hand.position = Vector3.SmoothDamp(hand.position, mousePos, ref handRef, handSmooth);
        hand.localPosition = new Vector2(Mathf.Clamp(hand.localPosition.x, -handClamp.x, handClamp.x), 
            Mathf.Clamp(hand.localPosition.y, -handClamp.y, handClamp.y));
    }

    void FixedUpdate()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rb.MovePosition((Vector2)transform.position + (moveInput.normalized * speed * Time.deltaTime));

        setWalkAnim();

        flipSprite();
    }

    private void flipSprite()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(mousePos.x > transform.position.x)
        {
            spr.flipX = false;
        }
        else if (mousePos.x < transform.position.x)
        {
            spr.flipX = true;
        }
    }

    private void setWalkAnim()
    {
        if (moveInput != Vector2.zero)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, handClamp*2);
    }
}
