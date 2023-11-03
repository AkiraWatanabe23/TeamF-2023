using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �e�E�F�C�|�C���g�̐ڑ�����M�Y����ɕ\������@�\�̃R���|�[�l���g
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
