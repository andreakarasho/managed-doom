//
// Copyright (C) 1993-1996 Id Software, Inc.
// Copyright (C) 2019-2020 Nobuaki Tanaka
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//



using System;
using System.Runtime.ExceptionServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ManagedDoom.UserInput
{
    public sealed class SfmlUserInput : IUserInput, IDisposable
    {
        private Config config;

        private DoomApplication doomApp;

        private bool useMouse;

        private bool[] weaponKeys;
        private int turnHeld;

        private bool mouseGrabbed;
        private int windowCenterX;
        private int windowCenterY;
        private int mouseX;
        private int mouseY;
        private bool cursorCentered;

        public SfmlUserInput(Config config, DoomApplication doomApp, bool useMouse)
        {
            try
            {
                Console.Write("Initialize user input: ");

                this.config = config;

                config.mouse_sensitivity = Math.Max(config.mouse_sensitivity, 0);

                this.doomApp = doomApp;

                this.useMouse = useMouse;

                weaponKeys = new bool[7];
                turnHeld = 0;

                mouseGrabbed = false;
                windowCenterX = doomApp.GraphicsDevice.PresentationParameters.BackBufferWidth / 2;
                windowCenterY = doomApp.GraphicsDevice.PresentationParameters.BackBufferHeight / 2;
                mouseX = 0;
                mouseY = 0;
                cursorCentered = false;

                Console.WriteLine("OK");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed");
                Dispose();
                ExceptionDispatchInfo.Throw(e);
            }
        }

        public void BuildTicCmd(TicCmd cmd)
        {
            /*
            var keyForward = IsPressed(config.key_forward);
            var keyBackward = IsPressed(config.key_backward);
            var keyStrafeLeft = IsPressed(config.key_strafeleft);
            var keyStrafeRight = IsPressed(config.key_straferight);
            var keyTurnLeft = IsPressed(config.key_turnleft);
            var keyTurnRight = IsPressed(config.key_turnright);
            var keyFire = IsPressed(config.key_fire);
            var keyUse = IsPressed(config.key_use);
            var keyRun = IsPressed(config.key_run);
            var keyStrafe = IsPressed(config.key_strafe);

            weaponKeys[0] = Keyboard.IsKeyPressed(Keyboard.Key.Num1);
            weaponKeys[1] = Keyboard.IsKeyPressed(Keyboard.Key.Num2);
            weaponKeys[2] = Keyboard.IsKeyPressed(Keyboard.Key.Num3);
            weaponKeys[3] = Keyboard.IsKeyPressed(Keyboard.Key.Num4);
            weaponKeys[4] = Keyboard.IsKeyPressed(Keyboard.Key.Num5);
            weaponKeys[5] = Keyboard.IsKeyPressed(Keyboard.Key.Num6);
            weaponKeys[6] = Keyboard.IsKeyPressed(Keyboard.Key.Num7);

            cmd.Clear();

            var strafe = keyStrafe;
            var speed = keyRun ? 1 : 0;
            var forward = 0;
            var side = 0;

            if (config.game_alwaysrun)
            {
                speed = 1 - speed;
            }

            if (keyTurnLeft || keyTurnRight)
            {
                turnHeld++;
            }
            else
            {
                turnHeld = 0;
            }

            int turnSpeed;
            if (turnHeld < PlayerBehavior.SlowTurnTics)
            {
                turnSpeed = 2;
            }
            else
            {
                turnSpeed = speed;
            }

            if (strafe)
            {
                if (keyTurnRight)
                {
                    side += PlayerBehavior.SideMove[speed];
                }
                if (keyTurnLeft)
                {
                    side -= PlayerBehavior.SideMove[speed];
                }
            }
            else
            {
                if (keyTurnRight)
                {
                    cmd.AngleTurn -= (short)PlayerBehavior.AngleTurn[turnSpeed];
                }
                if (keyTurnLeft)
                {
                    cmd.AngleTurn += (short)PlayerBehavior.AngleTurn[turnSpeed];
                }
            }

            if (keyForward)
            {
                forward += PlayerBehavior.ForwardMove[speed];
            }
            if (keyBackward)
            {
                forward -= PlayerBehavior.ForwardMove[speed];
            }

            if (keyStrafeLeft)
            {
                side -= PlayerBehavior.SideMove[speed];
            }
            if (keyStrafeRight)
            {
                side += PlayerBehavior.SideMove[speed];
            }

            if (keyFire)
            {
                cmd.Buttons |= TicCmdButtons.Attack;
            }

            if (keyUse)
            {
                cmd.Buttons |= TicCmdButtons.Use;
            }

            // Check weapon keys.
            for (var i = 0; i < weaponKeys.Length; i++)
            {
                if (weaponKeys[i])
                {
                    cmd.Buttons |= TicCmdButtons.Change;
                    cmd.Buttons |= (byte)(i << TicCmdButtons.WeaponShift);
                    break;
                }
            }

            UpdateMouse();
            var ms = 0.5F * config.mouse_sensitivity;
            var mx = (int)MathF.Round(ms * mouseX);
            var my = (int)MathF.Round(ms * mouseY);
            forward += my;
            if (strafe)
            {
                side += mx * 2;
            }
            else
            {
                cmd.AngleTurn -= (short)(mx * 0x8);
            }

            if (forward > PlayerBehavior.MaxMove)
            {
                forward = PlayerBehavior.MaxMove;
            }
            else if (forward < -PlayerBehavior.MaxMove)
            {
                forward = -PlayerBehavior.MaxMove;
            }
            if (side > PlayerBehavior.MaxMove)
            {
                side = PlayerBehavior.MaxMove;
            }
            else if (side < -PlayerBehavior.MaxMove)
            {
                side = -PlayerBehavior.MaxMove;
            }

            cmd.ForwardMove += (sbyte)forward;
            cmd.SideMove += (sbyte)side;
            */
        }

        private bool IsPressed(KeyBinding keyBinding)
        {
            /*
            foreach (var key in keyBinding.Keys)
            {
                if (Keyboard.IsKeyPressed((Keyboard.Key)key))
                {
                    return true;
                }
            }

            if (mouseGrabbed)
            {
                foreach (var mouseButton in keyBinding.MouseButtons)
                {
                    if (Mouse.IsButtonPressed((Mouse.Button)mouseButton))
                    {
                        return true;
                    }
                }
            }
            */

            return false;
        }

        public void Reset()
        {
            mouseX = 0;
            mouseY = 0;
            cursorCentered = false;
        }

        public void GrabMouse()
        {
            /*
            if (useMouse && !mouseGrabbed)
            {
                window.SetMouseCursorGrabbed(true);
                window.SetMouseCursorVisible(false);
                mouseGrabbed = true;
                mouseX = 0;
                mouseY = 0;
                cursorCentered = false;
            }
            */
        }

        public void ReleaseMouse()
        {
            /*
            if (useMouse && mouseGrabbed)
            {
                var posX = (int)(0.9 * window.Size.X);
                var posY = (int)(0.9 * window.Size.Y);
                Mouse.SetPosition(new Vector2i(posX, posY), window);
                window.SetMouseCursorGrabbed(false);
                window.SetMouseCursorVisible(true);
                mouseGrabbed = false;
            }
            */
        }

        private void UpdateMouse()
        {
            /*
            if (mouseGrabbed)
            {
                if (cursorCentered)
                {
                    var current = Mouse.GetPosition(window);

                    mouseX = current.X - windowCenterX;

                    if (config.mouse_disableyaxis)
                    {
                        mouseY = 0;
                    }
                    else
                    {
                        mouseY = -(current.Y - windowCenterY);
                    }
                }
                else
                {
                    mouseX = 0;
                    mouseY = 0;
                }

                Mouse.SetPosition(new Vector2i(windowCenterX, windowCenterY), window);
                var pos = Mouse.GetPosition(window);
                cursorCentered = (pos.X == windowCenterX && pos.Y == windowCenterY);
            }
            else
            {
                mouseX = 0;
                mouseY = 0;
            }
            */
        }

        public void Dispose()
        {
            Console.WriteLine("Shutdown user input.");

            ReleaseMouse();
        }

        public static DoomKey FromXnaKey(Keys xnaKey)
        {
            switch (xnaKey)
            {
                case Keys.Back: return DoomKey.Backspace;
                case Keys.Tab: return DoomKey.Tab;
                case Keys.Enter: return DoomKey.Enter;
                case Keys.Pause: return DoomKey.Pause;
                case Keys.Escape: return DoomKey.Escape;
                case Keys.Space: return DoomKey.Space;
                case Keys.PageUp: return DoomKey.PageUp;
                case Keys.PageDown: return DoomKey.PageDown;
                case Keys.End: return DoomKey.End;
                case Keys.Home: return DoomKey.Home;
                case Keys.Left: return DoomKey.Left;
                case Keys.Up: return DoomKey.Up;
                case Keys.Right: return DoomKey.Right;
                case Keys.Down: return DoomKey.Down;
                case Keys.Insert: return DoomKey.Insert;
                case Keys.Delete: return DoomKey.Delete;
                case Keys.A: return DoomKey.A;
                case Keys.B: return DoomKey.B;
                case Keys.C: return DoomKey.C;
                case Keys.D: return DoomKey.D;
                case Keys.E: return DoomKey.E;
                case Keys.F: return DoomKey.F;
                case Keys.G: return DoomKey.G;
                case Keys.H: return DoomKey.H;
                case Keys.I: return DoomKey.I;
                case Keys.J: return DoomKey.J;
                case Keys.K: return DoomKey.K;
                case Keys.L: return DoomKey.L;
                case Keys.M: return DoomKey.M;
                case Keys.N: return DoomKey.N;
                case Keys.O: return DoomKey.O;
                case Keys.P: return DoomKey.P;
                case Keys.Q: return DoomKey.Q;
                case Keys.R: return DoomKey.R;
                case Keys.S: return DoomKey.S;
                case Keys.T: return DoomKey.T;
                case Keys.U: return DoomKey.U;
                case Keys.V: return DoomKey.V;
                case Keys.W: return DoomKey.W;
                case Keys.X: return DoomKey.X;
                case Keys.Y: return DoomKey.Y;
                case Keys.Z: return DoomKey.Z;
                case Keys.NumPad0: return DoomKey.Numpad0;
                case Keys.NumPad1: return DoomKey.Numpad1;
                case Keys.NumPad2: return DoomKey.Numpad2;
                case Keys.NumPad3: return DoomKey.Numpad3;
                case Keys.NumPad4: return DoomKey.Numpad4;
                case Keys.NumPad5: return DoomKey.Numpad5;
                case Keys.NumPad6: return DoomKey.Numpad6;
                case Keys.NumPad7: return DoomKey.Numpad7;
                case Keys.NumPad8: return DoomKey.Numpad8;
                case Keys.NumPad9: return DoomKey.Numpad9;
                case Keys.Multiply: return DoomKey.Multiply;
                case Keys.Add: return DoomKey.Add;
                case Keys.Subtract: return DoomKey.Subtract;
                case Keys.Divide: return DoomKey.Divide;
                case Keys.F1: return DoomKey.F1;
                case Keys.F2: return DoomKey.F2;
                case Keys.F3: return DoomKey.F3;
                case Keys.F4: return DoomKey.F4;
                case Keys.F5: return DoomKey.F5;
                case Keys.F6: return DoomKey.F6;
                case Keys.F7: return DoomKey.F7;
                case Keys.F8: return DoomKey.F8;
                case Keys.F9: return DoomKey.F9;
                case Keys.F10: return DoomKey.F10;
                case Keys.F11: return DoomKey.F11;
                case Keys.F12: return DoomKey.F12;
                case Keys.F13: return DoomKey.F13;
                case Keys.F14: return DoomKey.F14;
                case Keys.F15: return DoomKey.F15;
                default: return DoomKey.Unknown;
            }
        }

        public int MaxMouseSensitivity
        {
            get
            {
                return 15;
            }
        }

        public int MouseSensitivity
        {
            get
            {
                return config.mouse_sensitivity;
            }

            set
            {
                config.mouse_sensitivity = value;
            }
        }
    }
}
