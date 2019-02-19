using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public static class DebugUI
{
    public static void CreateButton(Vector2 position, string text, UnityAction callback)
    {
        var canvas = FindDebugCanvas();

        Button button = CreateButton(text, callback);
        button.transform.SetParent(canvas.transform);
        button.transform.localPosition = position;
    }

    private static Canvas FindDebugCanvas()
    {
        var go = GameObject.Find("DebugCanvas");
        Canvas canvas = null;
        
        if (go)
            canvas = go.GetComponent<Canvas>();

        if (!canvas)
            canvas = CreateCanvas();

        var eventSystem = Object.FindObjectOfType<EventSystem>();
        if (!eventSystem)
            CreateEventSystem();

        return canvas;
    }

    private static Canvas CreateCanvas()
    {
        var canvas = Resources.Load<Canvas>("Debug/UI/DebugCanvas");
        canvas = Object.Instantiate(canvas);
        canvas.name = "DebugCanvas";

        return canvas;
    }

    private static void CreateEventSystem()
    {
        var go = new GameObject("EventSystem");
        go.AddComponent<EventSystem>();
        go.AddComponent<StandaloneInputModule>();
    }

    private static Button CreateButton(string text, UnityAction callback)
    {
        Button button = Resources.Load<Button>("Debug/UI/Button");
        button = Object.Instantiate(button);

        button.GetComponentInChildren<Text>().text = text;
        button.onClick.AddListener(callback);

        return button;
    }
}
