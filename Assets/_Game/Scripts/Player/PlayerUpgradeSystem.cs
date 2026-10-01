using UnityEngine;
using System.Collections.Generic;


public class PlayerUpgradeSystem : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private PlayerExperience playerExperience;
    private PlayerMovement playerMovement;
    
    private List<IUpgrade> upgrades;
     private List<IUpgrade> availableUpgrades;
     private List<IUpgrade> upgradeOptions;

    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
        playerExperience = GetComponent<PlayerExperience>();
        playerMovement = GetComponent<PlayerMovement>();
        upgrades = new List<IUpgrade>
        {
            new DamageUpgrade(playerAttack),
            new AttackSpeedUpgrade(playerAttack),
            new MovementSpeedUpgrade(playerMovement)
        };
    }
    
    private void OnEnable()
    {
        playerExperience.OnLevelUp += GenerateUpgradeOptions;
    }

    private void OnDisable()
    {
        playerExperience.OnLevelUp -= GenerateUpgradeOptions;
    }
    private void GenerateUpgradeOptions()
    {
        upgradeOptions = new List<IUpgrade>();
        availableUpgrades = new List<IUpgrade>(upgrades);
        int optionCount = Mathf.Min(3, availableUpgrades.Count);
        
        for (int i = 0; i < optionCount; i++)
        {
            int randomIndex = Random.Range(0, availableUpgrades.Count);

            IUpgrade selectedUpgrade = availableUpgrades[randomIndex];

            upgradeOptions.Add(selectedUpgrade);

            availableUpgrades.Remove(selectedUpgrade);

        }
        foreach (IUpgrade upgrade in upgradeOptions)
        {
            Debug.Log("Option: " + upgrade);
        }
        
    }
}