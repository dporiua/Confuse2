using System.Collections;
using UnityEngine;

namespace Dana
{
    /// <summary>
    /// Moves the sprite/ object this script is attached to upwards, (Or any direction if you tweak x,y,z values).
    /// Make sure to attach a collider to the object to detect the mouse.
    /// </summary>

    public class SpriteHoverBehaviour : MonoBehaviour
    {
        #region Variables
        private Vector3 _defaultPosition;
        private float hoverHeight = 0.5f;
        private float _hoverDuration = 0.3f;
        #endregion

        void Start()
        {
            _defaultPosition = transform.position;
        }

        private void OnMouseEnter()
        {
            if (transform.position != _defaultPosition) { return; }
            StartCoroutine(HoverAction());
        }

        #region Hover Logic.
        /// <summary>
        /// Moves the sprite to hover position then to default position when triggered.
        /// </summary>
        public IEnumerator HoverAction()
        {
            Vector3 hoverPosition = _defaultPosition + new Vector3(0, hoverHeight, 0);

            yield return MoveToPosition(hoverPosition, _hoverDuration);
            yield return MoveToPosition(_defaultPosition, _hoverDuration);
        }
        #endregion

        #region Character Movement Logic.
        /// <summary>
        /// Lerps the object's position to target position for the chosen duration amount.
        /// </summary>
        private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
        {
            Vector3 startPosition = transform.position;
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
        }
        #endregion
    }
}