using UnityEngine;

public class ControllerPower : MonoBehaviour
{
    [SerializeField]
    Transform MoveTarget;
    [SerializeField]
    float Speed = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float width = Screen.width;
        float height = Screen.height;
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(width, height, 1));
        Vector3 pos2 = Camera.main.ScreenToWorldPoint(new Vector3(width / width, height / height, 1));
        if (MoveTarget != null) transform.position = Vector3.MoveTowards(transform.position, MoveTarget.position, Speed * Time.deltaTime);
        if (transform.position.x < pos2.x || transform.position.x > pos.x) Destroy(gameObject);
        if (transform.position.y < pos2.y || transform.position.y > pos.y) Destroy(gameObject);
    }
}
