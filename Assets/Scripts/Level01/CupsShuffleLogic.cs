using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Lvl01
{ 
    public class CupsShuffleLogic : MonoBehaviour
    {
        #region Variables.
        public float shuffleSpeed = 2f;
        [SerializeField] public AudioClip shuffleSound;

        [SerializeField] private GameObject location;
        #endregion

        public IEnumerator CupsShuffle(List<Transform> cups, BallController ballController)
        {
            Transform _ballCup = ballController.transform.parent;

            for (int i = 0; i < 10; i++)
            {
                int _cupIndexA = Random.Range(0, cups.Count);
                int _cupIndexB = Random.Range(0, cups.Count);

                while (_cupIndexB == _cupIndexA)
                {
                    _cupIndexB = Random.Range(0, cups.Count);
                }

                yield return StartCoroutine(SwitchCups(cups[_cupIndexA], cups[_cupIndexB]));
            }
        }

        private IEnumerator SwitchCups(Transform cupA, Transform cupB)
        {
            Vector3 _posA = cupA.position;
            Vector3 _posB = cupB.position;
            float _time = 0;
            
            //AudioSource.PlayClipAtPoint(shuffleSound, location.transform.position, 0.7f);
            //Debug.Log("ok");
            AudioSource.PlayClipAtPoint(shuffleSound, location.transform.position, 0.7f);
            while (_time < 1f)
            {
                _time += Time.deltaTime * shuffleSpeed;
                cupA.position = Vector3.Lerp(_posA, _posB, _time);
                cupB.position = Vector3.Lerp(_posB, _posA, _time);
                yield return null;
            }

            if (_time >= 1f)
                shuffleSound.UnloadAudioData();

        }
    }
}