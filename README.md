# Unity-Coroutine-Sequence

 A lightweight utility class for managing and sequencing coroutines in Unity projects. It provides a convenient way to create complex sequences of actions, delays, and conditions using a fluent API.

Features:
- Sequence coroutines and actions in a linear order.
- Wait for conditions, delays, async operations, or the next frame.
- Pause and continue sequences as needed.
- Easily integrate complex logic with a clean and readable syntax.


Methods:
- Then(IEnumerator coroutine): Add a coroutine to the sequence.
- Then(Action action): Add an action to the sequence.
- Then(Sequence sequence): Add another sequence to the current sequence.
- WaitForAsyncOperation(AsyncOperation asyncOperation): Wait for the specified async operation to complete.
- WaitForSeconds(float delay): Wait for the specified delay in seconds.
- WaitUntil(Func<bool> predicate): Wait until the specified condition is true.
- WaitForNextFrame(): Wait for the next frame.
- Start(): Start the sequence.
- Pause(): Pause the sequence.
- Continue(): Continue the paused sequence.
- Stop(): Stop and reset the sequence.


Example:
```
// Wait for a condition to be true and then perform an action
var sequence = new Sequence(this)
    .WaitUntil(() => Input.GetKeyDown(KeyCode.Space))
    .Then(() => Debug.Log("Space is pressed!"))
    .WaitForSeconds(1f)
    .Then(() => Debug.Log("Finished!"))
    .Start();
```
