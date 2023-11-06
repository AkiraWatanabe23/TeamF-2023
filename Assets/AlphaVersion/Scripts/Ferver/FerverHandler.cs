using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Alpha
{
    public enum Tension
    {
        Normal,
        Ferver,
    }

    /// <summary>
    /// �t�B�[�o�[�^�C���̊J�n/�I�������m����@�\��񋟂���N���X
    /// ���̃N���X���p������ꍇ�� Awake �͎g�p���Ȃ����ƁB
    /// </summary>
    public class FerverHandler : MonoBehaviour
    {
        Tension _tension = Tension.Normal; // �����C��

        protected Tension Tension => _tension;

        void Awake()
        {
            // ���b�Z�[�W����M�����^�C�~���O�ŊJ�n/�I���̃R�[���o�b�N�����ꂼ��Ă�
            MessageBroker.Default.Receive<FerverTimeMessage>().Subscribe(msg =>
            {
                // ��Ԃ�2��ނ����Ȃ��O��̐؂�ւ�
                _tension = 1 - _tension;

                if (_tension == Tension.Ferver) OnFerverTimeEnter();
                else OnFerverTimeExit();
            }).AddTo(gameObject);

            OnAwakeOverride();
        }

        protected virtual void OnAwakeOverride() { }
        protected virtual void OnFerverTimeEnter() { }
        protected virtual void OnFerverTimeExit() { }
    }
}
