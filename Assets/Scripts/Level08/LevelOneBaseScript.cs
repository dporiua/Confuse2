using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lvl01
{
    public class LevelOneBaseScript : MonoBehaviour
    {
        #region Variables.
        public List<Transform> cups;
        public List<Transform> underCupTransforms;
        public BallController ballController;
        public GameObject xMarker; 

        [SerializeField] private CupsShuffleLogic cupsShuffleLogicScript;

        private bool _isGameOver = false;
        private bool _isPlayerInputAllowed = true;
        private Transform _currentCup;
        private Transform _currentUnderCupTransform;
        private int roundCounter = 0;
        private List<Vector3> _initialCupPositions = new List<Vector3>();
        #endregion

        private void Start()
        {
            foreach (var _cup in cups)
            {
                _initialCupPositions.Add(_cup.position);
            }

            InitializeNewRound();
        }

        #region Public Functions.
        public void OnCupSelected(Transform selectedCup)
        {
            if (!_isPlayerInputAllowed || _isGameOver) return;

            if (ballController.IsUnderCup(selectedCup))
            {
                Debug.Log("Ball is found!");
                RevealBall();
                selectedCup.GetComponent<CupController>().TransformCupUp();
                _isGameOver = true;
                _currentCup.GetComponent<CupController>().BallLocationHider();

                StartCoroutine(ResetGameAfterDelay(2f));
            }
            else
            {
                Debug.Log("Wrong guess!");
                StartCoroutine(IncorrectGuessLogic(selectedCup));
            }
        }
        #endregion

        #region Private Functions.
        private IEnumerator IncorrectGuessLogic(Transform chosenCup)
        {
            _isPlayerInputAllowed = false;

            ShowMarkerByMousePosition();
            chosenCup.GetComponent<CupController>().TransformCupUp();
            yield return new WaitForSeconds(1f);
            yield return chosenCup.GetComponent<CupController>().SlowlyLowerCup();

            StartCoroutine(ResetGameAfterDelay(1f));
        }

        private void ShowMarkerByMousePosition()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            xMarker.transform.position = worldPosition;
            xMarker.SetActive(true);
            StartCoroutine(HideMarkerAfterDelay(0.5f));
        }

        private IEnumerator HideMarkerAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            xMarker.SetActive(false);
        }

        private IEnumerator ResetGameAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            roundCounter++;

            for (int i = 0; i < cups.Count; i++)
            {
                cups[i].position = _initialCupPositions[i];
            }
            InitializeNewRound();
        }

        private void InitializeNewRound()
        {
            if (roundCounter == 0 || _currentCup == null)
            {
                int selectedCupIndex = Random.Range(0, cups.Count);
                _currentCup = cups[selectedCupIndex];
                _currentUnderCupTransform = underCupTransforms[selectedCupIndex];
            }
            else
            {
                SetBallToNearestUnderCup();
            }

            ballController.transform.SetParent(_currentUnderCupTransform);
            ballController.transform.localPosition = Vector3.zero;

            _isGameOver = false;
            _isPlayerInputAllowed = false;

            _currentCup.GetComponent<CupController>().BallLocactionRevealer();
            StartCoroutine(StartShuffleAfterReveal());
        }

        private IEnumerator StartShuffleAfterReveal()
        {
            _currentCup.GetComponent<CupController>().TransformCupUp();

            yield return new WaitForSeconds(0.5f);

            yield return _currentCup.GetComponent<CupController>().SlowlyLowerCup();
            ballController.SetBallParent(_currentCup);

            yield return StartCoroutine(cupsShuffleLogicScript.CupsShuffle(cups, ballController));

            _isPlayerInputAllowed = true;
        }

        private void RevealBall()
        {
            ballController.transform.localPosition = new Vector3(0, -0.05f, 0);
            ballController.transform.SetParent(null);
            SetBallToNearestUnderCup();
        }

        private void SetBallToNearestUnderCup()
        {
            Transform _nearestUnderCup = null;
            float minDistance = Mathf.Infinity;

            foreach (Transform _underCupTransform in underCupTransforms)
            {
                float distance = Vector3.Distance(ballController.transform.position, _underCupTransform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    _nearestUnderCup = _underCupTransform;
                }
            }

            if (_nearestUnderCup != null)
            {
                ballController.transform.SetParent(_nearestUnderCup);
                ballController.transform.localPosition = Vector3.zero;

                int _nearestIndex = underCupTransforms.IndexOf(_nearestUnderCup);
                _currentCup = cups[_nearestIndex];
                _currentUnderCupTransform = _nearestUnderCup;
            }
        }
        #endregion
    }
}
