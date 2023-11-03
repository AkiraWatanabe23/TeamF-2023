using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 各ウェイポイントの接続先をギズモ上に表示する機能のコンポーネント
    /// </summary>
    public class ConnectVisualizer : MonoBehaviour
    {
        [SerializeField] Transform[] _next;
        [SerializeField] float _height = 0.25f;

        void OnDrawGizmos()
        {
            Vector3 a = transform.position;
            a.y = _height;

            Gizmos.color = Color.red;

            if (_next != null)
            {
                foreach (Transform t in _next)
                {
                    Vector3 b = t.position;
                    b.y = _height;

                    Gizmos.DrawLine(a, b);
                }
            }
        }
    }
}
