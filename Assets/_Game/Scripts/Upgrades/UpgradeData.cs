using UnityEngine;

[CreateAssetMenu(
    fileName = "UpgradeData",
    menuName = "Game/Upgrade Data"
)]

public class UpgradeData : ScriptableObject
{
   
    [SerializeField] private string displayName;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private float value;
    
    public string DisplayName => displayName;
    public string Description => description;
    public Sprite Icon => icon;
    public float Value => value;
    
}
