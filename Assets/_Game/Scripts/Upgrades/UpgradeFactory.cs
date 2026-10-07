public class UpgradeFactory
{
    private PlayerAttack playerAttack;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

    public UpgradeFactory(
        PlayerAttack playerAttack,
        PlayerMovement playerMovement,
        PlayerHealth playerHealth)
    {
        this.playerAttack = playerAttack;
        this.playerMovement = playerMovement;
        this.playerHealth = playerHealth;
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
            case UpgradeType.MaxHealth:
                return new MaxHealthUpgrade(data, playerHealth);
        }

        return null;
    }
}