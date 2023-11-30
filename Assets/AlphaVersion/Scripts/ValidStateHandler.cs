using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Alpha
{
    /// <summary>
    /// �Q�[���J�n�̃^�C�~���O�ŗL�����A
    /// �Q�[���I�[�o�[�̃^�C�~���O�Ŗ����������@�\��񋟂���N���X�B
    /// ���̃N���X���p������ꍇ�� Awake OnEnable OnDisable Update �͎g�p���Ȃ����ƁB
    /// </summary>
    public class ValidStateHandler : MonoBehaviour
    {
        bool _isValid = false; // �����C��

        protected bool IsValid => _isValid;

        void Awake()
        {
            // ���b�Z�[�W�̎�M
            MessageBroker.Default.Receive<GameStartMessage>().Subscribe(_ => _isValid = true).AddTo(gameObject);
            MessageBroker.Default.Receive<GameOverMessage>().Subscribe(_ => _isValid = false).AddTo(gameObject);

            OnAwakeOverride();
        }

        void OnEnable()
        {
            OnEnableOverride();
        }

        void OnDisable()
        {
            OnDisableOverride();
        }

        void Update()
        {
            if (_isValid)
            {
                OnUpdateOverride();
            }
        }

        protected virtual void OnAwakeOverride() { }
        protected virtual void OnEnableOverride() { }
        protected virtual void OnDisableOverride() { }
        protected virtual void OnUpdateOverride() { }
    }
}
