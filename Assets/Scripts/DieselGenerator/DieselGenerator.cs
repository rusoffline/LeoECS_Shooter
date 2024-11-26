using Leopotam.Ecs;
using UnityEngine;

public class DieselGenerator : ElectricalConsumer
{
    [Header("Engine Settings:")]
    public Transform engineTransform; 
    public Transform fanTransform;
    public Vector3 funRotationAxis = Vector3.forward;
    public AudioSource audioSource;
    public AudioClip startSound;
    public AudioClip runningSound;

    [Header("Animation Settings:")]
    public float vibrationAmplitude = 0.02f;
    public float vibrationSpeed = 50f;
    public float fanSpeed = 500f;

    private bool isRunning = false;
    private Vector3 initialEnginePosition;
    private int power = 1;

    void Start()
    {
        if (engineTransform != null)
            initialEnginePosition = engineTransform.localPosition;
    }

    void Update()
    {
        if (isRunning)
        {
            AnimateEngineVibration();
            RotateFan();
        }
    }

    public void StartGenerator()
    {
        if (isRunning) return;

        isRunning = true;

        if (audioSource != null && startSound != null)
        {
            audioSource.clip = startSound;
            audioSource.Play();
            Invoke(nameof(PlayRunningSound), startSound.length);
        }

        AddPower(power);
    }

    public void StopGenerator()
    {
        if (!isRunning) return;

        isRunning = false;

        if (audioSource != null)
            audioSource.Stop();

        if (engineTransform != null)
            engineTransform.localPosition = initialEnginePosition;

        RemovePower(power);
    }

    private void AnimateEngineVibration()
    {
        if (engineTransform != null)
        {
            float offsetX = Mathf.Sin(Time.time * vibrationSpeed) * vibrationAmplitude;
            float offsetY = Mathf.Cos(Time.time * vibrationSpeed) * vibrationAmplitude;
            engineTransform.localPosition = initialEnginePosition + new Vector3(offsetX, offsetY, 0);
        }
    }

    private void RotateFan()
    {
        if (fanTransform != null)
        {
            fanTransform.Rotate(funRotationAxis, fanSpeed * Time.deltaTime);
        }
    }

    private void PlayRunningSound()
    {
        if (audioSource != null && runningSound != null && isRunning)
        {
            audioSource.clip = runningSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
