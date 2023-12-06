using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Alpha
{
    public class Ufo : MonoBehaviour
    {
        [Tooltip("UFO����x�ɍő傢���̃I�u�W�F�N�g�����o���邩")]
        [SerializeField]
        private int _maxCastCount = 10;
        [SerializeField]
        private Animator _animator = default;
        [Tooltip("�z���グ�n�_��offset")]
        [SerializeField]
        private Vector3 _offset = Vector3.zero;
        [Tooltip("�ړ���̃S�[��")]
        public Transform _moveTarget = default;

        private Transform _transform = default;
        private Vector3 _halfExtents = Vector3.zero;
        private bool _isMoved = false;

        private RaycastHit[] _suckUpDatas = default;

        private IEnumerator Start()
        {
            yield return Initialize();

            Movement();
        }

        /// <summary> �����f�[�^�̃Z�b�g�A�b�v </summary>
        private IEnumerator Initialize()
        {
            _transform = transform;

            if (_animator == null)
            {
                if (!gameObject.TryGetComponent(out _animator)) { _animator = gameObject.AddComponent<Animator>(); }
            }

            if (gameObject.TryGetComponent(out BoxCollider collider)) { _halfExtents = collider.size / 2f; }
            else { _halfExtents = _transform.localScale; }

            _suckUpDatas = new RaycastHit[_maxCastCount];

            _isMoved = false;

            yield return null;
        }

        private void Movement()
        {
            transform.
                DOMove(_moveTarget.position, 2f).
                OnComplete(() =>
                {
                    ItemSearch();
                    _isMoved = true;
                }).
                SetLink(gameObject);
        }

        private void Update()
        {
            if (!_isMoved) { return; }

            //��U������
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
                    DOMove(_transform.position + _offset, 1f).
                    OnComplete(() =>
                    {
                        Debug.Log("tween finish");
                        target.SetActive(false);
                    }).
                    SetLink(target);
            }
        }

        private void PlayAnimation(string animationName)
        {
            var clips = _animator.runtimeAnimatorController.animationClips;
            //�n���ꂽ���O��State��Animator�Ɋ܂܂�Ă��邩���ׂ�
            bool containsTargetState = false;
            foreach (var clip in clips)
            {
                if (clip.name == animationName) { containsTargetState = true; break; }
            }
            if (!containsTargetState) { Debug.Log($"Animation Play failed : {animationName} State is not found."); return; }

            _animator.Play(animationName);
        }

        /// <summary> Player����̒ǌ����Ȃ������ׂ� </summary>
        private void AttackedSearch()
        {
            if (Physics.BoxCast(_transform.position, _halfExtents, Vector3.zero, out RaycastHit hit, Quaternion.identity))
            {
                if (hit.collider.gameObject.TryGetComponent(out ThrowedItem _))
                {
#if UNITY_EDITOR
                    Debug.Log("�U�����󂯂��I�I�ė�");
#endif
                    PlayAnimation("Crash");
                }
            }
        }

        /// <summary> AnimationEvent�Ƃ��ŌĂяo���p </summary>
        public void ChangeActive(bool flag) { gameObject.SetActive(flag); }
    }
}
