using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CameraMoverScript : MonoBehaviour
{
    /// <summary>
    /// WASD�F�O�㍶�E�̈ړ�
    /// QE�F�㏸�E���~
    /// �E�h���b�N�F�J������]
    /// ���h���b�N�F�O�㍶�E�̈ړ�
    /// �X�y�[�X�F�J��������̗L���E����
    /// P�F��]�����s���̏�Ԃɏ���������
    /// </summary>

    //�J�����̈ړ���
    [SerializeField, Range(0.1f, 10.0f)]
    private float _positionStep = 4.0f;

    //�}�E�X���x
    [SerializeField, Range(30.0f, 150.0f)]
    private float _mouseSensitive = 120.0f;

    //�J�����̗L������
    private bool _cameraMoveActive = true;
    //�J������transform
    private Transform _cameraTransform;
    //�}�E�X�̎n�_
    private Vector3 _startMousePos;
    //�J�����̉�]�̎n�_���
    private Vector3 _presentCamRotation;
    private Vector3 _presentCamPos;
    //������� Rotation
    private Quaternion _initalCamRotation;
    //UI���b�Z�[�W�\��
    private bool _uiMessageActive;

    void Start()
    {
        _cameraTransform = this.gameObject.transform;

        //�����]�ۑ�
        _initalCamRotation = this.gameObject.transform.rotation;
    }


    void Update()
    {
        //�J��������̗L������
        CamControlIsActive();

        if (_cameraMoveActive)
        {
            //��]�p�x�̃��Z�b�g
            ResetCameraRotation();
            //�J������]�@�}�E�X
            CameraRotationMouseControl();
            //�J�����̏c���ړ��@�}�E�X
            CameraSlideMouseControl();
            //�J�����̃��[�J���ړ��@�L�[
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
                //UI���b�Z�[�W�̕\��
                StartCoroutine(DisplayUiMessage());
            }
            Debug.Log("CamControl�F" + _cameraMoveActive);
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
            //(�ړ��J�n���W - �}�E�X�̌��ݒn���W) / �𑜓x�Ő�����
            float x = (_startMousePos.x - Input.mousePosition.x) / Screen.width;
            float y = (_startMousePos.y - Input.mousePosition.y) / Screen.height;

            //�N�_�J�n���W + �}�E�X�̕ω��� * �}�E�X���x
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
            //(�ړ��J�n���W - �}�E�X�̌��ݒn���W) / �𑜓x�Ő�����
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
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 3, 100, 20), "�J��������@�L��");
        }

        if (_cameraMoveActive == false)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 3, 100, 20), "�J��������@����");
        }
    }
}
