public interface IUpgrade
{
    UpgradeData Data { get; }

    int Level { get; }

    void Apply();
}