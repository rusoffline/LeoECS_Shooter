using System.Collections;
using UnityEngine;

public class ElevatorDoor : BaseDoorAction
{
    [Header("Elevator Door:")]
    public Transform leftDoor;
    public Transform rightDoor;
    public float openOffset = 1f;
    public float speed = 2f;

    private Vector3 leftDoorDefaultPosition;
    private Vector3 rightDoorDefaultPosition;

    private Coroutine leftDoorCoroutine;
    private Coroutine rightDoorCoroutine;

    private void Start()
    {
        if(leftDoor != null)
        {
            leftDoorDefaultPosition = leftDoor.localPosition;
        }
        if (rightDoor!=null)
        {
           rightDoorDefaultPosition = rightDoor.localPosition;
        }
    }

    public override void Close()
    {
        base.Close();
        CloseDoor();
    }

    public override void OpenBackward()
    {
        base.OpenBackward();
        OpenDoor();
    }

    public override void OpenForward()
    {
        base.OpenForward();
        OpenDoor();
    }

    private void OpenDoor()
    {
        if (leftDoor != null)
        {
            if (leftDoorCoroutine != null) StopCoroutine(leftDoorCoroutine);
            leftDoorCoroutine = StartCoroutine(SlideDoor(leftDoor, leftDoorDefaultPosition - Vector3.right * openOffset));
        }
        if (rightDoor != null)
        {
            if (rightDoorCoroutine != null) StopCoroutine(rightDoorCoroutine);
            rightDoorCoroutine = StartCoroutine(SlideDoor(rightDoor, rightDoorDefaultPosition + Vector3.right * openOffset));
        }
    }

    private void CloseDoor()
    {
        if (leftDoor != null)
        {
            if (leftDoorCoroutine != null) StopCoroutine(leftDoorCoroutine);
            leftDoorCoroutine = StartCoroutine(SlideDoor(leftDoor, leftDoorDefaultPosition));
        }
        if (rightDoor != null)
        {
            if (rightDoorCoroutine != null) StopCoroutine(rightDoorCoroutine);
            rightDoorCoroutine = StartCoroutine(SlideDoor(rightDoor, rightDoorDefaultPosition));
        }
    }

    private IEnumerator SlideDoor(Transform doorTransform, Vector3 targetPosition)
    {
        while (Vector3.Distance(doorTransform.position, targetPosition) > 0.1f)
        {
            doorTransform.localPosition = Vector3.MoveTowards(doorTransform.localPosition, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        doorTransform.localPosition = targetPosition;
    }
}
