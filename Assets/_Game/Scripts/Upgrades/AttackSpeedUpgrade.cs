using UnityEngine;

public class AttackSpeedUpgrade :  IUpgrade
{
    private PlayerAttack playerAttack;

    public AttackSpeedUpgrade(PlayerAttack playerAttack)
    {
        this.playerAttack = playerAttack;
    }

    public void Apply()
    {
        playerAttack.IncreaseAttackSpeed(0.1f);
    }
}
