using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    public GameObject canvas;
    private string DataPath = "/Resources/Data";
    public List<CharacterSprite> CharacterSprites = new List<CharacterSprite>();
    public List<CharacterSprite> heads = new List<CharacterSprite>();
    public List<CharacterSprite> bodies = new List<CharacterSprite>();
    public List<CharacterSprite> toppings = new List<CharacterSprite>();
    public List<CharacterSprite> extras = new List<CharacterSprite>();

    private SpriteRenderer head;
    private SpriteRenderer body;
    private SpriteRenderer topping;
    private SpriteRenderer extra;

    private float currHead = -1;
    private float currBody = -1;
    private float currTopping = -1;
    private float currExtra = -1;

    // Use this for initialization
    void Start()
    {
        // Setup easy references to the Sprite Renderers
        head = transform.FindChild("Head").GetComponent<SpriteRenderer>();
        topping = head.transform.FindChild("Topping").GetComponent<SpriteRenderer>();
        extra = head.transform.FindChild("Extra").GetComponent<SpriteRenderer>();
        body = transform.FindChild("Body").GetComponent<SpriteRenderer>();

        // Load EVERYTHING :D
        LOADJSON();
    }

    // Function to go to the next sprite
    //part: 0=head, 1=body, 2=topping, 3=exrta
    public void ChangePart(int part, bool forward)
    {
        if (forward)
        {
            if (part == 0)
            {
                if (currHead + 1 == heads.Count)
                    currHead = 0;
                else
                    currHead += 1;

                head.sprite = heads[(int)currHead].sprites[1];
                canvas.transform.FindChild("Head Text").GetComponent<Text>().text = "Head (" + (currHead + 1) + "/" + heads.Count + ")";
            }
            else if (part == 1)
            {
                if (currBody + 1 == bodies.Count)
                    currBody = 0;
                else
                    currBody += 1;

                body.sprite = bodies[(int)currBody].sprites[1];
                canvas.transform.FindChild("Body Text").GetComponent<Text>().text = "Body (" + (currBody + 1) + "/" + bodies.Count + ")";
            }
            else if (part == 2)
            {
                if (currTopping + 1 == toppings.Count)
                    currTopping = 0;
                else
                    currTopping += 1;

                topping.sprite = toppings[(int)currTopping].sprites[1];
                canvas.transform.FindChild("Topping Text").GetComponent<Text>().text = "Topping (" + (currTopping + 1) + "/" + toppings.Count + ")";
            }
            else if (part == 3)
            {
                if (currExtra + 1 == extras.Count)
                    currExtra = 0;
                else
                    currExtra += 1;

                extra.sprite = extras[(int)currExtra].sprites[1];
                canvas.transform.FindChild("Extra Text").GetComponent<Text>().text = "Extra (" + (currExtra + 1) + "/" + extras.Count + ")";
            }
        }
        else
        {
            if (part == 0)
            {
                if (currHead - 1 == -1)
                    currHead = heads.Count-1;
                else
                    currHead -= 1;

                head.sprite = heads[(int)currHead].sprites[1];
                canvas.transform.FindChild("Head Text").GetComponent<Text>().text = "Head (" + (currHead + 1) + "/" + heads.Count + ")";
            }
            else if (part == 1)
            {
                if (currBody - 1 == -1)
                    currBody = bodies.Count-1;
                else
                    currBody -= 1;

                body.sprite = bodies[(int)currBody].sprites[1];
                canvas.transform.FindChild("Body Text").GetComponent<Text>().text = "Body (" + (currBody + 1) + "/" + bodies.Count + ")";
            }
            else if (part == 2)
            {
                if (currTopping - 1 == -1)
                    currTopping = toppings.Count-1;
                else
                    currTopping -= 1;

                topping.sprite = toppings[(int)currTopping].sprites[1];
                canvas.transform.FindChild("Topping Text").GetComponent<Text>().text = "Topping (" + (currTopping + 1) + "/" + toppings.Count + ")";
            }
            else if (part == 3)
            {
                if (currExtra - 1 == -1)
                    currExtra = extras.Count-1;
                else
                    currExtra -= 1;

                extra.sprite = extras[(int)currExtra].sprites[1];
                canvas.transform.FindChild("Extra Text").GetComponent<Text>().text = "Extra (" + (currExtra + 1) + "/" + extras.Count + ")";
            }
        }
    }

    void LOADJSON()
    {

        LoadSprites();
        // Find all json files in Resources/Data/
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + DataPath);
        FileInfo[] info = dir.GetFiles("*.json");

        // Parse all of them
        foreach (FileInfo file in info)
        {
            CreateSprites(JsonMapper.ToObject(File.ReadAllText(file.FullName)));
        }

        canvas.transform.FindChild("Head Text").GetComponent<Text>().text = "Head (0/" + heads.Count + ")";
        canvas.transform.FindChild("Body Text").GetComponent<Text>().text = "Body (0/" + bodies.Count + ")";
        canvas.transform.FindChild("Topping Text").GetComponent<Text>().text = "Topping (0/" + toppings.Count + ")";
        canvas.transform.FindChild("Extra Text").GetComponent<Text>().text = "Extra (0/" + extras.Count + ")";

        ChangePart(0, true);
        ChangePart(1, true);
        ChangePart(2, true);
        ChangePart(3, true);
    }

    public List<Sprite> usableSprites = new List<Sprite>();

    // Final-Loader for Sprites
    void CreateSprites(JsonData jsonData)
    {
        foreach (Texture2D texture in FoundImages)
        {
            if (texture.name == jsonData["texture"].ToString())
            {
                List<Sprite> quickList = new List<Sprite>();
                for (int i = 0; i < (int)jsonData["amount"]; i++)
                {
                    Sprite NewSprite = new Sprite();
                    NewSprite = Sprite.Create(texture, new Rect(i * (int)jsonData["size"], 0, (int)jsonData["size"], texture.height), new Vector2(0, 0), 100f);
                    NewSprite.name = jsonData["texture"] + "_" + i;
                    quickList.Add(NewSprite);
                }

                //Head
                if ((int)jsonData["part"] == 0)
                {
                    heads.Add(new CharacterSprite
                    {
                        name = jsonData["texture"].ToString(),
                        sprites = quickList
                    });
                    //Debug.Log("Chose head for " + jsonData["texture"].ToString());
                }
                //Body
                else if ((int)jsonData["part"] == 1)
                    bodies.Add(new CharacterSprite
                    {
                        name = jsonData["texture"].ToString(),
                        sprites = quickList
                    });
                //Topping
                else if ((int)jsonData["part"] == 2)
                    toppings.Add(new CharacterSprite
                    {
                        name = jsonData["texture"].ToString(),
                        sprites = quickList
                    });
                //Extra
                else if ((int)jsonData["part"] == 3)
                    extras.Add(new CharacterSprite
                    {
                        name = jsonData["texture"].ToString(),
                        sprites = quickList
                    });
            }
        }
    }

    // Half-Loader for Sprites
    void LoadSprites()
    {
        // Characters folder
        DirectoryInfo chardir = new DirectoryInfo(Application.dataPath + "/Resources/Sprites/Characters/");
        FileInfo[] charinfo = chardir.GetFiles("*.png");
        foreach (FileInfo file in charinfo)
        {
            Debug.Log("Characters folder: " + file.Name);
            FoundImages.Add(LoadNewSprite(file.FullName));
        }

        // Bodies folder
        DirectoryInfo bodydir = new DirectoryInfo(Application.dataPath + "/Resources/Sprites/Characters/Bodies/");
        FileInfo[] bodyinfo = bodydir.GetFiles("*.png");
        foreach (FileInfo file in bodyinfo)
        {
            Debug.Log("Bodies folder: " + file.Name);
            FoundImages.Add(LoadNewSprite(file.FullName));
        }

        // Heads folder
        DirectoryInfo headdir = new DirectoryInfo(Application.dataPath + "/Resources/Sprites/Characters/Heads/");
        FileInfo[] headinfo = headdir.GetFiles("*.png");
        foreach (FileInfo file in headinfo)
        {
            Debug.Log("Heads folder: " + file.Name);
            FoundImages.Add(LoadNewSprite(file.FullName));
        }

        // Toppings folder
        DirectoryInfo toppingdir = new DirectoryInfo(Application.dataPath + "/Resources/Sprites/Characters/Toppings/");
        FileInfo[] toppinginfo = toppingdir.GetFiles("*.png");
        foreach (FileInfo file in toppinginfo)
        {
            Debug.Log("Toppings folder: " + file.Name);
            FoundImages.Add(LoadNewSprite(file.FullName));
        }

        // Extras folder
        DirectoryInfo extradir = new DirectoryInfo(Application.dataPath + "/Resources/Sprites/Characters/Extras/");
        FileInfo[] extrainfo = extradir.GetFiles("*.png");
        foreach (FileInfo file in extrainfo)
        {
            Debug.Log("Extras folder: " + file.Name);
            FoundImages.Add(LoadNewSprite(file.FullName));
        }
    }

    public List<Texture2D> FoundImages = new List<Texture2D>();

    public Texture2D LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f)
    {
        Texture2D SpriteTexture = LoadTexture(FilePath);
        SpriteTexture.name = Path.GetFileName(FilePath);
        SpriteTexture.filterMode = FilterMode.Point;
        SpriteTexture.name = Path.GetFileName(FilePath).Replace(".png", "");
        return SpriteTexture;
    }

    public Texture2D LoadTexture(string FilePath)
    {
        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }



    // Functions for buttons!

    public void NextSprite(int part)
    {
        ChangePart(part, true);
    }

    public void PrevSprite(int part)
    {
        ChangePart(part, false);
    }
}

public class CharacterSprite
{
    public string name;
    public List<Sprite> sprites;
}
