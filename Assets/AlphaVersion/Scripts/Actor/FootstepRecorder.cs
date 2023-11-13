using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 経路の先頭から現在位置までの経路を記録する機能のクラス
    /// 辿った座標以外が記録されないよう、キャラクター自体への参照を保持する
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
        /// 経路のリセット
        /// </summary>
        public void Reset()
        {
            _path.Clear();
        }

        /// <summary>
        /// 移動する度にこのメソッドを呼び出す
        /// 1つ前に追加した座標とある程度離れていた場合は、経路に現在の座標を追加する
        /// </summary>
        public void TryRecord()
        {
            Vector3 position = _target.position;

            // 経路に座標が存在しない場合はそのまま追加する
            if (_path.Count == 0) _path.Add(position);

            float dist = (_path[_path.Count - 1] - position).sqrMagnitude;
            if (dist >= _space)
            {
                _path.Add(position);
            }
        }

        /// <summary>
        /// 現在地から初期値に戻る際にこのメソッドを呼び出す
        /// 辿った経路を反転させることで現在の座標から逆順に辿る経路を返す
        /// </summary>
        public IReadOnlyList<Vector3> GetReversePathFromCurrentPosition()
        {
            List<Vector3> copy = new(_path);
            copy.Add(_target.position);
            copy.Reverse();

            // 経路の末端が生成位置(画面外)なので削除する
            copy.RemoveAt(copy.Count - 1);

            copy.ForEach(f => DebugDraw(f));

            return copy;
        }

        // デバッグ用
        void DebugDraw(Vector3 p)
        {
            GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            g.transform.localScale = Vector3.one * 0.5f;
            g.transform.position = p;
        }
    }
}
