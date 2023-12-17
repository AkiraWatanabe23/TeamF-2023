using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;

namespace Alpha
{
    public enum RagDollType
    {
        Female,
        FemaleOnkou,
        FemaleTanki,
        Male,
        MaleOnkou,
        MaleTanki,
    }

    /// <summary>
    /// ���O�h�[���𐶐�����ۂɌ��ƂȂ郂�f���Ƃ��̎�ށA�Ԃ����������𑗎�M����
    /// </summary>
    public struct RagDollMessage
    {
        public RagDollType Type;
        public Transform Model;
        public Vector3 HitPosition;
    }

    /// <summary>
    /// ���O�h�[���̐������s���N���X
    /// </summary>
    public class RagDollSpawner : MonoBehaviour
    {
        static Transform _parent;

        [SerializeField] GameObject _malePrefab;
        [SerializeField] GameObject _maleOnkouPrefab;
        [SerializeField] GameObject _maleTankiPrefab;
        [SerializeField] GameObject _femalePrefab;
        [SerializeField] GameObject _femaleOnkouPrefab;
        [SerializeField] GameObject _femaleTankiPrefab;
        [SerializeField] int _max = 5;
        [SerializeField] float _lifeTime = 5.0f;
        [Header("���O�h�[����������ԍۂɉ�����")]
        [SerializeField] float _power = 10.0f;
        [SerializeField] float _upPower = 10.0f;

        Dictionary<RagDollType, GameObject> _table = new();
        Queue<GameObject> _ragDolls = new();

        void Awake()
        {
            if (_parent == null) _parent = new GameObject("RagDollParent").transform;
            
            CreateTable();
            MessageReceive();
        }

        void OnDestroy()
        {
            if (_parent != null) Destroy(_parent);
        }

        /// <summary>
        /// �j�Ə��̃��O�h�[���������ɒǉ�
        /// </summary>
        void CreateTable()
        {
            _table.Add(RagDollType.Male, _malePrefab);
            _table.Add(RagDollType.MaleOnkou, _maleOnkouPrefab);
            _table.Add(RagDollType.MaleTanki, _maleTankiPrefab);
            _table.Add(RagDollType.Female, _femalePrefab);
            _table.Add(RagDollType.FemaleOnkou, _femaleOnkouPrefab);
            _table.Add(RagDollType.FemaleTanki, _femaleTankiPrefab);
        }

        /// <summary>
        /// ���b�Z�[�W����M�����ꍇ�ɐ�������
        /// </summary>
        void MessageReceive()
        {
            MessageBroker.Default.Receive<RagDollMessage>()
                .Subscribe(msg => TrySpawn(msg.Type, msg.Model, msg.HitPosition)).AddTo(gameObject);
        }

        void TrySpawn(RagDollType type, Transform model, Vector3 hitPosition)
        {
            // �ő吔�ɒB���Ă������ԌÂ����̂��폜����
            if (_max <= _ragDolls.Count) Destroy(_ragDolls.Dequeue());

            if (_table.TryGetValue(type, out GameObject prefab))
            {
                // �L���[�ɒǉ����Đ������Ɏ擾�ł���悤�ɂ��Ă���
                GameObject ragDoll = Instantiate(prefab, model.position, model.rotation, _parent);
                _ragDolls.Enqueue(ragDoll);

                // �������xz���ʏ�̔�΂������ɑ΂��ė͂�������
                Vector3 dir = (model.position - hitPosition).normalized;
                Rigidbody[] children = ragDoll.GetComponentsInChildren<Rigidbody>();
                foreach (Rigidbody rb in children)
                {
                    rb.AddForce(Vector3.up * _upPower + (dir * _power), ForceMode.Impulse);
                }

                // ��莞�Ԍ�ɑ傫����0�ɂ��ĉ�ʊO�ɒǂ��o�����Ƃŉf��Ȃ�����
                DOVirtual.DelayedCall(_lifeTime, () => 
                {
                    ragDoll.transform.localScale = Vector3.zero;
                    ragDoll.transform.position = Vector3.one * 1000.0f;
                }).SetLink(ragDoll);
            }
        }
    }
}
