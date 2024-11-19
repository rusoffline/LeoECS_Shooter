using Leopotam.Ecs;
using UnityEngine;

public class PlayerInteractiveSystem : IEcsRunSystem
{
    private EcsFilter<InputComponent> inputFilter;
    private EcsFilter<PlayerComponent, VirtualCameraComponent> playerFilter;
    private EcsFilter<InteractableStay> interactableFilter;
    private PlayerData playerData;

    public void Run()
    {
        foreach (var plr in playerFilter)
        {
            ref var player = ref playerFilter.Get1(plr);
            ref var virtualCamera = ref playerFilter.Get2(plr);

            float closestDistance = float.MaxValue;
            InteractableStay closestInteractable = default;
            bool hasInteractable = false;

            foreach (var itr in interactableFilter)
            {
                ref var interactable = ref interactableFilter.Get1(itr);

                IconRotate(virtualCamera.cameraTransform, ref interactable);

                interactable.iconView.UpdateInteractionIcon(false);

                if (CheckInView(ref player, virtualCamera.cameraTransform, ref interactable, out float distance))
                {
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestInteractable = interactable;
                        hasInteractable = true;
                    }
                }
            }

            if (hasInteractable)
            {
                foreach (var inp in inputFilter)
                {
                    ref var input = ref inputFilter.Get1(inp);

                    closestInteractable.interactable.iconView.UpdateInteractionIcon(true);

                    if (input.fire && !input.aim)
                    {
                        closestInteractable.interactable.Interact();
                    }
                }
            }
        }
    }

    private void IconRotate(Transform cameraTranform, ref InteractableStay interactable)
    {
        Vector3 direction = cameraTranform.position - interactable.iconPosition;
        interactable.interactable.iconView.transform.rotation = Quaternion.LookRotation(direction);
    }

    private bool CheckInView(ref PlayerComponent player, Transform cameraTransform, ref InteractableStay interactable, out float distance)
    {
        Vector3 interactablePosition = interactable.iconPosition;
        interactablePosition.y = player.position.y;
        distance = Vector3.Distance(player.position, interactablePosition);

        Vector3 directionToInteractiveObject = interactable.iconPosition - cameraTransform.position;
        Vector3 cameraForward = cameraTransform.forward;
        float angle = Vector3.Angle(cameraForward, directionToInteractiveObject);

        return distance < playerData.interactableDistance && angle < playerData.interactableAngle / 2f;
    }
}
