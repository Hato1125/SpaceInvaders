using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpaceInvaders.Graphics;

internal class Font16x16
{
    private const int FONT_SIZE = 16;
    private const int ALPHABET_NUM = 26;

    private readonly Sprite fontSprite;
    private readonly List<sbyte[]> charIndexs = new();

    public Color TextColor { get; set; }
    public float Scale { get; set; }
    public int TextSpace { get; set; }
    public int LineSpace { get; set; }

    private string _text = string.Empty;
    public string Text
    {
        get => _text;
        set
        {
            if (value == _text)
                return;

            SetCharIndexs(value);

            _text = value;
        }
    }

    public float Width
        => charIndexs.Max(d => d.Length) * (FONT_SIZE * Scale + TextSpace) - TextSpace;

    public float Height
        => charIndexs.Count * (FONT_SIZE * Scale + LineSpace) - LineSpace;

    public Font16x16(Sprite font)
    {
        fontSprite = font;

        Scale = 1.0f;
        TextSpace = 0;
        LineSpace = 0;
        TextColor = Color.White;
    }

    public void Render(float x, float y, FontArrangement fontArrangement = FontArrangement.Left)
    {
        float positionX = 0;
        float positionY = 0;

        var hScale = fontSprite.HorizontalScale;
        var vScale = fontSprite.VerticalScale;
        var color = fontSprite.BrightColor;

        fontSprite.HorizontalScale = Scale;
        fontSprite.VerticalScale = Scale;
        fontSprite.BrightColor = TextColor;

        var span = CollectionsMarshal.AsSpan(charIndexs);
        for (int i = 0; i < span.Length; i++)
        {
            for (int j = 0; j < span[i].Length; j++)
            {
                if (i == -1)
                    continue;

                var fontPosition = CalclateFontPosition(fontArrangement, span[i].Length);
                if (span[i][j] < ALPHABET_NUM)
                {
                    fontSprite.Render(x + fontPosition + positionX, y + positionY, new(span[i][j] * FONT_SIZE, 0, FONT_SIZE, FONT_SIZE));
                }
                else
                {
                    int numIndex = span[i][j] - ALPHABET_NUM;
                    fontSprite.Render(x + fontPosition + positionX, y + positionY, new(numIndex * FONT_SIZE, FONT_SIZE, FONT_SIZE, FONT_SIZE));
                }

                positionX += FONT_SIZE * Scale + TextSpace;
            }

            positionX = 0;
            positionY += FONT_SIZE * Scale + LineSpace;
        }

        fontSprite.HorizontalScale = hScale;
        fontSprite.VerticalScale = vScale;
        fontSprite.BrightColor = color;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float CalclateFontPosition(FontArrangement fontArrangement, in int charLenght)
    {
        var textWidth = (FONT_SIZE * Scale + TextSpace) * charLenght - TextSpace;

        return fontArrangement switch
        {
            FontArrangement.Left => 0,
            FontArrangement.Center => (Width - textWidth) / 2.0f,
            FontArrangement.Right => Width - textWidth,
            _ => 0
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetCharIndexs(string text)
    {
        charIndexs.Clear();

        if (string.IsNullOrWhiteSpace(text))
            return;

            var split = text.Split('\n');

            for (int i = 0; i < split.Length; i++)
            {
                charIndexs.Add(new sbyte[split[i].Length]);

                for (int j = 0; j < split[i].Length; j++)
                    charIndexs[i][j] = GetCharIndex(split[i][j]);
            }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static sbyte GetCharIndex(in char ch) => ch switch
    {
        'a' or 'A' => 0,
        'b' or 'B' => 1,
        'c' or 'C' => 2,
        'd' or 'D' => 3,
        'e' or 'E' => 4,
        'f' or 'F' => 5,
        'g' or 'G' => 6,
        'h' or 'H' => 7,
        'i' or 'I' => 8,
        'j' or 'J' => 9,
        'k' or 'K' => 10,
        'l' or 'L' => 11,
        'm' or 'M' => 12,
        'n' or 'N' => 13,
        'o' or 'O' => 14,
        'p' or 'P' => 15,
        'q' or 'Q' => 16,
        'r' or 'R' => 17,
        's' or 'S' => 18,
        't' or 'T' => 19,
        'u' or 'U' => 20,
        'v' or 'V' => 21,
        'w' or 'W' => 22,
        'x' or 'X' => 23,
        'y' or 'Y' => 24,
        'z' or 'Z' => 25,
        '0' => 26,
        '1' => 27,
        '2' => 28,
        '3' => 29,
        '4' => 30,
        '5' => 31,
        '6' => 32,
        '7' => 33,
        '8' => 34,
        '9' => 35,
        ' ' => -1,
        _ => -1
    };
}