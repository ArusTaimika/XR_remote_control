using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class UDPReceiver : MonoBehaviour
{
    private UdpClient udpClient;
    private Thread receiveThread;
    public int port = 4000;
    private bool isRunning = true;
    public double[] values = new double[8];
    public Quaternion receivedRotation = Quaternion.identity;
    public Vector3 receivedPosition = Vector3.zero;

    void Start()
    {
        try
        {
            udpClient = new UdpClient();
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, port));

            Debug.Log($"UDP��M���J�n���܂����B�|�[�g: {port}");

            receiveThread = new Thread(ReceiveData)
            {
                IsBackground = true
            };
            receiveThread.Start();
        }
        catch (SocketException ex)
        {
            Debug.LogError($"�\�P�b�g�̏������G���[: {ex.Message}");
        }
    }

    void ReceiveData()
    {
        Debug.Log("UDP��M���J�n...");

        while (isRunning)
        {
            try
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, port);
                byte[] receivedData = udpClient.Receive(ref remoteEndPoint);

                Debug.Log($"��M�f�[�^�T�C�Y: {receivedData.Length} �o�C�g");

                if (receivedData.Length >= 64)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        values[i] = BitConverter.ToDouble(receivedData, i * 8);
                    }

                    float w = (float)values[0];
                    float x = (float)values[1] / 1000f;
                    float y = (float)values[2] / 1000f;
                    float z = (float)values[3] ;
                    receivedRotation = Quaternion.Euler(-90f,0f, z);
                    receivedPosition = new Vector3(x, 1.322488f, y);
                    //Debug.Log($"��M�����N�H�[�^�j�I��: w={w}, x={x}, y={y}, z={z}");
                }
                else
                {
                    Debug.LogWarning("��M�f�[�^�����҂���T�C�Y�ɖ����Ȃ��B");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"UDP��M�G���[: {ex.Message}");
            }
        }
    }

    void OnApplicationQuit()
    {
        isRunning = false;
        if (receiveThread != null && receiveThread.IsAlive)
        {
            receiveThread.Join();
        }

        udpClient.Close();
        Debug.Log("UDP��M���I�����܂����B");
    }
}
