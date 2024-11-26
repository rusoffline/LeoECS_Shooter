using System.Collections;
using UnityEngine;

public class OpenDoorWithScript : BaseDoorAction
{
    public Transform leftDoor;
    public Transform rightDoor;
    public float openAngle = 90f;
    public float closeAngle = 0f;
    public float speed = 2f;

    private Coroutine leftDoorCoroutine;
    private Coroutine rightDoorCoroutine;

    public override void OpenForward()
    {
        if (leftDoor != null)
        {
            if (leftDoorCoroutine != null) StopCoroutine(leftDoorCoroutine);
            leftDoorCoroutine = StartCoroutine(RotateDoor(leftDoor, openAngle));
        }
        if (rightDoor != null)
        {
            if (rightDoorCoroutine != null) StopCoroutine(rightDoorCoroutine);
            rightDoorCoroutine = StartCoroutine(RotateDoor(rightDoor, -openAngle));
        }
    }

    public override void OpenBackward()
    {
        if (leftDoor != null)
        {
            if (leftDoorCoroutine != null) StopCoroutine(leftDoorCoroutine);
            leftDoorCoroutine = StartCoroutine(RotateDoor(leftDoor, -openAngle));
        }
        if (rightDoor != null)
        {
            if (rightDoorCoroutine != null) StopCoroutine(rightDoorCoroutine);
            rightDoorCoroutine = StartCoroutine(RotateDoor(rightDoor, openAngle));
        }
    }

    public override void Close()
    {
        if (leftDoor != null)
        {
            if (leftDoorCoroutine != null) StopCoroutine(leftDoorCoroutine);
            leftDoorCoroutine = StartCoroutine(RotateDoor(leftDoor, closeAngle));
        }
        if (rightDoor != null)
        {
            if (rightDoorCoroutine != null) StopCoroutine(rightDoorCoroutine);
            rightDoorCoroutine = StartCoroutine(RotateDoor(rightDoor, closeAngle));
        }
    }

    private IEnumerator RotateDoor(Transform doorTransform, float targetAngle)
    {
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

        while (Quaternion.Angle(doorTransform.localRotation, targetRotation) > 0.1f)
        {
            doorTransform.localRotation = Quaternion.Slerp(doorTransform.localRotation, targetRotation, speed * Time.deltaTime);
            yield return null;
        }

        doorTransform.localRotation = targetRotation;
    }
}
