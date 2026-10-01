using UnityEngine;

public class DamageUpgrade :  IUpgrade
{
    private PlayerAttack playerAttack;
    private UpgradeData data;

    public DamageUpgrade(
        PlayerAttack playerAttack,
        UpgradeData data)
    {
        this.playerAttack = playerAttack;
        this.data = data;
    }
    public void Apply()
    {
        playerAttack.IncreaseDamage(Mathf.RoundToInt(data.Value));
    }
}
