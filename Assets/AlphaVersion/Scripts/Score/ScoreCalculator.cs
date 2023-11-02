using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    using static ScoreEventMessage;

    /// <summary>
    /// �X�R�A�̑����̃��b�Z�[�W���瑝������l���v�Z����@�\�̃N���X
    /// </summary>
    public class ScoreCalculator : MonoBehaviour
    {
        [SerializeField] ScoreTableSO _table;

        /// <summary>
        /// �X�R�A�̃e�[�u�����Q�Ƃ��A��������X�R�A�̒l��Ԃ�
        /// </summary>
        public int ToInt(ScoreEventMessage msg)
        {
            // �X�R�A�̔{���A�t�B�[�o�[�^�C�����ǂ����ŕς��
            float scoreRate = msg.State == EventState.Normal ? _table.DefaultScoreRate : _table.FeverScoreRate;

            // �C�x���g���N�������L�����N�^�[
            ScoreTableSO.Actor actor = msg.Actor == EventActor.Male ? _table.Male :
                                       msg.Actor == EventActor.Female ? _table.Female :
                                                                        _table.Muscle;
            // ����/���s�ő�������l�̊�l�����߂�
            float score = msg.Result == EventResult.Success ? actor.SuccessBonus : -actor.FailurePenalty;

            // �����̏ꍇ�̓X�R�A�{����������
            if (msg.Result == EventResult.Success) score *= scoreRate;

            return (int)score;
        }
    }
}
