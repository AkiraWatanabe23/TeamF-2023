using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �e��R���|�[�l���g�̋@�\��g�ݍ��킹�Ď�̑�����s���R���|�[�l���g
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

            // ���E�ړ��n���h��
            // �E�N���b�N�n���h��
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
            // �}�E�X�𓮂������ꍇ�͓�����B�������Ă��Ȃ��ꍇ�͐ςށB
            if (_mouseMovementChecker.IsMoved())
            {
                Vector3 velocity = _powerCalculator.CalculateThrowVelocity();
                _thrower.Throw(velocity);
            }
            else
            {
                // �A�C�e�����ς߂Ȃ������ꍇ�̓v�[���ɖ߂�
                ThrowedItem item = _itemSpawner.Spawn();
                if (!_thrower.TryStack(item)) _itemSpawner.Release(item);
            }

            // ��������
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

// �A�C�e����I�����鏈��
// 