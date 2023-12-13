using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class _Canvas : MonoBehaviour
{
    // Start is called before the first frame update
    Canvas[] _canvas;
    [SerializeField] Camera cam;
    public state State;
    public enum state { Camera, tracker }
    public int find_all_canvas()
    {
        _canvas = Resources.FindObjectsOfTypeAll(typeof(Canvas)) as Canvas[];
        return _canvas.Length;
    }
    public void delete_canvas()
    {
        for (int i = 0; i < _canvas.Length; i++)
        {
            DestroyImmediate(_canvas[i].gameObject);
        }
    }
    public void target()
    {
        switch (State)
        {
            case state.Camera:
                add_camera();
                break;
            case state.tracker:
                add_tracker();
                break;
        }
    }
    void add_tracker()
    {
        for (int i = 0; i < _canvas.Length; i++)
        {
            if (!_canvas[i].TryGetComponent<TrackedDeviceGraphicRaycaster>(out TrackedDeviceGraphicRaycaster found))
            {
                _canvas[i].transform.gameObject.AddComponent<TrackedDeviceGraphicRaycaster>();
            }
        }
    }
    void add_camera()
    {
        for (int i = 0; i < _canvas.Length; i++)
        {
            _canvas[i].renderMode = RenderMode.WorldSpace;
            _canvas[i].worldCamera = cam;
        }
    }
}
