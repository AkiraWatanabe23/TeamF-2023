using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �o�H�̐擪���猻�݈ʒu�܂ł̌o�H���L�^����@�\�̃N���X
    /// �H�������W�ȊO���L�^����Ȃ��悤�A�L�����N�^�[���̂ւ̎Q�Ƃ�ێ�����
    /// </summary>
    public class FootstepRecorder
    {
        readonly float _space;

        List<Vector3> _path = new();
        Transform _target;

        public FootstepRecorder(Transform target, float space = 0.5f)
        {
            _space = space;
            _target = target;
        }

        /// <summary>
        /// �o�H�̃��Z�b�g
        /// </summary>
        public void Reset()
        {
            _path.Clear();
        }

        /// <summary>
        /// �ړ�����x�ɂ��̃��\�b�h���Ăяo��
        /// 1�O�ɒǉ��������W�Ƃ�����x����Ă����ꍇ�́A�o�H�Ɍ��݂̍��W��ǉ�����
        /// </summary>
        public void TryRecord()
        {
            Vector3 position = _target.position;

            // �o�H�ɍ��W�����݂��Ȃ��ꍇ�͂��̂܂ܒǉ�����
            if (_path.Count == 0) _path.Add(position);

            float dist = (_path[_path.Count - 1] - position).sqrMagnitude;
            if (dist >= _space)
            {
                _path.Add(position);
            }
        }

        /// <summary>
        /// ���ݒn���珉���l�ɖ߂�ۂɂ��̃��\�b�h���Ăяo��
        /// �H�����o�H�𔽓]�����邱�ƂŌ��݂̍��W����t���ɒH��o�H��Ԃ�
        /// </summary>
        public IReadOnlyList<Vector3> GetReversePathFromCurrentPosition()
        {
            List<Vector3> copy = new(_path);
            copy.Add(_target.position);
            copy.Reverse();

            // �o�H�̖��[�������ʒu(��ʊO)�Ȃ̂ō폜����
            copy.RemoveAt(copy.Count - 1);

            copy.ForEach(f => DebugDraw(f));

            return copy;
        }

        // �f�o�b�O�p
        void DebugDraw(Vector3 p)
        {
            GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            g.transform.localScale = Vector3.one * 0.5f;
            g.transform.position = p;
        }
    }
}
