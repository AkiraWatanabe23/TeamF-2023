using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alpha
{
    /// <summary>
    /// フィーバータイムに表示されるUIのクラス
    /// </summary>
    public class FerverUI : FerverHandler
    {
        [SerializeField] RawImage _ferverUI;

        protected override void OnAwakeOverride()
        {
            _ferverUI.enabled = false;
        }

        protected override void OnFerverTimeEnter()
        {
            _ferverUI.enabled = true;
        }

        protected override void OnFerverTimeExit()
        {
            _ferverUI.enabled = false;
        }
    }
}
