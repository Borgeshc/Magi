using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    PlayerCombat playerCombat;

	void Start ()
    {
        playerCombat = GetComponentInParent<PlayerCombat>();	
	}
	
    public void PrimaryAttack()
    {
        playerCombat.PrimaryAttack();
    }

    public void SecondaryAttack()
    {
        playerCombat.SecondaryAttack();
    }
}
