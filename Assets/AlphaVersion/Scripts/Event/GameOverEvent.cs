using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    /// <summary>
    /// ゲームオーバーの演出イベントのクラス
    /// このイベント終了後にリトライとシーンの遷移が可能になる
    /// </summary>
    public class GameOverEvent : MonoBehaviour
    {
        [SerializeField] UIInvisibleAnimation _uiInvisible;
        [SerializeField] CloseDoorAnimation _door;
        [SerializeField] TempResultAnimation _result;

        /// <summary>
        /// イベントの再生
        /// アニメーション後、スコアの表示、特定のキー入力があるまで待つ。
        /// </summary>
        public async UniTask PlayAsync(object result, CancellationToken token)
        {
            await _uiInvisible.PlayAsync(token);
            await _door.PlayAsync(token);
            await _result.PlayAsync(result, token);
        }
    }
}
