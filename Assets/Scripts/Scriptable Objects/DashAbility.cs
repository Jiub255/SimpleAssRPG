using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Dash Ability",
    fileName = "New Dash Ability")]
public class DashAbility : GenericAbility
{
    public float dashForce;

    public override void Ability(Vector2 playerPosition, Vector2 playerFacingDirection = default,
    Animator playerAnimator = null, Rigidbody2D playerRigidbody = null)
    {
        //make sure player has enough magic
        if (playerMagic.currentValue >= magicCost)
        {
            playerMagic.currentValue -= magicCost;
            usePlayerMagic.Raise();
        }
        else
        {
            return;
        }
        if (playerRigidbody)
        {
            Vector3 dashVector = playerRigidbody.transform.position
                + (Vector3)playerFacingDirection.normalized * dashForce;
            playerRigidbody.DOMove(dashVector, abilityDuration);
        }
    }
}
