﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameCreator.Framework.Gml.Interpreter;
using GameCreator.Framework;

namespace GameCreator.Runtime.Game.Interpreted
{
    public static class InterpretedGame
    {
        public static void Run()
        {
            //System.Windows.Forms.Application
            try
            {
                /* Define all of the scripts */
                foreach (var s in Script.Manager.Resources.Values)
                    Interpreter.DefineScript(s.Name, s.Id, s.Code);

                /* Compile the scripts */
                Interpreter.CompileScripts();

                Game.InitRoom += new Action<Room>(Game_InitRoom);

                Game.Run();
            }
            catch (ProgramError err)
            {
                int line = 0, col = 0;

                var node = Interpreter.ExecutingNode;
                if (node != null)
                {
                    line = node.Line;
                    col = node.Column;
                }

                string msg = string.Format("ERROR in code at line {0} pos {1}:\n{2}", line, col, err.Message);
                switch (err.Location)
                {
                    case CodeLocation.Script:
                        msg = string.Format("COMPILAION ERROR in Script:\nError in code at line {0}:\n\n\nat position {1}: {2}", line, col, err.Message);
                        break;
                }
                System.Windows.Forms.MessageBox.Show(msg, Game.Name, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public static void Initialize()
        {
            LibraryContext.Current = new LibraryContext(new InterpretedGameLibraryInitializer());

            Game.Init();
        }

        static void Game_InitRoom(Room room)
        {
            room.Init();
        }
    }
}
