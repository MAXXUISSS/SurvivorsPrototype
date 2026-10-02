using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UpgradeSelectionUI upgradeSelectionUI;
    [SerializeField] private GameManager gameManager;

    private PlayerUpgradeSystem playerUpgradeSystem;

    private void OnEnable()
    {
        gameManager.OnPlayerSpawned += Initialize;

        upgradeSelectionUI.OnUpgradeSelected +=
            HandleUpgradeSelected;
    }

    private void OnDisable()
    {
        if (gameManager != null)
        {
            gameManager.OnPlayerSpawned -= Initialize;
        }

        if (upgradeSelectionUI != null)
        {
            upgradeSelectionUI.OnUpgradeSelected -=
                HandleUpgradeSelected;
        }

        if (playerUpgradeSystem != null)
        {
            playerUpgradeSystem.OnUpgradeOptionsGenerated -=
                ShowUpgradeSelection;
        }
    }

    private void Initialize(GameObject playerObject)
    {
        playerUpgradeSystem =
            playerObject.GetComponent<PlayerUpgradeSystem>();

        playerUpgradeSystem.OnUpgradeOptionsGenerated +=
            ShowUpgradeSelection;
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

        gameManager.ChangeState(GameState.Playing);
    }

    public void HideUpgradeSelection()
    {
        upgradeSelectionUI.Hide();
    }
}