using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class PlayerObjectSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private Transform _point;

    private NetworkObject _objectSpawned;

    public InputActionReference spawnAction;
    public InputActionReference despawnAction;
    private void Awake()
    {
        _point = GameObject.FindGameObjectWithTag("point").GetComponent<Transform>();
    }
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;

        if (spawnAction != null) spawnAction.action.Enable();
        if (despawnAction != null) despawnAction.action.Enable();
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner) return;

        if (spawnAction != null) spawnAction.action.Disable();
        if (despawnAction != null) despawnAction.action.Disable();
    }

    void Update()
    {
        if (!IsOwner) return;

        if (spawnAction != null && spawnAction.action.WasPressedThisFrame())
        {
            SpawnObjectServerRpc();
        }
        else if (despawnAction != null && despawnAction.action.WasPressedThisFrame())
        {
            DespawnObjectServerRpc();
        }
    }

    [ServerRpc]
    private void SpawnObjectServerRpc()
    {
        if (_objectSpawned != null) return;

        GameObject spawnedGo = Instantiate(_objectToSpawn, _point.position, _point.rotation);
        _objectSpawned = spawnedGo.GetComponent<NetworkObject>();

        if (_objectToSpawn != null)
        {
            _objectSpawned.Spawn(true);
            bool isRegistered = NetworkManager.Singleton.NetworkConfig.Prefabs.Contains(_objectToSpawn);

            Debug.Log($"O prefab {_objectToSpawn.name} está registrado no NetworkManager? {isRegistered}");
        }
    }

    [ServerRpc]
    private void DespawnObjectServerRpc()
    {
        if (_objectSpawned == null) return;

        _objectSpawned.Despawn(true);
        _objectSpawned = null;
    }
}