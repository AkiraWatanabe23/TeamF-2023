using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// キャラクターの生成が可能かをチェックする機能のクラス
    /// チェックしたい箇所の個数に応じて設置する
    /// </summary>
    public class SpawnRangeChecker : MonoBehaviour
    {
        [SerializeField] Transform _spawnPoint;
        [SerializeField] LayerMask _actorLayer;
        [SerializeField] float _radius;

        /// <summary>
        /// 範囲内にキャラクターがいるかどうかを判定する
        /// </summary>
        public bool Check()
        {
            // TODO:NonAllocに変更する
            Collider[] result = Physics.OverlapSphere(_spawnPoint.position, _radius, _actorLayer);
            foreach (Collider collider in result)
            {
                if (collider.transform.parent.TryGetComponent(out Actor _))
                {
                    return false;
                }
            }

            return true;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(_spawnPoint.position, _radius);
        }
    }
}