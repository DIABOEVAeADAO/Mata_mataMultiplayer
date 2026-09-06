using Unity.Netcode;
using UnityEngine;

public class NetworkPlataform : NetworkBehaviour
{
    [SerializeField] private float _distance = 5f;
    [SerializeField] private float velocty = 2f;

    private Vector3 point;

    private void Start()
    {
        point = transform.position;
    }

    private void Update()
    {
        if (!IsOwner) return;

        float distance = Mathf.PingPong(Time.time * velocty, _distance);
        transform.position = point + new Vector3(0, distance, 0); 
        //pontodepartida + ponto final
    }
}