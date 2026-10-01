using UnityEngine;

public class AttackSpeedUpgrade : IUpgrade
{
    private PlayerAttack playerAttack;
    private UpgradeData data; 

    public AttackSpeedUpgrade(
        PlayerAttack playerAttack,
        UpgradeData data)
    {
        this.playerAttack = playerAttack;
        this.data = data;
    }

    public void Apply()
    {
        Debug.Log("Attack Speed Upgrade Value: " + data.Value);

        Debug.Log("ATTACKSPEED UPGRADE" + data.Value);
    }
}