using UnityEngine;
public enum UpgradeType
{
    Damage,
    AttackSpeed,
    MovementSpeed,
    MaxHealth,
    HealthRegen,
    AttackRange,
}

[CreateAssetMenu(
    fileName = "UpgradeData",
    menuName = "Game/Upgrade Data"
)]

public class UpgradeData : ScriptableObject
{
    [SerializeField] private UpgradeType upgradeType;

    [SerializeField] private string displayName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private float value;
    [SerializeField] private int maxLevel = 5;

    public UpgradeType UpgradeType => upgradeType;
    public string DisplayName => displayName;
    public string Description => description;
    public Sprite Icon => icon;
    public float Value => value;
    public int MaxLevel => maxLevel;
}
