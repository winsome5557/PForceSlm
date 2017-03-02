using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework.Internal;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Common.Domain.Loc;
using ParcelForce.Test.Domain.Machine;
using ParcelForce.Test.Domain.Machine.Commands;
using ParcelForce.Test.Domain.Machine.Events;

namespace ParcelForce.Test.Domain.Machine    
{
    public class LawnMowerMachine : ILawnMowerMachine, IEntity, IDisposable
    {

        private ConcurrentQueue<ILawnMowerMachineCommand> _commandQueue = new ConcurrentQueue<ILawnMowerMachineCommand>();

        public Direction direction { get; set; }
        public ILocation CurrentLocation { get; set; }

        private int _maxX;
        public int MaxX {
            get
            {
                return _maxX;
            } 
            set
            {
                _maxX = value;
                StatusChanged?.Invoke(this, new LawnMowerMachineEventArgs(direction,  MaxX, MaxY, CurrentLocation,  LastCommandLog, Mowing, LastCommand, - 1 ));
            }
                }


        private int _maxY;
        public int MaxY
        {
            get
            {
                return _maxY;
            }
            set
            {
                _maxY = value;
                StatusChanged?.Invoke(this, new LawnMowerMachineEventArgs(direction, MaxX, MaxY, CurrentLocation, LastCommandLog, Mowing, LastCommand, - 1));
            }
        }

        public Boolean Mowing { get; private set; }

        public event EventHandler<LawnMowerMachineEventArgs> StatusChanged;

        public LawnMowerMachine()
        {
            Id = Guid.NewGuid();
            direction = Direction.North;
            CurrentLocation = new Location { X = 0, Y = 0 };
        }

        public LawnMowerMachine( ILocation position)
        {
            Id = Guid.NewGuid();
            direction = Direction.North;
            CurrentLocation = position;
        }

        public void RotateRight()
        {
            DateTime beforeAction = DateTime.Now;
            direction = direction == (Direction.West) ? Direction.North : direction + 1;
            System.Threading.Thread.Sleep(15000);
            DateTime afteAction = DateTime.Now;
            LastCommandLog = $"{beforeAction.TimeOfDay.ToString("g")} Start RotateRight - After the action finished at {afteAction.TimeOfDay.ToString("g")} End RotateRight";
            StatusChanged?.Invoke(this, new LawnMowerMachineEventArgs(direction, MaxX, MaxY, CurrentLocation, LastCommandLog, Mowing, LastCommand, - 1 ));
        }

        public void RotateLeft()
        {
            DateTime beforeAction = DateTime.Now;
            direction = direction == Direction.North ? Direction.West : direction - 1;
            System.Threading.Thread.Sleep(15000);
            DateTime afteAction = DateTime.Now;
            LastCommandLog = $"{beforeAction.TimeOfDay.ToString("g")} Start RotateLeft - After the action finished at {afteAction.TimeOfDay.ToString("g")} End RotateLeft";
            StatusChanged?.Invoke(this, new LawnMowerMachineEventArgs(direction, MaxX, MaxY, CurrentLocation, LastCommandLog, Mowing, LastCommand, - 1 ));
        }

        public void MoveForward(int moveBy)
        {
            var currentX = CurrentLocation.X;
            var currentY = CurrentLocation.Y;
            DateTime beforeAction = DateTime.Now;

            switch (direction)
            {
                case Direction.North:
                    currentY += moveBy;
                    break;
                case Direction.East:
                    currentX += moveBy;
                    break;
                case Direction.South:
                    currentY -= moveBy;
                    break;
                case Direction.West:
                    currentX -= moveBy;
                    break;
            }
            System.Threading.Thread.Sleep(15000);
            DateTime afteAction = DateTime.Now;

            if (currentX < 0 || currentY < 0 || currentX > MaxX || currentY > MaxY)
            {
                LastCommandLog =
                    $"{beforeAction.TimeOfDay.ToString("g")} WARNING: Start MoveForward Failed - Command Rrollbacked as going out of lawn area - After the action finished at {afteAction.TimeOfDay.ToString("g")} End MoveForward. COMMAND IGNORED";
                StatusChanged?.Invoke(this,
                    new LawnMowerMachineEventArgs(direction, MaxX, MaxY, CurrentLocation, LastCommandLog, Mowing,
                        LastCommand, -1));
            }
            else
            {
                CurrentLocation = new Location {X = currentX, Y = currentY};
                LastCommandLog =
                    $"{beforeAction.TimeOfDay.ToString("g")} Start MoveForward - After the action finished at {afteAction.TimeOfDay.ToString("g")} End MoveForward";
                StatusChanged?.Invoke(this,
                    new LawnMowerMachineEventArgs(direction, MaxX, MaxY, CurrentLocation, LastCommandLog, Mowing,
                        LastCommand, -1));
            }
        }

        public void MowLawn()
        {
            DateTime beforeAction = DateTime.Now;
            Mowing = true;
            LastCommandLog = $"{beforeAction.TimeOfDay.ToString("g")} Mow Lawn Command Started";
            StatusChanged?.Invoke(this,new LawnMowerMachineEventArgs(direction, MaxX, MaxY, CurrentLocation, LastCommandLog, Mowing, LastCommand,120));

            // Sleep current Thread for 120 seconds
            System.Threading.Thread.Sleep(120000);
            Mowing = false;
            DateTime afteAction = DateTime.Now;
            LastCommandLog = $"{beforeAction.TimeOfDay.ToString("g")} Start MowLawn - After the action finished at {afteAction.TimeOfDay.ToString("g")} End MowLawn";
            StatusChanged?.Invoke(this, new LawnMowerMachineEventArgs(direction, MaxX, MaxY, CurrentLocation, LastCommandLog, Mowing, LastCommand ,- 1 ));

        }

        public string CurrentPositionAndDirection => $"{CurrentLocation.X} {CurrentLocation.Y} {direction}";


        public void AddCommand(ILawnMowerMachineCommand command)
        {
            _commandQueue.Enqueue(command);
        }

        public void StartConsumingCommands()
        {
            var thread = new Thread(StartConsuming) {IsBackground = true};
            thread.Start();
        }

        public ILawnMowerMachineCommand LastCommand { get; private set; }
        public string LastCommandLog { get; private set; }

        /// <summary>
        /// Process Commands
        /// </summary>
        private void StartConsuming()
        {
            while (true)
            {
                try
                {
                    ILawnMowerMachineCommand currentCommand = null;
                    _commandQueue.TryDequeue(out currentCommand);

                    if (currentCommand != null)
                    {
                        // Execute Command
                        currentCommand.Execute(this);

                        LastCommand = currentCommand;
                    }
                }
                catch (Exception exp)
                {
                    // log
                    string details = exp.ToString();
                }

            }
        }

        public Guid Id { get;  }

        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose()
        {
            CurrentLocation = null;
        }


    }
 
}
