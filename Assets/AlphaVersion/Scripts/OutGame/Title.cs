using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    public class Title : MonoBehaviour
    {
        void Start()
        {
            UpdateAsync(this.GetCancellationTokenOnDestroy()).Forget();
        }

        async UniTaskVoid UpdateAsync(CancellationToken token)
        {
            // �t�F�[�h��҂�
            Fade.Instance.StartFadeIn();
            await UniTask.WaitForSeconds(Fade.Instance.FadeTime, cancellationToken: token);
        }
    }
}
