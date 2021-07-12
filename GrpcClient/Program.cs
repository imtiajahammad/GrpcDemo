using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            /*to solve Error starting gRPC call. HttpRequestException: An error occurred while sending the request. IOException: The response ended prematurely.", start*/
            // This switch must be set before creating the GrpcChannel/HttpClient.
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            /*to solve Error starting gRPC call. HttpRequestException: An error occurred while sending the request. IOException: The response ended prematurely.", end*/
            var input = new HelloRequest { Name = "Tim" };
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(input);

            Console.WriteLine(reply.Message);
            Console.ReadKey();
        }
    }
}
