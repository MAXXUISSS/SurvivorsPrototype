using UnityEngine;

public class AttackSpeedUpgrade : IUpgrade
{
    private PlayerAttack playerAttack;
    private UpgradeData data; 
    public UpgradeData Data => data;
    
    private int level; 
    public int Level => level;

    public AttackSpeedUpgrade(
        PlayerAttack playerAttack,
        UpgradeData data)
    {
        this.playerAttack = playerAttack;
        this.data = data;
    }

    public void Apply()
    {
        playerAttack.IncreaseAttackSpeed(data.Value);
        level++;
       
    }
}