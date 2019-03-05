FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["ODATA.Server/ODATA.Server.csproj", "ODATA.Server/"]
RUN dotnet restore "ODATA.Server/ODATA.Server.csproj"
COPY . .
WORKDIR "/src/ODATA.Server"
RUN dotnet build "ODATA.Server.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ODATA.Server.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ODATA.Server.dll"]