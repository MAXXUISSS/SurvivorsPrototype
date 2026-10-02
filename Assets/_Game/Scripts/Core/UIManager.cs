using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UpgradeSelectionUI upgradeSelectionUI;
    [SerializeField] private PlayerUpgradeSystem playerUpgradeSystem;

    private void Awake()
    {
        Initialize(playerUpgradeSystem);

        upgradeSelectionUI.OnUpgradeSelected += HandleUpgradeSelected;
    }

    private void Initialize(PlayerUpgradeSystem upgradeSystem)
    {
        playerUpgradeSystem = upgradeSystem;

        playerUpgradeSystem.OnUpgradeOptionsGenerated += ShowUpgradeSelection;
    }

    private void ShowUpgradeSelection()
    {
        upgradeSelectionUI.Show(
            playerUpgradeSystem.UpgradeOptions
        );
    }

    private void HandleUpgradeSelected(int index)
    {
        playerUpgradeSystem.SelectUpgrade(index);

        HideUpgradeSelection();
    }

    public void HideUpgradeSelection()
    {
        upgradeSelectionUI.Hide();
    }

    private void OnDestroy()
    {
        if (playerUpgradeSystem != null)
        {
            playerUpgradeSystem.OnUpgradeOptionsGenerated -= ShowUpgradeSelection;
        }

        if (upgradeSelectionUI != null)
        {
            upgradeSelectionUI.OnUpgradeSelected -= HandleUpgradeSelected;
        }
    }
}