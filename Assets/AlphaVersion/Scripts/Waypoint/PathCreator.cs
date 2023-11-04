using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    public enum PathType
    {
        Customer,
        Robber,
    }

    /// <summary>
    /// キャラクターの辿る経路を作成するクラス
    /// </summary>
    public class PathCreator : MonoBehaviour
    {
        [System.Serializable]
        public class Data
        {
            public PathType Type;
            [Header("経路の先頭")]
            public Waypoint Lead;
        }

        [SerializeField] Data[] _data;

        Dictionary<PathType, Waypoint> _dict = new();

        void Awake()
        {
            // TODO:必要に応じて経路の作成処理を作る。
            // 現在はWaypoint側に次のWaypointの情報を保持しているので、特に処理をせずそのまま
            _data.ToDictionary(d => d.Type, d => d.Lead);
        }

        /// <summary>
        /// 経路を取得する
        /// </summary>
        /// <returns>ある:経路の先頭のウェイポイント ない:null</returns>
        public Waypoint GetPath(PathType type)
        {
            if (_dict.TryGetValue(type, out Waypoint value))
            {
                return value;
            }
            else
            {
                Debug.LogError("経路が存在しない: " + type);
                return null;
            }
        }
    }
}
