using System.Runtime.CompilerServices;

namespace SpaceInvaders.Graphics;

internal class Sprite : IDisposable
{
    private bool isDispose = false;

    private SDL.SDL_Rect srcRect;
    private SDL.SDL_FRect dsrRect;

    private readonly nint texturePtr;
    private readonly nint rendererPtr;

    public readonly int Width;
    public readonly int Height;
    public float HorizontalScale { get; set; }
    public float VerticalScale { get; set; }
    public float ActualWidth { get => Width * HorizontalScale; }
    public float ActualHeight { get => Height * VerticalScale; }
    public Color BrightColor { get; set; }
    public SDL.SDL_BlendMode BlendMode { get; set; }
    public SDL.SDL_RendererFlip Flip { get; set; }

    private byte alpha;
    public byte Alpha
    {
        get => alpha;
        set
        {
            if (value > byte.MaxValue)
                alpha = value;
            else if (value < 0)
                alpha = 0;
            else
                alpha = value;
        }
    }

    private double rotation;
    public double Rotation
    {
        get => rotation;
        set
        {
            if (value > 360)
                rotation = 360;
            else if (value < 0)
                rotation = 0;
            else
                rotation = value;
        }
    }

    public Sprite()
    {
        texturePtr = nint.Zero;
        rendererPtr = nint.Zero;
        Alpha = 255;
        HorizontalScale = 1.0f;
        VerticalScale = 1.0f;
        BrightColor = Color.White;
        BlendMode = SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND;
        Flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE;
    }

    public Sprite(nint renderer, string fileName)
        : this()
    {
        if (renderer == nint.Zero)
            throw new ArgumentNullException(nameof(renderer), "Null passed.");

        rendererPtr = renderer;

        texturePtr = SDL_image.IMG_LoadTexture(rendererPtr, fileName);
        if (texturePtr == nint.Zero)
            throw new FileLoadException("Failed to load image.");

        SDL.SDL_QueryTexture(texturePtr, out _, out _, out int w, out int h);
        Width = w;
        Height = h;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Render(float x, float y, Rectangle? rectangle = null)
    {
        if (texturePtr == nint.Zero || rendererPtr == nint.Zero)
            return;

        if (rectangle == null)
            rectangle = new(0, 0, Width, Height);

        srcRect.x = rectangle.Value.X;
        srcRect.y = rectangle.Value.Y;
        srcRect.w = rectangle.Value.Width;
        srcRect.h = rectangle.Value.Height;

        dsrRect.x = x;
        dsrRect.y = y;
        dsrRect.w = rectangle.Value.Width * HorizontalScale;
        dsrRect.h = rectangle.Value.Height * VerticalScale;

        SDL.SDL_SetTextureBlendMode(texturePtr, BlendMode);
        SDL.SDL_SetTextureAlphaMod(texturePtr, alpha);
        SDL.SDL_SetTextureColorMod(texturePtr, BrightColor.R, BrightColor.G, BrightColor.B);
        SDL.SDL_RenderCopyExF(rendererPtr, texturePtr, ref srcRect, ref dsrRect, Rotation, 0, Flip);
    }

    public void Dispose()
    {
        if (isDispose)
            return;

        isDispose = true;

        SDL.SDL_DestroyTexture(texturePtr);
    }
}