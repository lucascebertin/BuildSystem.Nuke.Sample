FROM microsoft/dotnet:2.1-sdk as build-environment
WORKDIR /app
COPY . .
RUN ./build.sh

FROM microsoft/dotnet:2.1-runtime
WORKDIR /app
COPY --from=build-environment /app/output .
ENTRYPOINT ["dotnet", "BuildSystem.Nuke.Sample.App.dll"]
