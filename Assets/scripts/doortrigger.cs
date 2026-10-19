using UnityEngine;

public class doortrigger : MonoBehaviour
{
    public  Animator _doorAnimator;

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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _doorAnimator.SetTrigger("closed");
        }
    }
}
