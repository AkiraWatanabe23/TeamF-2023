using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 右クリックで雑巾を射出する機能のクラス
    /// </summary>
    public class DustClothShooter : MonoBehaviour
    {
        [SerializeField] DustCloth _prefab;
        [Header("射出する際の設定")]
        [SerializeField] Vector3 _offset;
        [SerializeField] float _shootPower;

        /// <summary>
        /// 雑巾を撃ち出す
        /// </summary>
        public void Shoot()
        {
            // TODO:雑巾を射出する度に雑巾を生成している
            DustCloth dustCloth = Instantiate(_prefab);
            dustCloth.transform.position = transform.position + _offset;
            dustCloth.Shoot(Vector3.forward * _shootPower);
        }
    }
}