﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class InitialServerConnect : MonoBehaviour
{	
    private TcpClient socketConnection;
    private Thread clientReceiveThread;

    public Parse scripter;


    // Use this for initialization 	
    void Start()
    {
        ConnectToTcpServer();
        //SendMessage();
    }
	
    // Setup socket connection. 	
    private void ConnectToTcpServer()
    {
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }

    // Runs in background clientReceiveThread; Listens for incoming data. 	    
    private void ListenForData()
    {
        try
        {
            socketConnection = new TcpClient("data.cs.purdue.edu", 1157);
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                // Get a stream object for reading 				
                using (NetworkStream stream = socketConnection.GetStream())
                {
                    int length;
                    // Read incoming stream into byte arrary. 					
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        Debug.Log("Length of message received is " + length.ToString());
                        var incomingData = new byte[length];
                        Array.Copy(bytes, 0, incomingData, 0, length);
                        // Convert byte array to string message. 						
                        string serverMessage = Encoding.UTF8.GetString(incomingData);
                        Debug.Log("server message received as: " + serverMessage);
                        //if (string.Compare(serverMessage, "Who are you") == 0)
                        if (serverMessage.Contains("are you"))
                        {
                            SendMessages("VR~");
                        }
                        else if (string.Compare(serverMessage, "You are connected. Waiting for PC Player...") == 0)
                        {
                            Debug.Log("Waiting for PC");//Display waiting message
                        }
                        else
                        {
                            if (length > 3) //RIP PC BUDDY :(
                            {
                                if(serverMessage.Contains("incorrect"))
                                {
                                    Debug.Log("Nooo incorrect action!");
                                    SendMessages("Hey Laptop Buddy Incorrect");
                                    scripter.resetWinCount();
                                }
                                else if(serverMessage.Contains("correct"))
                                {
                                    Debug.Log("Correct action!");
                                    SendMessages("Hey Laptop Buddy Correct");
                                    scripter.incrementWinCount();
                                }
                                else
                                {
                                    scripter.newMessage = serverMessage.Trim();
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    // Send message to server using socket connection. 		
    public void SendMessages(string clientMessage)
    {
        if (socketConnection == null)
        {
            return;
        }
        try
        {
            // Get a stream object for writing. 			
            NetworkStream stream = socketConnection.GetStream();
            if (stream.CanWrite)
            {
                //tring clientMessage = "VR~";
                // Convert string message to byte array.                 
                byte[] clientMessageAsByteArray = Encoding.UTF8.GetBytes(clientMessage);
                // Write byte array to socketConnection stream.                 
                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Client sent his message - should be received by server");
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Destroy");
        socketConnection.Close();
    }
}