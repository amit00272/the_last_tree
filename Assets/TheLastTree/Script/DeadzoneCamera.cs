using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Camera))]
public class DeadzoneCamera : MonoBehaviour
{
    public Renderer target;
    public Rect deadzone;
    public Vector3 smoothPos;

    public Rect[] limits;
    public float verticalEndPoint;
    protected Camera _camera;
    protected Vector3 _currentVelocity;

    public bool gameover ;

    public Transform endPoint;



    public void Start()
    {
        smoothPos = target.transform.position;
        smoothPos.z = transform.position.z;
        _currentVelocity = Vector3.zero;
        gameover = false;

        _camera = GetComponent<Camera>();
        if(!_camera.orthographic)
        {
            Debug.LogError("deadzone script require an orthographic camera!");
            Destroy(this);
        }
    }

    private void OnEnable() {
        if(gameover)
            gameover = false;
    }


    public void moveCameraToTarget_OnResumeGame(Vector3 lastPlayrPos)
    {


        Camera.main.transform.position =  new Vector3(Camera.main.transform.position.x, lastPlayrPos.y,Camera.main.transform.position.z);
        smoothPos = new Vector3(Camera.main.transform.position.x, lastPlayrPos.y, Camera.main.transform.position.z);
        gameover = false;


    }

    void DelayResetGameOver(){
        gameover = false;
    }
    public void Update()
    {

        if(gameover) return;
        float localX = target.transform.position.x - transform.position.x;
        float localY = target.transform.position.y - transform.position.y;


        if(target.transform.position.y < verticalEndPoint){
            gameover = true;
            Camera.main.transform.position =  new Vector3(Camera.main.transform.position.x, 1.18f,Camera.main.transform.position.z);
            PlayUIManager.instance.onfailed();
            return;

        }

        if(target.transform.position.x>endPoint.position.x){

            gameover = true;
            Camera.main.transform.position =  new Vector3(Camera.main.transform.position.x, 1.18f,Camera.main.transform.position.z);
            PlayUIManager.instance.GameOver();
            return;
        }


       // target.GetComponent<PlayerMovement>().localX=localX;

        if (localX < deadzone.xMin)
        {
            //smoothPos.x += localX - deadzone.xMin;

        }
        else if (localX > deadzone.xMax)
        {
            smoothPos.x += localX - deadzone.xMax;
        }

        if (localY < deadzone.yMin)
        {
            smoothPos.y += localY - deadzone.yMin;
        }
        else if (localY > deadzone.yMax)
        {
            smoothPos.y += localY - deadzone.yMax;
        }

        Rect camWorldRect = new Rect();
        camWorldRect.min = new Vector2(smoothPos.x - _camera.aspect * _camera.orthographicSize, smoothPos.y - _camera.orthographicSize);
        camWorldRect.max = new Vector2(smoothPos.x + _camera.aspect * _camera.orthographicSize, smoothPos.y + _camera.orthographicSize);

       // target.GetComponent<PlayerMovement>().leftEndX=camWorldRect.min.x;

        for (int i = 0; i < limits.Length; ++i)
        {
            if (limits[i].Contains(target.transform.position))
            {
                Vector3 localOffsetMin = limits[i].min + camWorldRect.size * 0.5f;
                Vector3 localOffsetMax = limits[i].max - camWorldRect.size * 0.5f;

                localOffsetMin.z = localOffsetMax.z = smoothPos.z;

                smoothPos = Vector3.Max(smoothPos, localOffsetMin);
                smoothPos = Vector3.Min(smoothPos, localOffsetMax);

                break;
            }
        }

        Vector3 current = transform.position;
        current.x = smoothPos.x; // we don't smooth horizontal movement


        transform.position = Vector3.SmoothDamp(current, smoothPos, ref _currentVelocity, 0.2f);
        target.GetComponent<PlayerMovement>().leftEndX=transform.position.x-9.5f;
       //transform.position=Vector3.Lerp(current,smoothPos,0.125f*Time.deltaTime);
    }

// private void FixedUpdate() {
//         Vector3 current = transform.position;
//         transform.position=Vector3.Lerp(current,smoothPos,0.125f*Time.deltaTime);
//     }
}

#if UNITY_EDITOR

[CustomEditor(typeof(DeadzoneCamera))]
public class DeadZonEditor : Editor
{
    public void OnSceneGUI()
    {
        DeadzoneCamera cam = target as DeadzoneCamera;

        Vector3[] vert =
        {
            cam.transform.position + new Vector3(cam.deadzone.xMin, cam.deadzone.yMin, 0),
            cam.transform.position + new Vector3(cam.deadzone.xMax, cam.deadzone.yMin, 0),
            cam.transform.position + new Vector3(cam.deadzone.xMax, cam.deadzone.yMax, 0),
            cam.transform.position + new Vector3(cam.deadzone.xMin, cam.deadzone.yMax, 0)
        };

        Color transp = new Color(0, 0, 0, 0);
        Handles.DrawSolidRectangleWithOutline(vert, transp, Color.red);

        for(int i = 0; i < cam.limits.Length; ++i)
        {
            Vector3[] vertLimit =
           {
                new Vector3(cam.limits[i].xMin, cam.limits[i].yMin, 0),
                new Vector3(cam.limits[i].xMax, cam.limits[i].yMin, 0),
                new Vector3(cam.limits[i].xMax, cam.limits[i].yMax, 0),
                new Vector3(cam.limits[i].xMin, cam.limits[i].yMax, 0)
            };

            Handles.DrawSolidRectangleWithOutline(vertLimit, transp, Color.green);
        }
    }
}
#endif