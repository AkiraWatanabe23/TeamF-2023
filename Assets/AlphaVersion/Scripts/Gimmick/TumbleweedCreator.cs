using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// タンブルウィードのプールを保持するクラス
    /// 複数の種類を1つのプール内に生成する
    /// </summary>
    public class TumbleweedCreator : MonoBehaviour
    {
        [SerializeField] Tumbleweed[] _prefabs;

        TumbleweedPool _pool;

        void Awake()
        {
            _pool = new("TumbleweedPool", _prefabs);
        }

        void OnDestroy()
        {
            _pool.Dispose();
        }

        /// <summary>
        /// プールから取り出して返す
        /// 落下の処理はプールから取り出した側で行うので、このクラスでは行わない
        /// </summary>
        public Tumbleweed Create()
        {
            return _pool.Rent();
        }
    }
}
