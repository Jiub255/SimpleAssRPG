using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Abilities/Generic Ability",
    fileName = "New Generic Ability")]
public class GenericAbility : ScriptableObject
{
    public int magicCost;
    public float abilityDuration;

    public IntValueHealth playerMagic;
    public Signal usePlayerMagic;

    public virtual void Ability(Vector2 playerPosition, Vector2 playerFacingDirection = default,
        Animator playerAnimator = null, Rigidbody2D playerRigidbody = null)
    {

    }
}
