using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Dyson.Dana
{
    /// <summary>
    /// This is originally Dyson's script which then I edited to display visual feedback to players
    /// on what they have pressed and what they haven't pressed and whether their choice is correct or wrong.
    /// The script contains logic that deals with detecting correct player input and rewarding/ 'punishing' the players according
    /// to the correctness of their choices.
    /// </summary>

    public class ClickOrder : MonoBehaviour
    {
        #region Variables.
        private int endOfList;
        public List<int> tilesList = new List<int>(6);

        [SerializeField] private Material correctKeyMaterial;
        [SerializeField] private Material incorrectFlashMaterialOne;
        [SerializeField] private Material incorrectFlashMaterialTwo;
        [SerializeField] private float flashDuration = 0.3f;
        [SerializeField] private Button[] buttons;

        private bool _isFlashing = false;
        #endregion

        // Update is called once per frame
        void Update()
        {
            if (endOfList == 7)
            {
                SceneManager.LoadScene(5);
            }
        }

        public void AButton()
        {
            if (tilesList.Count == 0)
            {
                StartCoroutine(FlashAndRestart());
            }
            else if (tilesList.Count == 4)
            {
                tilesList.Add(1);
                endOfList++;
                SetButtonMaterial(0, correctKeyMaterial);
            }
            else
            {
                StartCoroutine(FlashAndRestart());
            }
        }

        public void BButton()
        {
            if (tilesList.Count == 0)
            {
                tilesList.Add(2);
                endOfList++;
                SetButtonMaterial(1, correctKeyMaterial);
            }
            else
            {
                StartCoroutine(FlashAndRestart());
            }
        }

        public void CButton()
        {
            if (tilesList.Count == 0)
            {
                StartCoroutine(FlashAndRestart());
            }
            else if (tilesList.Count == 3)
            {
                tilesList.Add(3);
                endOfList++;
                SetButtonMaterial(2, correctKeyMaterial);
            }
            else
            {
                StartCoroutine(FlashAndRestart());
            }
        }

        public void DButton()
        {
            if (tilesList.Count == 0)
            {
                StartCoroutine(FlashAndRestart());
            }
            else if (tilesList.Count == 1)
            {
                tilesList.Add(4);
                endOfList++;
                SetButtonMaterial(3, correctKeyMaterial);
            }
            else
            {
                StartCoroutine(FlashAndRestart());
            }
        }

        public void EButton()
        {
            if (tilesList.Count == 0)
            {
                StartCoroutine(FlashAndRestart());
            }
            else if (tilesList.Count == 5)
            {
                tilesList.Add(5);
                endOfList++;
                SetButtonMaterial(4, correctKeyMaterial);
            }
            else
            {
                StartCoroutine(FlashAndRestart());
            }
        }

        public void FButton()
        {
            if (tilesList.Count == 0)
            {
                StartCoroutine(FlashAndRestart());
            }
            else if (tilesList.Count == 2)
            {
                tilesList.Add(6);
                endOfList++;
                SetButtonMaterial(5, correctKeyMaterial);
            }
            else
            {
                StartCoroutine(FlashAndRestart());
            }
        }

        public void GButton()
        {
            if (tilesList.Count == 0)
            {
                StartCoroutine(FlashAndRestart());
            }
            else if (tilesList.Count == 6)
            {
                tilesList.Add(7);
                endOfList++;
                SetButtonMaterial(6, correctKeyMaterial);
            }
            else
            {
                StartCoroutine(FlashAndRestart());
            }
        }

        /// <summary>
        /// Changes button 'colours' (Materials) depending on which button is pressed.
        /// </summary>
        private void SetButtonMaterial(int buttonIndex, Material material)
        {
            if (buttonIndex >= 0 && buttonIndex < buttons.Length)
            {
                Image buttonImage = buttons[buttonIndex].GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.material = material;
                }
            }
        }

        /// <summary>
        /// Flashing effect to flash 5 times to all buttons in the array !!!
        /// </summary>
        private IEnumerator FlashAndRestart()
        {
            if (_isFlashing) yield break;
            _isFlashing = true;

            for (int i = 0; i < 5; i++)
            {
                foreach (Button button in buttons)
                {
                    if (button != null)
                    {
                        Image buttonImage = button.GetComponent<Image>();
                        if (buttonImage != null)
                        {
                            buttonImage.material = incorrectFlashMaterialOne;
                        }
                    }
                }
                yield return new WaitForSeconds(flashDuration);

                foreach (Button button in buttons)
                {
                    if (button != null)
                    {
                        Image buttonImage = button.GetComponent<Image>();
                        if (buttonImage != null)
                        {
                            buttonImage.material = incorrectFlashMaterialTwo;
                        }
                    }
                }
                yield return new WaitForSeconds(flashDuration);
            }

            SceneManager.LoadScene(2);
        }
    }
}