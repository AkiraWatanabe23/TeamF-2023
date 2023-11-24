using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Alpha
{
    public class DebugItemThrower : MonoBehaviour
    {
        [SerializeField] ThrowedItem _item;
        [SerializeField] Button _button;

        void Start()
        {
            _button.onClick.AddListener(() => 
            {
                ThrowedItem item = Instantiate(_item, transform.position, Quaternion.identity);
                item.OnCreate(null);
                item.Throw(transform.forward * 5.0f);
            });
        }
    }
}