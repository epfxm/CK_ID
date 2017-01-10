using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class posM : MonoBehaviour
{
    List<RegenState> _Rstate;
    RegenState temp;
    public double curr_lat;
    public double curr_long;
    public pos[] _poss;
    Vector3 _pos;
    // Use this for initialization
    private void Awake()
    {
        _pos = new Vector3();
        _Rstate = new List<RegenState>();
        temp = new RegenState();
        _poss = new pos[9];
    }
    void Start()
    {
        for (int i = 0; i < 9; i++)
        {
            _poss[i] = GameObject.Find("pos" + i).GetComponent<pos>();
        }

        initState();
        InvokeRepeating("findState", 0.0f, 1.0f);
    }
    public RegenState[] re;
    void initState()
    {
        for (int i = 0; i < 20; i++)
        {
            temp = new RegenState();
            temp._index = i;
            temp._num = UnityEngine.Random.Range(0, 4);
            temp._lat = 37 + i;
            temp._long = 127 + i;
            _Rstate.Add(temp);
        }
        re =new RegenState[ _Rstate.Count];
        re = _Rstate.ToArray();
    }

    void findState()
    {
        int min = 0;
        double dis;
        double min_dis = 999;

        for (int i = 0; i < _Rstate.Count; i++)
        {
            _Rstate.ForEach(delegate (RegenState _r)
            {
                if (i == _r._index)
                  Debug.Log(_r._index);
            });

            dis = Math.Abs(calDistance(curr_lat, curr_long, _Rstate[i]._lat, _Rstate[i]._long));
            if (i == 0)
            {
                min = i;
            }
            else if (dis < min_dis)
            {
                min = i;
                min_dis = dis;
            }
        }

        if (min < 2)
        {
            min = _Rstate.Count - (min + 1);
        }

        for (int i = 0; i < _poss.Length; i++)
        {
            if (min > _Rstate.Count - 1)
            {
                min = 0;
                i--;
            }
            else
            {
                _poss[i].RegenMonster(_Rstate[min]._num);
                min++;
            }
        }
    }

    public double calDistance(double lat1, double lon1, double lat2, double lon2)
    {

        double theta, dist;
        theta = lon1 - lon2;
        dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1))
              * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
        dist = Math.Acos(dist);
        dist = rad2deg(dist);

        dist = dist * 60 * 1.1515;
        dist = dist * 1.609344;    // 단위 mile 에서 km 변환.  
        dist = dist * 1000.0;      // 단위  km 에서 m 로 변환  

        return dist;
    }

    // 주어진 도(degree) 값을 라디언으로 변환  
    private double deg2rad(double deg)
    {
        return (double)(deg * Math.PI / (double)180d);
    }

    // 주어진 라디언(radian) 값을 도(degree) 값으로 변환  
    private double rad2deg(double rad)
    {
        return (double)(rad * (double)180d / Math.PI);
    }
}

[System.Serializable]
public class RegenState
{
    public int _index;
    public int _num;
    public double _lat;
    public double _long;
}
