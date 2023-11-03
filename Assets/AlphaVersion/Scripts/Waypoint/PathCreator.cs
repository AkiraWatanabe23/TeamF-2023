using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    public enum PathType
    {
        Customer,
        Robber,
    }

    /// <summary>
    /// �L�����N�^�[�̒H��o�H���쐬����N���X
    /// </summary>
    public class PathCreator : MonoBehaviour
    {
        [System.Serializable]
        public class Data
        {
            public PathType Type;
            [Header("�o�H�̐擪")]
            public Waypoint Lead;
        }

        [SerializeField] Data[] _data;

        Dictionary<PathType, Waypoint> _dict = new();

        void Awake()
        {
            // TODO:�K�v�ɉ����Čo�H�̍쐬���������B
            // ���݂�Waypoint���Ɏ���Waypoint�̏���ێ����Ă���̂ŁA���ɏ������������̂܂�
            _data.ToDictionary(d => d.Type, d => d.Lead);
        }

        /// <summary>
        /// �o�H���擾����
        /// </summary>
        /// <returns>����:�o�H�̐擪�̃E�F�C�|�C���g �Ȃ�:null</returns>
        public Waypoint GetPath(PathType type)
        {
            if (_dict.TryGetValue(type, out Waypoint value))
            {
                return value;
            }
            else
            {
                Debug.LogError("�o�H�����݂��Ȃ�: " + type);
                return null;
            }
        }
    }
}
