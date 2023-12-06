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
    /// 強盗のクラス
    /// </summary>
    public class Robber : Actor
    {
        [SerializeField] Collider _collider;
        [SerializeField] GameObject _decal;
        [Header("ステート")]
        [SerializeField] MoveState _moveState;
        [SerializeField] AnimationState _animationState;
        [SerializeField] FireState _fireState;

        FootstepRecorder _recorder;
        PathConverter _pathConverter;
        BaseState _currentState;
        Waypoint _lead; // いる？
        Tension _tension;

        protected override void OnInitOverride(Waypoint lead, Tension tension)
        {
            _decal.SetActive(false);

            // 経路の先頭から現在地までの経路をリセット
            _recorder ??= new(transform);
            _recorder.Reset();
            // リストで経路を取得できるように経路の先頭を渡しておく
            _pathConverter = new(lead);
            // 移動ステートから開始
            _currentState = _moveState;

            _lead = lead;
            _tension = tension;
        }

        protected override void OnStartOverride()
        {
        }

        protected async override UniTaskVoid UpdateAsync(CancellationToken token)
        {
            // 登場音
            Cri.PlaySE("SE_DoorStrong");

            // アイテムがぶつかったフラグ
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
            
            // 3つの演出地点
            for (int i = 0; i < 3; i++)
            {
                // 演出地点までの経路を取得
                pathEnd = _pathConverter.GetPathByType(WaypointType.Stage, out path, pathEnd);

                // 演出地点まで移動
                _moveState.Init(path);
                while (!isItemHit && StepMoveToPathEnd()) await UniTask.Yield(token);

                // 演出地点でのアクション
                _animationState.Init();
                while (!isItemHit && StepAnimation()) await UniTask.Yield(token);
            }

            // 射撃地点までの経路を取得
            pathEnd = _pathConverter.GetPathByType(WaypointType.Fire, out path, pathEnd);

            // 射撃地点まで移動
            _moveState.Init(path);
            while (!isItemHit && StepMoveToPathEnd()) await UniTask.Yield(token);

            // 射撃
            _fireState.Init();
            while (!isItemHit && StepFire()) await UniTask.Yield(token);

            // 出口までの経路を取得
            path = _pathConverter.GetPathToExit(pathEnd);
            
            // 出口まで移動
            _moveState.Init(path);
            while (!isItemHit && StepMoveToPathEnd()) await UniTask.Yield(token);

            // この時点でアイテムにぶつかっていなければ出口到達している
            if (!isItemHit)
            {
                // 削除
            }
            else
            {
                // なんかアクション
                _animationState.Init();
                while (StepAnimation()) await UniTask.Yield(token);

                // 帰る
                path = _recorder.GetReversePathFromCurrentPosition();
                _moveState.Init(path, ignoreForward: true);
                while (StepMoveToPathEnd()) await UniTask.Yield(token);
            }
        }

        /// <summary>
        /// 経路の末尾まで移動する
        /// </summary>
        bool StepMoveToPathEnd()
        {
            // 移動する度に足跡を保持する
            _recorder.TryRecord();

            StepState(_moveState);
            return _moveState.IsRunning;
        }

        /// <summary>
        /// アニメーションを再生する
        /// </summary>
        bool StepAnimation()
        {
            StepState(_animationState);
            return _animationState.IsRunning;
        }

        /// <summary>
        /// 射撃する
        /// </summary>
        bool StepFire()
        {
            StepState(_fireState);
            return _fireState.IsRunning;
        }

        /// <summary>
        /// 1フレーム分だけステートを進める
        /// </summary>
        void StepState(BaseState nextState) => _currentState = _currentState.Step(nextState);
    }
}