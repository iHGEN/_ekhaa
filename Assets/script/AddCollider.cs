using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCollider : MonoBehaviour {

    // Use this for initialization
    void Awake () {

        Transform[] childObjects = GetComponentsInChildren<Transform>();
        Span<Transform> childObjectSpan = new Span<Transform>(childObjects);

        for (int i = 0; i < childObjectSpan.Length; i++)
        {
          
            if (childObjectSpan[i].gameObject.TryGetComponent<MeshFilter>(out _) && !childObjectSpan[i].gameObject.TryGetComponent<MeshCollider>(out _))
            {
                childObjectSpan[i].gameObject.AddComponent<MeshCollider>();
            }
        }
    }
}
