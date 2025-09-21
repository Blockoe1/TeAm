using UnityEngine;
using UnityEngine.SceneManagement;

public class THESTONE : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() == null)
            return;
        StaticData.EndID = 1;
        SceneManager.LoadScene("EndScreen");
    }
}
