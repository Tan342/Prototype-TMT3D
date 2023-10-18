using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Sprite image;
    [SerializeField] public int tileType;
    public int id;

    Rigidbody m_rigidbody;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
    public Sprite GetSprite()
    {
        return image;
    }

    public void SetSprite(Sprite sprite)
    {
        image = sprite;
    }

    public void Moving(Vector3 end, bool enablePhysicsAfterMoving)
    {
        StartCoroutine(StartMoving(end, enablePhysicsAfterMoving));
    }

    IEnumerator StartMoving(Vector3 end, bool enablePhysicsAfterMoving)
    {
        EnablePhysics(false);
        Vector3 start = transform.position;
        Quaternion startR = transform.rotation;
        Quaternion endR = Quaternion.identity;
        float timer = 0;

        while (timer <= 1)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, timer * 5);
            transform.rotation = Quaternion.Lerp(startR, endR, timer * 5f);
            yield return new WaitForEndOfFrame();
        }
        EnablePhysics(enablePhysicsAfterMoving);
    }

    public void EnablePhysics(bool value)
    {
        m_rigidbody.isKinematic = !value;
        m_rigidbody.detectCollisions = value;
    }
}
