using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// 生成したキャラクターの初期化を行うクラス
    /// ManagerとSpawnerの間で、処理を挟んで橋渡しを行う
    /// </summary>
    public class ActorInitializer : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

// ↓次やること
// キャラクターに経路を渡したい
// 生成クラスは生成のみを行いこのクラスに返ってくる
// 経路を生成するクラスからどうやって渡すか
// 経路を渡すインターフェースを作成して渡す案
// object型を渡すインターフェースを作成して渡す案