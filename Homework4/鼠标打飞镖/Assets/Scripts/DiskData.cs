using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruler//飞碟特性
{
    public Color color;
    public Vector3 position;
    public float speed;
    public Vector3 direction;

    public Ruler(Color _color, Vector3 _position, float _speed, Vector3 _direction)
    {
        color = _color;
        position = _position;
        speed = _speed;
        direction = _direction;
    }
}
public class DiskData : MonoBehaviour
{
    Ruler _ruler;
    public float x = 0, y = 0, z = 0;//用于存储该飞碟在飞行方向上的速度
    public Ruler ruler
    {
        get
        {
            return _ruler;
        }
        set
        {
            _ruler = value;
            x = value.speed * value.direction.x;
            y = value.speed * value.direction.y;
            z = value.speed * value.direction.z;
            gameObject.GetComponent<Renderer>().material.color = value.color;
            gameObject.transform.position = value.position;
        }
    }
}
