using UnityEngine;
using System.Collections;
using System;
public class GPSManager : MonoBehaviour
{

    public PosState[,] _posstate;

    public PoolManager _poolManager;

    public double curr_lat;
    public double curr_long;

    public int Long_Size;
    public int Lat_Size;
    double long_temp= 127.350302 ;
    double lat_temp = 37.205484;
    double dis;
    double min_dis=999.0;

    int min_long;
    int min_lat;

    int curr_i=999;
    int curr_j=999;

    int[] setMonsterNum;

    void Awake()
    {
        _posstate = new PosState[Long_Size, Lat_Size];                                  //전체 크기 지정
        setMonsterNum = new int[9];
    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 14; i++)
        {
          //  long_temp +=0.00056;               //가로 , 경도
            for (int j = 0; j < 5; j++)
            {
                _posstate[i, j] = new PosState();                                       //배열 하나 하나 초기화가 되지 않았기 때에 다시 한번 초기화
                lat_temp -=0.00045;                 //세로 , 위도
                
                _posstate[i, j]._lat = 37.205484-(0.00056*(j+1));
                _posstate[i, j]._long = 127.350302+(0.00045*(i+1));

                _posstate[i, j].monster_num = UnityEngine.Random.Range(0, 7);           //몬스터의 범위
            }
            long_temp = 127.350302;
            lat_temp = 37.205484;
        }
        StartCoroutine(ResetPos());
    }
    int count = 0;
    IEnumerator ResetPos()
    {
        for (int i = 0; i < Long_Size; i++)
        {
            for (int j = 0; j < Lat_Size; j++)
            {
                dis = Math.Abs(calDistance2(curr_lat, curr_long, _posstate[i, j]._lat, _posstate[i, j]._long));

                if (dis < min_dis)
                {
                    min_long = i;
                    min_lat = j;
                    min_dis = dis;
                }
            }
        }

        if (min_long != curr_i)
        {
            if (min_lat != curr_j)
            {
                count = 0;
                for (int i = min_long-1; i <= min_long+1; i++)
                {
                    if (i <= 0)
                        i = 0;
                    for(int j = min_lat-1; j <= min_lat+1; j++)
                    {
                        if (j <= 0)
                            j = 0;
                        else if (j >4)
                            break;
                        setMonsterNum[count] = _posstate[i, j].monster_num;
                        
                        count++;

                        Debug.Log("~~~"+j + "  "+i);
                    }
                }
                _poolManager.getMonsters(setMonsterNum);
               
            }
        }

        yield return new WaitForSeconds(1.0f);
    }

    public double calDistance2(double lat1, double lon1, double lat2, double lon2)
    {
        double theta, dist;
        theta = lon1 - lon2;
        dist = Math.Sin(deg2rad2(lat1)) * Math.Sin(deg2rad2(lat2)) + Math.Cos(deg2rad2(lat1))
              * Math.Cos(deg2rad2(lat2)) * Math.Cos(deg2rad2(theta));
        dist = Math.Acos(dist);
        dist = rad2deg2(dist);

        dist = dist * 60 * 1.1515;
        dist = dist * 1.609344;    // 단위 mile 에서 km 변환.  
        dist = dist * 1000.0;      // 단위  km 에서 m 로 변환  

        return dist;
    }

    // 주어진 도(degree) 값을 라디언으로 변환  
    private double deg2rad2(double deg)
    {
        return (double)(deg * Math.PI / (double)180d);
    }

    // 주어진 라디언(radian) 값을 도(degree) 값으로 변환  
    private double rad2deg2(double rad)
    {
        return (double)(rad * (double)180d / Math.PI);
    }

}

[System.Serializable]
public class PosState
{
    public double _lat;
    public double _long;
    public int monster_num;
}