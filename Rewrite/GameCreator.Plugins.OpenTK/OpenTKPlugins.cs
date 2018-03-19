﻿using System;
using GameCreator.Engine;

namespace GameCreator.Plugins.OpenTK
{
    public sealed class OpenTKPlugins : IDisposable
    {
        private readonly GameCreatorOpenTKGameWindow _gameWindow;
        public IGraphicsPlugin Graphics { get; }
        public IAudioPlugin Audio { get; }
        public IInputPlugin Input { get; }
        public ITimerPlugin Timer { get; }

        public OpenTKPlugins()
        {
            _gameWindow = new GameCreatorOpenTKGameWindow();
            Graphics = new OpenTKGraphicsPlugin(_gameWindow);
            Audio = new OpenTKAudioPlugin(_gameWindow);
            Input = new OpenTKInputPlugin(_gameWindow);
            Timer = new OpenTKTimerPlugin(_gameWindow);
        }

        public void Dispose()
        {
            _gameWindow?.Dispose();
        }
    }
}