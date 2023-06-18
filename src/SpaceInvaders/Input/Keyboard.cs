using System.Runtime.InteropServices;
using SDL2;

namespace SpaceInvaders.Input;

internal static class Keyboard
{
    private static readonly byte[] keyState = new byte[512];
    private static readonly sbyte[] keyValue = new sbyte[512];

    public static void Update()
    {
        var state = SDL.SDL_GetKeyboardState(out int length);
        Marshal.Copy(state, keyState, 0, length);

        for(int i = 0; i < keyState.Length; i++)
        {
            if(keyState[i] == 1)
                keyValue[i] = (sbyte)(IsPushing((SDL.SDL_Scancode)i) ? 2 : 1);
            else
                keyValue[i] = (sbyte)(IsPushing((SDL.SDL_Scancode)i) ? -1 : 0);
        }
    }

    public static bool IsPushing(SDL.SDL_Scancode keyCode)
        => keyValue[(int)keyCode] > 0;

    public static bool IsPushed(SDL.SDL_Scancode keyCode)
        => keyValue[(int)keyCode] == 1;

    public static bool IsSeparate(SDL.SDL_Scancode keyCode)
        => keyValue[(int)keyCode] == -1;
}