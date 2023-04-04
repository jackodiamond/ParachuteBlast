using UnityEngine;

public class MovingPoint : MonoBehaviour
{
    private Vector3 targetPosition = new Vector3(9.1f, 4.5f, 0); 
    private float timeToMove = 0.5f;
    private float currentTime = 0f; 
    private bool isActive = false; 

    private void Update()
    {
        if (isActive)
        {
            currentTime += Time.fixedDeltaTime; 

            if (currentTime < timeToMove)
            {
                float fraction = currentTime / timeToMove;

                transform.position = Vector3.Lerp(transform.position, targetPosition, fraction);
            }
            else
            {
                transform.position = targetPosition;
                gameObject.SetActive(false); 
                isActive = false; 
            }
        }
    }

    private void OnEnable()
    {
        isActive = true; 
        currentTime = 0f; 
    }
}
