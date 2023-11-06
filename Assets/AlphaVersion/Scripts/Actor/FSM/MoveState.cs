using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 渡された経路の先頭から末尾までを移動するステート
    /// </summary>
    public class MoveState : BaseState
    {
        [SerializeField] ActorSettingsSO _settings;
        [SerializeField] MoveRaycaster _raycaster;

        IReadOnlyList<Vector3> _path;
        float _lerpProgress;
        int _currentIndex;
        bool _ignoreForward; // 前方の障害物を無視するかのフラグ

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
        /// 進行方向に障害物があるかどうかを判定
        /// </summary>
        bool IsClearForward()
        {
            return _raycaster.IsClearForward(_path[NextIndex]);
        }

        /// <summary>
        /// 次の頂点に向ける
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
        /// 次の頂点に向けて移動
        /// </summary>
        void Move()
        {
            _lerpProgress += Time.deltaTime * _settings.MoveSpeed;
            transform.position = Vector3.Lerp(_path[_currentIndex], _path[NextIndex], _lerpProgress);
        }

        /// <summary>
        /// 向かう頂点を更新する
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
