using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    /// <summary>
    /// �p�[�e�B�N���Ƀr���{�[�h�̋@�\��񋟂���R���|�[�l���g
    /// </summary>
    public class Billboard : MonoBehaviour
    {
        Transform _t;
        Transform _c;

        void Awake()
        {
            _t = transform;
            _c = Camera.main.transform;
        }

        void Update()
        {
            Vector3 p = _c.position;
            p.y = _t.position.y;
            _t.LookAt(p);
        }
    }
}
