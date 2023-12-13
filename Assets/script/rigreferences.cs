using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigreferences : MonoBehaviour
{
    public Transform root;
    public Transform _RightHand;
    public Transform _LeftHand;
    public Transform _Head;
    public static rigreferences inactenc;
    private void Awake()
    {
        inactenc = this;
    }

}
