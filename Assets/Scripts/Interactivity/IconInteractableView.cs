using UnityEngine;

public class IconInteractableView : MonoBehaviour
{
    public Animator animator;
    public void UpdateInteractionIcon(bool isNear)
    {
        animator.SetBool("IsNear", isNear);
    }
}
