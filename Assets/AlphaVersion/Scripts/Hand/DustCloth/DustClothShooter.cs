using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �E�N���b�N�ŎG�Ђ��ˏo����@�\�̃N���X
    /// </summary>
    public class DustClothShooter : MonoBehaviour
    {
        [SerializeField] DustCloth _prefab;
        [Header("�ˏo����ۂ̐ݒ�")]
        [SerializeField] Vector3 _offset;
        [SerializeField] float _shootPower;

        /// <summary>
        /// �G�Ђ������o��
        /// </summary>
        public void Shoot()
        {
            // TODO:�G�Ђ��ˏo����x�ɎG�Ђ𐶐����Ă���
            DustCloth dustCloth = Instantiate(_prefab);
            dustCloth.transform.position = transform.position + _offset;
            dustCloth.Shoot(Vector3.forward * _shootPower);
        }
    }
}