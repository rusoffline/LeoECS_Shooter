using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : ElectricalConsumer
{
    [System.Serializable]
    public class ElevatorFloor
    {
        public Transform floor;
        public ElevatorDoor elevatorDoor;
    }

    [Header("Elevator Settings:")]
    public ElevatorDoor[] doors;
    public ElevatorFloor[] floors;
    public Transform cabin;
    public AudioSource audioSource;
    public AudioClip elevatorLoopClip;
    public float moveSpeed = 2f;

    private int currentFloor = 0;
    private bool isMoving = false;

    private void Start()
    {
        doors = GetComponentsInChildren<ElevatorDoor>();
    }

    public void MoveToFloor(int floorIndex)
    {
        if (isMoving || floorIndex < 0 || floorIndex >= floors.Length)
        {
            Debug.LogWarning("Перемещение невозможно!");
            return;
        }
        if (TryUsePower())
        {
            StartCoroutine(MoveElevator(floorIndex));
        }
    }

    private IEnumerator MoveElevator(int targetFloor)
    {
        isMoving = true;

        floors[currentFloor].elevatorDoor.Close();

        Vector3 startPosition = cabin.position;
        Vector3 targetPosition = new Vector3(cabin.position.x, floors[targetFloor].floor.position.y, cabin.position.z);

        //if (Vector3.Distance(startPosition, targetPosition) <= .1f)
        //{
        //    floors[currentFloor].elevatorDoor.OpenForward();
        //    yield break;
        //}

        audioSource.clip = elevatorLoopClip;
        audioSource.loop = true;
        audioSource.Play();

        float journey = 0f;

        while (journey < 1f)
        {
            journey += Time.fixedDeltaTime * moveSpeed / Vector3.Distance(startPosition, targetPosition);
            cabin.position = Vector3.Lerp(startPosition, targetPosition, journey);
            yield return null;
        }

        currentFloor = targetFloor;

        floors[currentFloor].elevatorDoor.OpenForward();

        audioSource.Stop();

        isMoving = false;
    }

    private void OnTriggerExit(Collider other)
    {
        foreach(var door in doors)
        {
            door.Close();
        }
    }

    public int GetCurrentFloor()
    {
        return currentFloor;
    }
}
