using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Alpha
{
    /// <summary>
    /// 客がアイテムをキャッチするエリアのサイズと位置を設定するクラス
    /// フィーバータイムの開始と終了を受信し、サイズを変更する
    /// </summary>
    public class CatchTransform : FerverHandler
    {
        [SerializeField] CatchSettingsSO _settings;
        [Header("設置する位置の設定")]
        [SerializeField] float _height = 0.5f;
        [SerializeField] float _border = 1.0f;

        Vector3 _basePosition;

        public Vector3 Position => transform.position;
        public float Radius { get; private set; }

        protected override void OnAwakeOverride()
        {
            SetScale(_settings.NormalSize);
            
            // 基準位置の設定
            _basePosition = transform.position;
        }

        protected override void OnFerverTimeEnter()
        {
            SetScale(_settings.FerverSize);
        }

        protected override void OnFerverTimeExit()
        {
            SetScale(_settings.NormalSize);
        }

        /// <summary>
        /// 指定した大きさにセットする
        /// </summary>
        void SetScale(float size)
        {
            transform.localScale = Vector3.one * size;
            Radius = size / 2;
        }

        /// <summary>
        /// ランダムな位置にセットする
        /// </summary>
        /// <returns>セットした位置</returns>
        public Vector3 SetRandomPosition()
        {
            return SetRandomPosition(_settings.NormalSize);
        } 

        public Vector3 SetRandomPosition(float size)
        {
            // 範囲からはみ出さないように半径の分だけ位置を制限する
            float x = Random.Range(-_border + size / 2, _border - size / 2);
            Vector3 pos = new Vector3(_basePosition.x + x, _height, _basePosition.z);

            return transform.position = pos;
        }

        void OnDrawGizmos()
        {
            DrawRange();
        }

        /// <summary>
        /// 設置できる範囲をギズモに描画
        /// </summary>
        void DrawRange()
        {
            // エディター上では現在地を基準にする
            Vector3 pos = !Application.isPlaying ? transform.position : _basePosition;

            Vector3 left = pos + Vector3.left * _border;
            Vector3 right = pos + Vector3.right * _border;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(left, right);
        }
    }
}
