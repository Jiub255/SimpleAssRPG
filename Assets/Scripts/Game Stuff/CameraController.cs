using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform player;

    [SerializeField] private float smoothing = 0.005f;

    private Animator animator;

    private void Awake()
    {
    }

    private void Start()
    {
        //want camera to start scenes directly above player, no weird pan over thing
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

        animator = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        if (transform.position != player.position)
        {
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }

    public void GoToKick()
    {
        animator.SetBool("KickActive", true);
    }

    public void LeaveKick()
    {
        animator.SetBool("KickActive", false);
    }

}
