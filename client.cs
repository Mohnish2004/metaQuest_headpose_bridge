using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UdpSocket : MonoBehaviour
{
    [SerializeField] string IP = "127.0.0.1"; // Local host
    [SerializeField] int rxPort = 8000; // Port to receive data from Python on
    [SerializeField] int txPort = 8001; // Port to send data to Python on

    UdpClient client;
    IPEndPoint remoteEndPoint;
    int i = 0; // Counter for the numbers to send

    void Start()
    {
        // Create remote endpoint (to Python) 
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), txPort);

        // Create local client
        client = new UdpClient(rxPort);

        // Start a thread for reception of incoming messages
        Thread receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

        Debug.Log("UDP Comms Initialized");
    }

    void Update()
    {
        // Send data continuously
        SendData("Sent from Unity: " + i.ToString());
        i++;
    }

    public void SendData(string message) // Use to send data to Python
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, remoteEndPoint);
        }
        catch (Exception err)
        {
            Debug.LogError(err.ToString());
        }
    }

    void ReceiveData()
    {
        // Add your code for receiving data from Python here
    }
}
