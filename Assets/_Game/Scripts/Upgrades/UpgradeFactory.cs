public class UpgradeFactory
{
    private PlayerAttack playerAttack;
    private PlayerMovement playerMovement;

    public UpgradeFactory(
        PlayerAttack playerAttack,
        PlayerMovement playerMovement)
    {
        this.playerAttack = playerAttack;
        this.playerMovement = playerMovement;
    }
    
    public IUpgrade Create(UpgradeData data)
    {
        switch (data.UpgradeType)
        {
            case UpgradeType.Damage:
                return new DamageUpgrade(playerAttack, data);

            case UpgradeType.AttackSpeed:
                return new AttackSpeedUpgrade(playerAttack, data);

            case UpgradeType.MovementSpeed:
                return new MovementSpeedUpgrade(playerMovement, data);
        }

        return null;
    }
}