using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// �t�B�[�o�[�^�C���̊J�n/�I�������m����@�\��񋟂���N���X
    /// ���̃N���X���p������ꍇ�� Awake �͎g�p���Ȃ����ƁB
    /// </summary>
    public class FerverHandler : MonoBehaviour
    {
        bool _isFerver = false; // �����C��

        protected bool IsFerver => _isFerver;

        void Awake()
        {
            // ���b�Z�[�W����M�����^�C�~���O�ŊJ�n/�I���̃R�[���o�b�N�����ꂼ��Ă�
            MessageBroker.Default.Receive<FerverTimeMessage>().Subscribe(msg =>
            {
                _isFerver = !_isFerver;

                if (_isFerver) OnFerverTimeEnter();
                else OnFerverTimeExit();
            }).AddTo(gameObject);

            OnAwakeOverride();
        }

        protected virtual void OnAwakeOverride() { }
        protected virtual void OnFerverTimeEnter() { }
        protected virtual void OnFerverTimeExit() { }
    }
}
