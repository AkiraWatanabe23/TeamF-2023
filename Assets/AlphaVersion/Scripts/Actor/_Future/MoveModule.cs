using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Alpha
{
    /// <summary>
    /// �O��̒n�_�Ɉړ�����@�\�̃N���X
    /// ���݂̒n�_�̏�񂵂��ێ����Ă��Ȃ��̂ŁA���ɂǂ̒n�_�Ɉړ������
    /// �ǂ��̂����O���ŊǗ����A�ړ�����ۂɓn���Ă��K�v������B
    /// </summary>
    public class MoveModule : MonoBehaviour
    {
        [SerializeField] MoveBetween _moveBetween;

        Waypoint _current;
        ActorSettingsSO _settings;
        CancellationTokenSource _cts;

        /// <summary>
        /// �o�H�̐擪��n�����Ƃŏ���������
        /// </summary>
        public void Init(Waypoint lead, ActorSettingsSO settings)
        {
            _current = lead;
            _settings = settings;

            // �o�H�̐擪�ɏ����z�u����
            transform.position = lead.Position;
        }

        /// <summary>
        /// ���̒n�_�܂ňړ�����A�Ō�̒n�_�̏ꍇ�͂��̏�Ɉړ�
        /// ���̒n�_����������ꍇ�͐擪�̒n�_��I������̂Œ���
        /// </summary>
        public async UniTask MoveToNextAsync(int index = 0)
        {
            Waypoint next = _current.IsFinal ? _current : _current.Next[index];
            _current = await MoveToAsync(next, _cts.Token);
        }

        /// <summary>
        /// �O�̒n�_�܂ňړ�����A�ŏ��̒n�_�̏ꍇ�͂��̏�Ɉړ�
        /// �O�̒n�_����������ꍇ�͐擪�̒n�_��I������̂Œ���
        /// </summary>
        public async UniTask MoveToPrevAsync(int index = 0)
        {
            Waypoint prev = _current.IsLead ? _current : _current.Prev[index];
            _current = await MoveToAsync(prev, _cts.Token);
        }

        /// <summary>
        /// ���݂̈ʒu����w�肵���n�_�܂ňړ�����
        /// </summary>
        /// <returns>���̒n�_</returns>
        async UniTask<Waypoint> MoveToAsync(Waypoint to, CancellationToken token)
        {
            await _moveBetween.MoveAsync(_settings.MoveSpeed, transform.position, to.Position, token);
            return to; // �ړ��������Ɍ��݂̒n�_���X�V
        }
    }
}


// ����N:��/�O�̃E�F�C�|�C���g�������������ꍇ�ǂ�����H�O������w�肵�Ȃ��Ƃ����Ȃ��B
// ���Ɉړ�->�L�����Z��->���Ɉړ� ���ƃo�O��Ȃ��H
// 