using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UpgradeSelectionUI upgradeSelectionUI;
    [SerializeField] private PlayerUpgradeSystem playerUpgradeSystem;

    private void Awake()
    {
        Initialize(playerUpgradeSystem);
    }

    private void Initialize(PlayerUpgradeSystem upgradeSystem)
    {
        playerUpgradeSystem = upgradeSystem;

        playerUpgradeSystem.OnUpgradeOptionsGenerated += ShowUpgradeSelection;
    }

    private void ShowUpgradeSelection()
    {
        Debug.Log("UIManager: Showing upgrade selection");

        upgradeSelectionUI.Show(
            playerUpgradeSystem.UpgradeOptions
        );
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
    }
}