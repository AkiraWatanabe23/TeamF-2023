using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 先頭のセルから経路をリストに変換する機能のクラス
    /// </summary>
    public class PathConverter
    {
        Waypoint _lead;

        public PathConverter(Waypoint lead)
        {
            _lead = lead;
        }

        /// <summary>
        /// 生成地点から席の後ろまでの経路を返す
        /// </summary>
        public IReadOnlyList<Vector3> GetPathToTableBehind()
        {
            List<Vector3> path = new();
            Waypoint c = _lead;
            while (c.Type == WaypointType.Way)
            {
                path.Add(c.Position);
                // 次の地点が複数あるような場合に対応していない
                c = c.Next[0];
            }

            return path;
        }

        /// <summary>
        /// 席の後ろから席までの経路を返す
        /// </summary>
        public IReadOnlyList<Vector3> GetPathToTable(Waypoint table)
        {
            List<Vector3> path = new();
            Waypoint c = _lead;

            // 席の後ろまで移動
            while (c.Next[0].Type == WaypointType.Way)
            {
                c = c.Next[0];
            }

            // 始点である席の後ろの位置を経路に追加
            path.Add(c.Position);
            // 席の位置を経路に追加
            path.Add(table.Position);

            return path;
        }

        /// <summary>
        /// 席から出口までの経路を返す
        /// </summary>
        public IReadOnlyList<Vector3> GetPathToExit(Waypoint table)
        {
            List<Vector3> path = new() { table.Position };
            Waypoint c = table;

            while (!c.IsFinal)
            {
                c = c.Next[0];
                path.Add(c.Position);
            }

            return path;
        }
    }
}
