using Godot;

public class MiniMap : CanvasLayer
{
    TextureRect rect;
    ImageTexture texture = new();
    Image image = new();
    
    Vector2 activeRoom;
    
    public override void _Ready()
    {
        rect = GetNode<TextureRect>("TextureRect");
        
        image.Create(8, 16, false, Image.Format.Rgba8);

        SetActiveRoom(Vector2.One);
    }

    public void SetActiveRoom(Vector2 room)
    {
        if (room == activeRoom) return;
        
        activeRoom = room;

        image.Fill(Colors.Gray);
        image.Lock();
        image.SetPixelv(room, Colors.Green);
        
        image.Unlock();
        
        texture.CreateFromImage(image, 0);
        
        GD.Print("ActiveRoom Drawn: ", room);
        
        rect.Texture = texture;
    }
}
