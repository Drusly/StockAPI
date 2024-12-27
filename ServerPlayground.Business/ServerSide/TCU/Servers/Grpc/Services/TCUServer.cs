using Grpc.Core;
using GrpcServerTest;
using Microsoft.Extensions.Logging;

namespace Karluna.Business.ServerSide.TCU.Servers.Grpc.Services
{
    public class TCUServer : Telemetry.TelemetryBase
    {
        private readonly ILogger<TCUServer> _logger;
        public TCUServer(ILogger<TCUServer> logger)
        {
            _logger = logger;
        }

        public override Task<ServerResponse> ChannelRead(TelemetryRequest request, ServerCallContext context)
        {

            //_logger.Log(LogLevel.Information, request.)
            return Task.FromResult(new ServerResponse
            {
                Message = "Success !"
            });
        }
    }
}