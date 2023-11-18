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

        Transform _transform;
        IReadOnlyList<Vector3> _path;
        float _lerpProgress;
        float _distance; // 移動する際に2点間のsqrtではない距離が必要
        int _currentIndex;
        bool _ignoreForward; // 前方の障害物を無視するかのフラグ

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

            // 既にフィーバーなら、このステートでトリガー出来ないのでここでチェックする
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
            _lerpProgress += _settings.MoveSpeed * Time.deltaTime / _distance;
            _transform.position = Vector3.Lerp(From, To, _lerpProgress);
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

        /// <summary>
        /// 距離を計算する
        /// </summary>
        void Distance()
        {
            _distance = (To - From).magnitude;
        }
    }
}