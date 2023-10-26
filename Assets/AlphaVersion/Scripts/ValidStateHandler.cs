using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �Q�[���J�n�̃^�C�~���O�ŗL�����A
    /// �Q�[���I�[�o�[�̃^�C�~���O�Ŗ����������@�\��񋟂���N���X�B
    /// ���̃N���X���p������ꍇ�� OnEnable OnDisable Update �͎g�p���Ȃ����ƁB
    /// </summary>
    public class ValidStateHandler : MonoBehaviour
    {
        bool _isValid = true; // �����C��

        void OnEnable()
        {
            //InGame.OnGameStart += () => _isValid = true;
            //InGame.OnGameOver += () => _isValid = false;

            OnEnableOverride();
        }

        void OnDisable()
        {
            //InGame.OnGameStart -= () => _isValid = true;
            //InGame.OnGameOver -= () => _isValid = false;

            OnDisableOverride();
        }

        void Update()
        {
            if (_isValid)
            {
                OnUpdateOverride();
            }
        }

        protected virtual void OnEnableOverride() { }
        protected virtual void OnDisableOverride() { }
        protected virtual void OnUpdateOverride() { }
    }
}
