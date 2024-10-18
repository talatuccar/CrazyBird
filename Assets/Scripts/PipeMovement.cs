using UnityEngine;
public class PipeMovement : MonoBehaviour
{
    public float speed = 2f;
    private PipeManager pipeManager;
    public void Initialize(PipeManager pipeManager)
    {
        this.pipeManager = pipeManager;
    }
    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Check if the pipe is out of the screen
        if (transform.position.x < -5f)
        {
            pipeManager.ReturnPipeToPool(gameObject);
        }
    }
}