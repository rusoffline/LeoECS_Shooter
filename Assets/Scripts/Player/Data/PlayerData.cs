using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "MyAssets/PlayerData")]
public class PlayerData : ScriptableObject
{
    public PlayerView playerPrefab;

    public float mouseSensitivity = 2f;
    public float cameraClamp = 85f;
    public float playerRotatoinSpeed = 15f;
    public float walkSpeed = 2.5f;
    public float jogSpeed = 4.5f;
    public float crouchSpeed = 2f;

    public LayerMask groundMask;
    public LayerMask weaponInteractableMask;
    public float visibleDistance = 3f;
    public float interactableDistance = 1.2f;
    public float interactableAngle = 80f;
}
