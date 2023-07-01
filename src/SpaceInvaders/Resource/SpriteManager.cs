using SpaceInvaders.Graphics;

namespace SpaceInvaders.Resource;

internal record SpriteResource
{
    public readonly string ResourceName;
    public readonly string ResourceFileName;

    public Sprite? Resource;

    public SpriteResource(string name, string fileName)
    {
        ResourceName = name;
        ResourceFileName = fileName;
    }
}

internal static class SpriteManager
{
    private static readonly List<SpriteResource> resources = new();

    public static IReadOnlyList<SpriteResource> RegistSpriteList
        => resources;

    public static void RegistSprite(string name, string fileName)
    {
        if (resources.Find(r => r.ResourceName == name) != null)
            return;

        resources.Add(new(name, fileName));
    }

    public static void RegistSprite(nint renderer, string name, string fileName)
    {
        if (resources.Find(r => r.ResourceName == name) != null)
            return;

        var resource = new SpriteResource(name, fileName)
        {
            Resource = new(renderer, fileName),
        };

        resources.Add(resource);
    }

    public static void LoadSprite(nint renderer, string name)
    {
        var resource = resources.Find(r => r.ResourceName == name);

        if (resource == null)
            return;

        if (resource.Resource == null)
            resource.Resource = new(renderer, resource.ResourceFileName);
    }

    public static void LoadSprite(nint renderer, string[] name)
    {
        foreach (var resname in name)
            LoadSprite(renderer, resname);
    }

    public static Sprite? GetResource(string name)
    {
        var resource = resources.Find(r => r.ResourceName == name);

        if (resource == null)
            return null;

        return resource.Resource;
    }

    public static Sprite?[] GetResource(string[] name)
    {
        var spriteArray = new Sprite?[name.Length];

        for (int i = 0; i < name.Length; i++)
            spriteArray[i] = GetResource(name[i]);

        return spriteArray;
    }

    public static void ReleaseResource(string name)
    {
        var resource = resources.Find(r => r.ResourceName == name);

        if (resource == null || resource.Resource == null)
            return;

        resource.Resource.Dispose();
        resource.Resource = null;
    }

    public static void ReleaseResource(string[] name)
    {
        foreach (var resname in name)
            ReleaseResource(resname);
    }

    public static void DeleteResource(string name)
    {
        var resource = resources.Find(r => r.ResourceName == name);

        if (resource == null)
            return;

        if (resource.Resource != null)
        {
            resource.Resource.Dispose();
            resource.Resource = null;
        }

        resources.Remove(resource);
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
            if (resource.Resource != null)
            {
                resource.Resource.Dispose();
                resource.Resource = null;
            }
        }

        resources.Clear();
    }
}