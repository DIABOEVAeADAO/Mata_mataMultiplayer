using Unity.Netcode;
using UnityEngine;

public class SpinPlataform : NetworkBehaviour
{
    [SerializeField] private float velocity = 50f;

    private void Update()
    {
        if (!IsServer) return;

        transform.Rotate(new Vector3(0,0,velocity) * Time.deltaTime);
    }
}