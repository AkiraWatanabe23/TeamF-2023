using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �擪�̃Z������o�H�����X�g�ɕϊ�����@�\�̃N���X
    /// </summary>
    public class PathConverter
    {
        Waypoint _lead;

        public PathConverter(Waypoint lead)
        {
            _lead = lead;
        }

        /// <summary>
        /// �����n�_����Ȃ̌��܂ł̌o�H��Ԃ�
        /// </summary>
        public IReadOnlyList<Vector3> GetPathToTableBehind()
        {
            List<Vector3> path = new();
            Waypoint c = _lead;
            while (c.Type == WaypointType.Way)
            {
                path.Add(c.Position);
                // ���̒n�_����������悤�ȏꍇ�ɑΉ����Ă��Ȃ�
                c = c.Next[0];
            }

            return path;
        }

        /// <summary>
        /// �Ȃ̌�납��Ȃ܂ł̌o�H��Ԃ�
        /// </summary>
        public IReadOnlyList<Vector3> GetPathToTable(Waypoint table)
        {
            List<Vector3> path = new();
            Waypoint c = _lead;

            // �Ȃ̌��܂ňړ�
            while (c.Next[0].Type == WaypointType.Way)
            {
                c = c.Next[0];
            }

            // �n�_�ł���Ȃ̌��̈ʒu���o�H�ɒǉ�
            path.Add(c.Position);
            // �Ȃ̈ʒu���o�H�ɒǉ�
            path.Add(table.Position);

            return path;
        }

        /// <summary>
        /// �Ȃ���o���܂ł̌o�H��Ԃ�
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
