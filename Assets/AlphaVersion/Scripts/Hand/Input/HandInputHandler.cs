using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Alpha
{
    /// <summary>
    /// 手の操作に関する入力を受け付けるクラス。
    /// ゲーム開始のタイミングで有効化、ゲームオーバーのタイミングで無効化される。
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
        /// ASDFGの5つのキーを入力した場合のみ呼ばれるコールバック
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
        /// 有効状態のときにUpdateのタイミングで各種入力を受け付ける。
        /// </summary>
        protected override void OnUpdateOverride()
        {
            // マウスクリック
            if (Input.GetMouseButtonDown(0)) OnLeftClickDown?.Invoke();
            else if (Input.GetMouseButton(0)) OnLeftClicking?.Invoke();
            else if (Input.GetMouseButtonUp(0)) OnLeftClickUp?.Invoke();
            else if (Input.GetMouseButtonDown(1)) OnRightClickDown?.Invoke();
            else if (Input.GetMouseButtonUp(1)) OnRightClickUp?.Invoke();

            // キー入力
            else if (Input.GetKeyDown(KeyCode.A)) OnGetSelectKeyDown?.Invoke(KeyCode.A);
            else if (Input.GetKeyDown(KeyCode.S)) OnGetSelectKeyDown?.Invoke(KeyCode.S);
            else if (Input.GetKeyDown(KeyCode.D)) OnGetSelectKeyDown?.Invoke(KeyCode.D);
            else if (Input.GetKeyDown(KeyCode.F)) OnGetSelectKeyDown?.Invoke(KeyCode.F);
            else if (Input.GetKeyDown(KeyCode.G)) OnGetSelectKeyDown?.Invoke(KeyCode.G);

            // マウスホイール操作
            OnMouseWheelAxis?.Invoke(Input.GetAxis("Mouse ScrollWheel"));
        }
    }
}