using System;
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

        /// <summary>
        /// ���ݒn�_����w�肵���n�_�̎�ނ܂ł̌o�H��Ԃ��B
        /// </summary>
        /// <returns>�o�H�̖����̒n�_</returns>
        public Waypoint GetPathByType(WaypointType type, out IReadOnlyList<Vector3> path, Waypoint start)
        {
            List<Vector3> tempPath = new() { start.Position };

            // ���ݒn�_���I�_�Ȃ�A���ݒn�_��ǉ��������_�ŕԂ�
            if (start.IsFinal) { path = tempPath; return start; }

            // ���̒n�_����������悤�ȏꍇ�ɑΉ����Ă��Ȃ�            
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
        /// ���̒n�_����擪�܂ł̌o�H��Ԃ�
        /// </summary>
        public IReadOnlyList<Vector3> GetPathToLeadFromPosition(Vector3 position)
        {
            List<Vector3> path = new() { position, _lead.Position };
            
            
            
            return path;
        }

        /// <summary>
        /// ���̒n�_�܂ł̌o�H��Ԃ��B
        /// ���̒n�_�������ꍇ�́A���̏�ւ̌o�H��Ԃ��B
        /// </summary>
        /// <returns>���̒n�_</returns>
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
                // ���̒n�_����������悤�ȏꍇ�ɑΉ����Ă��Ȃ�
                tempPath.Add(current.Next[0].Position);
                path = tempPath;
                return current.Next[0]; 
            }
        }
    }
}
