using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 右クリックで射出される雑巾のクラス
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class DustCloth : MonoBehaviour
    {
        /// <summary>
        /// 指定した方向へ力を加えて射出する
        /// </summary>
        public void Shoot(Vector3 velocity)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(velocity, ForceMode.Impulse);
        }
    }
}
