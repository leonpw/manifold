
# build Solution
docker run -it -v $(pwd):/app/ -w /app mcr.microsoft.com/dotnet/sdk:6.0 dotnet build

# run Manifold.Console
docker run -it -v $(pwd):/app/ -w /app mcr.microsoft.com/dotnet/sdk:6.0 dotnet run --project Manifold.Console

#/bin/bash
docker run -v $(pwd):/app/ -w /app mcr.microsoft.com/dotnet/sdk:6.0 dotnet run --project Manifold.Console
