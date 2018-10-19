using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CSToU.UI
{
    public class TextBox : MonoBehaviour
    {

        public TMPro.TextMeshProUGUI Lable;

        public void SetText(string _text)
        {
            Lable.text = _text;
        }

    }
}
