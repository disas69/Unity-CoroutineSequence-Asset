using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CoroutineSequence
{
    public class SequenceExample : MonoBehaviour
    {
        // Uncomment the example you want to run
        private void Start()
        {
            WaitAndPerformAction();
            // WaitAndLoadScene();
            // WaitAndPlayAnimation();
            // WaitForAsyncOpAndPerformAction();
            // WaitUntilConditionIsTrueAndPerformAction();
            // WaitForNextFrameAndPerformAction();
            // PerformMultipleActionsInSequence();
            // PauseAndResumeSequence();
            // StopSequence();
            // ChainSequencesTogether();
        }

        private void WaitAndPerformAction()
        {
            // Wait for a delay and then perform an action
            var sequence = new Sequence(this)
                .WaitForSeconds(1f)
                .Then(() => Debug.Log("Delayed action!"));
            sequence.Start();
        }

        private void WaitAndLoadScene()
        {
            // Wait for a delay and then load a scene
            var sequence = new Sequence(this)
                .WaitForSeconds(2f)
                .Then(() => SceneManager.LoadScene("MyScene"));
            sequence.Start();
        }

        private void WaitAndPlayAnimation()
        {
            // Wait for a delay and then play an animation
            var myAnimator = GetComponent<Animator>();
            var sequence = new Sequence(this)
                .WaitForSeconds(3f)
                .Then(() => myAnimator.Play("MyAnimation"));
            sequence.Start();
        }

        private void WaitForAsyncOpAndPerformAction()
        {
            // Wait for an async operation to complete and then perform an action
            var asyncOp = SceneManager.LoadSceneAsync("MyScene", LoadSceneMode.Additive);
            var sequence = new Sequence(this)
                .WaitForAsyncOperation(asyncOp)
                .Then(() => Debug.Log("Scene loaded!"));
            sequence.Start();
        }

        private void WaitUntilConditionIsTrueAndPerformAction()
        {
            // Wait for a condition to be true and then perform an action
            var sequence = new Sequence(this)
                .WaitUntil(() => Input.GetKeyDown(KeyCode.Space))
                .Then(() => Debug.Log("Space is pressed!"))
                .WaitForSeconds(1f)
                .Then(() => Debug.Log("Finished!"));
            sequence.Start();
        }

        private void WaitForNextFrameAndPerformAction()
        {
            // Wait for the next frame and then perform an action
            var sequence = new Sequence(this)
                .WaitForNextFrame()
                .Then(() => Debug.Log("Next frame!"));
            sequence.Start();
        }

        private void PerformMultipleActionsInSequence()
        {
            // Perform multiple actions in sequence
            var sequence = new Sequence(this)
                .Then(() => Debug.Log("Action 1!"))
                .WaitForSeconds(1f)
                .Then(() => Debug.Log("Action 2!"))
                .WaitForSeconds(1f)
                .Then(() => Debug.Log("Action 3!"));
            sequence.Start();
        }

        private void PauseAndResumeSequence()
        {
            // Pause and resume a sequence
            var sequence = new Sequence(this)
                .Then(() => Debug.Log("Action 1!"))
                .WaitForSeconds(1f)
                .Then(() => Debug.Log("Action 2!"))
                .WaitForSeconds(1f)
                .Then(() => Debug.Log("Action 3!"));
            sequence.Start();
        
            StartCoroutine(PauseAndResumeSequence(sequence));
        }

        private IEnumerator PauseAndResumeSequence(Sequence sequence)
        {
            yield return new WaitForSeconds(0.5f);
            sequence.Pause();
            Debug.Log("Sequence paused!");
            yield return new WaitForSeconds(3f);
            sequence.Continue();
            Debug.Log("Sequence resumed!");
        }

        private void StopSequence()
        {
            // Stop a sequence
            var sequence = new Sequence(this)
                .Then(() => Debug.Log("Action 1!"))
                .WaitForSeconds(1f)
                .Then(() => Debug.Log("Action 2!"))
                .WaitForSeconds(1f)
                .Then(() => Debug.Log("Action 3!"));
            sequence.Start();
        
            StartCoroutine(StopSequence(sequence));
        }

        private IEnumerator StopSequence(Sequence sequence)
        {
            yield return new WaitForSeconds(1.5f);
            sequence.Stop();
            Debug.Log("Sequence stopped!");
        }

        private void ChainSequencesTogether()
        {
            // Chain sequences together
            var sequence1 = new Sequence(this)
                .Then(() => Debug.Log("Sequence 1, Action 1!"))
                .WaitForSeconds(1f)
                .Then(() => Debug.Log("Sequence 1, Action 2!"))
                .WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            var sequence2 = new Sequence(this)
                .Then(() => Debug.Log("Sequence 2, Action 1!"))
                .WaitForSeconds(1f)
                .Then(() => Debug.Log("Sequence 2, Action 2!"))
                .WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            var sequence3 = new Sequence(this)
                .Then(() => Debug.Log("Sequence 3, Action 1!"))
                .WaitForSeconds(1f)
                .Then(() => Debug.Log("Sequence 3, Action 2!"));
            var mainSequence = new Sequence(this)
                .Then(sequence1)
                .Then(sequence2)
                .Then(sequence3);
            mainSequence.Start();
        }
    }
}