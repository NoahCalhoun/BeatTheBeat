using System.Collections;

using UnityEngine;

abstract public class BBPostEffectBase : BBBase
{
    public class PostEffectData
    {

    }

    protected Material Material;
    abstract protected string MaterialName { get; }

    protected Coroutine Working;

    public bool IsWorking { get { return Working != null; } }

    // Use this for initialization
    void Start ()
    {
        Material = Resources.Load<Material>(string.Format("Materials/{0}", MaterialName));
    }

    virtual public void BeginEffect(PostEffectData database = null)
    {
        if (Working != null)
        {
            StopCoroutine(Working);
            Working = null;
        }

        Working = StartCoroutine(PostEffect());
    }

    abstract protected IEnumerator PostEffect();

    abstract protected void OnRenderImage(RenderTexture source, RenderTexture destination);
}
