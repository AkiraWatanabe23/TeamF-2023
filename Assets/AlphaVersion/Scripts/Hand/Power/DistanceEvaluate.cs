using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

namespace Alpha
{
    /// <summary>
    /// 2�_�Ԃ̃x�N�g���̋�����]���֐���p���ĕ␳���s���N���X
    /// </summary>
    [System.Serializable]
    public class DistanceEvaluate
    {
        const float Max = 1.40f;
        const float Min = 0.01f;

        [SerializeField] HandSettingsSO _settings;

        /// <summary>
        /// ViewPoint���W�n(0,0 ~ 1,1)��2�_�̋�����]���֐��ɒʂ��A�␳���ꂽ�l��Ԃ�
        /// </summary>
        /// <returns>0~1�̒l</returns>
        public float Evaluate(Vector3 viewPointA, Vector3 viewPointB)
        {
            // 0~1 ��xy���ʏ�̍��W�Ȃ̂ŋ����� 0 ���� ��1.4 �ɂȂ�
            float time = (viewPointB - viewPointA).magnitude;
            time = math.remap(Min, Max, 0, 1, time);

            return time * _settings.Evaluate.Evaluate(time);
        }
    }
}