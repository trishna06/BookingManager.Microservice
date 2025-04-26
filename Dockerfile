ARG VERSION=6.0-focal

#Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:$VERSION as base

FROM mcr.microsoft.com/dotnet/sdk:$VERSION AS build-env
WORKDIR /app

ARG APPLICATION_PATH
RUN echo $APPLICATION_PATH

COPY $APPLICATION_PATH ./

# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=build-env /app .
ENTRYPOINT ["dotnet", "BookingManager.API.dll"]
