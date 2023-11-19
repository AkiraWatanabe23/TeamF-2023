using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    /// <summary>
    /// タンブルウィードを生成するクラス
    /// 基準の位置から一定範囲内にランダムに生成する
    /// </summary>
    public class TumbleweedSpawner : MonoBehaviour
    {
        /// <summary>
        /// タンブルウィードのギミック発動時に呼び出されるコールバック
        /// 既にギミック中でキャンセルされた場合は呼び出されない。
        /// </summary>
        public static event UnityAction OnSpawned;

        [SerializeField] TumbleweedCreator _creator;
        [Header("生成の設定")]
        [SerializeField] Transform _spawnPoint;
        [SerializeField] float _radius = 0.5f;
        [Header("瓦礫パラパラ時間(秒)")]
        [SerializeField] float _prevDelay = 2.0f;
        [Header("1つ落下する毎のディレイ(秒)")]
        [SerializeField] float _stepDelay;

        CancellationTokenSource _cts = new();
        bool _isRunning; // ギミック中に再度呼び出さないようにフラグ

        void OnDestroy()
        {
            OnSpawned = null;

            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }

        /// <summary>
        /// 非道期でタンブルウィードを生成するギミック
        /// </summary>
        public void Spawn(int count)
        {
            // 既にギミック実行中なので弾く
            if (_isRunning)
            {
                Debug.LogWarning("既にダンブルウィードのギミック中なのでキャンセルされた: " + Time.time);
                return;
            }

            OnSpawned?.Invoke();

            _cts = new();
            SpawnAsync(count, _cts.Token).Forget();
        }

        /// <summary>
        /// 基準地点からランダムな位置に生成する
        /// </summary>
        async UniTaskVoid SpawnAsync(int count, CancellationToken token)
        {
            _isRunning = true;

            // パラパラSE再生後、崩れるSE再生
            Cri.PlaySE("SE_Pre_Tumbleweed_2");
            await UniTask.Delay(System.TimeSpan.FromSeconds(_prevDelay), cancellationToken: token);
            Cri.PlaySE("SE_Fall_Tumbleweed_2");

            for (int i = 0; i < count; i++)
            {
                RentTumbleweed();
                CameraShakeMessageSender.SendMessage();

                await UniTask.Delay(System.TimeSpan.FromSeconds(_stepDelay), cancellationToken: token);
            }

            _isRunning = false;
        }

        /// <summary>
        /// タンブルウィードをプールから取り出して落下させる
        /// </summary>
        void RentTumbleweed()
        {
            // ランダムな位置
            float x = Random.Range(-_radius, _radius);
            float z = Random.Range(-_radius, _radius);
            Vector3 spawnPosition = _spawnPoint.position + new Vector3(x, 0, z);

            // プールから取り出して落下
            Tumbleweed t = _creator.Create();
            t.transform.position = spawnPosition;
            t.Fall();
        }
    }
}