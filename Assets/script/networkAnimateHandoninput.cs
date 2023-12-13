using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class networkAnimateHandoninput : NetworkBehaviour
{
    // Start is called before the first frame update
    public InputActionProperty _Handtrigger;
    public InputActionProperty _Handgrip;
    public Animator animator;
    private void Update()
    {
        if (IsOwner)
        {
            animator.SetFloat("Trigger", _Handtrigger.action.ReadValue<float>());
            animator.SetFloat("Grip", _Handgrip.action.ReadValue<float>());
        }
    }
}
