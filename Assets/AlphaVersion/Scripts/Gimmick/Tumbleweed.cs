using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    /// <summary>
    /// �^���u���E�B�[�h�{�̂̃N���X
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Tumbleweed : MonoBehaviour
    {
        [SerializeField] Rigidbody _rigidbody;
        [Header("���̍���")]
        [SerializeField] float _floorHeight = 0.2f;
        [Header("���ɗ�����A������܂ł̃f�B���C")]
        [SerializeField] float _floorFalledDelay = 1.0f;

        CancellationTokenSource _cts = new();
        TumbleweedPool _pool;

        void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }

        /// <summary>
        /// �������ăv�[���ɒǉ������ۂ�1�x�����v�[��������Ăяo����郁�\�b�h
        /// </summary>
        public void OnCreate(TumbleweedPool pool)
        {
            _pool = pool;
        }

        /// <summary>
        /// �O������v�[��������o�����ۂɗ��������邽�߂̃��\�b�h
        /// </summary>
        public void Fall()
        {
            _cts = new();
            PlayAsync(_cts.Token).Forget();
        }

        /// <summary>
        /// ���̍����ɓ��B��A���΂炭������v�[���ɖ߂�
        /// </summary>
        async UniTaskVoid PlayAsync(CancellationToken token)
        {
            ValidPhisics();

            // ���ɗ�����܂�
            await UniTask.WaitUntil(() => transform.position.y <= _floorHeight, cancellationToken: token);
            // ���΂炭�����������
            await UniTask.Delay(System.TimeSpan.FromSeconds(_floorFalledDelay), cancellationToken: token);

            InvalidPhisics();
            _pool.Return(this);
        }

        /// <summary>
        /// ����������^�C�~���O�ŕ���������L����
        /// </summary>
        void ValidPhisics()
        {
            _rigidbody.isKinematic = false;
        }

        /// <summary>
        /// �v�[���ɖ߂��^�C�~���O�ŕ��������𖳌���
        /// </summary>
        void InvalidPhisics()
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.isKinematic = true;
        }
    }
}
