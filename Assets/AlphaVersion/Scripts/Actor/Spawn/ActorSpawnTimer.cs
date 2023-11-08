using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Alpha
{
    /// <summary>
    /// �C���Q�[�����̌o�ߎ��ԂŃL�����N�^�[�̐����C�x���g���N�����@�\�̃N���X
    /// </summary>
    public class ActorSpawnTimer : MonoBehaviour
    {
        public event UnityAction OnSpawnTiming;

        [SerializeField] InGameSettingsSO _settings;

        float _elapsed;

        void OnDestroy()
        {
            OnSpawnTiming = null;
        }

        /// <summary>
        /// 1�t���[�������o�ߎ��Ԃ�ǉ����Ă����A���Ԋu�ŃR�[���o�b�N���Ă�
        /// </summary>
        public void Tick()
        {
            _elapsed += Time.deltaTime;
            if (_elapsed > _settings.CustomerSpawnRate)
            {
                _elapsed = 0;

                OnSpawnTiming?.Invoke();
            }
        }
    }
}
