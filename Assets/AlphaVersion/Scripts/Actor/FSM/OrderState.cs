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
        [SerializeField] ActorSettingsSO _settings;
        [SerializeField] Collider _collider;

        EmptyTable _table;

        public override StateType Type => StateType.Order;
        
        /// <summary>
        /// ���ʂ��m�肵��(����/���s)�ꍇ�͊O�����玟�̃X�e�[�g�ɑJ�ڂ�����
        /// </summary>
        public OrderResult Result { get; private set; }

        protected override void OnAwakeOverride()
        {
            // ���̃X�e�[�g��Stay�̍ۂ͓����蔻�肪�L���ɂȂ�
            // Init�͏����������x�ɌĂ΂��̂�Awake�̃^�C�~���O��1�x�����o�^����
            this.OnCollisionEnterAsObservable().Where(_ => CurrentStage == Stage.Stay).Subscribe(OnItemHit);
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
            Cri.PlaySE("SE_ChinBell");

            // �Ȃ�L�����A���Ԑ؂�(���s)�������̓L���b�`����(����)�ŃR�[���o�b�N���Ă΂��
            _table.Table.Valid(_settings.OrderTimeLimit, _settings.RandomOrder, result => 
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
            Vector3 dir = _table.Position - transform.position;
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
            if (!collision.gameObject.TryGetComponent(out ThrowedItem _)) return;

            // �A�C�e�����Ԃ������ꍇ�͐ȑ��Ŕ��肵�Ȃ��̂ŁA�����瑤�Ŗ��������A���ʂ����s�ɂ���
            _table.Table.Invalid();
            Result = OrderResult.Failure;

            // ���ƃp�[�e�B�N��
            Cri.PlaySE("SE_OrderHit");
            ParticleType particle = _settings.ItemHitParticle;
            Vector3 position = transform.position + _settings.ItemHitParticleOffset;
            ParticleMessageSender.SendMessage(particle, position, transform);
        }
    }
}