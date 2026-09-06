using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GamePlayMaenger : NetworkBehaviour
{
    [SerializeField] private TMP_Text TtP_Text;

    private void Awake()
    {
        TtP_Text = GameObject.FindGameObjectWithTag("Deus").GetComponent<TMP_Text>();
    }

    public override void OnNetworkSpawn()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        }

        if (IsServer)
        {
            Exibirtexto("Host entrou na partida", Color.green);
        }
        else
        {
            Exibirtexto("Cliente entrou na partida", Color.green);
        }
    }

    public override void OnNetworkDespawn()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
        }

        Exibirtexto("Você saiu da partida", Color.red);
    }

    private void OnClientConnected(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId) return;

        Exibirtexto($"Cliente {clientId} entrou na partida", Color.green);
    }

    private void OnClientDisconnected(ulong clientId)
    {
        if (clientId == 0)
        {
            Exibirtexto("Host saiu da partida", Color.red);
        }
        else
        {
            Exibirtexto($"Cliente {clientId} foi embora", Color.red);
        }
    }

    private void Exibirtexto(string s, Color cor)
    {
        if (TtP_Text == null) return;

        TtP_Text.color = cor;
        TtP_Text.text = s;
    }
}