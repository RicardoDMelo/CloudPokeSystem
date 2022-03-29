using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System.Net;
using System.Net.NetworkInformation;

namespace PokemonSystem.PokedexInjector
{
    internal enum ConnectionType
    {
        Local, Remote
    }

    internal static class DynamoConnector
    {
        private static readonly string Ip = "localhost";
        private static readonly int Port = 8000;
        private static readonly string EndpointUrl = "http://" + Ip + ":" + Port;

        private static bool IsPortInUse()
        {
            bool isAvailable = true;
            // Evaluate current system TCP connections. This is the same information provided
            // by the netstat command line application, just in .Net strongly-typed object
            // form.  We will look through the list, and if our port we would like to use
            // in our TcpClient is occupied, we will set isAvailable to false.
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();
            foreach (IPEndPoint endpoint in tcpConnInfoArray)
            {
                if (endpoint.Port == Port)
                {
                    isAvailable = false;
                    break;
                }
            }

            return isAvailable;
        }

        internal static IAmazonDynamoDB CreateClient(ConnectionType connectionType)
        {
            if (connectionType == ConnectionType.Local)
            {
                // First, check to see whether anyone is listening on the DynamoDB local port
                // (by default, this is port 8000, so if you are using a different port, modify this accordingly)
                var portUsed = IsPortInUse();
                if (portUsed)
                {
                    throw new Exception("The local version of DynamoDB is NOT running.");
                }

                // DynamoDB-Local is running, so create a client
                AmazonDynamoDBConfig ddbConfig = new AmazonDynamoDBConfig();
                ddbConfig.ServiceURL = EndpointUrl;

                return new AmazonDynamoDBClient(ddbConfig);
            }
            else
            {
                return new AmazonDynamoDBClient();
            }
        }
    }
}
