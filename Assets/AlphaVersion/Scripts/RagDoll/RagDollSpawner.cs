using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

namespace Alpha
{
    /// <summary>
    /// ラグドールを生成する際に元となるモデルとその種類、ぶつかった方向を送受信する
    /// </summary>
    public struct RagDollMessage
    {
        public ActorType Type;
        public Transform Model;
        public Vector3 HitPosition;
    }

    /// <summary>
    /// ラグドールの生成を行うクラス
    /// </summary>
    public class RagDollSpawner : MonoBehaviour
    {
        static Transform _parent;

        [SerializeField] GameObject _malePrefab;
        [SerializeField] GameObject _femalePrefab;
        [SerializeField] int _max = 5;
        [SerializeField] float _lifeTime = 5.0f;
        [Header("ラグドールが吹っ飛ぶ際に加わる力")]
        [SerializeField] float _power = 10.0f;
        [SerializeField] float _upPower = 10.0f;

        Dictionary<ActorType, GameObject> _table = new();
        Queue<GameObject> _ragDolls = new();

        void Awake()
        {
            if (_parent == null) _parent = new GameObject("RagDollParent").transform;
            
            CreateTable();
            MessageReceive();
        }

        void OnDestroy()
        {
            if (_parent != null) Destroy(_parent);
        }

        /// <summary>
        /// 男と女のラグドールを辞書に追加
        /// </summary>
        void CreateTable()
        {
            _table.Add(ActorType.Male, _malePrefab);
            _table.Add(ActorType.Female, _femalePrefab);
        }

        /// <summary>
        /// メッセージを受信した場合に生成する
        /// </summary>
        void MessageReceive()
        {
            MessageBroker.Default.Receive<RagDollMessage>()
                .Subscribe(msg => TrySpawn(msg.Type, msg.Model, msg.HitPosition)).AddTo(gameObject);
        }

        void TrySpawn(ActorType type, Transform model, Vector3 hitPosition)
        {
            // 最大数に達していたら一番古いものを削除する
            if (_max <= _ragDolls.Count) Destroy(_ragDolls.Dequeue());

            if (_table.TryGetValue(type, out GameObject prefab))
            {
                // キューに追加して生成順に取得できるようにしておく
                GameObject ragDoll = Instantiate(prefab, model.position, model.rotation, _parent);
                _ragDolls.Enqueue(ragDoll);

                // 上方向とxz平面上の飛ばす方向に対して力を加える
                Vector3 dir = (model.position - hitPosition).normalized;
                //Rigidbody[] children = ragDoll.GetComponentsInChildren<Rigidbody>();
                //foreach (Rigidbody rb in children)
                //{
                //    rb.AddForce(Vector3.up * _upPower + (dir * _power), ForceMode.Impulse);
                //}
                Rigidbody rb = ragDoll.GetComponentInChildren<Rigidbody>();
                rb.AddForce(Vector3.up * _upPower + (dir * _power), ForceMode.Impulse);
                
                // 一定時間後に大きさを0にして画面外に追い出すことで映らなくする
                DOVirtual.DelayedCall(_lifeTime, () => 
                {
                    ragDoll.transform.localScale = Vector3.zero;
                    ragDoll.transform.position = Vector3.one * 1000.0f;
                }).SetLink(ragDoll);
            }
        }
    }
}
