using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 選択したアイテムのメッセージを受信し、表示するUIのクラス
    /// </summary>
    public class ItemSelectUI : MonoBehaviour
    {
        [System.Serializable]
        public class Data
        {
            public ItemType Type;
            public Sprite Icon;
        }

        [SerializeField] ItemMessageReceiver _messageReceiver;
        [Header("順に左から右に表示される")]
        [SerializeField] Data[] _data;
        [SerializeField] ItemFrameUI[] _frames;

        /// <summary>
        /// アイテムの種類に対応したUIの辞書
        /// </summary>
        Dictionary<ItemType, ItemFrameUI> _dict = new();

        void OnEnable()
        {
            _messageReceiver.OnMessageReceived += OnMessageReceived;
        }

        void OnDisable()
        {
            _messageReceiver.OnMessageReceived -= OnMessageReceived;
        }

        void Start()
        {
            ApplyDataToFrame();
        }

        /// <summary>
        /// アイテムのアイコンのデータの数だけフレームを有効化する
        /// </summary>
        void ApplyDataToFrame()
        {
            for (int i = 0; i < _frames.Length; i++)
            {
                if (i < _data.Length)
                {
                    _frames[i].Valid(_data[i].Icon);

                    // アイテムで対応するUIを取得できるように辞書に追加
                    _dict.Add(_data[i].Type, _frames[i]);
                }
                else
                {
                    _frames[i].InValid();
                }
            }
        }

        /// <summary>
        /// Receiverがメッセージを受信した際の処理
        /// 選択したアイテムに対応するUIを選択する
        /// </summary>
        void OnMessageReceived(ItemSelectMessage msg)
        {
            // 一旦すべて無効化
            foreach (ItemFrameUI frame in _frames)
            {
                frame.Deselect();
            }

            // 選択したアイテムを有効化
            _dict[msg.Type].Select();
        }
    }
}
