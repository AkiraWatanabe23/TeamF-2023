using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    /// <summary>
    /// ���C�g����]������A�j���[�V����
    /// �����̃��C�g����]�����邽�߁A�{�g���l�b�N�ɂȂ�\��������
    /// </summary>
    public class LightRotate : MonoBehaviour
    {
        [SerializeField] float _duration = 5.0f;
        [SerializeField] float _maxRotSpeed = 15.0f;
        [SerializeField] float _moveY = 0.75f;
        [SerializeField] float _DefaultMirrorBallHeight = 1.93f;
        [Header("�񂷑Ώ�")]
        [SerializeField] Transform _spotLight;
        [SerializeField] Transform _mirrorBall;

        Tween _spotLightTween;
        Tween _mirrorBallTween;

        void Start()
        {
            _spotLight.gameObject.SetActive(false);
            SetOnDefaultPosition();
        }

        /// <summary>
        /// �C���X�y�N�^�ŘM���Ă��K�����̈ʒu����n�܂�
        /// </summary>
        void SetOnDefaultPosition()
        {
            if (_mirrorBall == null) return;
            Vector3 pos = _mirrorBall.position;
            pos.y = _DefaultMirrorBallHeight;
            _mirrorBall.position = pos;
        }

        /// <summary>
        /// ��]�̃A�j���[�V�������Đ�
        /// </summary>
        public void Play()
        {
            _spotLight.gameObject.SetActive(true);

            //_spotLightTween = _spotLight.DORotate(new Vector3(0, 360, 0), _duration, RotateMode.FastBeyond360)
            //    .SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear).SetLink(gameObject);

            LightAnimationAsync(this.GetCancellationTokenOnDestroy()).Forget();
            MirrorBallAnimationAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        /// <summary>
        /// �~���[�{�[�����ǂ�ǂ񑬓x���グ�ĉ�]����A�j���[�V����
        /// </summary>
        async UniTaskVoid MirrorBallAnimationAsync(CancellationToken token)
        {
            if (_mirrorBall == null) return;

            _mirrorBallTween = _mirrorBall.DOLocalMoveY(-_moveY, _duration/2).SetRelative().SetLink(gameObject);
            await UniTask.WaitForSeconds(_duration/2, cancellationToken: token);

            float delta = 1;
            while (true)
            {
                float speed = Time.deltaTime * delta;
                _mirrorBall.Rotate(new Vector3(0, speed, 0));
                delta += Time.deltaTime * 3;
                delta = Mathf.Clamp(delta, 0, 350);

                await UniTask.Yield(token);
            }
        }

        /// <summary>
        /// ���C�g���ǂ�ǂ񑬓x���グ�ĉ�]����A�j���[�V����
        /// </summary>
        async UniTaskVoid LightAnimationAsync(CancellationToken token)
        {
            if (_mirrorBall == null) return;

            float delta = 1;
            while (true)
            {
                float speed = Time.deltaTime * delta;
                _spotLight.Rotate(new Vector3(0, speed, 0));
                delta += Time.deltaTime * 3;
                delta = Mathf.Clamp(delta, 0, 130);

                await UniTask.Yield(token);
            }
        }

        /// <summary>
        /// �A�j���[�V�������~�߂ď���
        /// </summary>
        public void Stop()
        {
            _spotLightTween?.Kill();
            _mirrorBallTween?.Kill();
            _spotLight.gameObject.SetActive(false);
        }
    }
}