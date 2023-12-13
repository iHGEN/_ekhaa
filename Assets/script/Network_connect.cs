using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Network_connect : MonoBehaviour
{
   public void Create()
    {
        NetworkManager.Singleton.StartHost();
    }
    public void  Join()
    {
        NetworkManager.Singleton.StartClient();
    }
}
