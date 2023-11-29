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
        [SerializeField] DustClothShooter _dustClothShooter;
        [SerializeField] DamageReceiver _damage;

        HoldSEPlayer _holdSEPlayer = new();

        void OnEnable()
        {
            HandInputHandler.OnLeftClickDown += OnLeftClickDown;
            HandInputHandler.OnLeftClicking += OnLeftClicking;
            HandInputHandler.OnLeftClickUp += OnLeftClickUp;
            HandInputHandler.OnMouseWheelAxis += OnMouseWheelAxis;
            HandInputHandler.OnGetSelectKeyDown += OnGetSelectKeyDown;
            HandInputHandler.OnRightClickUp += OnRightClickUp;
        }

        void OnDisable()
        {
            HandInputHandler.OnLeftClickDown -= OnLeftClickDown;
            HandInputHandler.OnLeftClicking -= OnLeftClicking;
            HandInputHandler.OnLeftClickUp -= OnLeftClickUp;
            HandInputHandler.OnMouseWheelAxis -= OnMouseWheelAxis;
            HandInputHandler.OnGetSelectKeyDown -= OnGetSelectKeyDown;
            HandInputHandler.OnRightClickUp -= OnRightClickUp;
        }

        void OnLeftClickDown()
        {
            _mouseMovementChecker.SetStartingPoint();
            _powerCalculator.SetStartingPoint(_thrower.StackPoint);
            _itemCleaner.Clean();
        }

        void OnLeftClicking()
        {
            if (_thrower.StackCount > 0 && _mouseMovementChecker.IsMoved())
            {
                Vector3 start = _powerCalculator.StartingPoint;
                Vector3 end = _powerCalculator.PowerSizePoint;
                _powerVisualizer.Draw(start, end);
                _holdSEPlayer.HoldOn();
            }
            else
            {
                _powerVisualizer.Delete();
                _holdSEPlayer.HoldOff();
            }
        }

        void OnLeftClickUp()
        {
            if (!_damage.IsDamaged)
            {
                // �}�E�X�𓮂������ꍇ�͓�����B�������Ă��Ȃ��ꍇ�͐ςށB
                if (_mouseMovementChecker.IsMoved())
                {
                    // �_���[�W���󂯂Ă��钆�͂��̏�ɓ�����
                    Vector3 velocity = _powerCalculator.CalculateThrowVelocity();
                    _thrower.Throw(velocity);
                }
                else
                {
                    // �A�C�e�����ς߂Ȃ������ꍇ�̓v�[���ɖ߂�
                    ThrowedItem item = _itemSpawner.Spawn();
                    if (!_thrower.TryStack(item)) _itemSpawner.Release(item);
                }
            }

            // ��������
            _powerVisualizer.Delete();
        }

        void OnRightClickUp()
        {
            if (_damage.IsDamaged) return;

            // �A�C�e����ς�ł��Ȃ��ꍇ�̂�
            if (_thrower.StackCount == 0) _dustClothShooter.Shoot();
        }

        void OnMouseWheelAxis(float fov)
        {
            _itemSpawner.Select(fov);
        }

        void OnGetSelectKeyDown(KeyCode key)
        {
            if (!_damage.IsDamaged)
            {
                _itemSpawner.Select(key);
                // �A�C�e�����ς߂Ȃ������ꍇ�̓v�[���ɖ߂�
                ThrowedItem item = _itemSpawner.Spawn();
                if (!_thrower.TryStack(item)) _itemSpawner.Release(item);
            }
        }
    }
}