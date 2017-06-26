public interface _IsStat
{
    float GetCurrent();
    float GetBase();
    float GetMax();

    void AddToCurrent(float value);
    void SetCurrent(float value);
    void SetMax(float value);
}
