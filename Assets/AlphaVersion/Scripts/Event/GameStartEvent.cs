using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    /// <summary>
    /// ゲーム開始時の演出イベントのクラス
    /// このイベント終了直後にゲーム開始
    /// </summary>
    public class GameStartEvent : MonoBehaviour
    {
        // TODO:後々タイムライン再生のイベントに変更

        [SerializeField] GameObject _ui;
        [SerializeField] float _playTime = 1;

        void Awake()
        {
            _ui.SetActive(false);
        }

        /// <summary>
        /// イベントの再生、イベント終了まで待つ
        /// </summary>
        public async UniTask PlayAsync(CancellationToken token)
        {
            _ui.SetActive(true);
            await UniTask.Delay(System.TimeSpan.FromSeconds(_playTime), cancellationToken: token);
            _ui.SetActive(false);
        }
    }
}
