using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UniRx;

namespace Alpha
{
    public struct CameraShakeMessage
    {
        public float Power;
    }

    /// <summary>
    /// �J������U�������邽�߂̃��b�Z�[�W�𑗐M����N���X
    /// </summary>
    public static class CameraShakeMessageSender
    {
        public static void SendMessage(float power = default)
        {
            MessageBroker.Default.Publish(new CameraShakeMessage() { Power = power });
        }
    }

    /// <summary>
    /// ���b�Z�[�W����M���A�J�����̐U�����s���@�\�̃N���X
    /// </summary>
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] CinemachineImpulseSource _source;
        [SerializeField] float _power = 1;

        void Awake()
        {
            // ���b�Z�[�W����M������h�炷
            MessageBroker.Default.Receive<CameraShakeMessage>().Subscribe(msg => 
            {
                Shake(msg.Power == default ? _power : msg.Power);
            }).AddTo(gameObject);
        }

        void Shake(float power)
        {
            _source.GenerateImpulseAt(Vector3.zero, new Vector3(1, 1, 0) * Time.deltaTime * power);
        }
    }
}