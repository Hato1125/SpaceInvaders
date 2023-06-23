namespace SpaceInvaders.Scenes.Game;

internal static class DebugUtilt
{
    private static SDL.SDL_FRect rect;

    public static void WireRect(float x, float y, float width, float height, Color color)
    {
        rect.x = x;
        rect.y = y;
        rect.w = width;
        rect.h = height;

        SDL.SDL_SetRenderDrawColor(App.App.Window.RendererPtr, color.R, color.G, color.B, 255);
        SDL.SDL_RenderDrawRectF(App.App.Window.RendererPtr, ref rect);
        SDL.SDL_SetRenderDrawColor(App.App.Window.RendererPtr, 0, 0, 0, 255);
    }
}