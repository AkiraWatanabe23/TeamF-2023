using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Alpha
{
    /// <summary>
    /// ゲーム開始のタイミングで有効化、
    /// ゲームオーバーのタイミングで無効化される機能を提供するクラス。
    /// このクラスを継承する場合は Awake OnEnable OnDisable Update は使用しないこと。
    /// </summary>
    public class ValidStateHandler : MonoBehaviour
    {
        bool _isValid = false; // ここ修正

        protected bool IsValid => _isValid;

        void Awake()
        {
            // メッセージの受信
            MessageBroker.Default.Receive<GameStartMessage>().Subscribe(_ => _isValid = true).AddTo(gameObject);
            MessageBroker.Default.Receive<GameOverMessage>().Subscribe(_ => _isValid = false).AddTo(gameObject);

            OnAwakeOverride();
        }

        void OnEnable()
        {
            OnEnableOverride();
        }

        void OnDisable()
        {
            OnDisableOverride();
        }

        void Update()
        {
            if (_isValid)
            {
                OnUpdateOverride();
            }
        }

        protected virtual void OnAwakeOverride() { }
        protected virtual void OnEnableOverride() { }
        protected virtual void OnDisableOverride() { }
        protected virtual void OnUpdateOverride() { }
    }
}
