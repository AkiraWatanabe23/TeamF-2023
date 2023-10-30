using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 投げるアイテム全てが共通して持つコンポーネント
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class ThrowedItem : MonoBehaviour
    {
        [Header("滑らせる際に必要な値の設定")]
        [Range(0, 1.0f)]
        [SerializeField] float _hardness;
        [Header("積む際に必要な値の設定")]
        [SerializeField] float _height = 0.25f;

        Vector3 _startingPoint;
        public bool IsThrowed { get; private set; }

        public float Height => _height;

        /// <summary>
        /// 水平に移動した距離の2乗を返す
        /// </summary>
        public float MovingSqrDistance
        {
            get
            {
                Vector3 current = new Vector3(transform.position.x, 0, transform.position.z);
                Vector3 start = new Vector3(_startingPoint.x, 0, _startingPoint.z);
                return (current - start).sqrMagnitude;
            }
        }

        void Awake()
        {
            FreezeXZ();
        }

        /// <summary>
        /// 崩れないようにXとZ方向への移動を制限する
        /// </summary>
        void FreezeXZ()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionX |
                             RigidbodyConstraints.FreezePositionZ;
        }

        /// <summary>
        /// 指定した方向/威力で投げる
        /// </summary>
        public void Throw(Vector3 velocity)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            rb.velocity = velocity;

            // 投げた際の位置を保持する
            _startingPoint = transform.position;
            // 既に投げられたアイテムであるフラグを立てる
            IsThrowed = true;
        }

        void OnCollisionEnter(Collision collision)
        {
            // 床判定
            if (collision.gameObject.TryGetComponent(out FloorMarker _))
            {
                Crush();
            }
        }

        /// <summary>
        /// 破裂させる
        /// </summary>
        void Crush()
        {
            // TODO:現在はアイテムの硬さに関わらず、ビンが割れる音を再生する
            Cri.PlaySE("SE_ItemCrash_short");

            // TODO:削除処理が必要
        }
    }
}