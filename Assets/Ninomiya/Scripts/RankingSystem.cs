using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class PlayerScore : IComparable<PlayerScore>
{
    public int Score = 0;

    public PlayerScore(int score)
    {
        this.Score = score;
    }

    public int CompareTo(PlayerScore other)
    {
        return Score.CompareTo(other.Score);
    }
}

[System.Serializable]
public class ScoreData
{
    public List<PlayerScore> _scores;

    public ScoreData()
    {
        _scores = new List<PlayerScore>();
        for(int i = 0; i < 5; i++)
        {
            _scores.Add(new PlayerScore(0));
        }
    }

    public void AddScore(int score, int maxCount)
    {
        PlayerScore newScore = new PlayerScore(score);
        _scores.Add(newScore);
        _scores.Sort();
        _scores.Reverse();

        if (_scores.Count > maxCount)
        {
            _scores.RemoveRange(maxCount, _scores.Count - maxCount);
        }
    }

    public void ClearScore()
    {
        _scores.Clear();
    }
}

public class RankingSystem : MonoBehaviour
{
    [SerializeField, Header("�W�F�C�\���t�@�C���̖��O")] string _fileName;

    [SerializeField, Header("�ۑ�����X�R�A�̍ő吔")] int _scoreMaxCount = 5;

    ScoreData _scoreData;
    private void Awake()
    {
        _fileName = $"{_fileName}.json";

        LoadScore();
    }
    public void AddPlayerScore(int score) //Player��Score��ۑ�����
    {
        _scoreData.AddScore(score, _scoreMaxCount);
        SaveScore();
    }

    public List<PlayerScore> GetScores(int count) //�X�R�A���w�肳�ꂽ���擾���郁�\�b�h
    {
        return _scoreData._scores.GetRange(0, Mathf.Min(count, _scoreData._scores.Count));
    }

    private void LoadScore()
    {
        string filePath = Path.Combine(Application.persistentDataPath, _fileName);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            _scoreData = JsonUtility.FromJson<ScoreData>(json);
        }
        else
        {
            _scoreData = new ScoreData();
        }
    }

    public void AllClear()�@//���ݕۑ�����Ă���X�R�A�����ׂď���
    {
        _scoreData.ClearScore();
        for (int i = 0; i < 5; i++)
        {
            _scoreData._scores.Add(new PlayerScore(0));
        }
        SaveScore();
    }

    private void SaveScore()
    {
        string filePath = Path.Combine(Application.persistentDataPath, _fileName);
        string json = JsonUtility.ToJson(_scoreData);
        File.WriteAllText(filePath, json);
    }
}
