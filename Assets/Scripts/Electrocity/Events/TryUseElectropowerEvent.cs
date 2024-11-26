using UnityEngine.Events;

public struct TryUseElectropowerEvent
{
    public int requiredPower;
    public UnityAction OnSuccess;
    public UnityAction OnFailure;
}
