using UnityEngine;

public class CubeMover : MonoBehaviour
{
    public UDPReceiver udpReceiver;
    public Transform cube;

    void Update()
    {
        if (udpReceiver != null && cube != null)
        {
            // クォータニオンが更新されているか確認
            //Debug.Log($"受信したクォータニオン: {udpReceiver.receivedRotation}");

            // UDP受信スクリプトからクォータニオンを取得してキューブの回転を更新
            cube.localRotation = udpReceiver.receivedRotation  ;
            cube.localPosition = udpReceiver.receivedPosition ;
        }
    }

}
