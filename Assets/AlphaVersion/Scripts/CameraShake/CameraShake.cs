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
    /// カメラを振動させるためのメッセージを送信するクラス
    /// </summary>
    public static class CameraShakeMessageSender
    {
        public static void SendMessage(float power = default)
        {
            MessageBroker.Default.Publish(new CameraShakeMessage() { Power = power });
        }
    }

    /// <summary>
    /// メッセージを受信し、カメラの振動を行う機能のクラス
    /// </summary>
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] CinemachineImpulseSource _source;
        [SerializeField] float _power = 1;

        void Awake()
        {
            // メッセージを受信したら揺らす
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