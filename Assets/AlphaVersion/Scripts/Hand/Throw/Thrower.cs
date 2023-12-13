using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Alpha
{
    /// <summary>
    /// ������A�C�e���� �ς�/������ ���s���N���X
    /// </summary>
    public class Thrower : MonoBehaviour
    {
        [SerializeField] HandSettingsSO _settings;
        [SerializeField] ThrowEffector _effector;
        [Header("�A�C�e����ςވʒu�̃I�t�Z�b�g")]
        [SerializeField] Vector3 _offset;

        Queue<ThrowedItem> _tower = new();
        float _stackHeight;

        /// <summary>
        /// �A�C�e����ςވʒu
        /// ���̃I�u�W�F�N�g�̍��W�ɃI�t�Z�b�g�𑫂����ʒu
        /// </summary>
        public Vector3 StackPoint => transform.position + _offset;      
        /// <summary>
        /// ���ݐς�ł��鐔
        /// </summary>
        public int StackCount => _tower.Count;

        /// <summary>
        /// �ő吔�ɒB���Ă��Ȃ��ꍇ�́A�A�C�e����ς�ł���
        /// </summary>
        /// <returns>�ς�:true �ő吔�ɒB���Ă���:false</returns>
        public bool TryStack(ThrowedItem item)
        {
            // �ő吔�ς�ł���ꍇ�͒e��
            if (_tower.Count >= _settings.MaxStack) return false;

            // �����ʒu����̈ʒu���烉���_���ɂ��炷
            Vector3 shift = new Vector3(Random.Range(0, _settings.RandomShift), 0, Random.Range(0, _settings.RandomShift));

            // ��ԏ�ɐς�ŁA���ɐςލۂ̍������X�V����
            Vector3 stackPoint = transform.position + _offset + shift;
            stackPoint.y += _stackHeight;
            _stackHeight += item.Height;

            item.transform.position = stackPoint;
            item.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0); // ��]
            _tower.Enqueue(item);

            // �A�C�e���̈ʒu�ɉ��ƃp�[�e�B�N�����Đ�
            _effector.PlayStackEffect(item.transform);

            return true;
        }

        /// <summary>
        /// �ςݏグ���A�C�e���𓊂���
        /// </summary>
        public void Throw(Vector3 velocity)
        {
            // 1�ȏ�ς�ł���ꍇ�́A�ŉ��i�̃A�C�e���̈ʒu�ɉ��ƃp�[�e�B�N�����Đ�
            if (StackCount > 0)
            {
                _effector.PlayThrowEffect(_tower.Peek().transform, transform);
                StartCoroutine(DelayPlay());
            }

            // �Œ����ԋ�����ݒ�
            Vector3 minVelocity = velocity.normalized * _settings.MinPower;
            foreach (ThrowedItem item in _tower)
            {
                item.Throw(velocity + minVelocity);
            }

            // 1����ςނ��߂Ɋe�l�����Z�b�g
            _stackHeight = 0;
            _tower.Clear();
        }

        void OnDrawGizmos()
        {
            DrawStackPoint();
        }

        /// <summary>
        /// �A�C�e����ςވʒu���M�Y���ɕ`�悷��
        /// </summary>
        void DrawStackPoint()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(StackPoint, 0.05f);
        }

        IEnumerator DelayPlay()
        {
            yield return new WaitForSeconds(0.1f);
            Cri.PlaySE("SE_Swoosh");
        }
    }
}
