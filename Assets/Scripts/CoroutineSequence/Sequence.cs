using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoroutineSequence
{
    public class Sequence
    { 
        private bool _isActive;
        private bool _isPaused;
        private MonoBehaviour _monoBehaviour;
        private List<IEnumerator> _coroutines;
    
        public Sequence(MonoBehaviour monoBehaviour, IEnumerator coroutine = null)
        {
            _monoBehaviour = monoBehaviour;
            _coroutines = new List<IEnumerator>();

            if (coroutine != null)
            {
                _coroutines.Add(coroutine);
            }
        }

        public Sequence Then(IEnumerator coroutine)
        {
            _coroutines.Add(coroutine);
            return this;
        }

        public Sequence Then(Action action)
        {
            _coroutines.Add(ActionRoutine(action));
            return this;
        }
    
        public Sequence Then(Sequence sequence)
        {
            foreach (var coroutine in sequence._coroutines)
            {
                _coroutines.Add(coroutine);
            }
            return this;
        }

        public Sequence WaitForAsyncOperation(AsyncOperation asyncOperation)
        {
            _coroutines.Add(WaitForAsyncOperationRoutine(asyncOperation));
            return this;
        }

        public Sequence WaitForSeconds(float delay)
        {
            _coroutines.Add(WaitForSecondsRoutine(delay));
            return this;
        }

        public Sequence WaitUntil(Func<bool> predicate)
        {
            _coroutines.Add(WaitUntilRoutine(predicate));
            return this;
        }

        public Sequence WaitForNextFrame()
        {
            _coroutines.Add(WaitForNextFrameRoutine());
            return this;
        }

        public Sequence Start()
        {
            _isActive = true;
            _monoBehaviour.StartCoroutine(RunSequence());
            return this;
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Continue()
        {
            _isPaused = false;
        }

        public void Stop()
        {
            _isActive = false;
            _isPaused = false;
            _monoBehaviour = null;
            _coroutines.Clear();
        }

        private IEnumerator RunSequence()
        {
            for (var i = 0; i < _coroutines.Count; i++)
            {
                if (!_isActive)
                {
                    yield break;
                }
    
                while (_isPaused)
                {
                    yield return null;
                }

                var coroutine = _coroutines[i];
                while (coroutine.MoveNext())
                {
                    yield return coroutine.Current;
                }
            }
    
            Stop();
        }

        private IEnumerator WaitForSecondsRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);
        }

        private IEnumerator WaitUntilRoutine(Func<bool> predicate)
        {
            yield return new WaitUntil(predicate);
        }

        private IEnumerator WaitForNextFrameRoutine()
        {
            yield return null;
        }

        private IEnumerator WaitForAsyncOperationRoutine(AsyncOperation asyncOperation)
        {
            yield return asyncOperation;
        }

        private IEnumerator ActionRoutine(Action action)
        {
            action?.Invoke();
            yield break;
        }
    }
}