using UnityEngine;

public class DamageUpgrade :  IUpgrade
{
    private PlayerAttack playerAttack;

    public DamageUpgrade(PlayerAttack playerAttack)
    {
        this.playerAttack = playerAttack;
    }

    public void Apply()
    {
        playerAttack.IncreaseDamage(5);
    }
}
