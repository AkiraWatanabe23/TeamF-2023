using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �^���u���E�B�[�h�̃v�[����ێ�����N���X
    /// �����̎�ނ�1�̃v�[�����ɐ�������
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
        /// �v�[��������o���ĕԂ�
        /// �����̏����̓v�[��������o�������ōs���̂ŁA���̃N���X�ł͍s��Ȃ�
        /// </summary>
        public Tumbleweed Create()
        {
            return _pool.Rent();
        }
    }
}
