using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        [SerializeField] GimmickProvider _provider;
        [Header("生成の設定")]
        [SerializeField] Tumbleweed _prefab;
        [SerializeField] Transform _spawnPoint;
        [SerializeField] float _radius = 0.5f;
        [SerializeField] int _quantity = 10;

        CancellationTokenSource _cts = new();

        void OnEnable()
        {
            _provider.OnTumbleweedSpawned += Spawn;
        }

        void OnDisable()
        {
            _provider.OnTumbleweedSpawned -= Spawn;
        }

        void OnDestroy()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        /// <summary>
        /// 非道期でタンブルウィードを生成するギミック
        /// </summary>
        public void Spawn()
        {
            _cts = new();
            SpawnAsync(_cts.Token).Forget();
        }

        /// <summary>
        /// 基準地点からランダムな位置に生成する
        /// </summary>
        async UniTaskVoid SpawnAsync(CancellationToken token)
        {
            // TODO:ダンブルウィードを生成している
            for (int i = 0; i < _quantity; i++)
            {
                float x = Random.Range(-_radius, _radius);
                float z = Random.Range(-_radius, _radius);
                Vector3 spawnPosition = _spawnPoint.position + new Vector3(x, 0, z);
                Tumbleweed t = Instantiate(_prefab, spawnPosition, Quaternion.identity);

                await UniTask.Yield(token);
            }
        }
    }
}
