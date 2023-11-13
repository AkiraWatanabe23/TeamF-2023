using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    /// <summary>
    /// �Q�[���J�n���̉��o�C�x���g�̃N���X
    /// ���̃C�x���g�I������ɃQ�[���J�n
    /// </summary>
    public class GameStartEvent : MonoBehaviour
    {
        // TODO:��X�^�C�����C���Đ��̃C�x���g�ɕύX

        [SerializeField] GameObject _ui;
        [SerializeField] float _playTime = 1;

        void Awake()
        {
            _ui.SetActive(false);
        }

        /// <summary>
        /// �C�x���g�̍Đ��A�C�x���g�I���܂ő҂�
        /// </summary>
        public async UniTask PlayAsync(CancellationToken token)
        {
            _ui.SetActive(true);
            await UniTask.Delay(System.TimeSpan.FromSeconds(_playTime), cancellationToken: token);
            _ui.SetActive(false);
        }
    }
}
