using UnityEngine;

namespace Lvl08
{
    public class BallController : MonoBehaviour
    {
        #region Variables.
        private Transform _currentParent;
        #endregion


        #region Public Functions.
        public void SetBallParent(Transform cup)
        {
            _currentParent = cup;
            transform.SetParent(cup);
            transform.localPosition = Vector3.zero;
        }
        public void DetachBall()
        {
            transform.SetParent(null);
        }

        public bool IsUnderCup(Transform cup)
        {
            return _currentParent == cup;
        }
        #endregion
    }
}