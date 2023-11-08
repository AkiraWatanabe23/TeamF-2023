using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    public enum WaypointType
    {
        Way,    // そのまま次のウェイポイントに向かう
        Table,  // 客が座る椅子
        Stage,  // キャラクターが演出を行う地点
        Fire,   // 泥棒の射撃地点
    }

    /// <summary>
    /// 各ウェイポイント本体のクラス
    /// </summary>
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] WaypointType _type;
        [SerializeField] Waypoint[] _next;
        [SerializeField] Waypoint[] _prev;

        Transform _transform = null;

        void Awake()
        {
            _transform = transform;
        }

        public WaypointType Type => _type;
        public IReadOnlyList<Waypoint> Next => _next;
        public IReadOnlyList<Waypoint> Prev => _prev;
        public Vector3 Position => _transform.position;
        public bool IsLead => _prev == null || _prev.Length == 0;
        public bool IsFinal => _next == null || _next.Length == 0;
    }
}