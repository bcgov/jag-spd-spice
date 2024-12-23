FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENV DOTNET_gcServer=1
ARG VERSION
ENV VERSION=$VERSION

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN set -uex \
    && apt-get update \
    && NODE_MAJOR=18 \
    && apt-get install nodejs -y;

WORKDIR /src


COPY interfaces interfaces/
COPY spd-ess-portal spd-ess-portal/


RUN dotnet build "spd-ess-portal/spd-ess-portal.csproj" -c Release
# build
FROM build AS publish
RUN dotnet publish "spd-ess-portal/spd-ess-portal.csproj" -c Release -o /app/publish --runtime linux-musl-x64 --no-self-contained

FROM base AS final

# copy app
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "spd-ess-portal.dll"]
