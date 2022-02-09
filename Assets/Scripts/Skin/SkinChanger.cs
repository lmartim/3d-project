using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skin
{
    public class SkinChanger : MonoBehaviour
    {
        public SkinnedMeshRenderer mesh;

        public Texture2D texture;
        public string shaderIdName = "_EmissionMap";

        private Texture2D _defaultTexture;

        private void Awake()
        {
            _defaultTexture = (Texture2D) mesh.materials[0].GetTexture(shaderIdName);
        }

        private void ChangeTexture()
        {
            mesh.materials[0].SetTexture(shaderIdName, texture);
        }

        public void ChangeTexture(SkinSetup setup)
        {
            mesh.materials[0].SetTexture(shaderIdName, setup.texture);
        }

        public void ResetTexture()
        {
            mesh.materials[0].SetTexture(shaderIdName, _defaultTexture);
        }
    }
}
