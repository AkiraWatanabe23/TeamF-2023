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
    }

    /// <summary>
    /// �e�E�F�C�|�C���g�{�̂̃N���X
    /// </summary>
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] WaypointType _type;
        [SerializeField] Waypoint[] _next;

        public IReadOnlyCollection<Waypoint> Next => _next;

        void Start()
        {

        }

        void Update()
        {

        }
    }
}

// �擪���玟�̃E�F�C�|�C���g�����Ă����B
// �����̓v���C���[�ɍU�����ꂽ�ۂɓ����A�܂�o�H��߂��Ă����K�v������B
// 