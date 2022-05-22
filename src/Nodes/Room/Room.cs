using Godot;

public class Tile
{
    public int Value;
    public Tile(int value) => Value = value;
}

public class Coordinates : Object
{
    public Vector2 Value;
    public Coordinates(Vector2 value) => Value = value;
}

public class Blocked { }
public class Water { }
public class TileOf { }

public class Room : Node2D
{
    public static Vector2 Size = new(21, 13);

    [Export] public Vector2 Coordinates;

    public override void _Ready()
    {
        // Position = Coordinates * Size * Vector2.One * 16f;
    }

    // public void _Convert(Marshallable<Commands> commands)
    // {
    //     var entity = commands.Value.Spawn();
    //     entity.Add(this);
    //     
    //     var groundMap = GetNode<TileMap>("Map/Ground");
    //     var waterMap = GetNode<TileMap>("Map/Water");
    //     var wallMap = GetNode<TileMap>("Map/Walls");
    //
    //     for (var y = 0; y < Size.y; y++)
    //     {
    //         for (var x = 0; x < Size.x; x++)
    //         {
    //             var cell = new Vector2(x, y) * Coordinates;
    //             
    //             var tileEntity = commands.Value.Spawn()
    //                 .Add(new Coordinates(x, y))
    //                 .Add<TileOf>(entity);
    //     
    //             if (waterMap.GetCellv(cell) != TileMap.InvalidCell)
    //             {
    //                 tileEntity.Add(new Tile(waterMap.GetCellv(cell)))
    //                     .Add<Blocked>()
    //                     .Add<Water>();
    //             }
    //             else
    //             {
    //                 tileEntity.Add(new Tile(groundMap.GetCellv(cell)));
    //                 
    //                 if (wallMap.GetCellv(cell) != TileMap.InvalidCell)
    //                 {
    //                     tileEntity.Add<Blocked>();
    //                 }
    //             }
    //         }
    //     }
    // }
}