using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// UFOの生成を行う機能のクラス
    /// </summary>
    public class UfoSpawner : MonoBehaviour
    {
        [SerializeField] UFOGimmick _prefab;
        [SerializeField] Transform _spawnPoint;
        [SerializeField] Transform _target;

        public void Spawn()
        {
            var v = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
            //v._moveTarget = _target;
        }
    }
}
