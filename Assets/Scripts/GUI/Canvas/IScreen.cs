public interface IScreen
{
    void ShowScreen();
    bool IsActive { get; }
    bool TryHideScreen();
}
