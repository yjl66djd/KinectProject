using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ServeType {Client , Server }


public class ServeControllor : MonoBehaviour
{ 
    public ServeType Types;

    public Text client, Serve;

    void Start()
    { 
        Init();
    }

    public void Action_ProcessStringData(string _string)
    {
        if (FMNetworkManager.instance.NetworkType == FMNetworkType.Server)
        {
            if (Serve != null) Serve.text = "Server Received : " + _string;
        }
        else
        {
            if (client != null) client.text = "Client Received : " + _string;
        }
    }
     
    void Init()
    {
        if (Types == ServeType.Client)
        {
            FMNetworkManager.instance.Action_InitAsClient();
        }
        else
        {
            FMNetworkManager.instance.Action_InitAsServer();
        }
    }
    public void VibratButton()
    {
        string _value = Random.value.ToString();
        FMNetworkManager.instance.SendToOthers(_value);
    } 
}
