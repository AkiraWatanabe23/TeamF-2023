using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Alpha
{
    public enum ActorType
    {
        Male,
        Female,
        Muscle,
        Robber,
    }

    public enum BehaviorType
    {
        Customer,
        Robber,
    }

    /// <summary>
    /// �L�����N�^�[�̊��N���X
    /// �������ɐ���������Ă΂�� Init �� Start �̃I�[�o�[���C�h���\
    /// </summary>
    public class Actor : MonoBehaviour
    {
        [SerializeField] ActorSettingsSO _settings;

        public ActorType ActorType => _settings.ActorType;
        public BehaviorType BehaviorType => _settings.BehaviorType;
        protected ActorSettingsSO Settings => _settings;

        /// <summary>
        /// Awake�̑���A�O�����琶������ۂɂ��̃��\�b�h���ĂԈȊO�ɕK�v�ȏ������͖����B
        /// </summary>
        public void Init(Waypoint lead, Tension tension)
        {
            OnInitOverride(lead, tension);
        }

        /// <summary>
        /// Awake�̑���A�O�����琶������ۂɂ��̃��\�b�h���ĂԈȊO�ɕK�v�ȏ������͖����B
        /// </summary>
        public void Init<T>(Waypoint lead, Tension tension, T arg)
        {
            OnInitOverride(lead, tension, arg);
        }

        /// <summary>
        /// �񓯊������̎��s
        /// </summary>
        void Start()
        {
            CancellationTokenSource cts = new();
            UpdateAsync(cts.Token).Forget();

            // �I�u�W�F�N�g�̔j�����Ƀg�[�N����Dispose����
            this.OnDestroyAsObservable().Subscribe(_ => { cts.Cancel(); cts.Dispose(); });

            OnStartOverride();
        }

        protected virtual void OnInitOverride(Waypoint lead, Tension tension) { }
        protected virtual void OnInitOverride<T>(Waypoint lead, Tension tension, T arg) { }
        protected virtual void OnStartOverride() { }
        protected async virtual UniTaskVoid UpdateAsync(CancellationToken token) { }
    }
}

// �q
//  �Ȃ܂ŕ����Ă���
//  �������L���b�`�ł���܂őҋ@
//  �A�j���[�V����
//  �A��
// ����
//  �J�E���^�[�ׂ܂ňړ�
//  �\����
//  �����Ă���
//  ���C
//  �A��
// �m�莖��
//  ���_�ɂ����� �� ���_���璸�_�Ɉړ������� �����݂ɂȂ�
//  �t�B�[�o�[�^�C���́A�A�j���[�V�������؂�ւ�邾���A���݂̍s�����L�����Z�����ĉ��������ł͂Ȃ�

// ���݂̃E�F�C�|�C���g�ƌo�H��ێ�����N���X
//  ���݂Ɨ��ڂ���E�F�C�|�C���g�Ɉړ��\
//  �ړ����ɃL�����Z���\

// �L�������ɕ�����̂ł͂Ȃ��A���_�̎�ނ��Ƃɕ�����A�v���[�`�H
// �L�����Z���[�V�����g�[�N���̂悤�Ɋe��t���O���Q�ƌ^�œn�����A�v���[�`�H