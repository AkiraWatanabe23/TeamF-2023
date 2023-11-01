using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UniRx;

namespace Alpha
{
    /// <summary>
    /// �t�B�[�o�[�^�C���̊J�n/�I���̐؂�ւ����̃��b�Z�[�W���O�Ɏg�p����\����
    /// </summary>
    public struct FerverTimeMessage { }

    /// <summary>
    /// �t�B�[�o�[�^�C���̊J�n/�I���𐧌䂷��N���X
    /// </summary>
    public class FerverTime : MonoBehaviour
    {
        /// <summary>
        /// �t�B�[�o�[�^�C���J�n�̃R�[���o�b�N
        /// </summary>
        public static event UnityAction OnEnter;
        /// <summary>
        /// �t�B�[�o�[�^�C���I���̃R�[���o�b�N
        /// </summary>
        public static event UnityAction OnExit;

        static FerverTime _instance;

        [Header("�f�o�b�O�p:F�L�[�ŃI���I�t�؂�ւ�")]
        [SerializeField] bool _isDebug = true;

        bool _isFerver;

        public static bool IsFerver => _instance._isFerver;
        public static bool IsNormal => !_instance._isFerver;

        void Awake()
        {
            _instance ??= this;
        }

        void OnDestroy()
        {
            _instance = null;
            _isFerver = false;
        }

        void Update()
        {
            // �f�o�b�O�p�ɃL�[���͂Ő؂�ւ�����
            if (_isDebug && Input.GetKeyDown(KeyCode.F))
            {
                _isFerver = !_isFerver;

                // �R�[���o�b�N�Ăяo��
                if (_isFerver) OnEnter?.Invoke();
                else OnExit?.Invoke();
                
                // ���b�Z�[�W���O
                MessageBroker.Default.Publish(new FerverTimeMessage());
            }
        }
    }
}
