using Unity.VisualScripting;
using UnityEngine;

public class WeaponObject : MonoBehaviour
{
    public AudioSource audioSource;
    private void OnValidate()
    {
        audioSource = gameObject.GetOrAddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 1f;
    }
}
