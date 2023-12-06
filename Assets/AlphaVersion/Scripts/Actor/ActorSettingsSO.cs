using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �L�����N�^�[�̊e��l��ݒ肷��N���X
    /// </summary>
    [CreateAssetMenu(fileName = "ActorSettingsSO", menuName = "Settings/ActorSettings")]
    public class ActorSettingsSO : ScriptableObject
    {
        /// <summary>
        /// �e�L�����N�^�[�̃p�����[�^�̍\����
        /// </summary>
        [System.Serializable]
        public struct ActorParams
        {
            [Header("<color=#00FF76>�ړ����x</color>")]
            [Range(0.1f, 3.0f)]
            public float MoveSpeed;
            [Header("<color=#00FF76>�������󂯎�鐧������(�b)</color>")]
            [Range(1.0f, 60.0f)]
            public float OrderTimeLimit;
            [Header("�����i�̈ꗗ")]
            public ItemType[] Orders;
        }

        /// <summary>
        /// ���Ă���̐ݒ�p�̍\����
        /// </summary>
        [System.Serializable]
        public struct VisualParams
        {
            [System.Serializable]
            public struct Offset
            {
                const float Range = 1f;

                [Range(-Range, Range)]
                [SerializeField] float x;
                [Range(-Range, Range)]
                [SerializeField] float y;
                [Range(-Range, Range)]
                [SerializeField] float z;

                public Vector3 Position
                {
                    get
                    {
                        return new Vector3(x, y, z);
                    }
                }
            }

            [System.Serializable]
            public struct ParticleData
            {
                public ParticleType Particle;
                public Offset Offset;
            }

            [Header("�Ԃ����ۂɍĐ������p�[�e�B�N��")]
            public ParticleData ItemHitParticle;
            [Header("�����̐���/���s���ɍĐ�����p�[�e�B�N��")]
            public ParticleData SuccessParticle;
            public ParticleData FailureParticle;
        }

        [Header("�L�����N�^�[�����ʂ���l")]
        [SerializeField] ActorType _actorType;
        [SerializeField] BehaviorType _behaviorType;
        [SerializeField] string _failureVoice;
        [SerializeField] string _successVoice;
        [SerializeField] string _rareSuccessVoice;
        [Header("<color=#00FF76>���x���f�U�C���p�̒l</color>")]
        [SerializeField] ActorParams _actorParams;
        [Header("�f�U�C�i�[���M��l")]
        [SerializeField] VisualParams _visualParams;

        public float MoveSpeed => _actorParams.MoveSpeed;
        public ItemType[] Orders => _actorParams.Orders;
        public ItemType RandomOrder => Orders[Random.Range(0, Orders.Length)];
        public float OrderTimeLimit => _actorParams.OrderTimeLimit;
        public ActorType ActorType => _actorType;
        public BehaviorType BehaviorType => _behaviorType;
        public ParticleType SuccessParticle => _visualParams.SuccessParticle.Particle;
        public ParticleType FailureParticle => _visualParams.FailureParticle.Particle;
        public ParticleType ItemHitParticle => _visualParams.ItemHitParticle.Particle;
        public Vector3 SuccessParticleOffset => _visualParams.SuccessParticle.Offset.Position;
        public Vector3 FailureParticleOffset => _visualParams.FailureParticle.Offset.Position;
        public Vector3 ItemHitParticleOffset => _visualParams.ItemHitParticle.Offset.Position;
        public string FailureVoice => _failureVoice;
        // 1/10�̒�m���Ń��A�Ȑ����{�C�X
        public string SuccessVoice => Random.value <= 0.1f ? _rareSuccessVoice : _successVoice;
    }
}