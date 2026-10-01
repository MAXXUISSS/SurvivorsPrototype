using UnityEngine;

public class MovementSpeedUpgrade : IUpgrade
{
    private PlayerMovement playerMovement;
    private UpgradeData data;

    public MovementSpeedUpgrade(PlayerMovement playerMovement, UpgradeData upgradeData)
    {
        this.playerMovement = playerMovement;
        this.data = upgradeData;
    }

    public void Apply()
    {
        playerMovement.IncreaseMovementSpeed(data.Value);
    }
}
