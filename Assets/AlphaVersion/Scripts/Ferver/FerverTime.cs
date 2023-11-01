using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// フィーバータイムの開始/終了の切り替え時のメッセージングに使用する構造体
    /// </summary>
    public struct FerverTimeMessage { }

    /// <summary>
    /// フィーバータイムの開始/終了を制御するクラス
    /// </summary>
    public class FerverTime : MonoBehaviour
    {
        /// <summary>
        /// フィーバータイム開始のコールバック
        /// </summary>
        public static event UnityAction OnEnter;
        /// <summary>
        /// フィーバータイム終了のコールバック
        /// </summary>
        public static event UnityAction OnExit;

        static FerverTime _instance;

        [Header("デバッグ用:Fキーでオンオフ切り替え")]
        [SerializeField] bool _isDebug = true;

        bool _isFerver;

        public static bool IsFerver => _instance._isFerver;
        public static bool IsNormal => !_instance._isFerver;

        void Awake()
        {
            _instance ??= this;
        }

        void OnDestroy()
        {
            _instance = null;
            _isFerver = false;
        }

        void Update()
        {
            // デバッグ用にキー入力で切り替えられる
            if (_isDebug && Input.GetKeyDown(KeyCode.F))
            {
                _isFerver = !_isFerver;

                // コールバック呼び出し
                if (_isFerver) OnEnter?.Invoke();
                else OnExit?.Invoke();
                
                // メッセージング
                MessageBroker.Default.Publish(new FerverTimeMessage());
            }
        }
    }
}
