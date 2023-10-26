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
                Vector3 current = transform.position;
                current.y = 0;
                Vector3 start = _startingPoint;
                start.y = 0;
                return (current - start).sqrMagnitude;
            }
        }

        /// <summary>
        /// 指定した方向/威力で投げる
        /// </summary>
        public void Throw(Vector3 velocity)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = velocity;

            // 投げた際の位置を保持する
            _startingPoint = transform.position;
            // 既に投げられたアイテムであるフラグを立てる
            IsThrowed = true;
        }
    }
}
