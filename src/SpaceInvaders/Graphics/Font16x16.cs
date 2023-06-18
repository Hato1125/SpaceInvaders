namespace SpaceInvaders.Graphics;

internal class Font16x16
{
    private const int FONT_SIZE = 16;
    private const int ALPHABET_NUM = 26;

    private readonly Sprite fontSprite;
    private readonly List<sbyte[]> charIndexs = new();

    private string _text = string.Empty;
    public string Text
    {
        get => _text;
        set
        {
            if (value == _text)
                return;

            charIndexs.Clear();

            if (!string.IsNullOrWhiteSpace(value))
            {
                var split = value.Split('\n');

                Console.WriteLine(split.Length);

                for (int i = 0; i < split.Length; i++)
                {
                    charIndexs.Add(new sbyte[split[i].Length]);

                    for (int j = 0; j < split[i].Length; j++)
                        charIndexs[i][j] = GetCharIndex(split[i][j]);
                }
            }

            _text = value;
        }
    }

    private float scale;
    public float Scale
    {
        get => scale;
        set
        {
            scale = value;

            fontSprite.HorizontalScale = value;
            fontSprite.VerticalScale = value;
        }
    }

    public int TextSpace { get; set; }
    public int LineSpace { get; set; }

    public Font16x16(Sprite font)
    {
        fontSprite = font;

        Scale = 1.0f;
        TextSpace = 0;
        LineSpace = 0;
    }

    public void Render(float x, float y)
    {
        float positionX = 0;
        float positionY = 0;

        for(int i = 0; i < charIndexs.Count; i++)
        {
            for (int j = 0; j < charIndexs[i].Length; j++)
            {
                positionX += FONT_SIZE * scale + TextSpace;

                if (i == -1)
                    continue;

                if (charIndexs[i][j] < ALPHABET_NUM)
                {
                    fontSprite.Render(x + positionX, y + positionY, new(charIndexs[i][j] * FONT_SIZE, 0, FONT_SIZE, FONT_SIZE));
                }
                else
                {
                    int numIndex = charIndexs[i][j] - ALPHABET_NUM;
                    fontSprite.Render(x + positionX, y + positionY, new(numIndex * FONT_SIZE, FONT_SIZE, FONT_SIZE, FONT_SIZE));
                }
            }

            positionX = 0;
            positionY += FONT_SIZE * scale + LineSpace;
        }
    }

    private static sbyte GetCharIndex(char ch) => ch switch
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