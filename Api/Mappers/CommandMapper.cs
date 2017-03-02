using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParcelForce.Test.Common.Domain.Common;
using ParcelForce.Test.Domain.Machine.Commands;
using ParcelForce.Test.WebApi.ViewModel;

namespace ParcelForce.Test.WebApi.Mappers
{
    static class CommandMapper
    {


        public static IList<ILawnMowerMachineCommand> GetLawnMowerMachineCommand(Direction currentDir, Rotation newDirection)
        {
            IList<ILawnMowerMachineCommand> cmdToReturn = new List<ILawnMowerMachineCommand>();
            switch (currentDir)
            {
                case Direction.North:
                {
                    if ((int) newDirection == (int) Direction.East)
                    {
                        cmdToReturn.Add(new MachineRotateRightCommand());
                        break;
                    }
                    else if ((int) newDirection == (int) Direction.West)
                    {
                        cmdToReturn.Add(new MachineRotateLeftCommand());
                        break;
                    }
                    else if ((int) newDirection == (int) Direction.South)
                    {
                        cmdToReturn.Add(new MachineRotateLeftCommand());
                        cmdToReturn.Add(new MachineRotateLeftCommand());
                        break;
                    }
                    else
                    {
                            break;
                    }
                }

                case Direction.East:
                {
                        if ((int)newDirection == (int)Direction.East)
                        {
                            break;
                        }
                        else if ((int)newDirection == (int)Direction.West)
                        {
                            cmdToReturn.Add(new MachineRotateLeftCommand());
                            cmdToReturn.Add(new MachineRotateLeftCommand());
                            break;
                        }
                        else if ((int) newDirection == (int) Direction.South)
                        {
                            cmdToReturn.Add(new MachineRotateRightCommand());
                            break;
                        }
                        else if ((int) newDirection == (int) Direction.North)
                        {
                            cmdToReturn.Add(new MachineRotateLeftCommand());
                            break;
                        }
                        else
                        {
                            break;
                        }
                }
                case Direction.South:
                    {
                        if ((int)newDirection == (int)Direction.East)
                        {
                            cmdToReturn.Add(new MachineRotateLeftCommand());
                            break;
                        }
                        else if ((int)newDirection == (int)Direction.West)
                        {
                            cmdToReturn.Add(new MachineRotateRightCommand());
                            break;
                        }
                        else if ((int)newDirection == (int)Direction.South)
                        {
                            break;
                        }
                        else if ((int)newDirection == (int)Direction.North)
                        {
                            cmdToReturn.Add(new MachineRotateLeftCommand());
                            cmdToReturn.Add(new MachineRotateLeftCommand());
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                case Direction.West:
                    {
                        if ((int)newDirection == (int)Direction.East)
                        {
                            cmdToReturn.Add(new MachineRotateLeftCommand());
                            cmdToReturn.Add(new MachineRotateLeftCommand());
                            break;
                        }
                        else if ((int)newDirection == (int)Direction.West)
                        {
                            break;
                        }
                        else if ((int)newDirection == (int)Direction.South)
                        {
                            cmdToReturn.Add(new MachineRotateLeftCommand());
                            break;
                        }
                        else if ((int)newDirection == (int)Direction.North)
                        {
                            cmdToReturn.Add(new MachineRotateRightCommand());
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
            }

            return cmdToReturn;
        }
    }
}
