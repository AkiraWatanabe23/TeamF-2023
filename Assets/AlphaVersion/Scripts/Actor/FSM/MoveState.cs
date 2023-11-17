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

        Transform _transform;
        IReadOnlyList<Vector3> _path;
        float _lerpProgress;
        float _distance; // �ړ�����ۂ�2�_�Ԃ�sqrt�ł͂Ȃ��������K�v
        int _currentIndex;
        bool _ignoreForward; // �O���̏�Q���𖳎����邩�̃t���O

        int NextIndex => Mathf.Min(_currentIndex + 1, _path.Count - 1);
        Vector3 From => new Vector3(_path[_currentIndex].x, 0, _path[_currentIndex].z);
        Vector3 To => new Vector3(_path[NextIndex].x, 0, _path[NextIndex].z);

        public bool IsRunning => _currentIndex < _path.Count - 1;
        public override StateType Type => StateType.Move;

        public void Init(IReadOnlyList<Vector3> path, bool ignoreForward = false)
        {
            _transform = transform;
            _path = path;
            _lerpProgress = 0;
            _distance = 0;
            _currentIndex = 0;
            _ignoreForward = ignoreForward;
        }

        protected override void OnFerverTimeEnter()
        {
            DanceIfStayStage();
        }

        protected override void Enter()
        {
            LookAt();
            Distance();

            // ���Ƀt�B�[�o�[�Ȃ�A���̃X�e�[�g�Ńg���K�[�o���Ȃ��̂ł����Ń`�F�b�N����
            if (!DanceIfFerverTime()) Animator.Play("Walk");
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
                Distance();
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
            _lerpProgress += _settings.MoveSpeed * Time.deltaTime / _distance;
            _transform.position = Vector3.Lerp(From, To, _lerpProgress);
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

        /// <summary>
        /// �������v�Z����
        /// </summary>
        void Distance()
        {
            _distance = (To - From).magnitude;
        }
    }
}