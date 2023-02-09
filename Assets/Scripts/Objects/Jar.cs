using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Smash()
    {
        animator.SetBool("Smashed", true);
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
    }

    public void DisableJar()
    {
        gameObject.SetActive(false);
    }
}
