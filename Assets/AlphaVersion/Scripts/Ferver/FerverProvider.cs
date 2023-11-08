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
    /// �t�B�[�o�[�^�C���̔���/�I�������͂��̃N���X�Ɉˑ�����
    /// </summary>
    public class FerverProvider : MonoBehaviour
    {
        public static event UnityAction<bool> OnFerverSwitched;

        [SerializeField] InGameSettingsSO _settings;
        [SerializeField] ScoreManager _scoreManager; // TODO:�{���Ȃ�C���^�[�t�F�[�X�ŎQ�Ƃ���

        CancellationTokenSource _cts;
        // �t�B�[�o�[�^�C�����Ƀt�B�[�o�[�J�n��臒l�ɓ��B�����ꍇ�A���Ԃ���������
        float _timeLimit;
        int _nextThreshold;

        void OnDisable()
        {
            OnFerverSwitched = null;
        }

        void Start()
        {
            // ���v�X�R�A��臒l���z���Ă�����t�B�[�o�[�^�C���J�n
            _scoreManager.TotalScore.Skip(1).Where(Check).Subscribe(_ => Ferver()).AddTo(gameObject);
            // �t�B�[�o�[�ɕK�v�ȃX�R�A��ݒ�
            _nextThreshold = _settings.FerverScoreThreshold;
        }

        void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }

        /// <summary>
        /// ���݂̍��v�X�R�A��臒l�𒴂��Ă��邩�𒲂ׂ�
        /// �����Ă����ꍇ��臒l���X�V
        /// </summary>
        bool Check(int totalScore)
        {
            if (totalScore >= _nextThreshold)
            {
                _nextThreshold += _settings.FerverScoreThreshold;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// �ʏ펞�Ȃ�t�B�[�o�[�^�C���J�n
        /// �t�B�[�o�[�^�C�����̏ꍇ�͎��Ԃ���������
        /// </summary>
        void Ferver()
        {
            if (_timeLimit > 0)
            {
                _timeLimit += _settings.FerverTimeLimit;
            }
            else
            {
                _timeLimit = _settings.FerverTimeLimit;
                _cts = new();
                FerverAsync(_cts.Token).Forget();
            }
        }

        /// <summary>
        /// �������Ԃ����ς��t�B�[�o�[�^�C�������s����
        /// </summary>
        async UniTaskVoid FerverAsync(CancellationToken token)
        {
            OnFerverSwitched?.Invoke(true);
            while (_timeLimit >= 0)
            {
                await UniTask.Yield(token);
                _timeLimit -= Time.deltaTime;
            }
            OnFerverSwitched?.Invoke(false);
        }
    }
}
