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
        [SerializeField] ActorSettingsSO _female;
        [SerializeField] ActorSettingsSO _femaleOnkou;
        [SerializeField] ActorSettingsSO _femaleTanki;
        [SerializeField] ActorSettingsSO _male;
        [SerializeField] ActorSettingsSO _maleOnkou;
        [SerializeField] ActorSettingsSO _maleTanki;
        [SerializeField] ActorSettingsSO _robber;

        /// <summary>
        /// �X�R�A�̃e�[�u�����Q�Ƃ��A��������X�R�A�̒l��Ԃ�
        /// </summary>
        public int ToInt(ScoreEventMessage msg)
        {
            ActorSettingsSO so = null;
            if (msg.Key == ScoreKey.Female) { so = _female; }
            if (msg.Key == ScoreKey.FemaleOnkou) so = _femaleOnkou;
            if (msg.Key == ScoreKey.FemaleTanki) so = _femaleTanki;
            if (msg.Key == ScoreKey.Male) { so = _male; }
            if (msg.Key == ScoreKey.MaleOnkou) so = _maleOnkou;
            if (msg.Key == ScoreKey.MaleTanki) so = _maleTanki;
            if (msg.Key == ScoreKey.Robber) { so = _robber; }

            if (so == null) { return -1; }

            float add = msg.Result == EventResult.Success ? so.ActorParamsSet.IncreaseScore : 
                                                            -so.ActorParamsSet.DecreaseScore;
            //// �X�R�A�̔{���A�t�B�[�o�[�^�C�����ǂ����ŕς��
            float scoreRate = msg.State == EventState.Normal ? 1 : so.ActorParamsSet.FeverScoreRate;

            if (msg.Result == EventResult.Success) add *= scoreRate;

            return (int)add;
        }
    }
}