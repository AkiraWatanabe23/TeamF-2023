using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CameraMoverScript : MonoBehaviour
{
    /// <summary>
    /// WASD：前後左右の移動
    /// QE：上昇・下降
    /// 右ドラック：カメラ回転
    /// 左ドラック：前後左右の移動
    /// スペース：カメラ操作の有効・無効
    /// P：回転を実行時の状態に初期化する
    /// </summary>

    //カメラの移動量
    [SerializeField, Range(0.1f, 10.0f)]
    private float _positionStep = 4.0f;

    //マウス感度
    [SerializeField, Range(30.0f, 150.0f)]
    private float _mouseSensitive = 120.0f;

    //カメラの有効無効
    private bool _cameraMoveActive = true;
    //カメラのtransform
    private Transform _cameraTransform;
    //マウスの始点
    private Vector3 _startMousePos;
    //カメラの回転の始点情報
    private Vector3 _presentCamRotation;
    private Vector3 _presentCamPos;
    //初期状態 Rotation
    private Quaternion _initalCamRotation;
    //UIメッセージ表示
    private bool _uiMessageActive;

    void Start()
    {
        _cameraTransform = this.gameObject.transform;

        //初回回転保存
        _initalCamRotation = this.gameObject.transform.rotation;
    }


    void Update()
    {
        //カメラ操作の有効無効
        CamControlIsActive();

        if (_cameraMoveActive)
        {
            //回転角度のリセット
            ResetCameraRotation();
            //カメラ回転　マウス
            CameraRotationMouseControl();
            //カメラの縦横移動　マウス
            CameraSlideMouseControl();
            //カメラのローカル移動　キー
            CameraPositionKeyControl();
        }
    }

    public void CamControlIsActive()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _cameraMoveActive = !_cameraMoveActive;

            if (_uiMessageActive == false)
            {
                //UIメッセージの表示
                StartCoroutine(DisplayUiMessage());
            }
            Debug.Log("CamControl：" + _cameraMoveActive);
        }
    }

    private void ResetCameraRotation()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            this.gameObject.transform.rotation = _initalCamRotation;
            Debug.Log("Cam Rotate : " + _initalCamRotation.ToString());
        }
    }

    private void CameraRotationMouseControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startMousePos = Input.mousePosition;
            _presentCamRotation.x = _cameraTransform.transform.eulerAngles.x;
            _presentCamRotation.y = _cameraTransform.transform.eulerAngles.y;
        }

        if (Input.GetMouseButton(0))
        {
            //(移動開始座標 - マウスの現在地座標) / 解像度で正当化
            float x = (_startMousePos.x - Input.mousePosition.x) / Screen.width;
            float y = (_startMousePos.y - Input.mousePosition.y) / Screen.height;

            //起点開始座標 + マウスの変化量 * マウス感度
            float eulerX = _presentCamRotation.x + y * _mouseSensitive;
            float eulerY = _presentCamRotation.y + x * _mouseSensitive * -1;

            //_cameraTransform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
            _cameraTransform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
        }
    }

    private void CameraSlideMouseControl()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _startMousePos = Input.mousePosition;
            _presentCamPos = _cameraTransform.position;
        }

        if (Input.GetMouseButton(1))
        {
            //(移動開始座標 - マウスの現在地座標) / 解像度で正当化
            float x = (_startMousePos.x - Input.mousePosition.x) / Screen.width;
            float y = (_startMousePos.y - Input.mousePosition.y) / Screen.height;

            x = x * _positionStep;
            y = y * _positionStep;

            Vector3 velocity = _cameraTransform.rotation * new Vector3(x, y, 0);
            velocity = velocity + _presentCamPos;
            _cameraTransform.position = velocity;
        }
    }

    private void CameraPositionKeyControl()
    {
        Vector3 campos = _cameraTransform.position;

        if (Input.GetKey(KeyCode.D)) { campos += _cameraTransform.right * Time.deltaTime * _positionStep; }
        if (Input.GetKey(KeyCode.A)) { campos -= _cameraTransform.right * Time.deltaTime * _positionStep; }
        if (Input.GetKey(KeyCode.Q)) { campos += _cameraTransform.up * Time.deltaTime * _positionStep; }
        if (Input.GetKey(KeyCode.E)) { campos -= _cameraTransform.up * Time.deltaTime * _positionStep; }
        if (Input.GetKey(KeyCode.W)) { campos += _cameraTransform.forward * Time.deltaTime * _positionStep; }
        if (Input.GetKey(KeyCode.S)) { campos -= _cameraTransform.forward * Time.deltaTime * _positionStep; }

        _cameraTransform.position = campos;
    }

    private IEnumerator DisplayUiMessage()
    {
        _uiMessageActive = true;
        float time = 0;
        while (time < 2)
        {
            time = time + Time.deltaTime;
            yield return null;
        }
        _uiMessageActive = false;
    }

    void OnGUI()
    {
        if (_uiMessageActive == false) { return; }
        GUI.color = Color.black;
        if (_cameraMoveActive == true)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 3, 100, 20), "カメラ操作　有効");
        }

        if (_cameraMoveActive == false)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 3, 100, 20), "カメラ操作　無効");
        }
    }
}
