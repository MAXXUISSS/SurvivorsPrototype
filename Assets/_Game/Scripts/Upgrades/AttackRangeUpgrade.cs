using UnityEngine;

public class AttackRangeUpgrade : IUpgrade
{
    private UpgradeData data;
    public UpgradeData Data => data;

    private PlayerAttack playerAttack;

    private int level;
    public int Level => level;

    public AttackRangeUpgrade(
        UpgradeData data,
        PlayerAttack playerAttack)
    {
        this.data = data;
        this.playerAttack = playerAttack;
    }

    public void Apply()
    {
        playerAttack.IncreaseAttackRange(data.Value);
        level++;
    }
}