using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField, Header("生成する場所")] private Transform[] _transforms;

    [SerializeField, Header("生成したダンブルウィードを格納")] private List<GameObject> _objectPool;

    [SerializeField, Header("何個取り出すか")] private int _maxCount;

    [SerializeField, Header("生成したいobjectと何個生成したいか")] private List<ObjectData> _objectDatas;

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
    private void CreatePool() //objectPoolの作成メソッド
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

    public GameObject GetDumbleweed(Vector3 position) //生成したobjectの取得メソッド
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

    public void NonActiveDumbleweed() //今画面上に表示されているobjectを消すメソッド
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
    [SerializeField, Header("何個生成するか")]public int _count;
    [SerializeField, Header("生成するダンブルウィード")] public GameObject _dumbleweed;
}
