using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

namespace Alpha
{
    public class TempAudioManager : MonoBehaviour
    {
        public static TempAudioManager Instance;

        // 同時再生出来る最大数
        const int Max = 10;

        [System.Serializable]
        class Data
        {
            public string Key;
            public AudioClip Clip;
            [Range(0, 1)]
            public float Volume = 1;
        }

        [SerializeField] Data[] _data;

        Dictionary<string, Data> _table;
        AudioSource[] _sources = new AudioSource[Max];
        AudioSource _bgmSource = new();

        void Awake()
        {
            Init();
        }

        void OnDestroy()
        {
            Instance = null;
        }

        void Init()
        {
            Instance ??= this;

            // AudioSourceをたくさん追加
            for (int i = 0; i < _sources.Length; i++)
            {
                _sources[i] = gameObject.AddComponent<AudioSource>();
            }

            // BGM用のAudioSourceを追加
            _bgmSource = gameObject.AddComponent<AudioSource>();

            // 音データを辞書に追加
            _table = _data.ToDictionary(v => v.Key, v => v);
        }

        /// <summary>
        /// 指定したSE/BGMを再生
        /// </summary>
        void Play(string key)
        {
            AudioSource source = _sources.Where(v => !v.isPlaying).FirstOrDefault();
            if (source != null)
            {
                source.clip = _table[key].Clip;
                source.volume = _table[key].Volume;
                source.Play();
            }
        }

        /// <summary>
        /// SEを再生する
        /// </summary>
        public void PlaySE(string key)
        {
            if (_table.ContainsKey(key))
            {
                Play(key);
            }
            else
            {
                Debug.LogWarning("再生する音が辞書に登録されていない: " + key);
            }
        }

        /// <summary>
        /// 遅延してSEを再生する
        /// </summary>
        public void DelayedPlaySE(string key, float delay)
        {
            DOVirtual.DelayedCall(delay, () => PlaySE(key)).SetLink(gameObject);
        }

        /// <summary>
        /// BGMを再生する
        /// </summary>
        public void PlayBGM(string key)
        {
            _bgmSource.clip = _table[key].Clip;
            _bgmSource.volume = _table[key].Volume;
            _bgmSource.Play();
        }

        /// <summary>
        /// BGMを停止する
        /// </summary>
        public void StopBGM()
        {
            if (_bgmSource.isPlaying)
            {
                _bgmSource.Stop();
            }
        }
    }
}
