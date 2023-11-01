using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Alpha
{
    /// <summary>
    /// �q���A�C�e�����L���b�`����G���A�̃T�C�Y�ƈʒu��ݒ肷��N���X
    /// �t�B�[�o�[�^�C���̊J�n�ƏI������M���A�T�C�Y��ύX����
    /// </summary>
    public class CatchTransform : FerverHandler
    {
        enum Size
        {
            Normal,
            Fever,
        }

        [Header("�ݒu����ʒu�̐ݒ�")]
        [SerializeField] float _height = 0.5f;
        [SerializeField] float _border = 1.0f;

        Vector3 _basePosition;
        Vector3 _prevPosition;

        Size _size = Size.Normal;

        protected override void OnAwakeOverride()
        {
            // ��ʒu�̐ݒ�
            _basePosition = transform.position;
        }

        protected override void OnFerverTimeEnter()
        {
            //_prevPosition = transform.position;
            SetScale(2);
        }

        protected override void OnFerverTimeExit()
        {
            //transform.position = _prevPosition;
            SetScale(1);
        }

        /// <summary>
        /// �w�肵���傫���ɃZ�b�g����
        /// </summary>
        void SetScale(float size)
        {
            transform.localScale = Vector3.one * size;
        }

        /// <summary>
        /// �����_���Ȉʒu�ɃZ�b�g����
        /// </summary>
        public void SetRandomPosition(float size)
        {
            // �͈͂���͂ݏo���Ȃ��悤�ɔ��a�̕������ʒu�𐧌�����
            float x = Random.Range(-_border + size / 2, _border - size / 2);
            Vector3 pos = new Vector3(x, _height, _basePosition.z);

            transform.position = pos;
        }

        void OnDrawGizmos()
        {
            DrawRange();
        }

        /// <summary>
        /// �ݒu�ł���͈͂��M�Y���ɕ`��
        /// </summary>
        void DrawRange()
        {
            // �G�f�B�^�[��ł͌��ݒn����ɂ���
            Vector3 pos = !Application.isPlaying ? transform.position : _basePosition;

            Vector3 left = pos + Vector3.left * _border;
            Vector3 right = pos + Vector3.right * _border;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(left, right);
        }
    }
}
