using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Alpha
{
    /// <summary>
    /// 席を制御する側のインターフェース
    /// </summary>
    public interface ITableControl
    {
        public void Valid(float timeLimit, ItemType order, UnityAction<OrderResult> onCatched = null);
        public void Invalid();
    }
}
