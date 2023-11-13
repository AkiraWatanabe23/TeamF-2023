using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Alpha
{
    /// <summary>
    /// �^���u���E�B�[�h�𐶐�����N���X
    /// ��̈ʒu������͈͓��Ƀ����_���ɐ�������
    /// </summary>
    public class TumbleweedSpawner : MonoBehaviour
    {
        [SerializeField] TumbleweedCreator _creator;
        [Header("�����̐ݒ�")]
        [SerializeField] Transform _spawnPoint;
        [SerializeField] float _radius = 0.5f;
        [SerializeField] int _quantity = 10;
        [Header("���I�p���p������(�b)")]
        [SerializeField] float _prevDelay = 2.0f;
        [Header("1�������閈�̃f�B���C(�b)")]
        [SerializeField] float _stepDelay;

        CancellationTokenSource _cts = new();
        bool _isRunning; // �M�~�b�N���ɍēx�Ăяo���Ȃ��悤�Ƀt���O

        void OnDestroy()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }

        /// <summary>
        /// �񓹊��Ń^���u���E�B�[�h�𐶐�����M�~�b�N
        /// </summary>
        public void Spawn()
        {
            // ���ɃM�~�b�N���s���Ȃ̂Œe��
            if (_isRunning)
            {
                Debug.LogWarning("���Ƀ_���u���E�B�[�h�̃M�~�b�N���Ȃ̂ŃL�����Z�����ꂽ: " + Time.time);
                return;
            }

            _cts = new();
            SpawnAsync(_cts.Token).Forget();
        }

        /// <summary>
        /// ��n�_���烉���_���Ȉʒu�ɐ�������
        /// </summary>
        async UniTaskVoid SpawnAsync(CancellationToken token)
        {
            _isRunning = true;

            // �p���p��SE�Đ���A�����SE�Đ�
            Cri.PlaySE("SE_TumbleweedPrev");
            await UniTask.Delay(System.TimeSpan.FromSeconds(_prevDelay), cancellationToken: token);
            Cri.PlaySE("SE_TumbleweedFall");

            for (int i = 0; i < _quantity; i++)
            {
                RentTumbleweed();
                await UniTask.Delay(System.TimeSpan.FromSeconds(_stepDelay), cancellationToken: token);
            }

            _isRunning = false;
        }

        /// <summary>
        /// �^���u���E�B�[�h���v�[��������o���ė���������
        /// </summary>
        void RentTumbleweed()
        {
            // �����_���Ȉʒu
            float x = Random.Range(-_radius, _radius);
            float z = Random.Range(-_radius, _radius);
            Vector3 spawnPosition = _spawnPoint.position + new Vector3(x, 0, z);

            // �v�[��������o���ė���
            Tumbleweed t = _creator.Create();
            t.transform.position = spawnPosition;
            t.Fall();
        }
    }
}
