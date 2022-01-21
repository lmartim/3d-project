using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Items
{
    public class ItemLayout : MonoBehaviour
    {
        private ItemSetup _currSetup;

        public Image uiIcon;
        public TextMeshProUGUI uiValue;
        public Image uiKeyToActivate;
        public TextMeshProUGUI uiItemKey;

        public void Load(ItemSetup setup)
        {
            _currSetup = setup;

            updateUi();

            if (_currSetup.canUse)
            {

                uiKeyToActivate.enabled = true;

                uiItemKey.enabled = true;
                uiItemKey.text = _currSetup.keyCode.ToString();
            }
        }

        private void updateUi()
        {
            uiIcon.sprite = _currSetup.icon;
        }

        private void Update()
        {
            uiValue.text = _currSetup.soInt.value.ToString();
        }
    }
}
