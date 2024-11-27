using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDoorAction : MonoBehaviour
{
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;
    [SerializeField] private bool isOpen;
    public bool IsOpen => isOpen;

    public virtual void OpenForward()
    {
        if (isOpen)
            return;

        PlayClip(openClip);
        isOpen = true;
    }

    public virtual void OpenBackward()
    {
        if (isOpen)
            return;

        PlayClip(openClip);
        isOpen = true;
    }

    public virtual void Close()
    {
        if (!isOpen)
            return;

        PlayClip(closeClip);
        isOpen = false;
    }

    private void PlayClip(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }
}