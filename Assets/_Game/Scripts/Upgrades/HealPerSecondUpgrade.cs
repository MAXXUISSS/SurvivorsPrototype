using UnityEngine;

public class HealPerSecondUpgrade : IUpgrade
{
    private UpgradeData data;
    public UpgradeData Data => data;

    private PlayerHealth playerHealth;

    private int level;
    public int Level => level;

    public HealPerSecondUpgrade(
        UpgradeData data,
        PlayerHealth playerHealth)
    {
        this.data = data;
        this.playerHealth = playerHealth;
    }

    public void Apply()
    {
        playerHealth.IncreaseHealPerSecond(data.Value);
        level++;
    }
}