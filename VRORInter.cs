using Lib;
using System.IO.Ports;

namespace ArdInterface
{
    public class VRORInter : Interface
    {
        // Constructor for immediately starting. This will prime the port for connection.
        // If you wish to find the ports in a different way, override the establish_connection()
        // function.
        public VRORInter()
        {
            // This function can be overridden, define it later in this class
            establish_connection();
        }

        // Implement override functions or define some new methods that is specific to interfacing with Unity
        public override void write_movement(int pos, Movement cmd)
        {
            base.write_movement(pos, cmd);
        }







        // Destructor
        ~VRORInter()
        {
            // You cannot override the close_connection() function
            close_connection();
        }
    }
}
