using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Playernetwork : NetworkBehaviour
{
    // Start is called before the first frame update
    public Transform _Root;
    public Transform _Head;
    public Transform _RightHand;
    public Transform _LeftHand;
    public Renderer[] renderers;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].enabled = false;
            }
        }
    }
    private void Update()
    {
        if (IsOwner)
        {
            _Root.position = rigreferences.inactenc.root.position;
            _Root.rotation = rigreferences.inactenc.root.rotation;
            _Head.position = rigreferences.inactenc._Head.position;
            _Head.rotation = rigreferences.inactenc._Head.rotation;
            _RightHand.position = rigreferences.inactenc._RightHand.position;
            _RightHand.rotation = rigreferences.inactenc._RightHand.rotation;
            _LeftHand.position = rigreferences.inactenc._LeftHand.position;
            _LeftHand.rotation = rigreferences.inactenc._LeftHand.rotation;
        }
    }
}
