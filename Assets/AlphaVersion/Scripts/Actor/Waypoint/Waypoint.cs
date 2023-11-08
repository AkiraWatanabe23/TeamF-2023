using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    public enum WaypointType
    {
        Way,    // ���̂܂܎��̃E�F�C�|�C���g�Ɍ�����
        Table,  // �q������֎q
        Stage,  // �L�����N�^�[�����o���s���n�_
        Fire,   // �D�_�̎ˌ��n�_
    }

    /// <summary>
    /// �e�E�F�C�|�C���g�{�̂̃N���X
    /// </summary>
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] WaypointType _type;
        [SerializeField] Waypoint[] _next;
        [SerializeField] Waypoint[] _prev;

        Transform _transform = null;

        void Awake()
        {
            _transform = transform;
        }

        public WaypointType Type => _type;
        public IReadOnlyList<Waypoint> Next => _next;
        public IReadOnlyList<Waypoint> Prev => _prev;
        public Vector3 Position => _transform.position;
        public bool IsLead => _prev == null || _prev.Length == 0;
        public bool IsFinal => _next == null || _next.Length == 0;
    }
}