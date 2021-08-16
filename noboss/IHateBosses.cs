using Modding;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace noboss
{
    public class IHateBosses : Mod, ITogglableMod
    {
        string currentscene;
        public override string GetVersion()
        {
            return "3.0.1.1";
        }

        public IHateBosses() : base("noboss") 
        {
            return;
        }

        public override void Initialize()
        {
            Log("Initializing...");
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += SceneChange;
            ModHooks.Instance.HeroUpdateHook += Update;
            Log("Initializing done, have fun! ~Ruttie");
        }

        private void Update()
        {
            if (currentscene == null) return;
            if (!currentscene.Contains("GG_")) return;
            
            foreach (HealthManager hm in GameObject.FindObjectsOfType<HealthManager>())
            {
                Object.Destroy(hm.gameObject);
            }
        }

        private void SceneChange(Scene arg0, Scene arg1)
        {
            currentscene = arg1.name;
        }

        public void Unload()
        {
            Log("Unloading...");
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= SceneChange;
            ModHooks.Instance.HeroUpdateHook -= Update;
            currentscene = null;
            Log("Unloading done, goodbye.");
        }
    }
}
