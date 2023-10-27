using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Alpha
{
    /// <summary>
    /// ��̑���Ɋւ�����͂��󂯕t����N���X�B
    /// �Q�[���J�n�̃^�C�~���O�ŗL�����A�Q�[���I�[�o�[�̃^�C�~���O�Ŗ����������B
    /// </summary>
    public class HandInputHandler : ValidStateHandler
    {
        public static event UnityAction OnLeftClickDown;
        public static event UnityAction OnLeftClicking;
        public static event UnityAction OnLeftClickUp;
        public static event UnityAction OnRightClickDown;
        public static event UnityAction OnRightClickUp;
        public static event UnityAction<float> OnMouseWheelAxis;

        void OnDestroy()
        {
            OnLeftClickDown = null;
            OnLeftClickUp = null;
            OnRightClickDown = null;
            OnRightClickUp = null;
            OnMouseWheelAxis = null;
        }

        /// <summary>
        /// �L����Ԃ̂Ƃ���Update�̃^�C�~���O�Ŋe����͂��󂯕t����B
        /// </summary>
        protected override void OnUpdateOverride()
        {
            // �}�E�X�N���b�N
            if (Input.GetMouseButtonDown(0)) OnLeftClickDown?.Invoke();
            if (Input.GetMouseButton(0)) OnLeftClicking?.Invoke();
            if (Input.GetMouseButtonUp(0)) OnLeftClickUp?.Invoke();
            if (Input.GetMouseButtonDown(1)) OnRightClickDown?.Invoke();
            if (Input.GetMouseButtonUp(1)) OnRightClickUp?.Invoke();

            // �}�E�X�z�C�[������
            OnMouseWheelAxis?.Invoke(Input.GetAxis("Mouse ScrollWheel"));
        }
    }
}