using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 投げるアイテムを 積む/投げる を行うクラス
    /// </summary>
    public class Thrower : MonoBehaviour
    {
        [SerializeField] ThrowedItem _prefab;
        [Header("アイテムを積む際位置のオフセット")]
        [SerializeField] Vector3 _offset;
        [Header("任意:最低威力")]
        [SerializeField] float _minPower = 0;

        Queue<ThrowedItem> _tower = new();
        float _stackHeight;

        /// <summary>
        /// アイテムを積む位置
        /// このオブジェクトの座標にオフセットを足した位置
        /// </summary>
        public Vector3 StackPoint => transform.position + _offset;      
        /// <summary>
        /// 現在積んでいる数
        /// </summary>
        public int StackCount => _tower.Count;

        /// <summary>
        /// アイテムを積んでいく
        /// </summary>
        public void Stack(ThrowedItem item)
        {
            // 一番上に積んで、次に積む際の高さを更新する
            Vector3 spawnPoint = transform.position + _offset;
            spawnPoint.y += _stackHeight;
            _stackHeight += item.Height;

            item.transform.position = spawnPoint;
            _tower.Enqueue(item);
        }

        /// <summary>
        /// 積み上げたアイテムを投げる
        /// </summary>
        public void Throw(Vector3 velocity)
        {
            // 任意:最低威力を足すことで指定した距離は必ず飛ぶようになる
            Vector3 minVelocity = velocity.normalized * _minPower;

            foreach (ThrowedItem item in _tower)
            {
                item.Throw(velocity + minVelocity);
            }

            // 1から積むために各値をリセット
            _stackHeight = 0;
            _tower.Clear();
        }

        void OnDrawGizmos()
        {
            DrawStackPoint();
        }

        /// <summary>
        /// アイテムを積む位置をギズモに描画する
        /// </summary>
        void DrawStackPoint()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(StackPoint, 0.05f);
        }
    }
}
