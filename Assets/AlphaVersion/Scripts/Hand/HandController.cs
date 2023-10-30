using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 各種コンポーネントの機能を組み合わせて手の操作を行うコンポーネント
    /// </summary>
    public class HandController : MonoBehaviour
    {
        [SerializeField] Thrower _thrower;
        [SerializeField] ThrowPowerCalculator _powerCalculator;
        [SerializeField] ThrowPowerVisualizer _powerVisualizer;
        [SerializeField] SelectedItemSpawner _itemSpawner;
        [SerializeField] ThrowedItemCleaner _itemCleaner;
        [SerializeField] MouseMovementChecker _mouseMovementChecker;

        void OnEnable()
        {
            HandInputHandler.OnLeftClickDown += OnClickDown;
            HandInputHandler.OnLeftClicking += OnClicking;
            HandInputHandler.OnLeftClickUp += OnClickUp;
            HandInputHandler.OnMouseWheelAxis += OnMouseWheelAxis;
            HandInputHandler.OnGetSelectKeyDown += OnGetSelectKeyDown;

            // 左右移動ハンドラ
            // 右クリックハンドラ
        }

        void OnDisable()
        {
            HandInputHandler.OnLeftClickDown -= OnClickDown;
            HandInputHandler.OnLeftClicking -= OnClicking;
            HandInputHandler.OnLeftClickUp -= OnClickUp;
            HandInputHandler.OnMouseWheelAxis -= OnMouseWheelAxis;
            HandInputHandler.OnGetSelectKeyDown -= OnGetSelectKeyDown;
        }

        void OnClickDown()
        {
            _mouseMovementChecker.SetStartingPoint();
            _powerCalculator.SetStartingPoint(_thrower.StackPoint);
            _itemCleaner.Clean();
        }

        void OnClicking()
        {
            if (_thrower.StackCount > 0 && _mouseMovementChecker.IsMoved())
            {
                Vector3 start = _powerCalculator.StartingPoint;
                Vector3 end = _powerCalculator.PowerSizePoint;
                _powerVisualizer.Draw(start, end);
            }
            else
            {
                _powerVisualizer.Delete();
            }
        }

        void OnClickUp()
        {
            // マウスを動かした場合は投げる。動かしていない場合は積む。
            if (_mouseMovementChecker.IsMoved())
            {
                Vector3 velocity = _powerCalculator.CalculateThrowVelocity();
                _thrower.Throw(velocity);
            }
            else
            {
                // アイテムが積めなかった場合はプールに戻す
                ThrowedItem item = _itemSpawner.Spawn();
                if (!_thrower.TryStack(item)) _itemSpawner.Release(item);
            }

            // 矢印を消す
            _powerVisualizer.Delete();
        }

        void OnMouseWheelAxis(float fov)
        {
            _itemSpawner.Select(fov);
        }

        void OnGetSelectKeyDown(KeyCode key)
        {
            _itemSpawner.Select(key);
        }
    }
}

// アイテムを選択する処理
// 