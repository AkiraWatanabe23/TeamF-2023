using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// ゲーム開始のタイミングで有効化、
    /// ゲームオーバーのタイミングで無効化される機能を提供するクラス。
    /// このクラスを継承する場合は OnEnable OnDisable Update は使用しないこと。
    /// </summary>
    public class ValidStateHandler : MonoBehaviour
    {
        bool _isValid = true; // ここ修正

        void OnEnable()
        {
            //InGame.OnGameStart += () => _isValid = true;
            //InGame.OnGameOver += () => _isValid = false;

            OnEnableOverride();
        }

        void OnDisable()
        {
            //InGame.OnGameStart -= () => _isValid = true;
            //InGame.OnGameOver -= () => _isValid = false;

            OnDisableOverride();
        }

        void Update()
        {
            if (_isValid)
            {
                OnUpdateOverride();
            }
        }

        protected virtual void OnEnableOverride() { }
        protected virtual void OnDisableOverride() { }
        protected virtual void OnUpdateOverride() { }
    }
}
