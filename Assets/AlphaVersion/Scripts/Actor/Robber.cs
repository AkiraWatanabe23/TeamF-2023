using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Alpha
{
    /// <summary>
    /// �����̃N���X
    /// </summary>
    public class Robber : Actor
    {
        [SerializeField] Collider _collider;
        [SerializeField] GameObject _decal;
        [Header("�X�e�[�g")]
        [SerializeField] MoveState _moveState;
        [SerializeField] AnimationState _animationState;
        [SerializeField] FireState _fireState;

        FootstepRecorder _recorder;
        PathConverter _pathConverter;
        BaseState _currentState;
        Waypoint _lead; // ����H
        Tension _tension;

        protected override void OnInitOverride(Waypoint lead, Tension tension)
        {
            _decal.SetActive(false);

            // �o�H�̐擪���猻�ݒn�܂ł̌o�H�����Z�b�g
            _recorder ??= new(transform);
            _recorder.Reset();
            // ���X�g�Ōo�H���擾�ł���悤�Ɍo�H�̐擪��n���Ă���
            _pathConverter = new(lead);
            // �ړ��X�e�[�g����J�n
            _currentState = _moveState;

            _lead = lead;
            _tension = tension;
        }

        protected override void OnStartOverride()
        {
        }

        protected async override UniTaskVoid UpdateAsync(CancellationToken token)
        {
            // �o�ꉹ
            Cri.PlaySE("SE_DoorStrong");

            // �A�C�e�����Ԃ������t���O
            bool isItemHit = false;
            _collider.OnCollisionEnterAsObservable().Where(_ => !isItemHit)
                .Where(c => c.collider.TryGetComponent(out ThrowedItem _)).Subscribe(_ => 
                {
                    isItemHit = true;
                    Cri.PlaySE3D(transform.position, "SE_Robber_Voice_2");
                    _decal.SetActive(true);
                });

            Waypoint pathEnd = _lead;
            IReadOnlyList<Vector3> path;
            
            // 3�̉��o�n�_
            for (int i = 0; i < 3; i++)
            {
                // ���o�n�_�܂ł̌o�H���擾
                pathEnd = _pathConverter.GetPathByType(WaypointType.Stage, out path, pathEnd);

                // ���o�n�_�܂ňړ�
                _moveState.Init(path);
                while (!isItemHit && StepMoveToPathEnd()) await UniTask.Yield(token);

                // ���o�n�_�ł̃A�N�V����
                _animationState.Init();
                while (!isItemHit && StepAnimation()) await UniTask.Yield(token);
            }

            // �ˌ��n�_�܂ł̌o�H���擾
            pathEnd = _pathConverter.GetPathByType(WaypointType.Fire, out path, pathEnd);

            // �ˌ��n�_�܂ňړ�
            _moveState.Init(path);
            while (!isItemHit && StepMoveToPathEnd()) await UniTask.Yield(token);

            // �ˌ�
            _fireState.Init();
            while (!isItemHit && StepFire()) await UniTask.Yield(token);

            // �o���܂ł̌o�H���擾
            path = _pathConverter.GetPathToExit(pathEnd);
            
            // �o���܂ňړ�
            _moveState.Init(path);
            while (!isItemHit && StepMoveToPathEnd()) await UniTask.Yield(token);

            // ���̎��_�ŃA�C�e���ɂԂ����Ă��Ȃ���Ώo�����B���Ă���
            if (!isItemHit)
            {
                // �폜
            }
            else
            {
                // �Ȃ񂩃A�N�V����
                _animationState.Init();
                while (StepAnimation()) await UniTask.Yield(token);

                // �A��
                path = _recorder.GetReversePathFromCurrentPosition();
                _moveState.Init(path, ignoreForward: true);
                while (StepMoveToPathEnd()) await UniTask.Yield(token);
            }
        }

        /// <summary>
        /// �o�H�̖����܂ňړ�����
        /// </summary>
        bool StepMoveToPathEnd()
        {
            // �ړ�����x�ɑ��Ղ�ێ�����
            _recorder.TryRecord();

            StepState(_moveState);
            return _moveState.IsRunning;
        }

        /// <summary>
        /// �A�j���[�V�������Đ�����
        /// </summary>
        bool StepAnimation()
        {
            StepState(_animationState);
            return _animationState.IsRunning;
        }

        /// <summary>
        /// �ˌ�����
        /// </summary>
        bool StepFire()
        {
            StepState(_fireState);
            return _fireState.IsRunning;
        }

        /// <summary>
        /// 1�t���[���������X�e�[�g��i�߂�
        /// </summary>
        void StepState(BaseState nextState) => _currentState = _currentState.Step(nextState);
    }
}