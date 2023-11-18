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
            ExtendCTS cts = new();
            UpdateAsync(cts.Token).Forget();

            // �Q�[���I�[�o�[���Ƀg�[�N����Dispose����
            MessageBroker.Default.Receive<GameOverMessage>()
                .Subscribe(_ => cts.Dispose()).AddTo(gameObject);
            // �I�u�W�F�N�g�̔j�����Ƀg�[�N����Dispose����
            this.OnDestroyAsObservable().Subscribe(_ => cts.Dispose());

            OnStartOverride();
        }

        protected virtual void OnInitOverride(Waypoint lead, Tension tension) { }
        protected virtual void OnInitOverride<T>(Waypoint lead, Tension tension, T arg) { }
        protected virtual void OnStartOverride() { }
        protected async virtual UniTaskVoid UpdateAsync(CancellationToken token) { }
    }
}
