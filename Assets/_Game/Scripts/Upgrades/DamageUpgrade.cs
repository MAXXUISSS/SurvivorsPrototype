using UnityEngine;

public class DamageUpgrade :  IUpgrade
{
    private PlayerAttack playerAttack;
    private UpgradeData data;
    public UpgradeData Data => data;
    
    private int level;
    public int Level => level;

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
        level++;
    }
}
