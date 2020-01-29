using UnityEngine;

public class DummyTester : MonoBehaviour
{
    public GameObject pointer;
    public float initialY;
    public int count;
    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            var ng = Instantiate(pointer, PointInCircle(5f, 25f), Quaternion.identity);
        }
    }
    public Vector3 PointInCircle(float r1, float r2)
    {
        var v = Random.onUnitSphere * Random.Range(r1, r2);
        return new Vector3(v.x, initialY, v.z);
    }
    void Update()
    {
        transform.Rotate(Vector3.up, 5 * Time.deltaTime);
    }
}
