﻿using GameCreator.Resources.Api;

namespace GameCreator.Engine
{
    public abstract partial class GameContext
    {
        public IndexedResourceManager<GameSound> Sounds { get; }
    }
}