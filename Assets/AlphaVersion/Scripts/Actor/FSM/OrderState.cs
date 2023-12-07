using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Alpha
{
    /// <summary>
    /// ������҂X�e�[�g
    /// </summary>
    public class OrderState : BaseState
    {
        [SerializeField] GameObject _decal;
        [SerializeField] ActorSettingsSO _settings;
        [SerializeField] Collider _collider;
        [Header("���̑����ȏ�łԂ���ƃ��O�h�[��������")]
        [SerializeField] float _defeatableSpeed = 1.0f;

        EmptyTable _table;

        // ���������^�C�~���O�ŏ����������Ƃ��ď������N���X����n��
        public ItemType[] Orders;

        public override StateType Type => StateType.Order;
        
        /// <summary>
        /// ���ʂ��m�肵��(����/���s)�ꍇ�͊O�����玟�̃X�e�[�g�ɑJ�ڂ�����
        /// </summary>
        public OrderResult Result { get; private set; }

        protected override void OnAwakeOverride()
        {
            _decal.SetActive(false);

            // ���̃X�e�[�g��Stay�̍ۂ͓����蔻�肪�L���ɂȂ�
            // Init�͏����������x�ɌĂ΂��̂�Awake�̃^�C�~���O��1�x�����o�^����
            _collider.OnCollisionEnterAsObservable().Where(_ => CurrentStage == Stage.Stay).Subscribe(OnItemHit);
        }

        public void Init(EmptyTable table)
        {
            _table = table;
            Result = OrderResult.Unsettled;
        }

        protected override void Enter()
        {
            LookAt();
            Animator.Play("Order");
            Cri.PlaySE3D(transform.position, _settings.OrderVoice, "CueSheet_SE4");

            // �Ȃ�L�����A���Ԑ؂�(���s)�������̓L���b�`����(����)�ŃR�[���o�b�N���Ă΂��
            _table.Table.Valid(_settings.OrderTimeLimit, Orders[Random.Range(0, Orders.Length)], result => 
            {
                Result = result;
                _table.Table.Invalid();
            });
        }

        protected override void Exit()
        {
            // ���ԓ����A�܂��L���b�`���Ă��Ȃ���ԁA�A�C�e�����Ԃ����Ă��Ȃ���Ԃ�
            // �O������̃g���K�[�ŃX�e�[�g�𔲂���\�����l�����Ė���������
            _table.Table.Invalid();
        }

        protected override void Stay()
        {
            // ���̃X�e�[�g�͔񓯊������Ŏ��s�����̂ŁAEnter���Ă΂�Ă����Stay���Ă΂Ȃ��Ă��i��
        }

        /// <summary>
        /// �ȂɌ�����
        /// </summary>
        void LookAt()
        {
            Vector3 p1 = new Vector3(_table.Position.x, 0, _table.Position.z);
            Vector3 p2 = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 dir = p1 - p2;

            if (dir != Vector3.zero)
            {
                Model.rotation = Quaternion.LookRotation(dir, Vector3.up);
            }
        }

        /// <summary>
        /// �A�C�e�����Ԃ������ꍇ�̃R�[���o�b�N
        /// </summary>
        void OnItemHit(Collision collision)
        {
            // �A�C�e���ȊO���Ԃ������ꍇ�͒e��
            if (!collision.gameObject.TryGetComponent(out ThrowedItem item)) return;

            _decal.SetActive(true);

            // ��������
            if (Result == OrderResult.Unsettled)
            {
                // ���x�����ȏ�̏ꍇ�͌��j����A���O�h�[���𐶐�����
                if (item.SqrSpeed > _defeatableSpeed)
                {
                    Result = OrderResult.Defeated;
                    RagDollMessageSender.SendMessage(_settings.ActorType, Model, item.transform.position);
                }
                else
                {
                    Result = OrderResult.Failure;
                }

                // �A�C�e�����Ԃ������ꍇ�͐ȑ��Ŕ��肵�Ȃ��̂ŁA�����瑤�Ŗ��������A���ʂ����s�ɂ���
                _table.Table.Invalid();
            }

            // �p�[�e�B�N���A���̓A�C�e�������Đ�
            ParticleType particle = _settings.ItemHitParticle;
            Vector3 position = transform.position + _settings.ItemHitParticleOffset;
            ParticleMessageSender.SendMessage(particle, position, transform);
        }
    }
}