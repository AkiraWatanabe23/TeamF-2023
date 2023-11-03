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
    }

    /// <summary>
    /// 各ウェイポイント本体のクラス
    /// </summary>
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] WaypointType _type;
        [SerializeField] Waypoint[] _next;

        public IReadOnlyCollection<Waypoint> Next => _next;

        void Start()
        {

        }

        void Update()
        {

        }
    }
}

// 先頭から次のウェイポイントを見ていく。
// 強盗はプレイヤーに攻撃された際に逃走、つまり経路を戻っていく必要がある。
// 