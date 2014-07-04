using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;

public class OscServer : MonoBehaviour
{
    public int listenPort = 6666;
    UdpClient udpClient;
    IPEndPoint endPoint;
    Osc.Parser osc = new Osc.Parser ();
    
    void Start ()
    {
        endPoint = new IPEndPoint (IPAddress.Any, listenPort);
        udpClient = new UdpClient (endPoint);
    }

    void Update ()
    {
        while (udpClient.Available > 0) {
            osc.FeedData (udpClient.Receive (ref endPoint));
        }

        while (osc.MessageCount > 0) {
            var msg = osc.PopMessage ();
            // /1/push5:1を_1_push5:1に置換する
            
            var target = GameObject.Find("OVRPlayerController");
            if (target) {
            	Vector3 (float).temp;
  				temp.x = msg.data[1];
  				temp.z = msg.data[2];
  				temp.y = msg.data[3];
 				target.transform.position = temp;
            }
            //オブジェクトにmsg.data[0]の引数を尾耐える

            //msgにはOSCから送られて来るデータが /1/push5:0:
            //msg.data[0]には 0 の値が入っている
            Debug.Log (msg);
            Debug.Log (msg.data[0]);
            Debug.Log (msg.data[1]);
            Debug.Log (msg.data[2]);
            Debug.Log (msg.data[3]);
        }
    }
}