using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    [RequireComponent(typeof(Button))]
    public class AnimationButton : MonoBehaviour
    {
        [SerializeField] UnityEvent _onAnimationCompleted;
        [Header("������A�j���[�V�����̐ݒ�")]
        [SerializeField] GameObject _grass;
        [SerializeField] Transform _throwMuzzle;
        [SerializeField] Vector3 _throwMuzzleOffset;
        [SerializeField] float _power = 5.0f;
        [SerializeField] float _animationPlayTime = 1.0f;

        void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(() => PlayAnimationAsync(button, this.GetCancellationTokenOnDestroy()).Forget());
        }

        async UniTask PlayAnimationAsync(Button button, CancellationToken token)
        {
            button.interactable = false;

            // �������A�C�e�����j�􂷂�A�j���[�V�����̍Đ����I���܂ōēx�{�^���������Ȃ��悤�ɂ���
            GameObject grass = Instantiate(_grass, _throwMuzzle.position + _throwMuzzleOffset, Quaternion.identity);
            Rigidbody rb = grass.GetComponent<Rigidbody>();
            rb.AddForce(_throwMuzzle.forward * _power, ForceMode.Impulse);
            await UniTask.WaitForSeconds(_animationPlayTime, cancellationToken: token);

            button.interactable = true;

            await UniTask.Yield(PlayerLoopTiming.Update, token);

            _onAnimationCompleted?.Invoke();
        }
    }
}
