using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    public class StageSelect : MonoBehaviour
    {
        [SerializeField] Transform _mirrorball;
        [SerializeField] Transform _light;
        [Header("MirrorBall回転の調整用")]
        [SerializeField] Button _rotButton;
        [SerializeField] Text _text;
        [SerializeField] Slider _slider;

        bool _isRot = true;

        void Start()
        {
            UpdateAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        async UniTaskVoid UpdateAsync(CancellationToken token)
        {
            if (Fade.Instance != null)
            {
                // フェードを待つ
                Fade.Instance.StartFadeIn();
                await UniTask.WaitForSeconds(Fade.Instance.FadeTime, cancellationToken: token);
            }

            if (_rotButton == null || _text == null || _slider == null) return;

            while (!token.IsCancellationRequested)
            {
                if (_isRot)
                {
                    var v = _slider.value;
                    float speed = 30 + v * 60;
                    _text.text = speed.ToString("F2");

                    _light.Rotate(Vector3.up * Time.deltaTime * speed);
                    _mirrorball.Rotate(Vector3.up * Time.deltaTime * speed);
                }
                await UniTask.Yield(PlayerLoopTiming.Update,token);
            }
        }

        public void Switch()
        {
            _isRot = !_isRot;
        }
    }
}
