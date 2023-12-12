using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;
using UniRx.Triggers;
using UniRx;

namespace Alpha
{
    public class Ufo : MonoBehaviour
    {
        [SerializeField] Collider _collider;
        [Header("�����ړ����̒l")]
        [Tooltip("�ŏ��ɃX�e�[�W��܂ňړ����鎞��")]
        [SerializeField]
        private float _moveDuration = 1f;
        [Tooltip("�ړ���̃I�t�Z�b�g�i�����n�_����̈ړ��j")]
        [SerializeField]
        private Vector3 _moveOffset = default;

        [Tooltip("�ړ���ɏ㉺���镝")]
        [Min(0.1f)]
        [SerializeField]
        private float _upValue = 1f;

        [Header("�z���グ���̓����ɑ΂���l")]
        [Tooltip("UFO����x�ɍő傢���̃I�u�W�F�N�g�����o���邩")]
        [SerializeField]
        private int _maxCastCount = 10;
        [Tooltip("�z���グ�n�_�̃I�t�Z�b�g�iUFO��艺�̈ʒu�j")]
        [SerializeField]
        private Vector3 _suckUpOffset = Vector3.zero;
        [Tooltip("�I�u�W�F�N�g���z���グ����X�s�[�h")]
        [SerializeField]
        private float _suckUpDuration = 1f;

        [Header("�ė����̓����ɑ΂���l")]
        [Tooltip("�h���A�����鑬�x")]
        [SerializeField]
        private float _tweenSpeed = 1f;
        [Tooltip("�U�����󂯂Ă��������܂ł̎���")]
        [SerializeField]
        private float _activeFalseInterval = 2f;
        [Tooltip("�ė����̐U�ꕝ�i���j")]
        [SerializeField]
        private float _swayValue = 10f;
        [Tooltip("�ė����̗h�ꕝ�i��]�j")]
        [SerializeField]
        private float _rotateValue = 45f;

        private Transform _transform = default;

        /// <summary> �����ʒu </summary>
        private Vector3 _initPosition = Vector3.zero;
        /// <summary> BoxCast�p�̃p�����[�^ </summary>
        private Vector3 _halfExtents = Vector3.zero;

        /// <summary> �ړ��ς��ǂ��� </summary>
        private bool _isMoved = false;

        /// <summary> �z���グ����ɂ��������I�u�W�F�N�g���i�[���� </summary>
        private RaycastHit[] _suckUpDatas = default;

        private Sequence _sequence = default;

        private IEnumerator Start()
        {
            yield return Initialize();

            Movement();

            _collider.OnTriggerEnterAsObservable().Subscribe(c => 
            {
                if (c.gameObject.TryGetComponent(out ThrowedItem _)) { Crash(); }
            });
        }

        /// <summary> �����f�[�^�̃Z�b�g�A�b�v </summary>
        private IEnumerator Initialize()
        {
            _transform = transform;
            _initPosition = _transform.position;

            if (TryGetComponent(out BoxCollider collider)) { _halfExtents = collider.size / 2f; }
            else { _halfExtents = _transform.localScale; }

            _suckUpDatas = new RaycastHit[_maxCastCount];

            _isMoved = false;
#if UNITY_EDITOR
            Debug.Log("Finish Initialized");
#endif

            yield return null;
        }

        private void Movement()
        {
            _sequence ??= DOTween.Sequence();

            _sequence.
                AppendCallback(() =>
                {
                    Cri.PlaySE3D(_transform.position, "SE_UFO_1_Long");
                }).
                Append(_transform.DOMove(_initPosition + _moveOffset, _moveDuration)).
                AppendCallback(() =>
                {
                    _isMoved = true;
                    _transform.DOMoveY(_upValue, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                }).
                SetLink(gameObject);
        }

        private void Update()
        {
            if (!_isMoved) { return; }

            //����ΏےT��
            ItemSearch();
            //��U������i�ړ��O�A�ړ����͂��Ȃ��j
            AttackedSearch();
        }

        /// <summary> �z���グ��Ώۂ����邩�T�� </summary>
        private void ItemSearch()
        {
            var count = Physics.BoxCastNonAlloc(_transform.position, _halfExtents, Vector3.down, _suckUpDatas, Quaternion.identity);
            if (count > 0)
            {
                for (int i = 0; i < count; i++) { SuckUp(_suckUpDatas[i].collider.gameObject); }
            }
        }

        /// <summary> �z���グ�� </summary>
        private void SuckUp(GameObject target)
        {
            if (target.TryGetComponent(out ThrowedItem item))
            {
#if UNITY_EDITOR
                Debug.Log("������");
#endif
                //�����ɋz���グ����
                target.transform.
                    DOMove(_transform.position + _suckUpOffset, _suckUpDuration).
                    OnComplete(() =>
                    {
#if UNITY_EDITOR
                    Debug.Log("tween finish");
#endif
                    }).
                    SetLink(target);
            }
        }

        /// <summary> Player����̒ǌ����Ȃ������ׂ� </summary>
        private void AttackedSearch()
        {
            if (Physics.BoxCast(_transform.position, _halfExtents * 2, Vector3.up, out RaycastHit hit, Quaternion.identity))
            {
                if (hit.collider.gameObject.TryGetComponent(out ThrowedItem item)) { Crash(); }
            }
        }

        private void Crash()
        {
#if UNITY_EDITOR
            Debug.Log("�U�����󂯂��I�I�ė�");
#endif
            _sequence = DOTween.Sequence();
            _sequence.
                AppendCallback(() =>
                {
                    //_transform.DOMoveX(_swayValue, _tweenSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                    //_transform.DOMoveY(-1f, _tweenSpeed).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
                    _transform.DOMove(_initPosition, _tweenSpeed).SetEase(Ease.Linear); // ���̈ʒu�ɖ߂�

                    _transform.DORotate(new Vector3(0f, 0f, _rotateValue), _tweenSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
                }).
                AppendInterval(_activeFalseInterval).
                AppendCallback(() =>
                {
                    ChangeActiveSelf(false);
                });
        }

        private void ChangeActiveSelf(bool flag) { gameObject.SetActive(flag); }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, _halfExtents * 2);
        }
    }
}
