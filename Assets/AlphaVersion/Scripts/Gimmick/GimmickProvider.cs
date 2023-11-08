using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Alpha
{
    /// <summary>
    /// �M�~�b�N�̔����𐧌䂷��N���X
    /// �M�~�b�N�̏����͂��̃N���X�Ɉˑ�����
    /// </summary>
    public class GimmickProvider : ValidStateHandler, IRobberSpawnRegisterable
    {
        public event UnityAction OnTumbleweedSpawned;
        public event UnityAction OnRobberSpawned;

        [SerializeField] InGameSettingsSO _settings;

        float _tumbleweedElapsed;
        float _robberElapsed;

        protected override void OnDisableOverride()
        {
            OnTumbleweedSpawned = null;
            OnRobberSpawned = null;
        }

        /// <summary>
        /// �C���Q�[���J�n�Ɠ�����Update�ŁA�_���u���E�B�[�h�Ƌ���
        /// ���ꂼ��̃^�C�}�[��i�߁A�������Ő����������Ăяo��
        /// </summary>
        protected override void OnUpdateOverride()
        {
            // �_���u���E�B�[�h
            _tumbleweedElapsed += _settings.TumbleWeed.FixedDelta;
            if (_tumbleweedElapsed > _settings.TumbleWeed.Rate)
            {
                _tumbleweedElapsed = 0;
                OnTumbleweedSpawned?.Invoke();
            }

            // ����
            _robberElapsed += _settings.Robber.FixedDelta;
            if (_robberElapsed > _settings.Robber.Rate)
            {
                _robberElapsed = 0;
                OnRobberSpawned?.Invoke();
            }
        }
    }
}