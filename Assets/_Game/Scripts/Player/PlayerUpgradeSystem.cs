using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerUpgradeSystem : MonoBehaviour
{
    private PlayerAttack playerAttack;
    private PlayerExperience playerExperience;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

    private List<IUpgrade> upgrades;
    private List<IUpgrade> upgradeOptions;

    [SerializeField] private List<UpgradeData> upgradeData;
    
    public IReadOnlyList<IUpgrade> UpgradeOptions => upgradeOptions;
    public event Action OnUpgradeOptionsGenerated;

    private UpgradeFactory upgradeFactory;

    private void Awake()
    {
        playerAttack = GetComponent<PlayerAttack>();
        playerExperience = GetComponent<PlayerExperience>();
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();

        upgrades = new List<IUpgrade>();

        upgradeFactory = new UpgradeFactory(
            playerAttack,
            playerMovement,
            playerHealth
        );

        foreach (UpgradeData data in upgradeData)
        {
            IUpgrade upgrade = upgradeFactory.Create(data);

            upgrades.Add(upgrade);
        }
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

        List<IUpgrade> availableUpgrades =
            new List<IUpgrade>(upgrades);

        int optionCount = Mathf.Min(
            3,
            availableUpgrades.Count
        );

        for (int i = 0; i < optionCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(
                0,
                availableUpgrades.Count
            );

            IUpgrade selectedUpgrade =
                availableUpgrades[randomIndex];

            upgradeOptions.Add(selectedUpgrade);

            availableUpgrades.Remove(selectedUpgrade);
        }

        foreach (IUpgrade upgrade in upgradeOptions)
        {
            Debug.Log("Option: " + upgrade);
        }

        OnUpgradeOptionsGenerated?.Invoke();
    }

    public void SelectUpgrade(int index)
    {
        if (index >= 0 && index < upgradeOptions.Count)
        {
            IUpgrade selectedUpgrade = upgradeOptions[index];

            Debug.Log(
                "Applying upgrade: " +
                selectedUpgrade.Data.DisplayName +
                " | Level: " +
                (selectedUpgrade.Level + 1)
            );

            selectedUpgrade.Apply();

            Debug.Log(
                "Upgrade Level after apply: " +
                selectedUpgrade.Level
            );
        }
    }
}