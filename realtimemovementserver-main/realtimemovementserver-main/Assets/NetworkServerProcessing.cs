using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class NetworkServerProcessing
{

    #region Send and Receive Data Functions
    static public void ReceivedMessageFromClient(string msg, int clientConnectionID, TransportPipeline pipeline)
    {
        Debug.Log("Network msg received =  " + msg + ", from connection id = " + clientConnectionID + ", from pipeline = " + pipeline);

        string[] csv = msg.Split(',');
        int signifier = int.Parse(csv[0]);

        if (signifier == ClientToServerSignifiers.KeyboardInput)
        {
            int direction = int.Parse(csv[1]);

            gameLogic.GetComponent<GameLogic>().UpdateKeyboardInput(direction, clientConnectionID);
        }
    }
    static public void SendMessageToClient(string msg, int clientConnectionID, TransportPipeline pipeline)
    {
        networkServer.SendMessageToClient(msg, clientConnectionID, pipeline);
    }

    #endregion

    #region Connection Events

    static public void ConnectionEvent(int clientConnectionID)
    {
        Debug.Log("Client connection, ID == " + clientConnectionID);
    }
    static public void DisconnectionEvent(int clientConnectionID)
    {
        Debug.Log("Client disconnection, ID == " + clientConnectionID);
    }

    #endregion

    #region Setup
    static NetworkServer networkServer;
    static GameLogic gameLogic;

    static public void SetNetworkServer(NetworkServer NetworkServer)
    {
        networkServer = NetworkServer;
    }
    static public NetworkServer GetNetworkServer()
    {
        return networkServer;
    }
    static public void SetGameLogic(GameLogic GameLogic)
    {
        gameLogic = GameLogic;
    }

    #endregion
}

#region Protocol Signifiers
static public class ClientToServerSignifiers
{
    public const int KeyboardInput = 1;
}

static public class ServerToClientSignifiers
{
    public const int VelocityAndPosition = 1;
}

static public class KbInputDirections
{
    public const int Up = 1;
    public const int Down = 2;
    public const int Right = 3;
    public const int Left = 4;

    public const int UpRight = 5;
    public const int UpLeft = 6;
    public const int DownRight = 7;
    public const int DownLeft = 8;

    public const int NoInput = 9;
}

#endregion

