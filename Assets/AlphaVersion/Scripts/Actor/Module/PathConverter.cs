using System;
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

        /// <summary>
        /// 現在地点から指定した地点の種類までの経路を返す。
        /// </summary>
        /// <returns>経路の末尾の地点</returns>
        public Waypoint GetPathByType(WaypointType type, out IReadOnlyList<Vector3> path, Waypoint start)
        {
            List<Vector3> tempPath = new() { start.Position };

            // 現在地点が終点なら、現在地点を追加した時点で返す
            if (start.IsFinal) { path = tempPath; return start; }

            // 次の地点が複数あるような場合に対応していない            
            Waypoint c = start.Next[0];
            while (!c.IsFinal)
            {
                tempPath.Add(c.Position);
                if (c.Type == type) break;

                c = c.Next[0];
            }

            path = tempPath;
            return c;
        }

        /// <summary>
        /// その地点から先頭までの経路を返す
        /// </summary>
        public IReadOnlyList<Vector3> GetPathToLeadFromPosition(Vector3 position)
        {
            List<Vector3> path = new() { position, _lead.Position };
            
            
            
            return path;
        }

        /// <summary>
        /// 次の地点までの経路を返す。
        /// 次の地点が無い場合は、その場への経路を返す。
        /// </summary>
        /// <returns>次の地点</returns>
        public Waypoint GetPathToNext(out IReadOnlyList<Vector3> path, Waypoint current)
        {
            List<Vector3> tempPath = new() { current.Position };

            if (current.IsFinal)
            { 
                path = tempPath; 
                return current; 
            }
            else
            {
                // 次の地点が複数あるような場合に対応していない
                tempPath.Add(current.Next[0].Position);
                path = tempPath;
                return current.Next[0]; 
            }
        }
    }
}
