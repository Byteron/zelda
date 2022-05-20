using System;
using System.Collections.Generic;
using Godot;
using RelEcs;
using RelEcs.Godot;

namespace Zelda.Core
{
    public class CurrentGameState
    {
        public GameState State;
    }

    public struct GodotInput
    {
        public InputEvent Event;
    }

    public class DeltaTime
    {
        public float Value;
    }

    public class GameStateController : Node
    {
        public RelEcs.World World = new();
        
        Dictionary<Type, GameState> states = new();
        Stack<GameState> stack = new();
        
        Commands commands;

        public GameStateController()
        {
            World.AddElement(this);

            World.AddElement(new CurrentGameState());
            World.AddElement(new DeltaTime());
        }

        public override void _Ready()
        {
            var tree = GetTree();
            World.AddElement(tree);

            var m = new Marshallable<Commands>(new Commands(World, null));
            tree.Root.PropagateCall("_Convert", new Godot.Collections.Array() { m });
        }

        public override void _UnhandledInput(InputEvent e)
        {
            if (stack.Count == 0)
            {
                return;
            }

            var currentState = stack.Peek();
            // TODO: insert godot event as trigger
            currentState.InputSystems.Run(World);
        }

        public override void _Process(float delta)
        {
            if (stack.Count == 0)
            {
                return;
            }

            var currentState = stack.Peek();
            World.GetElement<DeltaTime>().Value = delta;
            currentState.UpdateSystems.Run(World);

            World.Tick();
        }

        public override void _ExitTree()
        {
            foreach (var state in stack)
            {
                state.ExitSystems.Run(World);
            }
        }

        public void PushState(GameState newState)
        {
            CallDeferred(nameof(PushStateDeferred), newState);
        }

        public void PopState()
        {
            CallDeferred(nameof(PopStateDeferred));
        }

        public void ChangeState(GameState newState)
        {
            CallDeferred(nameof(ChangeStateDeferred), newState);
        }

        void PopStateDeferred()
        {
            if (stack.Count == 0)
            {
                return;
            }

            var currentState = stack.Pop();
            currentState.ExitSystems.Run(World);
            RemoveChild(currentState);
            currentState.QueueFree();

            if (stack.Count > 0)
            {
                currentState = stack.Peek();
                World.GetElement<CurrentGameState>().State = currentState;
                currentState.ContinueSystems.Run(World);
            }
        }

        void PushStateDeferred(GameState newState)
        {
            if (stack.Count > 0)
            {
                var currentState = stack.Peek();

                if (currentState.GetType() == newState.GetType())
                {
                    GD.PrintErr($"{currentState.GetType().ToString()} already at the top of the stack!");
                    return;
                }

                currentState.PauseSystems.Run(World);
            }

            newState.Name = newState.GetType().ToString();
            stack.Push(newState);
            AddChild(newState);
            World.GetElement<CurrentGameState>().State = newState;
            newState.Init(this);
            newState.InitSystems.Run(World);
        }

        void ChangeStateDeferred(GameState newState)
        {
            if (stack.Count > 0)
            {
                var currentState = stack.Pop();
                currentState.ExitSystems.Run(World);
                RemoveChild(currentState);
                currentState.QueueFree();
            }

            newState.Name = newState.GetType().ToString();
            stack.Push(newState);
            AddChild(newState);
            World.GetElement<CurrentGameState>().State = newState;
            newState.Init(this);
            newState.InitSystems.Run(World);
        }
    }
}