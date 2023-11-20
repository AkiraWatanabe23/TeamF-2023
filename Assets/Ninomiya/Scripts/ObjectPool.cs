using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField, Header("��������ꏊ")] private Transform[] _transforms;

    [SerializeField, Header("���������_���u���E�B�[�h���i�[")] private List<GameObject> _objectPool;

    [SerializeField, Header("�����o����")] private int _maxCount;

    [SerializeField, Header("����������object�Ɖ�������������")] private List<ObjectData> _objectDatas;

    [SerializeField] private float _gravity = 9.8f;

    float _time;

    private void Awake()
    {
        CreatePool();
    }
    private void Update()
    {
        _time += Time.deltaTime;
        //ApplyGravityToObjects();
    }
    public void ReturnObject()
    {
        for(int i = 0; i < _maxCount; i++)
        {
            GetDumbleweed(this.transform.position).transform.position =
                _transforms[Random.Range(0, _transforms.Length)].position;
        }
        _time = 0;
    }
    private void ApplyGravityToObjects()
    {
        foreach (var obj in _objectPool)
        {
            if (obj.activeSelf)
            {
                obj.transform.Translate(Vector3.down * _gravity * _time);
            }
        }
    }
    private void CreatePool() //objectPool�̍쐬���\�b�h
    {
        _objectPool = new List<GameObject>();

        for (int i = 0; i < _objectDatas.Count; i++)
        {
            for (int j = 0; j < _objectDatas[i]._count; j++)
            {
                GameObject dumbleweed = Instantiate(_objectDatas[i]._dumbleweed);
                dumbleweed.SetActive(false);
                _objectPool.Add(dumbleweed);
                dumbleweed.transform.parent = this.transform;
            }
        }
    }

    public GameObject GetDumbleweed(Vector3 position) //��������object�̎擾���\�b�h
    {
        if (_objectPool.Find(obj => !obj.activeSelf) != null)
        {
            int count;
            do
            {
                count = Random.Range(0, _objectPool.Count);
            }
            while (_objectPool[count].activeSelf);
            GameObject dumbleweed = _objectPool[count];
            dumbleweed.SetActive(true);
            return dumbleweed;
        }

        GameObject newDumbleweed = Instantiate(_objectDatas[Random.Range(0,_objectDatas.Count)]._dumbleweed, position, transform.rotation);
        _objectPool.Add(newDumbleweed);
        newDumbleweed.transform.parent = this.transform;
        return newDumbleweed;
    }

    public void NonActiveDumbleweed() //����ʏ�ɕ\������Ă���object���������\�b�h
    {
        _objectPool.ForEach(obj =>
        {
            obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
        });
    }
}

[System.Serializable]
public class ObjectData
{
    [SerializeField, Header("���������邩")]public int _count;
    [SerializeField, Header("��������_���u���E�B�[�h")] public GameObject _dumbleweed;
}
