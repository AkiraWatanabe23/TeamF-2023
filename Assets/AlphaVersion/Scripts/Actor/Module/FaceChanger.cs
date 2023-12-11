using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alpha
{
    public enum Face
    {
        Normal,
        Happy,
        Angry,
        Panic,
    }

    public class FaceChanger : MonoBehaviour
    {
        readonly int _facialNumberPropertyId = Shader.PropertyToID("_FacialNumber");

        [SerializeField] Renderer _renderer;

        public void ChangeFace(Face face)
        {
            float value = face == Face.Normal ? 0f :
                          face == Face.Happy ? 1.0f :
                          face == Face.Angry ? 2.0f :
                          3.0f;

            _renderer.materials[1].SetFloat(_facialNumberPropertyId, value);
        }
    }
}
