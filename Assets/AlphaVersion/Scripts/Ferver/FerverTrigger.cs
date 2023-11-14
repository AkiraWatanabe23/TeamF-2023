using System.Collections;
using System.Collections.Generic;
using UniRx;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    /// <summary>
    /// �t�B�[�o�[�^�C���̔���/�I���𐧌䂷��N���X
    /// �C���Q�[�����̎��Ԍo�߂Ńt�B�[�o�[����
    /// </summary>
    public class FerverTrigger : MonoBehaviour
    {
        // TODO:����t�B�[�o�[�J�n�̂݌Ă΂��̂ŁA�t�B�[�o�[�I���̃n���h�����O���s���Ă��Ȃ�
        public event UnityAction OnFerverEnter;

        [SerializeField] InGameSettingsSO _settings;

        // 1�x�����R�[���o�b�N���ĂԂ��߂̃t���O
        bool _isFerver;

        void OnDisable()
        {
            OnFerverEnter = null;
        }

        /// <summary>
        /// �C���Q�[�������疈�t���[���ĂԂ��ƂŎ��Ԍo�߂ɂ��A�t�B�[�o�[�^�C���J�n
        /// </summary>
        public void Tick(float elapsed)
        {
            // ���Ƀt�B�[�o�[�^�C���������ꍇ�͒e��
            if (_isFerver) return;

            if (elapsed > _settings.TimeLimit - _settings.FerverTime)
            {
                OnFerverEnter?.Invoke();
                _isFerver = true;
            }
        }
    }
}
