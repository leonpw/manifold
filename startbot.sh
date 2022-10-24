#/bin/bash
docker run -d -v $(pwd):/app/ -w /app mcr.microsoft.com/dotnet/sdk:6.0 dotnet run --project Manifold.Console
