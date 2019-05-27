# build in build.Dockerfile
FROM proba-build AS BUILD

# not SDK just runtime
FROM mcr.microsoft.com/dotnet/core/aspnet AS HOST

WORKDIR /app
COPY --from=BUILD /app/SSBO5G__Szakdolgozat/out .
RUN mv appsettings.sample.json appsettings.json

ENTRYPOINT ["dotnet", "SSBO5G__Szakdolgozat.dll"]