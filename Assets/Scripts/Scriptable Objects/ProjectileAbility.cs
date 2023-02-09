using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Projectile Ability",
    fileName = "New Projectile Ability")]
public class ProjectileAbility : GenericAbility
{
    [SerializeField]
    private GameObject thisProjectile;


    public override void Ability(Vector2 playerPosition, Vector2 playerFacingDirection = default,
    Animator playerAnimator = null, Rigidbody2D playerRigidbody = null)
    {
        float facingRotation = Mathf.Atan2(playerFacingDirection.y, playerFacingDirection.x);
        GameObject newProjectile = Instantiate(thisProjectile, playerPosition,
            Quaternion.Euler(0f, facingRotation, 0f));
    }

}
