using UnityEngine;
using System.Collections;

namespace Lvl08
{
    public class CupController : MonoBehaviour
    {
        #region Variables.
        [SerializeField] private LevelEightBaseScript _lEightGameBaseScript;
        private SpriteRenderer _spriteRenderer;
        private Color _originalColor;
        private Color _highlightInspectorColor = Color.yellow;

        [SerializeField] private bool showBallLocation = false;

        private Vector3 _originalPosition;
        #endregion

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalColor = _spriteRenderer.color;
            _originalPosition = transform.position;
        }

        private void OnMouseDown()
        {
            OnCupSelected();
        }

        #region Public Functions.
        public void OnCupSelected()
        {
            _lEightGameBaseScript.OnCupSelected(transform);
        }

        public void TransformCupUp()
        {
            transform.Translate(Vector3.up * 2f);
        }

        public void BringCupDown()
        {
            transform.Translate(Vector3.down * 3f);
        }

        public IEnumerator SlowlyLowerCup()
        {
            Vector3 _startPos = transform.position;
            Vector3 _endPos = _startPos - new Vector3(0, 2f, 0);
            float _passedTime = 0;
            float _duration = 1f;

            while (_passedTime < _duration)
            {
                transform.position = Vector3.Lerp(_startPos, _endPos, _passedTime / _duration);
                _passedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = _endPos;
        }

        public void LowerCupToOriginalPosition()
        {
            transform.position = _originalPosition;
        }

        public void BallLocactionRevealer()
        {
            if (showBallLocation)
            {
                _spriteRenderer.color = _highlightInspectorColor;
            }
        }

        public void BallLocationHider()
        {
            _spriteRenderer.color = _originalColor;
        }
        #endregion
    }
}