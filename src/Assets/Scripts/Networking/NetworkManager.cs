using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    [Header("Network Settings")]
    public int maxConnections = 10;
    public int port = 7777;
    public string networkAddress = "localhost";

    private bool isServer = false;

    [Header("Player Prefab")]
    public GameObject playerPrefab;

    private List<GameObject> connectedPlayers = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start the server
    public void StartServer()
    {
        isServer = true;
        NetworkServer.Listen(port);
        NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnect);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnClientDisconnect);

        Debug.Log("Server started on port " + port);
    }

    // Connect to a server
    public void StartClient()
    {
        isServer = false;
        NetworkClient client = new NetworkClient();
        client.RegisterHandler(MsgType.Connect, OnConnected);
        client.RegisterHandler(MsgType.Disconnect, OnDisconnected);
        client.Connect(networkAddress, port);

        Debug.Log("Connecting to server at " + networkAddress + ":" + port);
    }

    // Stop the server or client
    public void Stop()
    {
        if (isServer)
        {
            NetworkServer.Shutdown();
            Debug.Log("Server stopped.");
        }
        else
        {
            NetworkManager.singleton.StopClient();
            Debug.Log("Client disconnected.");
        }
    }

    // Handle client connection to the server
    private void OnClientConnect(NetworkMessage netMsg)
    {
        Debug.Log("Client connected: " + netMsg.conn);

        if (playerPrefab != null)
        {
            GameObject player = Instantiate(playerPrefab);
            connectedPlayers.Add(player);

            NetworkServer.AddPlayerForConnection(netMsg.conn, player, 0);
        }
    }

    // Handle client disconnection from the server
    private void OnClientDisconnect(NetworkMessage netMsg)
    {
        Debug.Log("Client disconnected: " + netMsg.conn);

        foreach (var player in connectedPlayers)
        {
            if (player != null)
            {
                NetworkServer.Destroy(player);
            }
        }

        connectedPlayers.Clear();
    }

    // Handle client successfully connecting to the server
    private void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server.");
    }

    // Handle client disconnecting from the server
    private void OnDisconnected(NetworkMessage netMsg)
    {
        Debug.Log("Disconnected from server.");
    }

    // Add a player to the server
    public void AddPlayer(NetworkConnection conn)
    {
        if (playerPrefab != null)
        {
            GameObject player = Instantiate(playerPrefab);
            connectedPlayers.Add(player);

            NetworkServer.AddPlayerForConnection(conn, player, 0);
        }
    }

    // Remove a player from the server
    public void RemovePlayer(NetworkConnection conn)
    {
        GameObject playerToRemove = null;

        foreach (var player in connectedPlayers)
        {
            if (player.GetComponent<NetworkIdentity>().connectionToClient == conn)
            {
                playerToRemove = player;
                break;
            }
        }

        if (playerToRemove != null)
        {
            connectedPlayers.Remove(playerToRemove);
            NetworkServer.Destroy(playerToRemove);
        }
    }
}