using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverUnder : MonoBehaviour
{
    //It works, but is there an easier way?
    //  -put it on player/moving npcs and enemies?
    //  -do the every sprite renderer thing?
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))// or "Enemy" or "NPC"
        {
  //either needs adjusting for each different type of thing, or put it on a child object and adjust that?
            if (transform.position.y > collision.transform.position.y)
            {
                spriteRenderer.sortingOrder = -1;
            }
            else if (transform.position.y <= collision.transform.position.y)
            {
                spriteRenderer.sortingOrder = 1;
            }
        }
    }

}
