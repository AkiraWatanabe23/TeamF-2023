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

        /// <summary>
        /// ASDFG��5�̃L�[����͂����ꍇ�̂݌Ă΂��R�[���o�b�N
        /// </summary>
        public static event UnityAction<KeyCode> OnGetSelectKeyDown;

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
            else if (Input.GetMouseButton(0)) OnLeftClicking?.Invoke();
            else if (Input.GetMouseButtonUp(0)) OnLeftClickUp?.Invoke();
            else if (Input.GetMouseButtonDown(1)) OnRightClickDown?.Invoke();
            else if (Input.GetMouseButtonUp(1)) OnRightClickUp?.Invoke();

            // �L�[����
            else if (Input.GetKeyDown(KeyCode.A)) OnGetSelectKeyDown?.Invoke(KeyCode.A);
            else if (Input.GetKeyDown(KeyCode.S)) OnGetSelectKeyDown?.Invoke(KeyCode.S);
            else if (Input.GetKeyDown(KeyCode.D)) OnGetSelectKeyDown?.Invoke(KeyCode.D);
            else if (Input.GetKeyDown(KeyCode.F)) OnGetSelectKeyDown?.Invoke(KeyCode.F);
            else if (Input.GetKeyDown(KeyCode.G)) OnGetSelectKeyDown?.Invoke(KeyCode.G);

            // �}�E�X�z�C�[������
            OnMouseWheelAxis?.Invoke(Input.GetAxis("Mouse ScrollWheel"));
        }
    }
}