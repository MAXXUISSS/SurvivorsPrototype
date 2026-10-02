public interface IUpgrade
{
    UpgradeData Data { get; }

    void Apply();
}