using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Alpha
{
    /// <summary>
    /// �Ȃ𐧌䂷�鑤�̃C���^�[�t�F�[�X
    /// </summary>
    public interface ITableControl
    {
        public void Valid(float timeLimit, ItemType order, UnityAction<OrderResult> onCatched = null);
        public void Invalid();
    }
}
