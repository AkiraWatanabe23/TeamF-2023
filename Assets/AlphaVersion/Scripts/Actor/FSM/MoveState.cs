using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �n���ꂽ�o�H�̐擪���疖���܂ł��ړ�����X�e�[�g
    /// </summary>
    public class MoveState : BaseState
    {
        [SerializeField] ActorSettingsSO _settings;
        [SerializeField] MoveRaycaster _raycaster;

        IReadOnlyList<Vector3> _path;
        float _lerpProgress;
        int _currentIndex;
        bool _ignoreForward; // �O���̏�Q���𖳎����邩�̃t���O

        int NextIndex => Mathf.Min(_currentIndex + 1, _path.Count - 1);
        public bool IsRunning => _currentIndex < _path.Count - 1;
        public override StateType Type => StateType.Move;

        public void Init(IReadOnlyList<Vector3> path, bool ignoreForward = false)
        {
            _path = path;
            _lerpProgress = 0;
            _currentIndex = 0;
            _ignoreForward = ignoreForward;
        }

        protected override void Enter()
        {
            LookAt();
            Animator.Play("Walk");
        }

        protected override void Exit()
        {        
        }

        protected override void Stay()
        {
            if (_ignoreForward) Move();
            else if (IsClearForward()) Move();

            if (_lerpProgress >= 1.0f && IsRunning)
            {
                TryStepVertex();
                LookAt();
            }
        }

        /// <summary>
        /// �i�s�����ɏ�Q�������邩�ǂ����𔻒�
        /// </summary>
        bool IsClearForward()
        {
            return _raycaster.IsClearForward(_path[NextIndex]);
        }

        /// <summary>
        /// ���̒��_�Ɍ�����
        /// </summary>
        void LookAt()
        {
            Vector3 dir = _path[NextIndex] - _path[_currentIndex];
            if (dir != Vector3.zero)
            {
                Model.rotation = Quaternion.LookRotation(dir, Vector3.up);
            }
        }

        /// <summary>
        /// ���̒��_�Ɍ����Ĉړ�
        /// </summary>
        void Move()
        {
            _lerpProgress += Time.deltaTime * _settings.MoveSpeed;
            transform.position = Vector3.Lerp(_path[_currentIndex], _path[NextIndex], _lerpProgress);
        }

        /// <summary>
        /// ���������_���X�V����
        /// </summary>
        bool TryStepVertex()
        {
            _lerpProgress = 0;

            if (NextIndex < _path.Count)
            {
                _currentIndex++;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
