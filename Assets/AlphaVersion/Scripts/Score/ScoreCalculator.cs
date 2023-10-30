using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// スコアの増減のメッセージから増減する値を計算する機能のクラス
    /// </summary>
    public class ScoreCalculator
    {
        /// <summary>
        /// スコアのテーブルを参照し、増減するスコアの値を返す
        /// </summary>
        public int ToInt(ScoreEventMessage msg)
        {
            // TODO:マスターデータからテーブルを読み込んで対応する値に変換
            // フィーバータイムにも考慮

            // ↓適当な値
            return 100;
        }
    }
}
