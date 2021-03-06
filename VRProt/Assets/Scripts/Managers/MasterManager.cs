﻿using UnityEngine;
using Utility;

namespace Managers
{
    [CreateAssetMenu(menuName = "Singletons/MasterManager")]
    public class MasterManager : SingletonScriptableObject<MasterManager>
    {
        [SerializeField] private GameSettings gameSettings;
        public GameSettings GameSettings => Instance.gameSettings;
    }
}
