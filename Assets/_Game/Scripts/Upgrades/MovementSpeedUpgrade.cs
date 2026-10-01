using UnityEngine;

public class MovementSpeedUpgrade : IUpgrade
{
    private PlayerMovement playerMovement;

    public MovementSpeedUpgrade(PlayerMovement playerMovement)
    {
        this.playerMovement = playerMovement;
    }

    public void Apply()
    {
        playerMovement.IncreaseMovementSpeed(0.5f);
    }
}
