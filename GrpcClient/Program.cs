using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using GrpcServer.Protos;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {


            /*
             GRPC is a method of web communication between services. 
            It is kind of like an API. 
            It responds to requests accross the web and give the caller back the requested information. 
            The differences are in how it is set up and how it transport a data.
GRPC relies on a known configuration that is shared between the client and the server, think of it like a contract. 
            These contracts are called protocal buffers.  
The other big difference between GRPC and API is that GRPC communicates using a binary stream. 
            This is much more efficient than a web server which can use JSON or XML.
There are other differences but those are the private biggest tools. 
            We would go through them by the context of our application.
             
             
             */


            //https://www.youtube.com/watch?v=QyxCX2GYHxk&t=1842s&ab_channel=IAmTimCorey








            /*to solve Error starting gRPC call. HttpRequestException: An error occurred while sending the request. IOException: The response ended prematurely.", start*/
            // This switch must be set before creating the GrpcChannel/HttpClient.
            //AppContext.SetSwitch(
            //    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            ///*to solve Error starting gRPC call. HttpRequestException: An error occurred while sending the request. IOException: The response ended prematurely.", end*/
            //var input = new HelloRequest { Name = "Tim" };
            //var channel = GrpcChannel.ForAddress("http://localhost:5000");
            //var client = new Greeter.GreeterClient(channel);
            //var reply = await client.SayHelloAsync(input);

            //Console.WriteLine(reply.Message);








            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            /*to solve Error starting gRPC call. HttpRequestException: An error occurred while sending the request. IOException: The response ended prematurely.", end*/
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var customerClient = new Customer.CustomerClient(channel);
            var clientRequested = new CustomerLookupModel { UserId = 2 };
            var customer = await customerClient.GetCustomerInfoAsync(clientRequested);
            Console.WriteLine($"{customer.FirstName} {customer.LastName}");

            Console.WriteLine();
            Console.WriteLine("new customer List");
            Console.WriteLine();
            using (var call=customerClient.GetNewCustomers(new NewCustomerRequest()))
            {
                while(await call.ResponseStream.MoveNext())
                {
                    var currentCustomer = call.ResponseStream.Current;

                    Console.WriteLine($"{currentCustomer.FirstName} {currentCustomer.LastName}: {currentCustomer.EmailAddress}");
                }
            }



            Console.ReadKey();
        }
    }
}
