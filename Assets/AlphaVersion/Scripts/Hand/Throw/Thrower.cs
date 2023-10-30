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
        [Header("アイテムを積む位置のオフセット")]
        [SerializeField] Vector3 _offset;
        [Header("積む位置のランダムなずらし幅")]
        [SerializeField] float _randomShift = 0.1f;
        [Header("積める最大数")]
        [SerializeField] int _maxStack = 6;
        [Header("最低威力")]
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
        /// 最大数に達していない場合は、アイテムを積んでいく
        /// </summary>
        /// <returns>積んだ:true 最大数に達している:false</returns>
        public bool TryStack(ThrowedItem item)
        {
            // 最大数積んでいる場合は弾く
            if (_tower.Count >= _maxStack) return false;

            // 音を再生
            Cri.PlaySE("SE_ItemSet");

            // 生成位置を基準の位置からランダムにずらす
            Vector3 shift = new Vector3(Random.Range(0, _randomShift), 0, Random.Range(0, _randomShift));

            // 一番上に積んで、次に積む際の高さを更新する
            Vector3 stackPoint = transform.position + _offset + shift;
            stackPoint.y += _stackHeight;
            _stackHeight += item.Height;

            item.transform.position = stackPoint;
            item.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0); // 回転
            _tower.Enqueue(item);

            return true;
        }

        /// <summary>
        /// 積み上げたアイテムを投げる
        /// </summary>
        public void Throw(Vector3 velocity)
        {
            // 1つ以上積んでいる場合は音を再生
            if (StackCount > 0) Cri.PlaySE("SE_Slide");

            // 最低限飛ぶ距離を設定
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
