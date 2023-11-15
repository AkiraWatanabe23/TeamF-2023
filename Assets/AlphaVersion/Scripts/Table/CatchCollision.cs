using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

namespace Alpha
{
    /// <summary>
    /// �������ꂽ�A�C�e�����L���b�`���锻��̃N���X
    /// ���炩���߁A������̃A�C�e�����`�F�b�N����Observable���쐬���Ă����A
    /// �҂ۂɃA�C�e�����w�肵�A�L���b�`���肪�����܂ő҂B
    /// </summary>
    public class CatchCollision : MonoBehaviour
    {
        [SerializeField] InGameSettingsSO _settings;

        IObservable<Collider> _onTriggerStay;
        // Observable�̍쐬��Awake���_�ōs���̂ɑ΂���
        // �����͑ҋ@���Ɍ��܂�̂Ń����o�Ƃ��ĕێ����Ă����K�v������B
        ItemType _order;

        void Awake()
        {
            // �q�I�u�W�F�N�g����R���C�_�[���擾���A
            // ����������ɐڐG���Ă���ꍇ�͑Ώۂ��`�F�b�N����B
            Collider collider = GetComponentInChildren<Collider>();
            _onTriggerStay = collider.OnTriggerStayAsObservable().Where(c => Check(c));
        }

        /// <summary>
        /// ���������A�C�e�����A�L���b�`�\�ȑ��x�̏ꍇ�́A�����Ƃ���B
        /// �A�C�e�����̃L���b�`�����ۂ̃��\�b�h���Ăяo���B
        /// </summary>
        bool Check(Collider collider)
        {
            if (collider.TryGetComponent(out ICatchable catchable))
            {
                if (catchable.Type == _order && catchable.SqrSpeed <= _settings.CatchSettings.CatchableSpeed)
                {
                    catchable.Catch();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// �A�C�e�����w�肵�āA�L���b�`����܂ő҂�
        /// </summary>
        /// <returns>�A�C�e�����L���b�`: ����</returns>
        public async UniTask<OrderResult> WaitAsync(ItemType order, CancellationToken token)
        {
            _order = order;

            await _onTriggerStay.ToUniTask(useFirstValue: true, cancellationToken: token);

            return OrderResult.Success;
        }
    }
}
