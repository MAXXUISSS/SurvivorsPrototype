using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerUpgradeSystem playerUpgradeSystem;

    public float ElapsedTime { get; private set; }

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        CurrentState = GameState.Playing;
        ApplyState(CurrentState);
    }

    private void OnEnable()
    {
        playerUpgradeSystem.OnUpgradeOptionsGenerated += HandleUpgradeOptionsGenerated;
    }

    private void OnDisable()
    {
        playerUpgradeSystem.OnUpgradeOptionsGenerated -= HandleUpgradeOptionsGenerated;
    }

    private void Update()
    {
        ElapsedTime += Time.deltaTime;
    }

    private void HandleUpgradeOptionsGenerated()
    {
        ChangeState(GameState.ChoosingUpgrade);
    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState = newState;

        ApplyState(CurrentState);

        Debug.Log("Game State: " + CurrentState);
    }
    
    private void ApplyState(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                break;

            case GameState.ChoosingUpgrade:
                Time.timeScale = 0f;
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                break;
        }
    }
}