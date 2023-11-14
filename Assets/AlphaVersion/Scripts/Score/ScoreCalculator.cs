using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    using ActorScore = ScoreSettings.ActorScore;
    using static ScoreEventMessage;

    /// <summary>
    /// �X�R�A�̑����̃��b�Z�[�W���瑝������l���v�Z����@�\�̃N���X
    /// </summary>
    public class ScoreCalculator : MonoBehaviour
    {
        [SerializeField] InGameSettingsSO _settings;

        /// <summary>
        /// �X�R�A�̃e�[�u�����Q�Ƃ��A��������X�R�A�̒l��Ԃ�
        /// </summary>
        public int ToInt(ScoreEventMessage msg)
        {
            ScoreSettings settings = _settings.ScoreSettings;
            
            // �X�R�A�̔{���A�t�B�[�o�[�^�C�����ǂ����ŕς��
            float scoreRate = msg.State == EventState.Normal ? settings.DefaultScoreRate : settings.FeverScoreRate;

            // �C�x���g���N�������L�����N�^�[
            ActorScore actor = default;
            if      (msg.Actor == EventActor.Male) actor = settings.Male;
            else if (msg.Actor == EventActor.Female) actor = settings.Female;
            else if (msg.Actor == EventActor.Muscle) actor = settings.Muscle;
            else throw new System.Exception("�X�R�A�𑗐M�����L�������z��O: " + actor);

            // ����/���s�ő�������l�̊�l�����߂�
            float score = msg.Result == EventResult.Success ? actor.SuccessBonus : -actor.FailurePenalty;

            // �����̏ꍇ�̓X�R�A�{����������
            if (msg.Result == EventResult.Success) score *= scoreRate;

            return (int)score;
        }
    }
}
