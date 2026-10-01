using UnityEngine;
public enum UpgradeType
{
    Damage,
    AttackSpeed,
    MovementSpeed
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

    public UpgradeType UpgradeType => upgradeType;
    public string DisplayName => displayName;
    public string Description => description;
    public Sprite Icon => icon;
    public float Value => value;
}
