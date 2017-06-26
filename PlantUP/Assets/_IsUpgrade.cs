public interface _IsUpgrade
{
    int GetCost();
    int GetCurrent();
    int GetMax();


    bool Inkrement();
    bool Dekrement();
    void ResetUpgrade();

    UpgradeType GetUpgradeType();
    string GetInfo();

}
public enum UpgradeType
{
    HEIGHT, LEAVES, STALK, PETAL, REGENERATION, INSECTS,
    DEEPROOTS, POROUSROOTS, SPREADROOTS, EFFICIENCY
}
