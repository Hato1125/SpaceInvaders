using SpaceInvaders.Graphics;

namespace SpaceInvaders.Resource;

internal record SpriteResource
{
    public readonly string ResourceFileName;
    public Sprite? Resource;

    public SpriteResource(string fileName)
    {
        ResourceFileName = fileName;
    }
}

internal static class SpriteManager
{
    private static readonly Dictionary<string, SpriteResource> resources = new();

    public static IReadOnlyDictionary<string, SpriteResource> RegistSpriteList
        => resources;

    public static void RegistSprite(string name, string fileName)
    {
        if (!resources.ContainsKey(name))
            return;

        resources.Add(name, new(fileName));
    }

    public static void RegistSprite(nint renderer, string name, string fileName)
    {
        if (resources.ContainsKey(name))
            return;

        var resource = new SpriteResource(fileName)
        {
            Resource = new(renderer, fileName),
        };

        resources.Add(name, resource);
    }

    public static void LoadSprite(nint renderer, string name)
    {
        if (!resources.ContainsKey(name))
            return;

        if (resources[name].Resource == null)
            resources[name].Resource = new(renderer, resources[name].ResourceFileName);
    }

    public static void LoadSprite(nint renderer, string[] name)
    {
        foreach (var resname in name)
            LoadSprite(renderer, resname);
    }

    public static Sprite GetResource(string name)
    {
        if (!resources.ContainsKey(name))
            return new();

        return resources[name].Resource ?? new();
    }

    public static Sprite[] GetResource(string[] name)
    {
        var spriteArray = new Sprite[name.Length];

        for (int i = 0; i < name.Length; i++)
            spriteArray[i] = GetResource(name[i]);

        return spriteArray;
    }

    public static void ReleaseResource(string name)
    {
        if (!resources.ContainsKey(name) || resources[name].Resource == null)
            return;

        resources[name].Resource?.Dispose();
        resources[name].Resource = null;
    }

    public static void ReleaseResource(string[] name)
    {
        foreach (var resname in name)
            ReleaseResource(resname);
    }

    public static void DeleteResource(string name)
    {
        if (resources.ContainsKey(name))
            return;

        if (resources[name].Resource != null)
        {
            resources[name].Resource?.Dispose();
            resources[name].Resource = null;
        }

        resources.Remove(name);
    }

    public static void DeleteResource(string[] name)
    {
        foreach (var resname in name)
            DeleteResource(resname);
    }

    public static void DeleteAllResource()
    {
        foreach (var resource in resources)
        {
            if (resource.Value.Resource != null)
            {
                resource.Value.Resource.Dispose();
                resource.Value.Resource = null;
            }
        }

        resources.Clear();
    }
}