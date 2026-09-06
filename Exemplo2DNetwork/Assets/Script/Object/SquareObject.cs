using Unity.Netcode;
using UnityEngine;

public class SquareObject : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (TryGetComponent<MeshRenderer>(out var renderer))
        {
            renderer.material.color = Color.red;
        }
    }

    public override void OnNetworkDespawn()
    {
        Debug.Log("O ObjetoSumiu");
    }
}