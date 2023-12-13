using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandoninput : MonoBehaviour
{
    public InputActionProperty _Handtrigger;
    public InputActionProperty _Handgrip;
    public Animator animator;
    private void Update()
    {
        animator.SetFloat("Trigger", _Handtrigger.action.ReadValue<float>());
        animator.SetFloat("Grip", _Handgrip.action.ReadValue<float>());
    }
}
