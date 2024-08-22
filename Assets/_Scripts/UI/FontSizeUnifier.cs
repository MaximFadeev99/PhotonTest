using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

namespace PhotonTest.UI 
{
    public class FontSizeUnifier : MonoBehaviour
    {
        [SerializeField] private List<Button> _createdButtons;

        private void Awake()
        {
            IEnumerable<TMP_Text> tmpTexts = _createdButtons.Select(button => button.gameObject.GetComponentInChildren<TMP_Text>());

            SetUnifiedFontSize(tmpTexts);
        }

        private void SetUnifiedFontSize(IEnumerable<TMP_Text> tmpTexts)
        {
            float smallestFont = tmpTexts.Min(tmpText => tmpText.fontSize);

            foreach (TMP_Text tmpText in tmpTexts)
            {
                tmpText.enableAutoSizing = false;
                tmpText.fontSize = smallestFont;
            }
        }
    }
}