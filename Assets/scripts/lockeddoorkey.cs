using UnityEngine;

public class lockeddoorkey : MonoBehaviour
{
    public Animator _doorAnimator;

    void start()
    {
        _doorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("test");
        if (other.CompareTag("Player"))
        {
            _doorAnimator.SetTrigger("open");
        }
    }

    
}
