using UnityEngine;
using UnityEngine.SceneManagement;

public class THESTONE : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() == null)
            return;
        Debug.Log("WIN");
        StaticData.EndID = 1;
        SceneManager.LoadScene("EndScreen");
    }
}
