using UnityEngine;

public class CubeMover : MonoBehaviour
{
    public UDPReceiver udpReceiver;
    public Transform cube;

    void Update()
    {
        if (udpReceiver != null && cube != null)
        {
            // �N�H�[�^�j�I�����X�V����Ă��邩�m�F
            //Debug.Log($"��M�����N�H�[�^�j�I��: {udpReceiver.receivedRotation}");

            // UDP��M�X�N���v�g����N�H�[�^�j�I�����擾���ăL���[�u�̉�]���X�V
            cube.localRotation = udpReceiver.receivedRotation  ;
            cube.localPosition = udpReceiver.receivedPosition ;
        }
    }

}
