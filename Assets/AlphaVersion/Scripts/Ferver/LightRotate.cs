using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Alpha
{
    /// <summary>
    /// ���C�g����]������A�j���[�V����
    /// �����̃��C�g����]�����邽�߁A�{�g���l�b�N�ɂȂ�\��������
    /// </summary>
    public class LightRotate : MonoBehaviour
    {
        [SerializeField] float _duration = 5.0f;
        [Header("�񂷑Ώ�")]
        [SerializeField] Transform _spotLight;
        [SerializeField] Transform _mirrorBall;

        /// <summary>
        /// ��]�̃A�j���[�V�������Đ�
        /// </summary>
        public void Play()
        {
            _spotLight.DORotate(new Vector3(0, 360, 0), _duration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetLink(gameObject);

            _mirrorBall.DORotate(new Vector3(0, 360, 0), _duration / 10, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetLink(gameObject);
        }
    }
}